				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 单位操作日志操作
    /// </summary>
    public class UnitActionLogDao
    {
        static UnitActionLogDao instance = new UnitActionLogDao();

        private UnitActionLogDao()
        {
        }
        public static UnitActionLogDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取单位操作日志信息
		
        /// <summary>
        /// 获取单位操作日志信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_UnitActionLog> GetList(QueryParams param, ref int iTotalRecord)
        {
			            
				string sSql = "Select * From EHECD_UnitActionLog Where 1=1";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_UnitActionLog>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取单位操作日志详情

        /// <summary>
        /// 获取单位操作日志详情
        /// </summary>
        /// <param name="iUnitActionLogID"></param>
        /// <returns></returns>
        public EHECD_UnitActionLog Get(long iUnitActionLogID)
        {
            return DBHelper.QuerySingle<EHECD_UnitActionLog>(iUnitActionLogID);
        }
		
		#endregion
		
		#region 添加单位操作日志

        /// <summary>
        /// 添加单位操作日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_UnitActionLog entity)
        {
            return DBHelper.Insert<EHECD_UnitActionLog>(entity) > 0;
        }
		
		#endregion
		
		#region 编辑单位操作日志
		
        /// <summary>
        /// 编辑单位操作日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_UnitActionLog entity)
        {
			string sSql =
                @"Update [EHECD_UnitActionLog] Set 
															
				[iUnitUserID]=@iUnitUserID,
												
				[sType]=@sType,
												
				[sIpAddress]=@sIpAddress,
												
				[sContent]=@sContent,
												
				[iUnitID]=@iUnitID,
																
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除单位操作日志

        /// <summary>
        /// 批量删除单位操作日志
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
            return DBHelper.Execute(string.Format("Delete EHECD_UnitActionLog Where ID In ({0})", sIds)) > 0;
					}
		
		#endregion
    }
}
