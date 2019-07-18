			
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
	/// <summary>
    /// 单位后台用户控制器
    /// </summary>
    public class UnitUserController : Controller
    {
		#region 后台单位后台用户列表视图
	
        /// <summary>
        /// 后台单位后台用户列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台单位后台用户编辑视图

        /// <summary>
        /// 后台单位后台用户编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(long id)
		        
		{
            EHECD_UnitUser entity = new  EHECD_UnitUser();
					if (id != 0)
			
						
				entity = UnitUserService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台单位后台用户详情视图

        /// <summary>
        /// 后台单位后台用户详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(UnitUserService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取单位后台用户数据

        /// <summary>
        /// 获取单位后台用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UnitUserService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑单位后台用户

        /// <summary>
        /// 添加编辑单位后台用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_UnitUser entity)
        {
            return Json(UnitUserService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除单位后台用户

        /// <summary>
        /// 删除单位后台用户
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(UnitUserService.Instance.Delete(sIds));
        }

		#endregion
    }
}
