using System.Web.Mvc;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin
{
    /// <summary>
    /// 使用单位
    /// </summary>
    public class UseDeptAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UseDept";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UseDept_default",
                "UseDept/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers" }
            );
        }
    }
}