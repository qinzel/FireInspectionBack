
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
	/// <summary>
    /// 前端用户控制器
    /// </summary>
    public class ClientController : Controller
    {
        #region 后台点检员列表视图

        /// <summary>
        /// 后台点检员列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult InspectorList()
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

        #region 后台值班人员列表视图

        /// <summary>
        /// 后台值班人员列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult DutyPeopleList()
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

        #region 后台前端用户编辑视图

        /// <summary>
        /// 后台前端用户编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Set(int id)
        {
            EHECD_Client entity = new EHECD_Client();
            if (id != 0)

                entity = ClientService.Instance.Get(id);

            return View(entity);
        }

        #endregion

        #region 编辑前端用户

        /// <summary>
        /// 编辑前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetPeople(EHECD_Client entity)
        {
            return Json(ClientService.Instance.SetPeople(entity));
        }

        #endregion

        #region 后台前端用户编辑视图

        /// <summary>
        /// 后台前端用户编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetPeople(int id)
        {
            EHECD_Client entity = new EHECD_Client();
            if (id != 0)
                entity = ClientService.Instance.GetClient(id);

            #region 绑定使用单位列表

            List<EHECD_Unit> unitList = UnitService.Instance.GetListByTypeContainsFrozen(0).ToList();
            foreach(var unit in unitList)
            {
                unit.sName = unit.sName + (unit.iStatus ? "(已冻结)" : "");
            }
            //List<SelectListItem> unitselect = new List<SelectListItem>();
            //unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            //foreach (var unit in unitList)
            //{
            //    unitselect.Add(new SelectListItem() { Text = unit.sName + (entity.iStatus ? "(已冻结)" : ""), Value = unit.ID.ToString(), Selected = unit.ID == entity.iUnitID });
            //}
            ViewBag.unitList = unitList;

            #endregion

            return View(entity);
        }

        #endregion

        #region 后台前端用户编辑视图

        /// <summary>
        /// 后台前端用户编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeptDetail(int id)
        {
            ViewBag.unitList = UnitService.Instance.GetAllList().Where(o => o.iType == 0);

            EHECD_Client entity = new EHECD_Client();
            if (id != 0)
                entity = ClientService.Instance.Get(id);

            return View(entity);
        }

        #endregion

        #region 后台前端用户详情视图

        /// <summary>
        /// 后台前端用户详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            return View(ClientService.Instance.Get(id));
        }

        #endregion

        #region 后台前端值班人员详情视图

        /// <summary>
        /// 后台前端值班人员详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DutyPeopleDetail(int id)
        {
            return View(ClientService.Instance.GetClient(id));
        }

        #endregion

        #region 获取前端用户数据

        /// <summary>
        /// 获取前端用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(ClientService.Instance.GetTotalGridData(param));
        }

        #endregion

        #region 获取值班用户数据

        /// <summary>
        /// 获取值班用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetDutyGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(ClientService.Instance.GetDutyGridData(param));
        }

        #endregion

        #region 编辑前端用户

        /// <summary>
        /// 编辑前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Update(EHECD_Client entity)
        {
            return Json(ClientService.Instance.Update(entity));
        }

        #endregion

        #region 删除前端用户

        /// <summary>
        /// 删除前端用户
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(ClientService.Instance.Delete(sIds));
        }

        #endregion

        #region 冻结前端用户

        /// <summary>
        /// 冻结前端用户
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public JsonResult Frozen(int iClientID)
        {
            return Json(ClientService.Instance.Frozen(iClientID));
        }

        #endregion

        #region 解冻前端用户

        /// <summary>
        /// 解冻前端用户
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public JsonResult UnFrozen(int iClientID)
        {
            return Json(ClientService.Instance.UnFrozen(iClientID));
        }

        #endregion

        #region 重置前端用户密码

        /// <summary>
        /// 重置前端用户密码
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Reset(string sIds)
        {
            return Json(ClientService.Instance.Reset(sIds));
        }

        #endregion

        #region 修改所属使用单位/部门

        /// <summary>
        /// 修改所属使用单位
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sUnitIds"></param>
        /// <param name="sDeptIds"></param>
        /// <returns></returns>
        public JsonResult Change(int iClientID, List<string> sUnitIds, List<string> sDeptIds)
        {
            EHECD_Client entity = ClientService.Instance.Get(iClientID);
            entity.sUnitIds = sUnitIds;
            entity.sDeptIds = sDeptIds;
            LoginUser user = AuthHelper.GetLogUser();
            return Json(ClientService.Instance.Change(entity, user.sLoginName));
        }

        #endregion
    }
}