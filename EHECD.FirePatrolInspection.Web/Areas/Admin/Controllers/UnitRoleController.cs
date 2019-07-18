			
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
    /// 单位角色控制器
    /// </summary>
    public class UnitRoleController : Controller
    {
		#region 后台单位角色列表视图
	
        /// <summary>
        /// 后台单位角色列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台单位角色编辑视图

        /// <summary>
        /// 后台单位角色编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(long id)
		        
		{
            EHECD_UnitRole entity = new  EHECD_UnitRole();
					if (id != 0)
			
						
				entity = UnitRoleService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台单位角色详情视图

        /// <summary>
        /// 后台单位角色详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(UnitRoleService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取单位角色数据

        /// <summary>
        /// 获取单位角色数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UnitRoleService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑单位角色

        /// <summary>
        /// 添加编辑单位角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_UnitRole entity)
        {
            return Json(UnitRoleService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除单位角色

        /// <summary>
        /// 删除单位角色
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(UnitRoleService.Instance.Delete(sIds));
        }

		#endregion
    }
}
