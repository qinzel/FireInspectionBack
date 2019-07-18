using System.Collections.Generic;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.EntityFramework.EFWork;
using EHECD.Common;
using EHECD.EntityFramework.Models;
using EHECD.FirePatrolInspection.Web.Helper;

namespace EHECD.FirePatrolInspection.Web.Areas.RepairDept.Controllers
{
	/// <summary>
    /// 角色表控制器
    /// </summary>
    public class UnitRoleController : Controller
    {
        #region 列表页视图

        /// <summary>
        /// 列表页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            LoginUser user = AuthHelper.GetLogRepairUser();

            if (Request.HttpMethod == "GET")
            {
                #region 获取所有模块

                List<EHECD_UnitModule> ModuleList = UnitModuleService.Instance.GetList(user.iUserUnitType);

                #endregion

                #region 获取所有模块权限

                List<EHECD_UnitModuleAction> ModuleActionList = UnitModuleActionService.Instance.GetList();

                #endregion

                ViewBag.ModuleList = ModuleList;
                ViewBag.ModuleActionList = ModuleActionList;
                return View();
            }
            else
            {
                EHECD_UnitRole item = new EHECD_UnitRole()
                {
                    sRoleName = Request.Form["sRoleName"],
                    sDescription = Request.Form["sDescription"],
                    iUnitID = user.iUnitID
                };
                List<EHECD_UnitRoleAction> List = new List<EHECD_UnitRoleAction>();
                string sAction = Request.Form["Action"];
                if (!string.IsNullOrEmpty(sAction))
                {
                    string[] sActions = sAction.Split(',');
                    foreach (string Action in sActions)
                    {
                        EHECD_UnitRoleAction RoleAction = new EHECD_UnitRoleAction()
                        {
                            iModuleID = TConvert.toInt64(Action.Split('_')[0]),
                            iActionID = TConvert.toInt64(Action.Split('_')[1])
                        };
                        List.Add(RoleAction);
                    }
                }
                return Content(UnitRoleService.Instance.Add(item, List, user));
            }
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(long id)
        {
            if (Request.HttpMethod == "GET")
            {
                #region 获取所有模块

                LoginUser user = AuthHelper.GetLogRepairUser();
                List<EHECD_UnitModule> ModuleList = UnitModuleService.Instance.GetList(user.iUserUnitType);

                #endregion

                #region 获取所有模块权限

                List<EHECD_UnitModuleAction> ModuleActionList = UnitModuleActionService.Instance.GetList();

                #endregion

                #region 获取角色的权限

                List<EHECD_UnitRoleAction> RoleActionList = UnitRoleActionService.Instance.GetRoleActionListByRoleID(id);

                #endregion

                ViewBag.ModuleList = ModuleList;
                ViewBag.ModuleActionList = ModuleActionList;
                ViewBag.RoleActionList = RoleActionList;

                return View(UnitRoleService.Instance.Get(id));
            }
            else
            {
                LoginUser user = AuthHelper.GetLogRepairUser();

                EHECD_UnitRole item = new EHECD_UnitRole()
                {
                    ID = id,
                    iUnitID = user.iUnitID,
                    sRoleName = Request.Form["sRoleName"],
                    sDescription = Request.Form["sDescription"]
                };
                List<EHECD_UnitRoleAction> List = new List<EHECD_UnitRoleAction>();
                string sAction = Request.Form["Action"];
                if (!string.IsNullOrEmpty(sAction))
                {
                    string[] sActions = sAction.Split(',');
                    foreach (string Action in sActions)
                    {
                        EHECD_UnitRoleAction RoleAction = new EHECD_UnitRoleAction()
                        {
                            iUnitRoleID = id,
                            iModuleID = TConvert.toInt64(Action.Split('_')[0]),
                            iActionID = TConvert.toInt64(Action.Split('_')[1])
                        };
                        List.Add(RoleAction);
                    }
                }
                return Content(UnitRoleService.Instance.Edit(item, List, user));
            }
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string ids)
        {
            LoginUser user = AuthHelper.GetLogRepairUser();
            return Content(UnitRoleService.Instance.Delete(ids, user));
        }

        #endregion

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult List(int rows, int page, string sort, string order, string sRoleName)
        {
            LoginUser user = AuthHelper.GetLogRepairUser();
            return Content(UnitRoleService.Instance.GetList(rows, page, sort, order, sRoleName, user.iUnitID));
        }

        #endregion
    }
}