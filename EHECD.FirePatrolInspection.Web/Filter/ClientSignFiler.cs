using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using EHECD.Common;
using Newtonsoft.Json;
using System.Net.Http;
using EHECD.Core.APIHelper;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Specialized;
using EHECD.WebApi;
using EHECD.WebApi.Entities;
using EHECD.WebApi.Exceptions;
using System.Reflection;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using EHECD.FirePatrolInspection.Service;

namespace EHECD.FirePatrolInspection.Web.Filter
{
    /// <summary>
    /// 客户端签名过滤器
    /// </summary>
    public class ClientSignFiler: ActionFilterAttribute
    {
        /// <summary>
        /// 进入方法前检查签名
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            #region 判断接口是否存在

            //获取接口方法
            string method = string.Empty;
            KeyValuePair<string,string> methodPair = actionContext.Request.GetQueryNameValuePairs().FirstOrDefault(x=>x.Key == "method");
            if(string.IsNullOrEmpty(methodPair.Key))
            {
                ResultMessage msg = new ResultMessage() { success = false, message = "缺少方法名", code = ErrorCode.MethodIsNull };
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(msg));
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                return;
            }
            method = methodPair.Value;

            //接口是否存在
            ApiConfig config = ApiConfigManager.GetConfig(method);
            if (config == null)
            {
                ResultMessage msg = new ResultMessage() { success = false, message = "方法不存在", code = ErrorCode.MethodCannotFound };
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(msg));
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                return;
            }

            #endregion

            #region 接口秘钥验证 

            //接口是否需要验证
            if (!IsClientAPI(config.method))
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            //秘钥验证
            string secret = ConfigurationManager.AppSettings["api.appSecret"];
            string apiappSecret = actionContext.Request.Headers.GetValues("appSecret").First();
            if (secret != apiappSecret)
            {
                ResultMessage msg = new ResultMessage() { success = false, message = "接口签名验证失败", code = ErrorCode.SecretError };
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(msg));
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                return;
            }

            #endregion

            #region 登录验证

            //是否需要登录验证
            if (!IsLoginCheck(config.method))
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            //登录验证
            if (!actionContext.Request.Headers.Contains("token"))
            {
                ResultMessage msg = new ResultMessage() { success = false, message = "缺少验证请求头",code = ErrorCode.TokenIsNull };
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(msg));
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                return;
            }
            else
            {
                string token = actionContext.Request.Headers.GetValues("token").First();
                if (!TokenService.Instance.Check(token))
                {
                    ResultMessage msg = new ResultMessage() { success = false, message = "当前用户登录失效,请重新登录", code = ErrorCode.TokenError };
                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(msg));
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    return;
                }
            }

            #endregion
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        //操作是否需要判断
        private  bool IsClientAPI(MethodInfo method)
        {
            //有ClientAPI属性
            return method.GetCustomAttributes(typeof(ClientAPIAttribute), true).Length > 0;
        }

        //操作是否需要登录
        private bool IsLoginCheck(MethodInfo method)
        {
            object[] list = method.GetCustomAttributes(typeof(ClientAPIAttribute), true);
            if(list.Length == 0)
            {
                return false;
            }

            ClientAPIAttribute attr = (ClientAPIAttribute)list[0];
            return attr.LoginCheck;
        }
    }
}