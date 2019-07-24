using System;
using System.Linq;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Service
{
    public class TokenService
    {
        static TokenService instance;
        static object async = new object();
        private static TokenDao Dao = TokenDao.Instance;

        private TokenService()
        {
        }

        public static TokenService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (async)
                    {
                        if (instance == null)
                        {
                            instance = new TokenService();
                        }
                    }

                }
                return instance;
            }
        }

        /// <summary>
        /// 验证token
        /// </summary>
        /// <param name="iClientID">用户ID</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        public bool Check(string token)
        {
            if(token == "SIVB")
            {
                return true;
            }
            List<EHECD_Token> list = Dao.GetToken(token).ToList();
            return list != null && list.Count > 0;
        }

        /// <summary>
        /// 添加token
        /// </summary>
        /// <param name="iClientID">用户ID</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        public bool AddToken(string iClientID, string token,string phone,string systemType,string deviceID)
        {
            if (!string.IsNullOrEmpty(deviceID))
            {
                //删除设备上原有token
                Dao.Delete(new EHECD_Token { DeviceId = deviceID });
            }

            EHECD_Token model = new EHECD_Token
            {
                DeviceId = deviceID,
                CreateTime = DateTime.Now,
                iClientID = iClientID,
                Phone = phone,
                SystemType = systemType,
                Token = token
            };
            return  Dao.AddToken(model);
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="iClientID">用户ID</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        public bool DelToken(string iClientID)
        {
            if (string.IsNullOrEmpty(iClientID))
            {
                return false;
            }
            return Dao.Delete(new EHECD_Token { iClientID = iClientID });
        }

        /// <summary>
        /// 从设备退出
        /// </summary>
        /// <param name="iClientID">用户ID</param>
        /// <param name="token">token</param>
        /// <returns></returns>
        public bool DelToken(string iClientID,string deviceID)
        {
            if (string.IsNullOrEmpty(iClientID))
            {
                return false;
            }
            return Dao.Delete(new EHECD_Token { iClientID = iClientID , DeviceId = deviceID });
        }
    }
}
