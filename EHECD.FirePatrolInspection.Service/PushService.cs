using EHECD.Common;
using EHECD.Core.APIHelper;
using EHECD.Core.Push;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 通知服务
    /// </summary>
    public class PushService
    {
        private static ClientDao Dao = ClientDao.Instance;
        public static object async = new object();
        static PushService instance = new PushService();

        private PushService()
        {
        }

        public static PushService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (async)
                    {
                        if (instance == null)
                        {
                            instance = new PushService();
                        }
                    }

                }
                return instance;
            }
        }

        /// <summary>
        /// 个人发送通知
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public void PushMsgToSingleAsync(int type, string iClientID, object content)
        {
            if (string.IsNullOrEmpty(iClientID))
            {
                return;
            }
            List<EHECD_CID> deviceList = Dao.GetCIDByClientID(iClientID);
            if (deviceList != null && deviceList.Count > 0)
            {
                Thread thread = new Thread(() =>
                {
                    lock (async)
                    {
                        IPushManager push = PushFactory.GetPushManager("getui");
                        foreach (EHECD_CID item in deviceList)
                        {
                            push.PushMessageToSingle(new PushModel
                            {
                                CID = item.CID,
                                Content = content,
                                Type = type,
                                iClientID = iClientID
                            });
                        }
                    }
                });
                thread.Start();
            }
        }

        /// <summary>
        /// 批量发送通知
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public void PushMsgToBatchAsync(List<PushModel> msgs)
        {
            if (msgs.Count == 0)
            {
                return;
            }
            Thread thread = new Thread(() =>
            {
                lock (async)
                {
                    IPushManager push = PushFactory.GetPushManager("getui");
                    foreach (PushModel msg in msgs)
                    {
                        //获取用户的所有设备
                        List<EHECD_CID> deviceList = Dao.GetCIDByClientID(msg.iClientID);
                        foreach (EHECD_CID device in deviceList)
                        {
                            //发送消息
                            push.PushMessageToSingle(new PushModel
                            {
                                CID = device.CID,
                                Content = msg.Content,
                                Type = msg.Type,
                                iClientID = msg.iClientID
                            });
                        }
                    }
                }
            });
            thread.Start();
        }

        #region 发送通知

        [APIAttribute(name: "push.test", desc: "发送通知测试")]
        [ClientAPI]

        public ResultMessage PushTest(string cid)
        {
            ResultMessage result = new ResultMessage()
            {
                success = true,
                message = ""
            };

            try
            {
                IPushManager push = PushFactory.GetPushManager("getui");
                push.PushMessageToSingle(new PushModel
                {
                    CID = cid,
                    Content = "这是一条测试消息",
                    Type = 1
                });
                result.success = true;
            }
            catch (Exception e)
            {
                result.success = false;
                result.message = e.Message;
            }

            return result;
        }

        #endregion

    }
}
