using System.Collections.Generic;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.EntityFramework.EFWork;
using EHECD.FirePatrolInspection.Web.Filter;
using EHECD.Common;
using System.Text.RegularExpressions;
using System;
using EHECD.EntityFramework.Models;
using EHECD.FirePatrolInspection.Web.Helper;
using System.Linq;
using System.Web;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
{
	/// <summary>
    /// 用户管理控制器
    /// </summary>
    public class UnitUserController : Controller
    {
        #region 列表视图

        /// <summary>
        /// 【后台用户】列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            #region 获取角色下拉信息

            LoginUser user = AuthHelper.GetLogUseUser();

            List<EHECD_UnitRole> RoleList = UnitRoleService.Instance.GetAllList().Where(o => o.iUnitID == user.iUnitID).ToList();
            List<SelectListItem> roleselect = new List<SelectListItem>();
            roleselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var role in RoleList)
            {
                roleselect.Add(new SelectListItem() { Text = role.sRoleName, Value = role.ID.ToString() });
            }
            ViewBag.RoleSelectList = roleselect;

            #endregion

            return View();
        }

        #endregion

        #region 编辑视图

        /// <summary>
        /// 【后台用户】编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            EHECD_UnitUser item = new EHECD_UnitUser();
            if (!string.IsNullOrEmpty(id))
            {
                item = UnitUserService.Instance.Get(TConvert.toInt64(id));
            }
            #region 获取角色下拉信息

            LoginUser user = AuthHelper.GetLogUseUser();

            List<EHECD_UnitRole> RoleList = UnitRoleService.Instance.GetAllList().Where(o => o.iUnitID == user.iUnitID).ToList();
            List<SelectListItem> roleselect = new List<SelectListItem>();
            foreach (var role in RoleList)
            {
                roleselect.Add(new SelectListItem() { Text = role.sRoleName, Value = role.ID.ToString(), Selected = item.iUnitRoleID == role.ID });
            }
            ViewBag.rolelist = roleselect;

            #endregion

            return View(item);
        }

        #endregion

        #region 重置密码视图

        // GET: /Admin/User/Reset
        /// <summary>
        /// 【后台用户】重置密码视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reset(string id)
        {
            ViewBag.ID = id;
            return View();
        }

        #endregion

        #region 获取列表数据

        // POST /Admin/User/GetGridData
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="param">easyui基本查询参数</param>
        /// <param name="iUnitRoleID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetGridData(QueryParams param, string iUnitRoleID)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            var result = UnitUserService.Instance.GetGridData(param, user.iUnitID, iUnitRoleID);
            return Content(TCommon.ToJsonString(result));
        }

        #endregion

        #region 新增

        // POST: /Admin/User/AddModel
        /// <summary>
        /// 新增【后台用户】数据
        /// </summary>
        /// <param name="model">新增数据模型</param>
        /// <returns>操作结果</returns> 
        [HttpPost]
        public JsonResult InsertModel(EHECD_UnitUser model)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            model.sPassWord = TCommon.Md5("123456");
            model.dCreateTime = System.DateTime.Now;
            model.iUnitID = user.iUnitID;
            //用户单位类别[0:使用单位,1:维护公司,2:消防部门]
            model.iUserUnitType = 0;
            return Json(UnitUserService.Instance.Add(model, user));
        }

        #endregion

        #region 编辑

        // POST: /Admin/User/AddModel
        /// <summary>
        /// 编辑【后台用户】数据
        /// </summary>
        /// <param name="model">新增数据模型</param>
        /// <returns>操作结果</returns> 
        [HttpPost]
        public JsonResult UpdateModel(EHECD_UnitUser model)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            return Json(UnitUserService.Instance.Edit(model, user));
        }

        #endregion

        #region 重置密码

        /// <summary>
        /// 重置【后台用户】密码
        /// </summary>
        /// <param name="model">新增数据模型</param>
        /// <returns>操作结果</returns> 
        [HttpPost]
        public JsonResult ResetModel(EHECD_UnitUser model)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            model.sPassWord = TCommon.Md5(model.sPassWord);
            return Json(UnitUserService.Instance.ResetPwd(model, user));
        }

        #endregion

        #region 批量删除

        // GET: /Admin/User/Delete 
        /// <summary>
        /// 批量删除【后台用户】数据
        /// </summary>
        /// <param name="ids">以','隔开的id字符串 </param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string ids)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            return Json(UnitUserService.Instance.Delete(ids, user));
        }

        #endregion

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [NoSign]
        public ActionResult Login()
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {//后台用户登录

                string sLoginName = Request.Form["sLoginName"];

                Regex clearSpecialSymbol = new Regex("[?*=]");
                string sPassWord = clearSpecialSymbol.Replace(Request.Form["sPassword"], string.Empty);
                ResultMessage result = UnitUserService.Instance.Login(sLoginName, sPassWord, Convert.ToInt32(Request.Form["iUserUnitType"]));
                if (result.success)
                {
                    LoginUser user = (LoginUser)result.data;
                    HttpContext.Session["UseUser"] = user;

                    //唯一的登陆ID,作为连接ID
                    Response.Cookies.Add(new HttpCookie("SignalRID") { Value = user.iUnitID.ToString() });
                }
                return Content(result.ToJson());
            }
        }

        #endregion

        #region 登出

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [NoSign]
        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.Session["UseUser"] = null;
            if (Request.IsAjaxRequest())
            {
                return null;
            }
            return Redirect("/UseDept/UnitUser/Login");
        }

        #endregion

        #region 提示页面

        /// <summary>
        /// 提示页面
        /// </summary>
        /// <returns></returns>
        [NoSign]
        public ActionResult Tip()
        {
            int iState = TConvert.toInt(Request["state"]);
            string sMessage = string.Empty;
            if (iState == 0)
            {
                sMessage = "登录已过期，";
            }
            else if (iState == 1)
            {
                sMessage = "无权限";
            }
            ViewBag.message = sMessage;
            ViewBag.state = iState;
            return View();
        }

        #endregion

        #region 修改登录密码

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public JsonResult EditPwd(long iUserID, string sOldPwd, string sPwd)
        {
            return Json(UnitUserService.Instance.EditPwd(iUserID, sOldPwd, sPwd));
        }

        #endregion
    }
}