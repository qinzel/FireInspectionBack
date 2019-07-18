using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHECD.Core.Cache
{
    public interface ICacheManager : IDisposable
    {
        #region String

        /// <summary>
        /// String数据获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T StringGet<T>(string key);

        /// <summary>
        /// String数据设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        bool StringSet(string key, object data, int cacheTime = -1);

        #endregion

        #region Hash

        /// <summary>
        /// Hash数据根据Key获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Dictionary<string, T> HashGetAll<T>(string key);

        /// <summary>
        /// Hash数据根据Key和name获取hash值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        T HashGet<T>(string key, string name);

        /// <summary>
        /// 设置Hash对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="keyval"></param>
        bool HashSet<T>(string key, Dictionary<string, T> keyval);

        /// <summary>
        /// 设置Hash值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        bool HashSet<T>(string key, string name, T val);

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
        bool SortedSetAdd<T>(string key, T val, double score);

        /// <summary>
        /// SortedSet数据新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vals"></param>
        bool SortedSetAdd<T>(string key, List<SortedSetModel<T>> vals);

        /// <summary>
        /// 获取某两个score值中间的SortedSet数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        List<T> SortedSetRangeByScore<T>(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity);

        #endregion

        #region Methods

        /// <summary>
        /// Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsSet(string key);

        /// <summary>
        /// 删除key
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 删除匹配到的Key
        /// </summary>
        /// <param name="pattern"></param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();

        #endregion
    }
}
