using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 前端用户Token
    /// </summary>
    public class TokenDao
    {
        static TokenDao instance = new TokenDao();

        private TokenDao()
        {
        }
        public static TokenDao Instance
        {
            get
            {
                return instance;
            }
        }

        #region 根据token获取登录数据

        /// <summary>
        /// 根据token获取登录数据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Token> GetToken(string token)
        {
            return DBHelper.Query<EHECD_Token>(string.Format("SELECT * FROM EHECD_Token WHERE Token = '{0}'", token));
        }

        #endregion

        #region 根据用户ID获取Token

        /// <summary>
        /// 根据用户ID获取Token
        /// </summary>
        /// <param name="sPhones"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Token> GetTokenByClientID(string iClientID)
        {
            return DBHelper.Query<EHECD_Token>(string.Format("SELECT * FROM EHECD_Token WHERE iClientID = '{0}')", iClientID));
        }

        #endregion

        #region 删除用户登录信息

        /// <summary>
        /// 删除用户登录信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool Delete(EHECD_Token token)
        {
            string sql = "delete from  EHECD_Token where 1 = 1";
            if(string.IsNullOrEmpty(token.Token) && string.IsNullOrEmpty(token.DeviceId) 
                && string.IsNullOrEmpty(token.iClientID) && string.IsNullOrEmpty(token.Phone))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(token.Token))
            {
                sql += " and Token = @Token";
            }

            if (!string.IsNullOrEmpty(token.DeviceId))
            {
                sql += " and DeviceId = @DeviceId";
            }

            if (!string.IsNullOrEmpty(token.iClientID))
            {
                sql += " and iClientID = @iClientID";
            }

            if (!string.IsNullOrEmpty(token.Phone))
            {
                sql += " and Phone = @Phone";
            }

            return DBHelper.Execute(sql, token) > 0;
        }

        #endregion

        #region 新增Token

        /// <summary>
        /// 新增Token
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public bool AddToken(EHECD_Token token)
        {
            return DBHelper.Insert<EHECD_Token>(token) > 1;
        }

        #endregion
    }
}