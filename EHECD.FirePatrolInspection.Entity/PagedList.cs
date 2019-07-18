using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class PagedList
    {
        public int total { get; set; }
        public object rows { get; set; }
    }

    /// <summary>
    /// 分页扩展
    /// </summary>
    public static class PagedListExtion
    {
        #region 分页后json字符串

        /// <summary>
        /// 分页后json字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="input">数据</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页记录数</param>
        /// <returns></returns>
        public static string EHECDAsPagedString<T>(this IEnumerable<T> input, int page, int rows)
        {
            var total = input.Count();
            var list = input.Skip((page - 1) * rows).Take(rows).ToList();
            return JsonConvert.SerializeObject(new { total = total, rows = list }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" });
        }

        #endregion

        #region 分页后json字符串

        /// <summary>
        /// 分页后json字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="input">数据</param>
        /// <param name="page">页码</param>
        /// <param name="rows">每页记录数</param>
        /// <returns></returns>
        public static PagedList EHECDAsPagedList<T>(this IEnumerable<T> input, int page, int rows)
        {
            var total = input.Count();
            var list = input.Skip((page - 1) * rows).Take(rows).ToList();
            return new PagedList() { total = total, rows = list };
        }

        #endregion

        #region 分页后json字符串

        /// <summary>
        /// 分页后json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="iTotalRecocd"></param>
        /// <returns></returns>
        public static string EHECDAsPagedString<T>(this IEnumerable<T> input,int iTotalRecocd)
        {
            return JsonConvert.SerializeObject(new { total = iTotalRecocd, rows = input.ToList() }, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" });
        }

        #endregion

        #region 分页后json字符串

        /// <summary>
        /// 分页后json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EHECDAsPagedString<T>(this IEnumerable<T> input)
        {
            return JsonConvert.SerializeObject(input, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" });
        }

        #endregion
    }
}
