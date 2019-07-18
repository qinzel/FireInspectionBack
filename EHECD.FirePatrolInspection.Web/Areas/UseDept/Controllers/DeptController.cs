
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
    /// 部门控制器
    /// </summary>
    public class DeptController : Controller
    {
		#region 后台部门列表视图
	
        /// <summary>
        /// 后台部门列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            #region 绑定使用单位列表

            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(0).ToList();
            List<SelectListItem> unitselect = new List<SelectListItem>();
            unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in unitList)
            {
                unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.unitList = unitselect;

            #endregion

            return View();
        }

        #endregion

        #region 后台部门下辖所有点检员列表视图

        /// <summary>
        /// 后台部门下辖所有点检员列表视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult InspectorList(int id)
        {
            ViewBag.iOrganID = id;
            return View();
        }

        #endregion

        #region 后台部门点检员下辖设备列表视图

        /// <summary>
        /// 后台部门点检员下辖设备列表视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeviceList(int id)
        {
            #region 绑定使用单位列表

            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(0).ToList();
            List<SelectListItem> unitselect = new List<SelectListItem>();
            unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in unitList)
            {
                unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.unitList = unitselect;

            #endregion

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

            ViewBag.iClientID = id;
            return View();
        }

        #endregion

        #region 后台部门编辑视图

        /// <summary>
        /// 后台部门编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Set(int id)
		{
            EHECD_Dept entity = new  EHECD_Dept();
            if (id != 0)
				entity = DeptService.Instance.Get(id);

            #region 绑定使用单位列表

            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(0).ToList();
            List<SelectListItem> unitselect = new List<SelectListItem>();
            unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in unitList)
            {
                unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString(), Selected = entity.iUseDeptID == unit.ID });
            }
            ViewBag.unitList = unitselect;

            #endregion

            return View(entity);
        }
		
		#endregion
		
		#region 后台部门详情视图

        /// <summary>
        /// 后台部门详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(int id)
		{
            return View(DeptService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取部门数据

        /// <summary>
        /// 获取部门数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(DeptService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取部门下辖点检员数据

        /// <summary>
        /// 获取部门下辖点检员数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetInspectorsByDeptIDGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(ClientService.Instance.GetInspectorsByDeptIDGridData(param));
        }

        #endregion

        #region 获取部门点检员下辖设备数据

        /// <summary>
        /// 获取部门点检员下辖设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetDevicesByClientIDGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(DeviceService.Instance.GetDevicesByClientIDGridData(param));
        }

        #endregion

        #region 添加编辑部门

        /// <summary>
        /// 添加编辑部门
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_Dept entity)
        {
            if (entity.ID == 0)
            {
                LoginUser user = AuthHelper.GetLogUseUser();
                entity.iUseDeptID = user.iUnitID;
            }
            return Json(DeptService.Instance.SetSelf(entity));
        }

		#endregion
		
		#region 删除部门

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(DeptService.Instance.Delete(sIds));
        }

		#endregion
    }
}
