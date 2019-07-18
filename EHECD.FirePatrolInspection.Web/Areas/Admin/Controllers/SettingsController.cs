using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
	/// <summary>
    /// 总后台基础设置控制器
    /// </summary>
    public class SettingsController : Controller
    {
		#region 后台总后台基础设置编辑视图

        /// <summary>
        /// 后台总后台基础设置编辑视图
        /// </summary>
        /// <returns></returns>
		public ActionResult Set()
		{
            EHECD_Settings entity = SettingsService.Instance.Get();

            return View(entity);
        }
		
		#endregion
		
		#region 后台总后台基础设置详情视图

        /// <summary>
        /// 后台总后台基础设置详情视图
        /// </summary>
        /// <returns></returns>
				
		public ActionResult Detail()
		{
            return View(SettingsService.Instance.Get());
        }
		       
        #endregion
		
		#region 添加编辑总后台基础设置

        /// <summary>
        /// 添加编辑总后台基础设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_Settings entity)
        {
            return Json(SettingsService.Instance.Set(entity));
        }

		#endregion
    }
}