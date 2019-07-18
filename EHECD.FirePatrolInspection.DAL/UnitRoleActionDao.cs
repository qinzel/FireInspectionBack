				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 单位角色操作信息操作
    /// </summary>
    public class UnitRoleActionDao
    {
        static UnitRoleActionDao instance = new UnitRoleActionDao();

        private UnitRoleActionDao()
        {
        }
        public static UnitRoleActionDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取单位角色操作信息信息
		
        /// <summary>
        /// 获取单位角色操作信息信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_UnitRoleAction> GetList(QueryParams param, ref int iTotalRecord)
        {
			            
				string sSql = "Select * From EHECD_UnitRoleAction Where 1=1";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_UnitRoleAction>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取单位角色操作信息详情

        /// <summary>
        /// 获取单位角色操作信息详情
        /// </summary>
        /// <param name="iUnitRoleActionID"></param>
        /// <returns></returns>
        public EHECD_UnitRoleAction Get(long iUnitRoleActionID)
        {
            return DBHelper.QuerySingle<EHECD_UnitRoleAction>(iUnitRoleActionID);
        }
		
		#endregion
		
		#region 添加单位角色操作信息

        /// <summary>
        /// 添加单位角色操作信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_UnitRoleAction entity)
        {
            return DBHelper.Insert<EHECD_UnitRoleAction>(entity) > 0;
        }
		
		#endregion
		
		#region 编辑单位角色操作信息
		
        /// <summary>
        /// 编辑单位角色操作信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_UnitRoleAction entity)
        {
			string sSql =
                @"Update [EHECD_UnitRoleAction] Set 
															
				[iUnitRoleID]=@iUnitRoleID,
												
				[iModuleID]=@iModuleID,
												
				[iActionID]=@iActionID,
										
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除单位角色操作信息

        /// <summary>
        /// 批量删除单位角色操作信息
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
            return DBHelper.Execute(string.Format("Delete EHECD_UnitRoleAction Where ID In ({0})", sIds)) > 0;
					}
		
		#endregion
    }
}
