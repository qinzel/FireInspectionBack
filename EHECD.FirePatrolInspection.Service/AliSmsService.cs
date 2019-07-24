using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.EntityFramework.EFWork;
using EHECD.WebApi.Attributes;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    public class AliSmsService
    {
        static readonly string sKeyId = ReadConfig.ReadAppSetting("accessKeyId");
        static readonly string sKeySecret = ReadConfig.ReadAppSetting("accessKeySecret");
        static readonly string sSign = ReadConfig.ReadAppSetting("accessSign");

        static AliSmsService instance;
        static object async = new object();
        //待发送短信队列
        private Queue<QueueInfo> msgQueue = new Queue<QueueInfo>();

        private AliSmsService() { }

        public static AliSmsService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (async)
                    {
                        if (instance == null)
                        {
                            instance = new AliSmsService();
                            Thread thread = new Thread(instance.StartSend);
                            thread.Start();
                        }
                    }

                }
                return instance;
            }
        }

        public void  StartSend()
        {
            while (true)
            {
                if (msgQueue.Count > 0)
                {
                    try
                    {
                        QueueInfo msg = msgQueue.Dequeue();
                        if(msg != null)
                        {
                            SendPhoneMsg(msg.sPhone, msg.sTemplateCode, msg.sJson);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        #region 用户注册发送短信验证码

        /// <summary>
        /// 用户注册发送短信验证码
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        [APIAttribute(name: "sms.regcode", desc: "用户注册发送短信验证码")]
        [ClientAPI]
        public ResultMessage SendMemberRegisterMessage(string sPhone)
        {
            return SendCode(sPhone, MsgType.Reg);
        }

        #endregion

        #region 用户找回登录密码发送短信验证码

        /// <summary>
        /// 用户找回登录密码发送短信验证码
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        [APIAttribute(name: "sms.findcode", desc: "用户找回登录密码发送短信验证码")]
        [ClientAPI]
        public ResultMessage SendMemberFindPwdMessage(string sPhone)
        {
            return SendCode(sPhone, MsgType.FindPwd);
        }

        #endregion

        #region 发送账号审核结果

        /// <summary>
        /// 账号审核通过
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        [APIAttribute(name: "sms.applyadopt", desc: "账号审核通过通知")]
        [ClientAPI]
        public ResultMessage SendApplyAdoptMessage(string sPhone)
        {
            Ali_MessageConfig mPaySuccess = Ali_SendMessageConfig.ReadMessageNode(TemplateType.ApplyAdopt.ToString());
            string json = JsonConvert.SerializeObject(new { phone = sPhone });
            return SendPhoneMsg(sPhone, mPaySuccess.TemplateCode, json);
        }

        /// <summary>
        /// 账号审核拒绝通知
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        [APIAttribute(name: "sms.applyrefused", desc: "账号审核拒绝通知")]
        [ClientAPI]
        public ResultMessage SendApplyRefusedMessage(string sPhone,string sOperator)
        {
            Ali_MessageConfig mPaySuccess = Ali_SendMessageConfig.ReadMessageNode(TemplateType.ApplyRefused.ToString());
            string json = JsonConvert.SerializeObject(new { phone = sPhone, sOperator = sOperator });
            return SendPhoneMsg(sPhone, mPaySuccess.TemplateCode, json);
        }

        #endregion

        #region 本月巡检通知

        /// <summary>
        /// 本月巡检通知
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        [APIAttribute(name: "sms.insmonthly", desc: "本月巡检通知")]
        public ResultMessage SendInsMonthlyMessage(string sPhone)
        {
            Ali_MessageConfig mPaySuccess = Ali_SendMessageConfig.ReadMessageNode(TemplateType.InsMonthly.ToString());
            return SendPhoneMsg(sPhone, mPaySuccess.TemplateCode,"{}");
        }

        /// <summary>
        /// 本月巡检通知(异步)
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        public void SendInsMonthlyMessageAsync(string sPhone)
        {
            Ali_MessageConfig mPaySuccess = Ali_SendMessageConfig.ReadMessageNode(TemplateType.InsMonthly.ToString());
            msgQueue.Enqueue(new QueueInfo
            {
                sJson = "{}",
                sPhone = sPhone,
                sTemplateCode = mPaySuccess.TemplateCode,
                Type = TemplateType.InsMonthly
            });
        }

        #endregion

        #region 设备异常通知

        /// <summary>
        /// 设备异常通知
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        [APIAttribute(name: "sms.deviceabnormal", desc: "设备异常通知")]
        public ResultMessage SendDeviceAbnormalMessage(string sPhone,string sDeviceName,string sDeviceNumber,string sTime)
        {
            Ali_MessageConfig mPaySuccess = Ali_SendMessageConfig.ReadMessageNode(TemplateType.DeviceAbnormal.ToString());
            string json = JsonConvert.SerializeObject(new { sDeviceName = sDeviceName, sDeviceNumber = sDeviceNumber, sTime = sTime });
            return SendPhoneMsg(sPhone, mPaySuccess.TemplateCode, json);
        }

        /// <summary>
        /// 设备异常通知(异步)
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        public void SendDeviceAbnormalMessageAsync(string sPhone, string sDeviceName, string sDeviceNumber, string sTime)
        {
            Ali_MessageConfig mPaySuccess = Ali_SendMessageConfig.ReadMessageNode(TemplateType.DeviceAbnormal.ToString());
            string json = JsonConvert.SerializeObject(new { sDeviceName = sDeviceName, sDeviceNumber = sDeviceNumber, sTime = sTime });
            msgQueue.Enqueue(new QueueInfo
            {
                sJson = json,
                Type = TemplateType.DeviceAbnormal,
                sPhone = sPhone,
                sTemplateCode = mPaySuccess.TemplateCode
            });
        }

        #endregion

        #region 发送短信

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sPhone">手机号 多个号码 请用英文逗号隔开</param>
        /// <param name="sTemplateCode">短信模板id-可在短信控制台中找到</param>
        /// <param name="sJson">模板中的变量替换JSON串</param>
        /// <returns></returns>
        public ResultMessage SendPhoneMsg(string sPhone, string sTemplateCode, string sJson)
        {
            ResultMessage result = new ResultMessage() { };
            String product = "Dysmsapi";//短信API产品名称
            String domain = "dysmsapi.aliyuncs.com";//短信API产品域名

            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", sKeyId, sKeySecret);
            //IAcsClient client = new DefaultAcsClient(profile);
            // SingleSendSmsRequest request = new SingleSendSmsRequest();

            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为20个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = sPhone;
                //必填:短信签名-可在短信控制台中找到
                request.SignName = sSign;
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = sTemplateCode;
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                request.TemplateParam = sJson;
                //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                request.OutId = "";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                result.success = sendSmsResponse.Code == "OK";
                if (!result.success)
                {
                    TTracer.WriteLog(result.message);
                }
                result.message = sendSmsResponse.Message;
            }
            catch (Exception ex)
            {
                TTracer.WriteLog(ex.ToString());
            }
            return result;
        }

        #endregion
        
        #region 发送验证码

        /// <summary>
        /// 验证合法性并发送验证码
        /// </summary>
        /// <param name="sPhone">手机号</param>
        /// <param name="msgType">验证码类型</param>
        /// <returns></returns>
        public ResultMessage SendCode(string sPhone, MsgType msgType)
        {
            ResultMessage result = new ResultMessage();

            #region 用户信息验证

            var entity = ClientDao.Instance.GetByPhone(sPhone);
            if (entity != null && entity.iStatus)
            {
                result.message = "该用户已被冻结";
                return result;
            }
            if (msgType == MsgType.Reg && entity != null)
            {
                result.message = "该手机已被注册";
                return result;
            }
            if (msgType == MsgType.FindPwd && entity == null)
            {
                result.message = "该手机已不存在";
                return result;
            }

            #endregion

            Ali_MessageConfig mPaySuccess = Ali_SendMessageConfig.ReadMessageNode(msgType.ToString());
            string sCode = TCommon.GetRandom(5);
            JObject jJson = new JObject();
            jJson.Add(new JProperty("code", sCode));
            string sJson = JsonConvert.SerializeObject(jJson);
            result = SendPhoneMsg(sPhone, mPaySuccess.TemplateCode, sJson);
            result.data = sCode;
            if (result.success)
            {
                //发送验证码成功，将验证码写入数据库
                EHECD_PhoneMsg model = new EHECD_PhoneMsg()
                {
                    sPhone = sPhone,
                    sCode = sCode,
                    iType = (int)msgType,
                    dSendTime = DateTime.Now,
                    dValidTime = DateTime.Now.AddMinutes(15),
                    iState = 0
                };
                PhoneMsgService.Instance.Add(model);
            }
            
            return result;
        }

        #endregion
    }

    #region 模板配置

    public class Ali_MessageConfig
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string sName { set; get; }

        /// <summary>
        /// 模板id
        /// </summary>
        public string TemplateCode { set; get; }
    }

    /// <summary>
    /// 管理消息配置信息
    /// </summary>
    public class Ali_SendMessageConfig
    {
        /// <summary>
        /// 读取指定ID的消息模板
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        static public Ali_MessageConfig ReadMessageNode(string sId)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(TCommon.APP_PATH + "\\Helper\\Ali_Message.config.xml");
            XmlNode node = xmlDoc.SelectSingleNode("/Messages/Message[@ID='" + sId + "']");

            //<!--名称，主要是说明是什么消息--><Name>注册消息</Name><!--信息内容模板格式--><Content>您好。您的验证码为{0}。请及时操作！</Content>
            Ali_MessageConfig config = new Ali_MessageConfig()
            {
                sName = node.ChildNodes[0].InnerText,
                TemplateCode = node.ChildNodes[1].InnerText
            };
            return config;
        }
    }

    #endregion

    #region 短信类型补充
    public enum TemplateType
    {
        ApplyRefused = 2,
        ApplyAdopt = 3,
        InsMonthly = 4,
        DeviceAbnormal = 5
    }
    #endregion

    /// <summary>
    /// 待发送短信
    /// </summary>
    public class QueueInfo
    {
        /// <summary>
        /// 短信类型
        /// </summary>
        public TemplateType Type { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string sPhone { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        public string sTemplateCode { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string sJson { get; set; }
    }
}