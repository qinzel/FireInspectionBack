using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Specialized;
using EHECD.FirePatrolInspection.Service;
using EHECD.EntityFramework.EFWork;
using EHECD.Common;
using Newtonsoft.Json.Linq;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
	/// <summary>
    /// 后台模块表控制器
    /// </summary>
    public class ModuleController : Controller
    {
        #region 列表页视图

        /// <summary>
        /// 列表页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region 获取顶级模块

            List<EHECD_Module> RoleList = ModuleService.Instance.GetTopList();
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
            return Content(ModuleService.Instance.GetChildrenList(rows, page, sPID, sModuleName));
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
                EHECD_Module item = new EHECD_Module()
                {
                    dInsertTime = DateTime.Now,
                    iOrderID = TJson.getTextAsInt(jInput, "iOrderID"),
                    iPID = TJson.getTextAsInt64(jInput, "iPID"),
                    sModuleName = TJson.getText(jInput, "sModuleName"),
                    sModuleUrl = TJson.getText(jInput, "sModuleUrl"),
                    bIsLink = TJson.getTextAsBool(jInput, "bIsLink")
                };
                List<EHECD_ModuleAction> ModuleActionList = new List<EHECD_ModuleAction>();
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
                    EHECD_ModuleAction ModuleAction = new EHECD_ModuleAction()
                    {
                        sActionName = sActionNameList[i],
                        sActionUrl = sActionUrlList[i],
                        iOrder = TConvert.toInt(iOrderList[i])
                    };
                    ModuleActionList.Add(ModuleAction);
                }
                return Content(ModuleService.Instance.Add(item, ModuleActionList));
            }
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(EHECD_Module item)
        {
            if (Request.HttpMethod == "GET")
            {
                EHECD_Module olditem = ModuleService.Instance.Get(item.ID);

                #region 获取顶级模块

                List<EHECD_Module> RoleList = ModuleService.Instance.GetNoLinkList();
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

                List<EHECD_ModuleAction> ModuleActionList = new List<EHECD_ModuleAction>();
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
                    EHECD_ModuleAction ModuleAction = new EHECD_ModuleAction()
                    {
                        sActionName = sActionNameList[i],
                        sActionUrl = sActionUrlList[i],
                        iOrder = TConvert.toInt(iOrderList[i])
                    };
                    ModuleActionList.Add(ModuleAction);
                }
                return Content(ModuleService.Instance.Edit(item, ModuleActionList));
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
            return Content(ModuleActionService.Instance.GetModuleAction(id));
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
            return Content(ModuleService.Instance.Delete(ids));
        }

        #endregion

        #region 获取无链接的数据

        /// <summary>
        /// 获取无链接的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNoLink()
        {
            return Content(ModuleService.Instance.GetNoLinkForcombotree());
        }

        #endregion
    }
}