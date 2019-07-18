			
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
    /// 各平台超管账号注册控制器
    /// </summary>
    public class UnitController : Controller
    {
        #region 后台使用单位列表视图

        /// <summary>
        /// 后台使用单位列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult UseDeptList()
        {
            #region 绑定消防部门列表

            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(1).ToList();
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

        #region 后台消防部门列表视图

        /// <summary>
        /// 后台消防部门列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FireDeptList()
        {
            return View();
        }

        #endregion

        #region 后台维护单位列表视图

        /// <summary>
        /// 后台维护单位列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult RepairDeptList()
        {
            return View();
        }

        #endregion

        #region 后台使用单位账号申请列表视图

        /// <summary>
        /// 后台使用单位账号申请列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult UseDeptApplyList()
        {
            return View();
        }

        #endregion

        #region 后台消防部门账号申请列表视图

        /// <summary>
        /// 后台使用单位账号申请列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FireDeptApplyList()
        {
            return View();
        }

        #endregion

        #region 后台维护单位账号申请列表视图

        /// <summary>
        /// 后台维护单位账号申请列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult RepairDeptApplyList()
        {
            return View();
        }

        #endregion

        #region 后台各平台超管账号注册编辑视图

        /// <summary>
        /// 后台各平台超管账号注册编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Set(int id)
		{
            EHECD_Unit entity = new  EHECD_Unit();
            if (id != 0)
			
				entity = UnitService.Instance.Get(id);
					
            return View(entity);
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
            return View(UnitService.Instance.Get(id));
        }

        #endregion

        #region 后台使用单位账号注册详情视图

        /// <summary>
        /// 后台使用单位账号注册详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UseDeptApplyDetail(int id)
		{
            return View(UnitService.Instance.Get(id));
        }

        #endregion

        #region 后台消防部门账号注册详情视图

        /// <summary>
        /// 后台消防部门账号注册详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult FireDeptApplyDetail(int id)
        {
            return View(UnitService.Instance.Get(id));
        }

        #endregion

        #region 后台维护单位账号注册详情视图

        /// <summary>
        /// 后台维护单位账号注册详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult RepairDeptApplyDetail(int id)
        {
            return View(UnitService.Instance.Get(id));
        }

        #endregion

        #region 获取使用单位审核数据

        /// <summary>
        /// 获取使用单位审核数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetUseDeptGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            param.condition.Add("iType", 0);
            return Content(UnitService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取消防部门审核数据

        /// <summary>
        /// 获取使用单位审核数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetFireDeptGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            param.condition.Add("iType", 1);
            return Content(UnitService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取维护单位审核数据

        /// <summary>
        /// 获取维护单位审核数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetRepairDeptGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            param.condition.Add("iType", 2);
            return Content(UnitService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取使用单位注册数据

        /// <summary>
        /// 获取使用单位注册数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetAdoptedUseDeptGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UnitService.Instance.GetAdoptedUseDeptGridData(param));
        }

        #endregion

        #region 获取消防部门注册数据

        /// <summary>
        /// 获取使用单位注册数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetAdoptedFireDeptGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UnitService.Instance.GetAdoptedFireDeptGridData(param));
        }

        #endregion

        #region 获取维护单位注册数据

        /// <summary>
        /// 获取维护单位注册数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetAdoptedRepairDeptGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(UnitService.Instance.GetAdoptedRepairDeptGridData(param));
        }

        #endregion

        #region 添加编辑各平台超管账号注册

        /// <summary>
        /// 添加编辑各平台超管账号注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_Unit entity)
        {
            return Json(UnitService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除各平台超管账号注册

        /// <summary>
        /// 删除各平台超管账号注册
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(UnitService.Instance.Delete(sIds));
        }

        #endregion

        #region 通过审核

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public JsonResult Adopt(int iUnitID)
        {
            return Json(UnitService.Instance.Adopt(iUnitID));
        }

        #endregion

        #region 拒绝通过

        /// <summary>
        /// 拒绝通过
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public JsonResult Refused(int iUnitID)
        {
            return Json(UnitService.Instance.Refused(iUnitID));
        }

        #endregion

        #region 冻结单位

        /// <summary>
        /// 冻结单位
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public JsonResult Frozen(int iUnitID)
        {
            return Json(UnitService.Instance.Frozen(iUnitID));
        }

        #endregion

        #region 解冻单位

        /// <summary>
        /// 解冻单位
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public JsonResult UnFrozen(int iUnitID)
        {
            return Json(UnitService.Instance.UnFrozen(iUnitID));
        }

        #endregion
    }
}