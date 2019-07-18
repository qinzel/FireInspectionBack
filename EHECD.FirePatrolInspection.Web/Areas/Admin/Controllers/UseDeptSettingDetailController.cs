			
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
    /// 使用单位基础设置关联维护公司控制器
    /// </summary>
    public class UseDeptSettingDetailController : Controller
    {
		#region 后台使用单位基础设置关联维护公司列表视图
	
        /// <summary>
        /// 后台使用单位基础设置关联维护公司列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台使用单位基础设置关联维护公司编辑视图

        /// <summary>
        /// 后台使用单位基础设置关联维护公司编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(long id)
		        
		{
            EHECD_UseDeptSettingDetail entity = new  EHECD_UseDeptSettingDetail();
					if (id != 0)
			
						
				entity = UseDeptSettingDetailService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台使用单位基础设置关联维护公司详情视图

        /// <summary>
        /// 后台使用单位基础设置关联维护公司详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(UseDeptSettingDetailService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取使用单位基础设置关联维护公司数据

        /// <summary>
        /// 获取使用单位基础设置关联维护公司数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UseDeptSettingDetailService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑使用单位基础设置关联维护公司

        /// <summary>
        /// 添加编辑使用单位基础设置关联维护公司
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_UseDeptSettingDetail entity)
        {
            return Json(UseDeptSettingDetailService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除使用单位基础设置关联维护公司

        /// <summary>
        /// 删除使用单位基础设置关联维护公司
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(UseDeptSettingDetailService.Instance.Delete(sIds));
        }

		#endregion
    }
}
