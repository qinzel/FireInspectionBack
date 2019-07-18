using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EHECD.Core.Cache
{
    public class RedisCacheManager:ICacheManager
    {
        #region Fields

        private readonly ConnectionMultiplexer _muxer;
        private readonly IDatabase _db;

        #endregion

        #region Ctor

        public RedisCacheManager(string conn)
        {
            if (String.IsNullOrEmpty(conn))
                throw new Exception("Redis connection string is empty");

            this._muxer = ConnectionMultiplexer.Connect(conn);

            this._db = _muxer.GetDatabase();
        }

        #endregion

        #region Utilities

        protected virtual byte[] Serialize(object item)
        {
            if(item == null)
            {
                item = "";
            }
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        protected virtual T Deserialize<T>(byte[] serializedObject)
        {
            if (serializedObject == null)
                return default(T);

            var jsonString = Encoding.UTF8.GetString(serializedObject);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        #endregion

        #region String

        /// <summary>
        /// String数据获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T StringGet<T>(string key)
        {

            var rValue = _db.StringGet(key);
            if (!rValue.HasValue)
                return default(T);
            var result = Deserialize<T>(rValue);
            return result;
        }

        /// <summary>
        /// String数据设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public virtual bool StringSet(string key, object data, int cacheTime = -1)
        {
            var entryBytes = Serialize(data);
            if (cacheTime > 0)
            {
                var expiresIn = TimeSpan.FromMinutes(cacheTime);
                return _db.StringSet(key, entryBytes, expiresIn);
            }
            else
            {
               return _db.StringSet(key, entryBytes);
            }
        }

        #endregion

        #region Hash

        /// <summary>
        /// Hash数据根据Key获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Dictionary<string,T> HashGetAll<T>(string key)
        {
            HashEntry[] rValue = _db.HashGetAll(key);
            if (rValue.Length == 0)
                return new Dictionary<string, T>();

            Dictionary<string, T> dic = new Dictionary<string, T>();
            foreach(var item in rValue)
            {
                dic[item.Name] = Deserialize<T>(item.Value);
            }
            return dic;
        }

        /// <summary>
        /// Hash数据根据Key和name获取hash值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual T HashGet<T>(string key,string name)
        {
           RedisValue val = _db.HashGet(key, name);
            return Deserialize<T>(val);
        }

        /// <summary>
        /// 设置Hash对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="keyval"></param>
        public virtual bool HashSet<T>(string key,Dictionary<string,T> keyval)
        {
            if(keyval == null)
            {
                return false;
            }
            HashEntry[] arr = new HashEntry[keyval.Keys.Count];
            int i = 0;
            foreach (string k in keyval.Keys)
            {
                arr[i++] = new HashEntry(k, Serialize(keyval[k]));
            }
            _db.HashSet(key, arr);
            return true;
        }

        /// <summary>
        /// 设置Hash值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public virtual bool HashSet<T>(string key,string name,T val)
        {
            return  _db.HashSet(key, name, Serialize(val));
        }

        #endregion

        #region SortedSet

        /// <summary>
        /// SortedSet数据新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public virtual bool SortedSetAdd<T>(string key,T val,double score)
        {
            if(val == null)
                return false;
            
            return _db.SortedSetAdd(key, Serialize(val), score);
        }

        /// <summary>
        /// SortedSet数据新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        public virtual bool SortedSetAdd<T>(string key,List<SortedSetModel<T>> vals)
        {
            List<SortedSetEntry> arr = new List<SortedSetEntry>();
            int i = 0;
            foreach(SortedSetModel<T> item in vals)
            {
                if(item.Entity == null)
                {
                    continue;
                }
                arr[i++] = new SortedSetEntry(Serialize(item.Entity), item.Score);
            }
            _db.SortedSetAdd(key, arr.ToArray());
            return true;
        }

        /// <summary>
        /// 获取某两个score值中间的SortedSet数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public virtual List<T> SortedSetRangeByScore<T>(string key,double start = double.NegativeInfinity, double stop = double.PositiveInfinity)
        {
            List<T> result = new List<T>();
            RedisValue[] vals = _db.SortedSetRangeByScore(key, start, stop);
            if(vals == null)
            {
                return result;
            }

            foreach(RedisValue item in vals)
            {
                result.Add(Deserialize<T>(item));
            }
            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="key"></param>
        public virtual void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        /// <summary>
        /// 删除匹配到的Key
        /// </summary>
        /// <param name="pattern"></param>
        public virtual void RemoveByPattern(string pattern)
        {
            foreach (var ep in _muxer.GetEndPoints())
            {
                var server = _muxer.GetServer(ep);
                var keys = server.Keys(pattern: "*" + pattern + "*");
                foreach (var key in keys)
                    _db.KeyDelete(key);
            }
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public virtual void Clear()
        {
            foreach (var ep in _muxer.GetEndPoints())
            {
                var server = _muxer.GetServer(ep);
                var keys = server.Keys();
                foreach (var key in keys)
                    _db.KeyDelete(key);
            }
        }
        
        /// <summary>
        /// 释放链接
        /// </summary>
        public virtual void Dispose()
        {
            if (_muxer != null)
                _muxer.Dispose();
        }

        #endregion
    }
}
