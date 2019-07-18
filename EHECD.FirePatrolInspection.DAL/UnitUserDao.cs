				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 单位后台用户操作
    /// </summary>
    public class UnitUserDao
    {
        static UnitUserDao instance = new UnitUserDao();

        private UnitUserDao()
        {
        }
        public static UnitUserDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取单位后台用户信息
		
        /// <summary>
        /// 获取单位后台用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_UnitUser> GetList(QueryParams param, ref int iTotalRecord)
        {
						
				string sSql = "Select * From EHECD_UnitUser Where bIsDeleted=0";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_UnitUser>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取单位后台用户详情

        /// <summary>
        /// 获取单位后台用户详情
        /// </summary>
        /// <param name="iUnitUserID"></param>
        /// <returns></returns>
        public EHECD_UnitUser Get(long iUnitUserID)
        {
            return DBHelper.QuerySingle<EHECD_UnitUser>(iUnitUserID);
        }
		
		#endregion
		
		#region 添加单位后台用户

        /// <summary>
        /// 添加单位后台用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_UnitUser entity)
        {
            return DBHelper.Insert<EHECD_UnitUser>(entity) > 0;
        }
		
		#endregion
		
		#region 编辑单位后台用户
		
        /// <summary>
        /// 编辑单位后台用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_UnitUser entity)
        {
			string sSql =
                @"Update [EHECD_UnitUser] Set 
															
				[sLoginName]=@sLoginName,
												
				[sPassWord]=@sPassWord,
												
				[sRealName]=@sRealName,
												
				[iUnitRoleID]=@iUnitRoleID,
												
				[iUserType]=@iUserType,
												
				[iUnitID]=@iUnitID,
																						
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除单位后台用户

        /// <summary>
        /// 批量删除单位后台用户
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_UnitUser Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
					}
		
		#endregion
    }
}
