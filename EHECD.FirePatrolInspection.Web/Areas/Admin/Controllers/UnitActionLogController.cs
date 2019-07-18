			
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
    /// 单位操作日志控制器
    /// </summary>
    public class UnitActionLogController : Controller
    {
		#region 后台单位操作日志列表视图
	
        /// <summary>
        /// 后台单位操作日志列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台单位操作日志编辑视图

        /// <summary>
        /// 后台单位操作日志编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(long id)
		        
		{
            EHECD_UnitActionLog entity = new  EHECD_UnitActionLog();
					if (id != 0)
			
						
				entity = UnitActionLogService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台单位操作日志详情视图

        /// <summary>
        /// 后台单位操作日志详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(UnitActionLogService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取单位操作日志数据

        /// <summary>
        /// 获取单位操作日志数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UnitActionLogService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑单位操作日志

        /// <summary>
        /// 添加编辑单位操作日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_UnitActionLog entity)
        {
            return Json(UnitActionLogService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除单位操作日志

        /// <summary>
        /// 删除单位操作日志
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(UnitActionLogService.Instance.Delete(sIds));
        }

		#endregion
    }
}
