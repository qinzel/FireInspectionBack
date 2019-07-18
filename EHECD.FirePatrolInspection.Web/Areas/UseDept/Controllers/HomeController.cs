using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using EHECD.Common;
using EHECD.EntityFramework.EFWork;
using EHECD.EntityFramework.Models;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Web.Helper;
using System.Threading;
using System.Globalization;
using System.Web;
using System;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 后台首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region 获取模块列表

            LoginUser user = AuthHelper.GetLogUseUser();
            if (user == null)
            {
                return Redirect("/UseDept/UnitUser/Tip");
            }

            StringBuilder sMenu = new StringBuilder();
            List<EHECD_UnitModule> moduleList = UnitModuleService.Instance.GetList(user.iUserUnitType);
            if (moduleList.Count > 0)
            {
                List<EHECD_UnitModule> parentList = moduleList.Where(m => m.iPID == 0).ToList();//获取大模块
                if (parentList != null)
                {
                    foreach (EHECD_UnitModule pModule in parentList)
                    {//遍历大模块
                        long iModuleID = pModule.ID;
                        string sChildJson = InitMenu(iModuleID, moduleList);
                        if (sChildJson != string.Empty)
                        {
                            if (sMenu.Length == 0)
                            {
                                sMenu.AppendLine("{'menus':[");
                            }
                            sMenu.AppendLine("   { " + InitJsonItem(iModuleID.ToString(), pModule.sModuleName, pModule.sModuleUrl, pModule.bIsLink));//添加大模块菜单
                            sMenu.AppendLine("    " + sChildJson);//添加子模块菜单
                            sMenu.AppendLine("},");
                        }
                    }
                }
                if (sMenu.Length > 0)
                {//添加最后一个]
                    int iDouHaoIndex = sMenu.ToString().LastIndexOf(',');
                    sMenu = sMenu.Remove(iDouHaoIndex, 1);
                    sMenu.Append("]}");
                }
            }
            string menu = sMenu.ToString();
            if (string.IsNullOrEmpty(menu))
            {
                menu = "{menus:[]}";
            }
            ViewBag.menu = menu;

            #endregion

            ViewBag.sRealName = user.sRealName;
            ViewBag.iUserID = user.ID;
            EHECD_Unit unit = UnitService.Instance.Get(user.iUnitID);
            ViewBag.sUnitName = unit.ID + "." + unit.sName + "." + user.ID;

            return View();
        }

        /// <summary>
        /// 组织子菜单
        /// </summary>
        /// <param name="iModuleID"></param>
        /// <param name="moduleList"></param>
        /// <returns></returns>
        private string InitMenu(long iModuleID, List<EHECD_UnitModule> modulelist)
        {
            StringBuilder sResult = new StringBuilder();
            LoginUser user = AuthHelper.GetLogUseUser();
            if (user == null)
            {
                return String.Empty;
            }
            List<EHECD_UnitModule> childList = modulelist.Where(m => m.iPID == iModuleID).ToList();//获取子模块菜单
            if (childList != null && childList.Count > 0)
            {
                if (user.UserType == UserType.Admin)
                {//管理员 则显示所有的菜单模块
                    foreach (EHECD_UnitModule module in childList)
                    {
                        long iTempModuleID = module.ID;
                        if (sResult.Length == 0)
                        {
                            sResult.AppendLine("'menus':[");
                        }
                        if (module.bIsLink || (!module.bIsLink && modulelist.Any(m => m.iPID == iTempModuleID)))
                        {
                            sResult.Append("    {" +
                                           InitJsonItem(iTempModuleID.ToString(), module.sModuleName, module.sModuleUrl,
                                               module.bIsLink));
                            if (!module.bIsLink)
                            {
                                sResult.Append(InitMenu(iTempModuleID, modulelist));
                            }
                            sResult.AppendLine("},");
                        }
                    }
                }
                else
                //if (loginUser.UserType == UserType.普通用户)
                {//普通后台用户判断权限
                    List<RoleAction> userActionList = user.UserActionList;//当前登录用户权限
                    foreach (EHECD_UnitModule module in childList)
                    {
                        long iTempModuleID = module.ID;
                        if (module.bIsLink)
                        {
                            if (!userActionList.Exists(m => m.iModuleID == iTempModuleID))
                            {
                                //如果用户没有该模块的权限 则进入下一个循环
                                continue;
                            }
                        }
                        else
                        {
                            bool bIsHave = false;
                            List<EHECD_UnitModule> ThirdList = modulelist.Where(m => m.iPID == iTempModuleID).ToList();//获取三级模块菜单
                            foreach (EHECD_UnitModule ThirdModule in ThirdList)
                            {
                                if (userActionList.Exists(m => m.iModuleID == ThirdModule.ID))
                                {
                                    //如果用户没有该模块的权限 则进入下一个循环
                                    bIsHave = true;
                                }
                            }
                            if (!bIsHave)
                            {
                                continue;
                            }
                        }
                        if (sResult.Length == 0)
                        {
                            sResult.AppendLine("'menus':[");
                        }
                        if (module.bIsLink || (!module.bIsLink && modulelist.Any(m => m.iPID == iTempModuleID)))
                        {
                            sResult.Append("    {" +
                                           InitJsonItem(iTempModuleID.ToString(), module.sModuleName, module.sModuleUrl,
                                               module.bIsLink));
                            if (!module.bIsLink)
                            {
                                sResult.Append(InitMenu(iTempModuleID, modulelist));
                            }
                            sResult.AppendLine("},");
                        }
                    }
                }
            }
            if (sResult.Length > 0)
            {
                //添加最后一个]
                int iDouHaoIndex = sResult.ToString().LastIndexOf(',');
                if (iDouHaoIndex >= 0)
                {
                    sResult = sResult.Remove(iDouHaoIndex, 1);
                }
                sResult.Append("]");
            }
            return sResult.ToString();
        }

        /// <summary>
        /// 组装单个json菜单数据
        /// </summary>
        /// <param name="sID"></param>
        /// <param name="sText"></param>
        /// <param name="sValue"></param>
        /// <returns></returns>
        private string InitJsonItem(string sID, string menuname, string url, bool bIsLink)
        {
            StringBuilder sResult = new StringBuilder();
            //sResult.AppendLine("{");
            sResult.Append("'menuid':'" + sID + "',");
            sResult.Append("'menuname':'" + menuname + "',");
            sResult.Append("'url':'" + url + "',");
            sResult.Append("'bIsLink':'" + bIsLink + "',");
            //sResult.AppendLine("}");
            return sResult.ToString();
        }
    }
}