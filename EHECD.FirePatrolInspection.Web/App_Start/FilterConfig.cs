//using EHECD.Core.APIHelper;
using EHECD.FirePatrolInspection.Web.Filter;
using System.Configuration;
using System.Web.Mvc;

namespace EHECD.FirePatrolInspection.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionAndAuthority());
        }
    }
}
