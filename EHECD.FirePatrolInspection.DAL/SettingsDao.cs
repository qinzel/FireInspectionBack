				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 总后台基础设置操作
    /// </summary>
    public class SettingsDao
    {
        static SettingsDao instance = new SettingsDao();

        private SettingsDao()
        {
        }
        public static SettingsDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取总后台基础设置详情

        /// <summary>
        /// 获取总后台基础设置详情
        /// </summary>
        /// <returns></returns>
        public EHECD_Settings Get()
        {
            return DBHelper.QuerySingle<EHECD_Settings>("SELECT * FROM EHECD_Settings");
        }
		
		#endregion

        #region 编辑基础设置

        /// <summary>
        /// 编辑基础设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Modify(EHECD_Settings entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IF NOT EXISTS(Select * From EHECD_Settings) ")
                .Append("BEGIN ")
                .Append(" INSERT INTO EHECD_Settings(sPhone) ")
                .Append(" Values (@sPhone) ")
                .Append("END ")
                .Append("ELSE ")
                .Append(" Update EHECD_Settings Set sPhone = @sPhone ");

            return DBHelper.Execute(sb.ToString(), entity) > 0;
        }

        #endregion
    }
}
