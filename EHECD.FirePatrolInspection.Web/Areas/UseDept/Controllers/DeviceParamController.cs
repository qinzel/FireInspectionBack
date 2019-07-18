
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
{
	/// <summary>
    /// 设备指标控制器
    /// </summary>
    public class DeviceParamController : Controller
    {
		#region 后台设备指标列表视图
	
        /// <summary>
        /// 后台设备指标列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            #region 绑定设备类型列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().Where(o => o.iUseDeptID == user.iUnitID || o.iUseDeptID == 0).ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var type in typeList)
            {
                typeselect.Add(new SelectListItem() { Text = type.sName, Value = type.ID.ToString() });
            }
            ViewBag.typeList = typeselect;

            #endregion

            return View();
        }
		
		#endregion
		
		#region 后台设备指标编辑视图

        /// <summary>
        /// 后台设备指标编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Set(long id)
		{
            EHECD_DeviceParam entity = new  EHECD_DeviceParam();
            if (id != 0)
				entity = DeviceParamService.Instance.Get(id);

            #region 绑定设备类型列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().Where(o => o.iUseDeptID == user.iUnitID || o.iUseDeptID == 0).ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var type in typeList)
            {
                typeselect.Add(new SelectListItem() { Text = type.sName + "-" + type.sUnitName, Value = type.ID.ToString(), Selected = type.ID == entity.iDeviceTypeID });
            }
            ViewBag.typeList = typeselect;

            #endregion

            return View(entity);
        }
		
		#endregion
		
		#region 后台设备指标详情视图

        /// <summary>
        /// 后台设备指标详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(DeviceParamService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取设备指标数据

        /// <summary>
        /// 获取设备指标数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUnitDeptID", user.iUnitID);
            return Content(DeviceParamService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑设备指标

        /// <summary>
        /// 添加编辑设备指标
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_DeviceParam entity)
        {
            if (entity.ID == 0)
            {
                LoginUser user = AuthHelper.GetLogUseUser();
                entity.iUseDeptID = user.iUnitID;
            }
            return Json(DeviceParamService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除设备指标

        /// <summary>
        /// 删除设备指标
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(DeviceParamService.Instance.Delete(sIds));
        }

		#endregion
    }
}
