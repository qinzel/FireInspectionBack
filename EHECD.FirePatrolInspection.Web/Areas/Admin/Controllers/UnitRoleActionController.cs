			
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
    /// 单位角色操作信息控制器
    /// </summary>
    public class UnitRoleActionController : Controller
    {
		#region 后台单位角色操作信息列表视图
	
        /// <summary>
        /// 后台单位角色操作信息列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台单位角色操作信息编辑视图

        /// <summary>
        /// 后台单位角色操作信息编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(long id)
		        
		{
            EHECD_UnitRoleAction entity = new  EHECD_UnitRoleAction();
					if (id != 0)
			
						
				entity = UnitRoleActionService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台单位角色操作信息详情视图

        /// <summary>
        /// 后台单位角色操作信息详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(UnitRoleActionService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取单位角色操作信息数据

        /// <summary>
        /// 获取单位角色操作信息数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UnitRoleActionService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑单位角色操作信息

        /// <summary>
        /// 添加编辑单位角色操作信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_UnitRoleAction entity)
        {
            return Json(UnitRoleActionService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除单位角色操作信息

        /// <summary>
        /// 删除单位角色操作信息
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(UnitRoleActionService.Instance.Delete(sIds));
        }

		#endregion
    }
}
