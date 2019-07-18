using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;
using System.Linq;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
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
            #region 绑定所属部门列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_Dept> deptList = DeptService.Instance.GetListByUnitID(user.iUnitID).ToList();
            List<SelectListItem> deptselect = new List<SelectListItem>();
            deptselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in deptList)
            {
                deptselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.deptList = deptselect;

            #endregion

            return View();
        }

        #endregion

        #region 后台点检员申请列表视图

        /// <summary>
        /// 后台点检员申请列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult InspectorAdoptList()
        {
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
            return View();
        }

        #endregion

        #region 后台人员列表视图

        /// <summary>
        /// 后台人员列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
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
        public JsonResult Set(EHECD_Client entity)
        {
            if (entity.ID == 0)
            {
                LoginUser user = AuthHelper.GetLogUseUser();
                entity.iUnitID = user.iUnitID;
                entity.iType = 2;
            }
            return Json(ClientService.Instance.Set(entity));
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

        #region 后台前端待审核用户详情视图

        /// <summary>
        /// 后台前端待审核用户详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UnAdoptDetail(int id)
        {
            return View(ClientService.Instance.GetAdoptInfo(id));
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
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(ClientService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取前端用户申请数据

        /// <summary>
        /// 获取前端用户申请数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetAdoptGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(ClientService.Instance.GetAdoptList(param));
        }

        #endregion

        #region 获取单位部门前端用户数据

        /// <summary>
        /// 获取使用单位前端人员信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetUseDataList([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(ClientService.Instance.GetUseDataList(param));
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
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(ClientService.Instance.GetDutyGridData(param));
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
            LoginUser user = AuthHelper.GetLogUseUser();
            entity.iUnitID = user.iUnitID;
            return Json(ClientService.Instance.SetPeople(entity));
        }

        #endregion

        #region 通过审核

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="iClientDeptRelID"></param>
        /// <returns></returns>
        public JsonResult Adopt(int iClientDeptRelID)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            return Json(ClientService.Instance.Adopt(iClientDeptRelID, user));
        }

        #endregion

        #region 拒绝通过

        /// <summary>
        /// 拒绝通过
        /// </summary>
        /// <param name="iClientDeptRelID"></param>
        /// <returns></returns>
        public JsonResult Refused(int iClientDeptRelID)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            return Json(ClientService.Instance.Refused(iClientDeptRelID, user));
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
    }
}