using System.Web.Mvc;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin
{
    /// <summary>
    /// 总平台
    /// </summary>
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers" }
            );
        }
    }
}