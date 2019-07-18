using System.Web;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Service
{
    public class ClientHelper
    {
        static ClientHelper instance = new ClientHelper();

        private ClientHelper()
        {
        }

        public static ClientHelper Instance
        {
            get { return instance; }
        }

        #region 获取当前登录用户

        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        public static EHECD_Client GetLoginClient()
        {
            if (HttpContext.Current.Request.Cookies["ClientID"] != null &&
               !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["ClientID"].Value))
            {
                var client = ClientService.Instance.Get(int.Parse(HttpContext.Current.Request.Cookies["ClientID"].Value));
                HttpContext.Current.Session["Client"] = client;
                return client;
            }
            return null;
        }

        #endregion

        #region 获取当前登录用户ID

        /// <summary>
        /// 获取当前登录用户ID
        /// </summary>
        /// <returns></returns>
        public static int GetLoginClientID()
        {
            if (HttpContext.Current.Request.Cookies["ClientID"] != null &&
                !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["ClientID"].Value))
            {
                return int.Parse(HttpContext.Current.Request.Cookies["ClientID"].Value);
            }
            return 0;
        }

        #endregion
    }
}