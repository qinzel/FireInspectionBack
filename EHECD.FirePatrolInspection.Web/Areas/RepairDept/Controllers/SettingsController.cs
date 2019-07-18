using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Web.Areas.RepairDept.Controllers
{
	/// <summary>
    /// 后台基础设置控制器
    /// </summary>
    public class SettingsController : Controller
    {
        #region 后台后台基础设置视图

        /// <summary>
        /// 后台后台基础设置视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
		{
            EHECD_Settings entity = SettingsService.Instance.Get();

            return View(entity);
        }
		
		#endregion
    }
}