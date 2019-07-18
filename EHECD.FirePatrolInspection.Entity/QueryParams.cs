using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 通用查询参数
    /// </summary>
    [Serializable]
    public class QueryParams
    {
        public QueryParams()
        {
            rows = 10;
            page = 1;
            sort = "ID";
            order = "Desc";
            condition = new Dictionary<string, object>();
        }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 排序方式(asc-正序,desc-倒序)
        /// </summary>
        public string order { get; set; }
       
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string keyword { set; get; }   

        /// <summary>
        /// 其他查询条件
        /// </summary>
        public Dictionary<string,object> condition { set; get; } 
    }
}
