using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHECD.Core.Cache
{
    public class NopNullCache : ICacheManager
    {
        public void Clear()
        {
            
        }

        public void Dispose()
        {
           
        }

        public T HashGet<T>(string key, string name)
        {
            return default(T);
        }

        public Dictionary<string, T> HashGetAll<T>(string key)
        {
            return null;
        }

        public bool HashSet<T>(string key, Dictionary<string, T> keyval)
        {
            return false;
        }

        public bool HashSet<T>(string key, string name, T val)
        {
            return false;
        }

        public bool IsSet(string key)
        {
            return false;
        }

        public void Remove(string key)
        {
           
        }

        public void RemoveByPattern(string pattern)
        {
            
        }

        public bool SortedSetAdd<T>(string key, List<SortedSetModel<T>> vals)
        {
            return false;
        }

        public bool SortedSetAdd<T>(string key, T val, double score)
        {
            return false;
        }

        public List<T> SortedSetRangeByScore<T>(string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity)
        {
            return null;
        }

        public T StringGet<T>(string key)
        {
            return default(T);
        }

        public bool StringSet(string key, object data, int cacheTime = -1)
        {
            return false;
        }
    }
}
