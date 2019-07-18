				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 使用单位基础设置关联维护公司操作
    /// </summary>
    public class UseDeptSettingDetailDao
    {
        static UseDeptSettingDetailDao instance = new UseDeptSettingDetailDao();

        private UseDeptSettingDetailDao()
        {
        }
        public static UseDeptSettingDetailDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取使用单位基础设置关联维护公司信息
		
        /// <summary>
        /// 获取使用单位基础设置关联维护公司信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_UseDeptSettingDetail> GetList(QueryParams param, ref int iTotalRecord)
        {
						
				string sSql = "Select * From EHECD_UseDeptSettingDetail Where bIsDeleted=0";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_UseDeptSettingDetail>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取使用单位基础设置关联维护公司详情

        /// <summary>
        /// 获取使用单位基础设置关联维护公司详情
        /// </summary>
        /// <param name="iUseDeptSettingDetailID"></param>
        /// <returns></returns>
        public EHECD_UseDeptSettingDetail Get(long iUseDeptSettingDetailID)
        {
            return DBHelper.QuerySingle<EHECD_UseDeptSettingDetail>(iUseDeptSettingDetailID);
        }
		
		#endregion
		
		#region 添加使用单位基础设置关联维护公司

        /// <summary>
        /// 添加使用单位基础设置关联维护公司
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_UseDeptSettingDetail entity)
        {
            return DBHelper.Insert<EHECD_UseDeptSettingDetail>(entity) > 0;
        }
		
		#endregion
		
		#region 编辑使用单位基础设置关联维护公司
		
        /// <summary>
        /// 编辑使用单位基础设置关联维护公司
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_UseDeptSettingDetail entity)
        {
			string sSql =
                @"Update [EHECD_UseDeptSettingDetail] Set 
															
				[iUseDeptSettingsID]=@iUseDeptSettingsID,
												
				[iRepairDeptID]=@iRepairDeptID
																						
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除使用单位基础设置关联维护公司

        /// <summary>
        /// 批量删除使用单位基础设置关联维护公司
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_UseDeptSettingDetail Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion
    }
}
