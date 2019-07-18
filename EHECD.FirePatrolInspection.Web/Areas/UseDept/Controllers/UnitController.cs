
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
    /// 各平台超管账号注册控制器
    /// </summary>
    public class UnitController : Controller
    {
        #region 后台维护单位列表视图

        /// <summary>
        /// 后台维护单位列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult RepairDeptList(int id)
        {
            ViewBag.iUnitID = id;
            return View();
        }

        #endregion

        #region 后台维护公司下辖设备列表视图

        /// <summary>
        /// 后台维护公司下辖设备列表视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeviceList(int id)
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

            ViewBag.iRepairDeptID = id;
            return View();
        }

        #endregion

        #region 获取设备数据

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(DeviceService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取维护单位注册数据

        /// <summary>
        /// 获取维护单位注册数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetRelRepairDeptGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(UnitService.Instance.GetRelRepairDeptList(param));
        }

        #endregion

        #region 后台各平台超管账号注册详情视图

        /// <summary>
        /// 后台各平台超管账号注册详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Detail(int id)
		{
            EHECD_Unit entity = UnitService.Instance.Get(id) ?? new EHECD_Unit();
            if (entity.ID > 0)
            {
                entity.UnitList = UnitService.Instance.GetRelRepairDeptAllList(entity.ID).ToList();
            }
            return View(entity);
        }

        #endregion
    }
}