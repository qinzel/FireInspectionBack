using EHECD.FirePatrolInspection.Web.Filter;
using System.Configuration;
using System.Web.Http;
using System.Web.Routing;

namespace EHECD.FirePatrolInspection.Web
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            string isSign = ConfigurationManager.AppSettings["IsClientSign"];
            if (isSign == "1")
            {
                config.Filters.Add(new ClientSignFiler());
            }

            RouteTable.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "router/{name}",
                defaults: new { controller = "JsonRoute", name = RouteParameter.Optional }
            );
        }
    }
}