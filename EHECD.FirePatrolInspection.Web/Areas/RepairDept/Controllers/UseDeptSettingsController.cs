using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Web.Areas.RepairDept.Controllers
{
	/// <summary>
    /// 使用单位基础设置控制器
    /// </summary>
    public class UseDeptSettingsController : Controller
    {
        #region 后台使用单位基础设置列表视图

        /// <summary>
        /// 后台使用单位基础设置列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            LoginUser user = AuthHelper.GetLogRepairUser();
            ViewBag.unit = UnitService.Instance.Get(user.iUnitID);
            ViewBag.dutyList = UnitService.Instance.GetRelUnitList(user.iUnitID);
            return View();
        }

        #endregion

        #region 后台使用单位基础设置编辑视图

        /// <summary>
        /// 后台使用单位基础设置编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Set(int id)
		        
		{
            EHECD_UseDeptSettings entity = new  EHECD_UseDeptSettings();
					if (id != 0)
			
						
				entity = UseDeptSettingsService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台使用单位基础设置详情视图

        /// <summary>
        /// 后台使用单位基础设置详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(int id)
		{
            return View(UseDeptSettingsService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取使用单位基础设置数据

        /// <summary>
        /// 获取使用单位基础设置数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UseDeptSettingsService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑使用单位基础设置

        /// <summary>
        /// 添加编辑使用单位基础设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_UseDeptSettings entity)
        {
            return Json(UseDeptSettingsService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除使用单位基础设置

        /// <summary>
        /// 删除使用单位基础设置
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(UseDeptSettingsService.Instance.Delete(sIds));
        }

		#endregion
    }
}
