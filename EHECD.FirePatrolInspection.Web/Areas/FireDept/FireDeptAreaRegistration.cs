using System.Web.Mvc;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin
{
    /// <summary>
    /// 消防部门
    /// </summary>
    public class FireDeptAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FireDept";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FireDept_default",
                "FireDept/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "EHECD.FirePatrolInspection.Web.Areas.FireDept.Controllers" }
            );
        }
    }
}