using System;
using System.Linq;
using EHECD.Common;
using EHECD.EntityFramework.EFWork;

namespace EHECD.FirePatrolInspection.Service
{
    public class PhoneMsgService
    {
        static PhoneMsgService instance;
        static object async = new object();

        private PhoneMsgService()
        {
        }

        public static PhoneMsgService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (async)
                    {
                        if (instance == null)
                        {
                            instance = new PhoneMsgService();
                        }
                    }

                }
                return instance;
            }
        }

        /// <summary>
        /// 新建【测试】信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ResultMessage Add(EHECD_PhoneMsg model)
        {
            using (var Context = new Entities())
            {
                Context.EHECD_PhoneMsg.Add(model);
                ResultMessage result = new ResultMessage();
                result.success = Context.SaveChanges() > 0;

                result.message = result.success ? "短信发送成功" : "短信发送失败";
                result.data = model.sCode;
                return result;
            }
        }

        /// <summary>
        /// 验证短信
        /// </summary>
        /// <param name="sPhone">手机号</param>
        /// <param name="sCode">验证码</param>
        /// <param name="iType">验证码类别[0:注册,1:找回密码]</param>
        /// <returns></returns>
        public ResultMessage Check(string sPhone, string sCode, int iType)
        {
            ResultMessage result = new ResultMessage();

            using (var Context = new Entities())
            {
                EHECD_PhoneMsg item =
                    Context.EHECD_PhoneMsg.OrderByDescending(m => m.ID)
                        .FirstOrDefault(m => m.sPhone == sPhone && m.iType == iType);
                if (null == item || item.iState == 1 || item.sCode != sCode)
                {
                    //不存在或已验证或验证失败
                    result.message = "短信验证失败";
                }
                else if (item.dValidTime < DateTime.Now)
                {
                    result.message = "验证短信已过期,请重新获取!";
                }
                else
                {
                    item.iState = 1;
                    result.success = Context.SaveChanges() > 0;
                    result.message = result.success ? "短信验证成功" : "短信验证失败";
                }
                return result;
            }
        }
    }
}
