using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using EHECD.Common;
using EHECD.EntityFramework.EFWork;
using EHECD.EntityFramework.Models;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Web.Helper;

namespace EHECD.FirePatrolInspection.Web.Filter
{
    /// <summary>
    /// 后台session 权限 过滤器
    /// </summary>
    public class SessionAndAuthority : ActionFilterAttribute
    {
        //后台登录用户
        protected LoginUser adminloginUser
        {
            get
            {
                return AuthHelper.GetLogUser();
            }
        }

        //消防部门登录用户
        protected LoginUser loginFireUser
        {
            get
            {
                return AuthHelper.GetLogFireUser();
            }
        }

        //维护公司登录用户
        protected LoginUser loginRepairUser
        {
            get
            {
                return AuthHelper.GetLogRepairUser();
            }
        }

        //使用单位登录用户
        protected LoginUser loginUseUser
        {
            get
            {
                return AuthHelper.GetLogUseUser();
            }
        }

        private static List<EHECD_Module> _Share_ModuleList = null;
        /// <summary>
        /// 模块列表
        /// </summary>
        private List<EHECD_Module> Share_ModuleList
        {
            get
            {
                if (_Share_ModuleList == null)
                {
                    _Share_ModuleList = ModuleService.Instance.GetList();
                    return _Share_ModuleList;
                }
                else
                {
                    return _Share_ModuleList;
                }
            }
        }

        private static List<EHECD_ModuleAction> _Share_ActionList = null;
        /// <summary>
        /// 模块权限库列表
        /// </summary>
        private List<EHECD_ModuleAction> Share_ActionList
        {
            get
            {
                if (_Share_ActionList == null)
                {
                    _Share_ActionList = ModuleActionService.Instance.GetList();
                    return _Share_ActionList;
                }
                else
                {
                    return _Share_ActionList;
                }
            }
        }

        private static List<EHECD_UnitModule> _Share_UnitModuleList = null;
        /// <summary>
        /// 模块列表
        /// </summary>
        private List<EHECD_UnitModule> Share_UnitModuleList
        {
            get
            {
                if (_Share_UnitModuleList == null)
                {
                    _Share_UnitModuleList = UnitModuleService.Instance.GetAllList();
                    return _Share_UnitModuleList;
                }
                else
                {
                    return _Share_UnitModuleList;
                }
            }
        }

        private static List<EHECD_UnitModuleAction> _Share_UnitActionList = null;
        /// <summary>
        /// 模块权限库列表
        /// </summary>
        private List<EHECD_UnitModuleAction> Share_UnitActionList
        {
            get
            {
                if (_Share_UnitActionList == null)
                {
                    _Share_UnitActionList = UnitModuleActionService.Instance.GetList();
                    return _Share_UnitActionList;
                }
                else
                {
                    return _Share_UnitActionList;
                }
            }
        }


        //操作是否需要判断
        private static bool SkipNoSign(ActionExecutingContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes(typeof(NoSignAttribute), true).Length == 1;//有NoSign属性  true
        }

        //在执行操作方法之前 判断登录情况和页面权限
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SkipNoSign(filterContext)) //是否该类标记为NoSign，如果是则不需要判断
            {
                base.OnActionExecuting(filterContext);
                return;
            }
            if (filterContext.RouteData.DataTokens["Area"] as string == "Admin")
            {
                #region Admin后台判断

                #region 先判断session

                if (null == adminloginUser)
                {
                    //session 过期
                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        // 请求跳转到Tip页面
                        filterContext.Result = new RedirectResult("/Admin/User/Tip?state=0");
                    }
                    else
                    {
                        //ajax请求 返回json格式提示
                        if (filterContext.HttpContext.Request.HttpMethod == "GET")
                        {
                            filterContext.Result = new RedirectResult("/Admin/User/Tip?state=0");
                        }
                        else
                        {
                            ContentResult content = new ContentResult();
                            ResultMessage msg = new ResultMessage() { success = false, message = "登录已过期，请重新登录！" };
                            content.Content = msg.ToJson();
                            filterContext.Result = content;
                        }
                    }
                }
                #endregion

                #region 再判断权限

                else
                {
                    string sPath =
                        filterContext.RequestContext.HttpContext.Request.Path.ToUpper()
                            .Replace("INDEX", "")
                            .TrimEnd('/');
                    if (adminloginUser.UserType != UserType.Admin)
                    {
                        //普通用户和员工需要判断权限
                        EHECD_Module tempModule =
                            Share_ModuleList.FirstOrDefault(
                                m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath); //查询地址在模块中是否存在
                        if (tempModule != null)
                        {
                            //如果存在 表示该页面受权限控制
                            bool bHasRight = adminloginUser.UserActionList.Exists(m => m.iModuleID == tempModule.ID);
                            //检查用户是否拥有该模块的权限
                            if (!bHasRight)
                            {
                                //无权限
                                filterContext.Result = new RedirectResult("/Admin/User/Tip?state=1");
                            }
                        }
                        else
                        {
                            EHECD_ModuleAction tempAction =
                                Share_ActionList.FirstOrDefault(
                                    m => m.sActionUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath);
                            //查询地址在权限库中是否存在
                            if (tempAction != null)
                            {
                                //如果是权限库中的页面 则应判断权限
                                bool bHasRight = adminloginUser.UserActionList.Exists(m => m.iActionID == tempAction.ID);
                                //检查用户是否拥有该操作的权限
                                if (!bHasRight)
                                {
                                    //无权限
                                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                                    //模块权限库需要区分 get 和 post 两种请求方式
                                    {
                                        //请求直接返回无权限
                                        filterContext.Result = new RedirectResult("/Admin/User/Tip?state=1");
                                    }
                                    else
                                    {
                                        //ajax请求 返回json格式提示
                                        if (filterContext.HttpContext.Request.HttpMethod == "GET")
                                        {
                                            filterContext.Result = new RedirectResult("/Admin/User/Tip?state=1");
                                        }
                                        else
                                        {
                                            var content = new ContentResult();
                                            content.Content = "{\"success\":false,\"message\":\"无权限\"}";
                                            filterContext.Result = content;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }
            else if (filterContext.RouteData.DataTokens["Area"] as string == "FireDept")
            {
                #region Admin后台判断

                #region 先判断session

                if (null == loginFireUser)
                {
                    //session 过期
                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        // 请求跳转到Tip页面
                        filterContext.Result = new RedirectResult("/FireDept/UnitUser/Tip?state=0");
                    }
                    else
                    {
                        //ajax请求 返回json格式提示
                        if (filterContext.HttpContext.Request.HttpMethod == "GET")
                        {
                            filterContext.Result = new RedirectResult("/FireDept/UnitUser/Tip?state=0");
                        }
                        else
                        {
                            ContentResult content = new ContentResult();
                            ResultMessage msg = new ResultMessage() { success = false, message = "登录已过期，请重新登录！" };
                            content.Content = msg.ToJson();
                            filterContext.Result = content;
                        }
                    }
                }

                #endregion

                #region 再判断权限

                else
                {
                    string sPath =
                        filterContext.RequestContext.HttpContext.Request.Path.ToUpper()
                            .Replace("INDEX", "")
                            .TrimEnd('/');
                    if (loginFireUser.UserType != UserType.Admin)
                    {
                        //普通用户和员工需要判断权限
                        EHECD_Module tempModule =
                            Share_ModuleList.FirstOrDefault(
                                m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath); //查询地址在模块中是否存在
                        if (tempModule != null)
                        {
                            //如果存在 表示该页面受权限控制
                            bool bHasRight = loginFireUser.UserActionList.Exists(m => m.iModuleID == tempModule.ID);
                            //检查用户是否拥有该模块的权限
                            if (!bHasRight)
                            {
                                //无权限
                                filterContext.Result = new RedirectResult("/FireDept/UnitUser/Tip?state=1");
                            }
                        }
                        else
                        {
                            EHECD_ModuleAction tempAction =
                                Share_ActionList.FirstOrDefault(
                                    m => m.sActionUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath);
                            //查询地址在权限库中是否存在
                            if (tempAction != null)
                            {
                                //如果是权限库中的页面 则应判断权限
                                bool bHasRight = loginFireUser.UserActionList.Exists(m => m.iActionID == tempAction.ID);
                                //检查用户是否拥有该操作的权限
                                if (!bHasRight)
                                {
                                    //无权限
                                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                                    //模块权限库需要区分 get 和 post 两种请求方式
                                    {
                                        //请求直接返回无权限
                                        filterContext.Result = new RedirectResult("/FireDept/UnitUser/Tip?state=1");
                                    }
                                    else
                                    {
                                        //ajax请求 返回json格式提示
                                        if (filterContext.HttpContext.Request.HttpMethod == "GET")
                                        {
                                            filterContext.Result = new RedirectResult("/FireDept/UnitUser/Tip?state=1");
                                        }
                                        else
                                        {
                                            var content = new ContentResult();
                                            content.Content = "{\"success\":false,\"message\":\"无权限\"}";
                                            filterContext.Result = content;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }
            else if (filterContext.RouteData.DataTokens["Area"] as string == "RepairDept")
            {
                #region Admin后台判断

                #region 先判断session

                if (null == loginRepairUser)
                {
                    //session 过期
                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        // 请求跳转到Tip页面
                        filterContext.Result = new RedirectResult("/RepairDept/UnitUser/Tip?state=0");
                    }
                    else
                    {
                        //ajax请求 返回json格式提示
                        if (filterContext.HttpContext.Request.HttpMethod == "GET")
                        {
                            filterContext.Result = new RedirectResult("/RepairDept/UnitUser/Tip?state=0");
                        }
                        else
                        {
                            ContentResult content = new ContentResult();
                            ResultMessage msg = new ResultMessage() { success = false, message = "登录已过期，请重新登录！" };
                            content.Content = msg.ToJson();
                            filterContext.Result = content;
                        }
                    }
                }

                #endregion

                #region 再判断权限

                else
                {
                    string sPath =
                        filterContext.RequestContext.HttpContext.Request.Path.ToUpper()
                            .Replace("INDEX", "")
                            .TrimEnd('/');
                    if (loginRepairUser.UserType != UserType.Admin)
                    {
                        //普通用户和员工需要判断权限
                        EHECD_Module tempModule =
                            Share_ModuleList.FirstOrDefault(
                                m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath); //查询地址在模块中是否存在
                        if (tempModule != null)
                        {
                            //如果存在 表示该页面受权限控制
                            bool bHasRight = loginRepairUser.UserActionList.Exists(m => m.iModuleID == tempModule.ID);
                            //检查用户是否拥有该模块的权限
                            if (!bHasRight)
                            {
                                //无权限
                                filterContext.Result = new RedirectResult("/RepairDept/UnitUser/Tip?state=1");
                            }
                        }
                        else
                        {
                            EHECD_ModuleAction tempAction =
                                Share_ActionList.FirstOrDefault(
                                    m => m.sActionUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath);
                            //查询地址在权限库中是否存在
                            if (tempAction != null)
                            {
                                //如果是权限库中的页面 则应判断权限
                                bool bHasRight = loginRepairUser.UserActionList.Exists(m => m.iActionID == tempAction.ID);
                                //检查用户是否拥有该操作的权限
                                if (!bHasRight)
                                {
                                    //无权限
                                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                                    //模块权限库需要区分 get 和 post 两种请求方式
                                    {
                                        //请求直接返回无权限
                                        filterContext.Result = new RedirectResult("/RepairDept/UnitUser/Tip?state=1");
                                    }
                                    else
                                    {
                                        //ajax请求 返回json格式提示
                                        if (filterContext.HttpContext.Request.HttpMethod == "GET")
                                        {
                                            filterContext.Result = new RedirectResult("/RepairDept/UnitUser/Tip?state=1");
                                        }
                                        else
                                        {
                                            var content = new ContentResult();
                                            content.Content = "{\"success\":false,\"message\":\"无权限\"}";
                                            filterContext.Result = content;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }
            else if (filterContext.RouteData.DataTokens["Area"] as string == "UseDept")
            {
                #region Admin后台判断

                #region 先判断session

                if (null == loginUseUser)
                {
                    //session 过期
                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        // 请求跳转到Tip页面
                        filterContext.Result = new RedirectResult("/UseDept/UnitUser/Tip?state=0");
                    }
                    else
                    {
                        //ajax请求 返回json格式提示
                        if (filterContext.HttpContext.Request.HttpMethod == "GET")
                        {
                            filterContext.Result = new RedirectResult("/UseDept/UnitUser/Tip?state=0");
                        }
                        else
                        {
                            ContentResult content = new ContentResult();
                            ResultMessage msg = new ResultMessage() { success = false, message = "登录已过期，请重新登录！" };
                            content.Content = msg.ToJson();
                            filterContext.Result = content;
                        }
                    }
                }

                #endregion

                #region 再判断权限

                else
                {
                    string sPath =
                        filterContext.RequestContext.HttpContext.Request.Path.ToUpper()
                            .Replace("INDEX", "")
                            .TrimEnd('/');
                    if (loginUseUser.UserType != UserType.Admin)
                    {
                        //普通用户和员工需要判断权限
                        EHECD_Module tempModule =
                            Share_ModuleList.FirstOrDefault(
                                m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath); //查询地址在模块中是否存在
                        if (tempModule != null)
                        {
                            //如果存在 表示该页面受权限控制
                            bool bHasRight = loginUseUser.UserActionList.Exists(m => m.iModuleID == tempModule.ID);
                            //检查用户是否拥有该模块的权限
                            if (!bHasRight)
                            {
                                //无权限
                                filterContext.Result = new RedirectResult("/UseDept/UnitUser/Tip?state=1");
                            }
                        }
                        else
                        {
                            EHECD_ModuleAction tempAction =
                                Share_ActionList.FirstOrDefault(
                                    m => m.sActionUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath);
                            //查询地址在权限库中是否存在
                            if (tempAction != null)
                            {
                                //如果是权限库中的页面 则应判断权限
                                bool bHasRight = loginUseUser.UserActionList.Exists(m => m.iActionID == tempAction.ID);
                                //检查用户是否拥有该操作的权限
                                if (!bHasRight)
                                {
                                    //无权限
                                    if (!filterContext.HttpContext.Request.IsAjaxRequest())
                                    //模块权限库需要区分 get 和 post 两种请求方式
                                    {
                                        //请求直接返回无权限
                                        filterContext.Result = new RedirectResult("/UseDept/UnitUser/Tip?state=1");
                                    }
                                    else
                                    {
                                        //ajax请求 返回json格式提示
                                        if (filterContext.HttpContext.Request.HttpMethod == "GET")
                                        {
                                            filterContext.Result = new RedirectResult("/UseDept/UnitUser/Tip?state=1");
                                        }
                                        else
                                        {
                                            var content = new ContentResult();
                                            content.Content = "{\"success\":false,\"message\":\"无权限\"}";
                                            filterContext.Result = content;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #endregion
            }
            else { }
        }

        //在执行操作结果后 再根据权限决定页面显示情况
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.RouteData.DataTokens["Area"] as string == "Admin")
            {
                #region 后台处理页面显示情况

                try
                {
                    if (null != adminloginUser && adminloginUser.UserType != UserType.Admin)
                    {
                        if (filterContext.Result.GetType() == typeof(ViewResult))
                        {
                            //视图类结果才需要修改页面显示
                            string sPath =
                                filterContext.RequestContext.HttpContext.Request.Path.ToUpper()
                                    .Replace("INDEX", "")
                                    .TrimEnd('/');
                            EHECD_Module tempModule =
                                Share_ModuleList.FirstOrDefault(
                                    m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath); //请求的模块
                            if (tempModule == null)
                            {
                                sPath =
                                    filterContext.RequestContext.HttpContext.Request.RawUrl.ToUpper()
                                        .Replace("INDEX", "")
                                        .TrimEnd('/');
                                tempModule =
                                    Share_ModuleList.FirstOrDefault(
                                        m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath);
                            }
                            if (tempModule != null)
                            {
                                List<EHECD_ModuleAction> tempActionList =
                                    Share_ActionList.Where(m => m.iModuleID == tempModule.ID).ToList(); //模块对应的权限库
                                if (tempActionList != null && tempActionList.Count > 0)
                                {
                                    List<string> removeActionList = new List<string>(); //应该在页面上被移除的操作按钮

                                    List<RoleAction> tempUserActionList =
                                        adminloginUser.UserActionList.Where(m => m.iModuleID == tempModule.ID).ToList();
                                    //用户所拥有的模块对应的权限
                                    removeActionList = RemovedButton(tempActionList, tempUserActionList);

                                    if (removeActionList.Count > 0)
                                    {
                                        StringBuilder sScriptBlock = new StringBuilder();
                                        sScriptBlock.Append("$('a').each(function () {if (");
                                        string sButton = string.Empty;
                                        foreach (string str in removeActionList)
                                        {
                                            sButton += "  $(this).text() == '" + str + "'||"; //移除没有权限的按钮
                                        }
                                        sButton = sButton.TrimEnd('|');
                                        sScriptBlock.Append(sButton);
                                        sScriptBlock.Append("){$(this).remove();}});");

                                        ViewResult viewResult = (ViewResult)filterContext.Result;
                                        Controller controller = (Controller)filterContext.Controller;
                                        string sViewName =
                                            controller.ControllerContext.RouteData.GetRequiredString("action");
                                        ViewEngineResult result =
                                            viewResult.ViewEngineCollection.FindView(controller.ControllerContext,
                                                sViewName, null);
                                        ViewContext viewContext = new ViewContext(controller.ControllerContext,
                                            result.View, controller.ViewData, controller.TempData,
                                            filterContext.HttpContext.Response.Output);
                                        viewContext.Writer.Write("<script type=\"text/javascript\">" +
                                                                 sScriptBlock.ToString() + "</script>");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TTracer.WriteLog(ex.ToString());
                }

                #endregion
            }
            else if (filterContext.RouteData.DataTokens["Area"] as string == "FireDept")
            {
                #region 后台处理页面显示情况

                try
                {
                    if (null != loginFireUser && loginFireUser.UserType != UserType.Admin)
                    {
                        if (filterContext.Result.GetType() == typeof(ViewResult))
                        {
                            //视图类结果才需要修改页面显示
                            string sPath =
                                filterContext.RequestContext.HttpContext.Request.Path.ToUpper()
                                    .Replace("INDEX", "")
                                    .TrimEnd('/');
                            EHECD_UnitModule tempModule =
                                Share_UnitModuleList.Where(o => o.iUnitType == loginFireUser.iUserUnitType).FirstOrDefault(
                                    m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath); //请求的模块
                            if (tempModule == null)
                            {
                                sPath =
                                    filterContext.RequestContext.HttpContext.Request.RawUrl.ToUpper()
                                        .Replace("INDEX", "")
                                        .TrimEnd('/');
                                tempModule =
                                    Share_UnitModuleList.Where(o => o.iUnitType == loginFireUser.iUserUnitType).FirstOrDefault(
                                        m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath);
                            }
                            if (tempModule != null)
                            {
                                List<EHECD_UnitModuleAction> tempActionList =
                                    Share_UnitActionList.Where(m => m.iModuleID == tempModule.ID).ToList(); //模块对应的权限库
                                if (tempActionList != null && tempActionList.Count > 0)
                                {
                                    List<string> removeActionList = new List<string>(); //应该在页面上被移除的操作按钮

                                    List<RoleAction> tempUserActionList =
                                        loginFireUser.UserActionList.Where(m => m.iModuleID == tempModule.ID).ToList();
                                    //用户所拥有的模块对应的权限
                                    removeActionList = UnitRemovedButton(tempActionList, tempUserActionList);

                                    if (removeActionList.Count > 0)
                                    {
                                        StringBuilder sScriptBlock = new StringBuilder();
                                        sScriptBlock.Append("$('a').each(function () {if (");
                                        string sButton = string.Empty;
                                        foreach (string str in removeActionList)
                                        {
                                            sButton += "  $(this).text() == '" + str + "'||"; //移除没有权限的按钮
                                        }
                                        sButton = sButton.TrimEnd('|');
                                        sScriptBlock.Append(sButton);
                                        sScriptBlock.Append("){$(this).remove();}});");

                                        ViewResult viewResult = (ViewResult)filterContext.Result;
                                        Controller controller = (Controller)filterContext.Controller;
                                        string sViewName =
                                            controller.ControllerContext.RouteData.GetRequiredString("action");
                                        ViewEngineResult result =
                                            viewResult.ViewEngineCollection.FindView(controller.ControllerContext,
                                                sViewName, null);
                                        ViewContext viewContext = new ViewContext(controller.ControllerContext,
                                            result.View, controller.ViewData, controller.TempData,
                                            filterContext.HttpContext.Response.Output);
                                        viewContext.Writer.Write("<script type=\"text/javascript\">" +
                                                                 sScriptBlock.ToString() + "</script>");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TTracer.WriteLog(ex.ToString());
                }

                #endregion
            }
            else if (filterContext.RouteData.DataTokens["Area"] as string == "RepairDept")
            {
                #region 后台处理页面显示情况

                try
                {
                    if (null != loginRepairUser && loginRepairUser.UserType != UserType.Admin)
                    {
                        if (filterContext.Result.GetType() == typeof(ViewResult))
                        {
                            //视图类结果才需要修改页面显示
                            string sPath =
                                filterContext.RequestContext.HttpContext.Request.Path.ToUpper()
                                    .Replace("INDEX", "")
                                    .TrimEnd('/');
                            EHECD_UnitModule tempModule =
                                Share_UnitModuleList.Where(o => o.iUnitType == loginRepairUser.iUserUnitType).FirstOrDefault(
                                    m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath); //请求的模块
                            if (tempModule == null)
                            {
                                sPath =
                                    filterContext.RequestContext.HttpContext.Request.RawUrl.ToUpper()
                                        .Replace("INDEX", "")
                                        .TrimEnd('/');
                                tempModule =
                                    Share_UnitModuleList.Where(o => o.iUnitType == loginRepairUser.iUserUnitType).FirstOrDefault(
                                        m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath);
                            }
                            if (tempModule != null)
                            {
                                List<EHECD_UnitModuleAction> tempActionList =
                                    Share_UnitActionList.Where(m => m.iModuleID == tempModule.ID).ToList(); //模块对应的权限库
                                if (tempActionList != null && tempActionList.Count > 0)
                                {
                                    List<string> removeActionList = new List<string>(); //应该在页面上被移除的操作按钮

                                    List<RoleAction> tempUserActionList =
                                        loginRepairUser.UserActionList.Where(m => m.iModuleID == tempModule.ID).ToList();
                                    //用户所拥有的模块对应的权限
                                    removeActionList = UnitRemovedButton(tempActionList, tempUserActionList);

                                    if (removeActionList.Count > 0)
                                    {
                                        StringBuilder sScriptBlock = new StringBuilder();
                                        sScriptBlock.Append("$('a').each(function () {if (");
                                        string sButton = string.Empty;
                                        foreach (string str in removeActionList)
                                        {
                                            sButton += "  $(this).text() == '" + str + "'||"; //移除没有权限的按钮
                                        }
                                        sButton = sButton.TrimEnd('|');
                                        sScriptBlock.Append(sButton);
                                        sScriptBlock.Append("){$(this).remove();}});");

                                        ViewResult viewResult = (ViewResult)filterContext.Result;
                                        Controller controller = (Controller)filterContext.Controller;
                                        string sViewName =
                                            controller.ControllerContext.RouteData.GetRequiredString("action");
                                        ViewEngineResult result =
                                            viewResult.ViewEngineCollection.FindView(controller.ControllerContext,
                                                sViewName, null);
                                        ViewContext viewContext = new ViewContext(controller.ControllerContext,
                                            result.View, controller.ViewData, controller.TempData,
                                            filterContext.HttpContext.Response.Output);
                                        viewContext.Writer.Write("<script type=\"text/javascript\">" +
                                                                 sScriptBlock.ToString() + "</script>");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TTracer.WriteLog(ex.ToString());
                }

                #endregion
            }
            else
            {
                #region 后台处理页面显示情况

                try
                {
                    if (null != loginUseUser && loginUseUser.UserType != UserType.Admin)
                    {
                        if (filterContext.Result.GetType() == typeof(ViewResult))
                        {
                            //视图类结果才需要修改页面显示
                            string sPath =
                                filterContext.RequestContext.HttpContext.Request.Path.ToUpper()
                                    .Replace("INDEX", "")
                                    .TrimEnd('/');
                            EHECD_UnitModule tempModule =
                                Share_UnitModuleList.Where(o => o.iUnitType == loginUseUser.iUserUnitType).FirstOrDefault(
                                    m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath); //请求的模块
                            if (tempModule == null)
                            {
                                sPath =
                                    filterContext.RequestContext.HttpContext.Request.RawUrl.ToUpper()
                                        .Replace("INDEX", "")
                                        .TrimEnd('/');
                                tempModule =
                                    Share_UnitModuleList.Where(o => o.iUnitType == loginUseUser.iUserUnitType).FirstOrDefault(
                                        m => m.sModuleUrl.ToUpper().Replace("INDEX", "").TrimEnd('/') == sPath);
                            }
                            if (tempModule != null)
                            {
                                List<EHECD_UnitModuleAction> tempActionList =
                                    Share_UnitActionList.Where(m => m.iModuleID == tempModule.ID).ToList(); //模块对应的权限库
                                if (tempActionList != null && tempActionList.Count > 0)
                                {
                                    List<string> removeActionList = new List<string>(); //应该在页面上被移除的操作按钮

                                    List<RoleAction> tempUserActionList =
                                        loginUseUser.UserActionList.Where(m => m.iModuleID == tempModule.ID).ToList();
                                    //用户所拥有的模块对应的权限
                                    removeActionList = UnitRemovedButton(tempActionList, tempUserActionList);

                                    if (removeActionList.Count > 0)
                                    {
                                        StringBuilder sScriptBlock = new StringBuilder();
                                        sScriptBlock.Append("$('a').each(function () {if (");
                                        string sButton = string.Empty;
                                        foreach (string str in removeActionList)
                                        {
                                            sButton += "  $(this).text() == '" + str + "'||"; //移除没有权限的按钮
                                        }
                                        sButton = sButton.TrimEnd('|');
                                        sScriptBlock.Append(sButton);
                                        sScriptBlock.Append("){$(this).remove();}});");

                                        ViewResult viewResult = (ViewResult)filterContext.Result;
                                        Controller controller = (Controller)filterContext.Controller;
                                        string sViewName =
                                            controller.ControllerContext.RouteData.GetRequiredString("action");
                                        ViewEngineResult result =
                                            viewResult.ViewEngineCollection.FindView(controller.ControllerContext,
                                                sViewName, null);
                                        ViewContext viewContext = new ViewContext(controller.ControllerContext,
                                            result.View, controller.ViewData, controller.TempData,
                                            filterContext.HttpContext.Response.Output);
                                        viewContext.Writer.Write("<script type=\"text/javascript\">" +
                                                                 sScriptBlock.ToString() + "</script>");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    TTracer.WriteLog(ex.ToString());
                }

                #endregion
            }
        }

        #region 后台获取应被移除的按钮

        /// <summary>
        /// 后台获取应被移除的按钮
        /// </summary>
        /// <param name="actionList">操作库</param>
        /// <param name="userActionList">用户所拥有的权限操作</param>
        /// <returns></returns>
        private List<string> RemovedButton(List<EHECD_ModuleAction> actionList, List<RoleAction> userActionList)
        {
            List<string> result = new List<string>();
            if (userActionList == null)
            {
                userActionList = new List<RoleAction>();
            }
            foreach (EHECD_ModuleAction node in actionList)
            {
                if (!userActionList.Exists(m => m.iActionID == node.ID))
                {//如果用户没有该操作的权限
                    result.Add(node.sActionName);
                }
            }
            return result;
        }

        #endregion

        #region 单位后台获取应被移除的按钮

        /// <summary>
        /// 单位后台获取应被移除的按钮
        /// </summary>
        /// <param name="actionList">操作库</param>
        /// <param name="userActionList">用户所拥有的权限操作</param>
        /// <returns></returns>
        private List<string> UnitRemovedButton(List<EHECD_UnitModuleAction> actionList, List<RoleAction> userActionList)
        {
            List<string> result = new List<string>();
            if (userActionList == null)
            {
                userActionList = new List<RoleAction>();
            }
            foreach (EHECD_UnitModuleAction node in actionList)
            {
                if (!userActionList.Exists(m => m.iActionID == node.ID))
                {//如果用户没有该操作的权限
                    result.Add(node.sActionName);
                }
            }
            return result;
        }

        #endregion
    }
}