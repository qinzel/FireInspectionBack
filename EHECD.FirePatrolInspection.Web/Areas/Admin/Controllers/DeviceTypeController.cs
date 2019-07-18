			
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
    /// 设备分类控制器
    /// </summary>
    public class DeviceTypeController : Controller
    {
		#region 后台设备分类列表视图
	
        /// <summary>
        /// 后台设备分类列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台设备分类编辑视图

        /// <summary>
        /// 后台设备分类编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Set(int id)
		{
            EHECD_DeviceType entity = new  EHECD_DeviceType();
            if (id != 0)
				entity = DeviceTypeService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion

		#region 获取设备分类数据

        /// <summary>
        /// 获取设备分类数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(DeviceTypeService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑设备分类

        /// <summary>
        /// 添加编辑设备分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_DeviceType entity)
        {
            return Json(DeviceTypeService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除设备分类

        /// <summary>
        /// 删除设备分类
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(DeviceTypeService.Instance.Delete(sIds));
        }

		#endregion
    }
}
