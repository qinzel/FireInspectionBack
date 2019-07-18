using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EHECD.Common;
using EHECD.EntityFramework.Models;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Service;
using Newtonsoft.Json;

namespace EHECD.FirePatrolInspection.Web.Helper
{
    /// <summary>
    /// 授权帮助类
    /// </summary>
    public class AuthHelper
    {
        /// <summary>
        /// 获取当前后台登录者信息
        /// </summary>
        /// <returns></returns>
        public static LoginUser GetLogUser()
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                return HttpContext.Current.Session["User"] as LoginUser;
            }
            return null;
        }

        /// <summary>
        /// 获取当前消防部门登录者信息
        /// </summary>
        /// <returns></returns>
        public static LoginUser GetLogFireUser()
        {
            if (HttpContext.Current.Session["FireUser"] != null)
            {
                return HttpContext.Current.Session["FireUser"] as LoginUser;
            }
            return null;
        }

        /// <summary>
        /// 获取当前维护公司登录者信息
        /// </summary>
        /// <returns></returns>
        public static LoginUser GetLogRepairUser()
        {
            if (HttpContext.Current.Session["RepairUser"] != null)
            {
                return HttpContext.Current.Session["RepairUser"] as LoginUser;
            }
            return null;
        }

        /// <summary>
        /// 获取当前使用单位登录者信息
        /// </summary>
        /// <returns></returns>
        public static LoginUser GetLogUseUser()
        {
            if (HttpContext.Current.Session["UseUser"] != null)
            {
                return HttpContext.Current.Session["UseUser"] as LoginUser;
            }
            return null;
        }

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        public static EHECD_Client GetLogClient()
        {
            if (HttpContext.Current.Session["Client"] != null)
            {
                return HttpContext.Current.Session["Client"] as EHECD_Client;
            }
            if (HttpContext.Current.Request.Cookies["ClientID"] != null &&
                !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["ClientID"].Value))
            {
                var client = ClientService.Instance.Get(int.Parse(HttpContext.Current.Request.Cookies["ClientID"].Value));
                HttpContext.Current.Session["Client"] = client;
                return client;
            }

            return null;
        }
    }
}