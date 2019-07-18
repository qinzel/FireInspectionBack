using System;
using System.Web;
using EHECD.Common;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Service
{
    public	class UserSession
	{
		/// <summary>
		/// 获取当前后台登录者信息
		/// </summary>
		/// <returns></returns>
		public static LoginUser GetLogUser()
		{
            #region 获取当前登录者信息
            LoginUser result = null;
			try
			{
				if (HttpContext.Current.Session["User"] != null)
				{
					result = HttpContext.Current.Session["User"] as LoginUser;
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				TTracer.WriteLog(ex.ToString());
			}
			return result;
			#endregion
		}
    }
}