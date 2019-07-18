using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Web.Helper
{
    /// <summary>
    /// 初始化查询方法参数
    /// </summary>
    public class QueryParamBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection collection = controllerContext.HttpContext.Request.Form;

            QueryParams param = new QueryParams()
            {
                rows = int.Parse(collection["rows"]),
                page = int.Parse(collection["page"]),
                sort = collection["sort"] ?? "ID",
                order = collection["order"] ?? "Desc"
            };
            Dictionary<string, object> condition = new Dictionary<string, object>();
            foreach (var key in collection.AllKeys)
            {
                if (key.Contains("condition."))
                {
                    condition[key.Split('.')[1]] = collection[key];
                }
            }
            param.condition = condition;
            return param;
        }
    }
}