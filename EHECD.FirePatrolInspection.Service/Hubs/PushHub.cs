using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Linq;

namespace EHECD.FirePatrolInspection.Service
{
    public class PushHub : Hub
    {
        /// <summary>
        /// 静态用户列表
        /// </summary>
        private IList<string> userList = UserInfo.userList;

        /// <summary>
        /// 用户的connectionID与用户名对照表
        /// </summary>
        private readonly static Dictionary<string, string> _connections = new Dictionary<string, string>();

        /// <summary>
        /// 发送函数，前端触发该函数给服务器，服务器在将消息发送给前端，（Clients.All.(函数名)是全体广播，另外Clients提供了组播，广播排除，组播排除，指定用户播发等等）
        /// 该函数名在前端使用时一定要注意，前端调用该函数时，函数首字母一定要小写
        /// </summary>
        /// <param name="sName">消息接收者</param>
        /// <param name="sMsg">消息内容</param>
        public void SendMessage(string sName, string sMsg)
        {
            //Client内为用户的id，是唯一的，SendMessage函数是前端函数，意思是服务器将该消息推送至前端
            try
            {
                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PushHub>();

                if (sName.Contains('.'))
                {
                    sName = sName.Substring(0, sName.LastIndexOf('.'));
                }

                var tempAry = from d in _connections where d.Key.StartsWith(sName) select d;
                if (tempAry != null && tempAry.Count() > 0)
                {
                    foreach (var item in tempAry)
                    {
                        context.Clients.Client(item.Value).SendMessage(sMsg);
                    }
                }
                
            }
            catch { }
        }

        /// <summary>
        /// 用户上线函数
        /// </summary>
        /// <param name="sName"></param>
        public void SendLogin(string sName)
        {
            if (!userList.Contains(sName))
            {
                userList.Add(sName);
                //这里便是将用户id和姓名联系起来
                _connections.Add(sName, Context.ConnectionId);
            }
            else
            {
                //每次登陆id会发生变化
                _connections[sName] = Context.ConnectionId;
            }
        }
    }

    /// <summary>
    /// 用于保存登录用户
    /// </summary>
    public class UserInfo
    {
        public static IList<string> userList = new List<string>();
    }
}