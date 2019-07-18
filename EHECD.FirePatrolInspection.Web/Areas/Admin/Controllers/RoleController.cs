using System.Collections.Generic;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.EntityFramework.EFWork;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
	/// <summary>
    /// 角色表控制器
    /// </summary>
    public class RoleController : Controller
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
            if (Request.HttpMethod == "GET")
            {
                #region 获取所有模块

                List<EHECD_Module> ModuleList = ModuleService.Instance.GetList();

                #endregion

                #region 获取所有模块权限

                List<EHECD_ModuleAction> ModuleActionList = ModuleActionService.Instance.GetList();

                #endregion

                ViewBag.ModuleList = ModuleList;
                ViewBag.ModuleActionList = ModuleActionList;
                return View();
            }
            else
            {
                EHECD_Role item = new EHECD_Role()
                {
                    sRoleName = Request.Form["sRoleName"],
                    sDescription = Request.Form["sDescription"]
                };
                List<EHECD_RoleAction> List = new List<EHECD_RoleAction>();
                string sAction = Request.Form["Action"];
                if (!string.IsNullOrEmpty(sAction))
                {
                    string[] sActions = sAction.Split(',');
                    foreach (string Action in sActions)
                    {
                        EHECD_RoleAction RoleAction = new EHECD_RoleAction()
                        {
                            iModuleID = TConvert.toInt64(Action.Split('_')[0]),
                            iActionID = TConvert.toInt64(Action.Split('_')[1])
                        };
                        List.Add(RoleAction);
                    }
                }
                return Content(RoleService.Instance.Add(item, List));
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

                List<EHECD_Module> ModuleList = ModuleService.Instance.GetList();

                #endregion

                #region 获取所有模块权限

                List<EHECD_ModuleAction> ModuleActionList = ModuleActionService.Instance.GetList();

                #endregion

                #region 获取角色的权限

                List<EHECD_RoleAction> RoleActionList = RoleActionService.Instance.GetRoleActionListByRoleID(id);

                #endregion

                ViewBag.ModuleList = ModuleList;
                ViewBag.ModuleActionList = ModuleActionList;
                ViewBag.RoleActionList = RoleActionList;

                return View(RoleService.Instance.Get(id));
            }
            else
            {
                EHECD_Role item = new EHECD_Role()
                {
                    ID = id,
                    sRoleName = Request.Form["sRoleName"],
                    sDescription = Request.Form["sDescription"]
                };
                List<EHECD_RoleAction> List = new List<EHECD_RoleAction>();
                string sAction = Request.Form["Action"];
                if (!string.IsNullOrEmpty(sAction))
                {
                    string[] sActions = sAction.Split(',');
                    foreach (string Action in sActions)
                    {
                        EHECD_RoleAction RoleAction = new EHECD_RoleAction()
                        {
                            iRoleID = id,
                            iModuleID = TConvert.toInt64(Action.Split('_')[0]),
                            iActionID = TConvert.toInt64(Action.Split('_')[1])
                        };
                        List.Add(RoleAction);
                    }
                }
                return Content(RoleService.Instance.Edit(item, List));
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
            return Content(RoleService.Instance.Delete(ids));
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
            return Content(RoleService.Instance.GetList(rows, page, sort, order, sRoleName));
        }

        #endregion
    }
}