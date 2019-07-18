				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 前端用户所属单位关系表操作
    /// </summary>
    public class ClientDeptRelDao
    {
        static ClientDeptRelDao instance = new ClientDeptRelDao();

        private ClientDeptRelDao()
        {
        }
        public static ClientDeptRelDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取前端用户所属单位关系表信息
		
        /// <summary>
        /// 获取前端用户所属单位关系表信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_ClientDeptRel> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = "Select * From EHECD_ClientDeptRel Where bIsDeleted = 0";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_ClientDeptRel>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取前端用户所属单位关系表详情

        /// <summary>
        /// 获取前端用户所属单位关系表详情
        /// </summary>
        /// <param name="iClientDeptRelID"></param>
        /// <returns></returns>
        public EHECD_ClientDeptRel Get(long iClientDeptRelID)
        {
            return DBHelper.QuerySingle<EHECD_ClientDeptRel>(iClientDeptRelID);
        }
		
		#endregion
		
		#region 添加前端用户所属单位关系表

        /// <summary>
        /// 添加前端用户所属单位关系表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_ClientDeptRel entity)
        {
            return DBHelper.Insert<EHECD_ClientDeptRel>(entity) > 0;
        }
		
		#endregion
		
		#region 编辑前端用户所属单位关系表
		
        /// <summary>
        /// 编辑前端用户所属单位关系表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_ClientDeptRel entity)
        {
			string sSql =
                @"Update [EHECD_ClientDeptRel] Set 
															
				[iClientID]=@iClientID,
												
				[iUnitID]=@iUnitID,
												
				[iOrganID]=@iOrganID,
																						
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除前端用户所属单位关系表

        /// <summary>
        /// 批量删除前端用户所属单位关系表
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_ClientDeptRel Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }
		
		#endregion
    }
}
