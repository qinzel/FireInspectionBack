using System.Web.Mvc;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin
{
    /// <summary>
    /// 维护公司
    /// </summary>
    public class RepairDeptAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "RepairDept";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "RepairDept_default",
                "RepairDept/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "EHECD.FirePatrolInspection.Web.Areas.RepairDept.Controllers" }
            );
        }
    }
}