				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 单位角色操作
    /// </summary>
    public class UnitRoleDao
    {
        static UnitRoleDao instance = new UnitRoleDao();

        private UnitRoleDao()
        {
        }
        public static UnitRoleDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取单位角色信息
		
        /// <summary>
        /// 获取单位角色信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_UnitRole> GetList(QueryParams param, ref int iTotalRecord)
        {
						
				string sSql = "Select * From EHECD_UnitRole Where bIsDeleted=0";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_UnitRole>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取单位角色详情

        /// <summary>
        /// 获取单位角色详情
        /// </summary>
        /// <param name="iUnitRoleID"></param>
        /// <returns></returns>
        public EHECD_UnitRole Get(long iUnitRoleID)
        {
            return DBHelper.QuerySingle<EHECD_UnitRole>(iUnitRoleID);
        }
		
		#endregion
		
		#region 添加单位角色

        /// <summary>
        /// 添加单位角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_UnitRole entity)
        {
            return DBHelper.Insert<EHECD_UnitRole>(entity) > 0;
        }
		
		#endregion
		
		#region 编辑单位角色
		
        /// <summary>
        /// 编辑单位角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_UnitRole entity)
        {
			string sSql =
                @"Update [EHECD_UnitRole] Set 
															
				[sRoleName]=@sRoleName,
												
				[sDescription]=@sDescription,
												
				[iUnitID]=@iUnitID,
																						
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除单位角色

        /// <summary>
        /// 批量删除单位角色
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_UnitRole Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
					}
		
		#endregion
    }
}
