using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Specialized;
using EHECD.FirePatrolInspection.Service;
using EHECD.EntityFramework.EFWork;
using EHECD.Common;
using Newtonsoft.Json.Linq;

namespace EHECD.FirePatrolInspection.Web.Areas.FireDept.Controllers
{
	/// <summary>
    /// 后台模块表控制器
    /// </summary>
    public class UnitModuleController : Controller
    {
        #region 列表页视图

        /// <summary>
        /// 列表页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region 获取顶级模块

            List<EHECD_UnitModule> RoleList = UnitModuleService.Instance.GetTopList();
            List<SelectListItem> roleselect = new List<SelectListItem>();
            roleselect.Add(new SelectListItem() { Text = "所有类别", Value = string.Empty });
            foreach (var item in RoleList)
            {
                roleselect.Add(new SelectListItem() { Text = item.sModuleName, Value = item.ID.ToString() });
            }
            ViewBag.rolelist = roleselect;

            #endregion

            return View();
        }

        #endregion

        #region 获取列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult List(int rows, int page, string sPID, string sModuleName)
        {
            return Content(UnitModuleService.Instance.GetChildrenList(rows, page, sPID, sModuleName, 2));
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
                return View();
            }
            else
            {
                var inputs = new NameValueCollection(Request.Form);


                JObject jInput = TJson.LoadFromStr(Tool.ToJson(inputs));
                EHECD_UnitModule item = new EHECD_UnitModule()
                {
                    dInsertTime = DateTime.Now,
                    iOrderID = TJson.getTextAsInt(jInput, "iOrderID"),
                    iPID = TJson.getTextAsInt64(jInput, "iPID"),
                    sModuleName = TJson.getText(jInput, "sModuleName"),
                    sModuleUrl = TJson.getText(jInput, "sModuleUrl"),
                    bIsLink = TJson.getTextAsBool(jInput, "bIsLink")
                };
                List<EHECD_UnitModuleAction> ModuleActionList = new List<EHECD_UnitModuleAction>();
                string[] sActionNameList = { };
                string[] sActionUrlList = { };
                string[] iOrderList = { };


                foreach (var k in inputs.AllKeys)
                {
                    if (k == "sActionName") sActionNameList = inputs[k].Split(',');
                    if (k == "sActionUrl") sActionUrlList = inputs[k].Split(',');
                    if (k == "iOrder") iOrderList = inputs[k].Split(',');
                }
                for (int i = 0; i < sActionNameList.Count(); i++)
                {
                    EHECD_UnitModuleAction ModuleAction = new EHECD_UnitModuleAction()
                    {
                        sActionName = sActionNameList[i],
                        sActionUrl = sActionUrlList[i],
                        iOrder = TConvert.toInt(iOrderList[i])
                    };
                    ModuleActionList.Add(ModuleAction);
                }
                return Content(UnitModuleService.Instance.Add(item, ModuleActionList));
            }
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(EHECD_UnitModule item)
        {
            if (Request.HttpMethod == "GET")
            {
                EHECD_UnitModule olditem = UnitModuleService.Instance.Get(item.ID);

                #region 获取顶级模块

                List<EHECD_UnitModule> RoleList = UnitModuleService.Instance.GetNoLinkList();
                List<SelectListItem> roleselect = new List<SelectListItem>();
                foreach (var module in RoleList)
                {
                    roleselect.Add(new SelectListItem() { Text = module.sModuleName, Value = module.ID.ToString(), Selected = module.ID == olditem.iPID });
                }
                ViewBag.rolelist = roleselect;

                #endregion

                return View(olditem);
            }
            else
            {
                var inputs = new NameValueCollection(Request.Form);
                JObject jInput = TJson.LoadFromStr(Tool.ToJson(inputs));
                item.sModuleUrl = TJson.getText(jInput, "sModuleUrl");

                List<EHECD_UnitModuleAction> ModuleActionList = new List<EHECD_UnitModuleAction>();
                string[] sActionNameList = { };
                string[] sActionUrlList = { };
                string[] iOrderList = { };


                foreach (var k in inputs.AllKeys)
                {
                    if (k == "sActionName") sActionNameList = inputs[k].Split(',');
                    if (k == "sActionUrl") sActionUrlList = inputs[k].Split(',');
                    if (k == "iOrder") iOrderList = inputs[k].Split(',');
                }
                for (int i = 0; i < sActionNameList.Count(); i++)
                {
                    EHECD_UnitModuleAction ModuleAction = new EHECD_UnitModuleAction()
                    {
                        sActionName = sActionNameList[i],
                        sActionUrl = sActionUrlList[i],
                        iOrder = TConvert.toInt(iOrderList[i])
                    };
                    ModuleActionList.Add(ModuleAction);
                }
                return Content(UnitModuleService.Instance.Edit(item, ModuleActionList));
            }
        }

        #endregion

        #region 获取模块

        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetModuleAction(long id)
        {
            return Content(UnitModuleActionService.Instance.GetModuleAction(id));
        }

        #endregion

        #region 删除模块

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string ids)
        {
            return Content(UnitModuleService.Instance.Delete(ids));
        }

        #endregion

        #region 获取无链接的数据

        /// <summary>
        /// 获取无链接的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNoLink()
        {
            return Content(UnitModuleService.Instance.GetNoLinkForcombotree());
        }

        #endregion
    }
}