using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui.template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using com.igetui.api.openservice.igetui;
using EHECD.WebApi;

namespace EHECD.Core.Push
{
    public class GeTuiPushManager : IPushManager
    {
        private GeTuiConfig config;

        private IGtPush push;

        private GeTuiPushManager()
        {

        }

        public GeTuiPushManager(GeTuiConfig config)
        {
            this.config = config;
            push = new IGtPush(config.Host, config.APPKey, config.MasterSecret);
        }

        /// <summary>
        /// 群发
        /// </summary>
        /// <param name="content"></param>
        public void PushMessageToApp(PushModel content)
        {
            if (content == null)
            {
                throw new ArgumentNullException("推送内容不能为null");
            }

            //消息模版：TransmissionTemplate:透传模板
            string json = JsonConvert.SerializeObject(content);
            TransmissionTemplate template = GetTransmissionTemplate(json);

            //消息模型
            AppMessage message = new AppMessage();
            message.Data = template;
            message.Speed = 100;
            message.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
            message.OfflineExpireTime = 1000 * 3600;            // 离线有效时间，单位为毫秒，可选
            List<String> appIdList = new List<string>();
            appIdList.Add(config.APPId);
            message.AppIdList = appIdList;

            try
            {
                String pushResult = push.pushMessageToApp(message);
            }
            catch (RequestException e)
            {
                ApiService._logManager.WriteLog("推送到APP失败：" + e);
                String requestId = e.RequestId;
                //发送失败后的重发
                String pushResult = push.pushMessageToApp(message, requestId);
            }
        }

        /// <summary>
        /// 单个推送
        /// </summary>
        /// <param name="content"></param>
        public void PushMessageToSingle(PushModel content)
        {
            if(content == null)
            {
                throw new ArgumentNullException("推送内容不能为null");
            }

            //消息模版：TransmissionTemplate:透传模板
            string json = JsonConvert.SerializeObject(content);
            TransmissionTemplate template = GetTransmissionTemplate(json);
           
            //单推消息模型
            SingleMessage message = new SingleMessage();
            message.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
            message.OfflineExpireTime = 1000 * 3600 * 12;            // 离线有效时间，单位为毫秒，可选
            message.Data = template;

            //推送目的
            Target target = new Target();
            target.appId = config.APPId;
            target.clientId = content.CID;

            try
            {
                String pushResult = push.pushMessageToSingle(message, target);
            }
            catch (RequestException e)
            {
                ApiService._logManager.WriteLog("推送到"+ content.CID + "失败："+e);
                String requestId = e.RequestId;
                //发送失败后的重发
                String pushResult = push.pushMessageToSingle(message, target, requestId);
            }
        }

        /// <summary>
        /// 批量单推
        /// </summary>
        public void BatchPushMessageToSingle(List<PushModel> list)
        {
            IBatch batch = new BatchImpl(config.APPKey, push);

            foreach (PushModel model in list)
            {
                TransmissionTemplate temp = GetTransmissionTemplate(JsonConvert.SerializeObject(model));
                // 单推消息模型
                SingleMessage messageTrans = new SingleMessage();
                messageTrans.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
                messageTrans.OfflineExpireTime = 1000 * 3600 * 12;            // 离线有效时间，单位为毫秒，可选
                messageTrans.Data = temp;
                Target targetTrans = new Target();
                targetTrans.appId = config.APPId;
                targetTrans.clientId = model.CID;
                batch.add(messageTrans, targetTrans);
            }
            
            try
            {
                String ret = batch.submit();
            }
            catch (Exception e)
            {
                ApiService._logManager.WriteLog("批量推送失败：" + e);
                batch.retry();
            }
        }

        //透传模板动作内容
        private TransmissionTemplate GetTransmissionTemplate(string content)
        {
            TransmissionTemplate template = new TransmissionTemplate();
            template.AppId = config.APPId;
            template.AppKey = config.APPKey;
            //应用启动类型，1：强制应用启动 2：等待应用启动
            template.TransmissionType = 1;
            //透传内容  
            template.TransmissionContent = content;
            //设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）
            DateTime now = DateTime.Now;
            String begin = now.ToString("yyyy-MM-dd HH:mm:ss");
            String end = now.AddMinutes(10).ToString("yyyy-MM-dd HH:mm:ss");
            template.setDuration(begin, end);
            return template;
        }
    }
}
