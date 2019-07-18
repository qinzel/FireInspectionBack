				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 部门操作
    /// </summary>
    public class DeptDao
    {
        static DeptDao instance = new DeptDao();

        private DeptDao()
        {
        }
        public static DeptDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取部门信息
		
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Dept> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT D.*, 
                    (SELECT sName FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND IAuditState = 1 AND ID = D.iUseDeptID AND iType = 0) sUnitName
                    FROM EHECD_DEPT D WHERE bIsDeleted = 0
                ";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And D.sName Like '%{0}%'", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And D.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }

            param.sort = "D.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Dept>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取部门详情

        /// <summary>
        /// 获取部门详情
        /// </summary>
        /// <param name="iDeptID"></param>
        /// <returns></returns>
        public EHECD_Dept Get(int iDeptID)
        {
            return DBHelper.QuerySingle<EHECD_Dept>(string.Format(@"
                    SELECT D.*,
                    (SELECT sName FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND IAuditState = 1 AND ID = D.iUseDeptID AND iType = 0) sUnitName
                    FROM EHECD_DEPT D WHERE bIsDeleted = 0 AND D.ID = {0}
                ", iDeptID));
        }
		
		#endregion
		
		#region 添加部门

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_Dept entity)
        {
            return DBHelper.Execute("INSERT INTO EHECD_DEPT (iUseDeptID, sName) VALUES (@iUseDeptID, @sName)", entity) > 0;
        }
		
		#endregion
		
		#region 编辑部门
		
        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_Dept entity)
        {
			string sSql =
                @"Update [EHECD_Dept] Set 
				[iUseDeptID]=@iUseDeptID,
				[sName]=@sName
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}

        #endregion

        #region 编辑部门

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Modify(EHECD_Dept entity)
        {
            string sSql =
                @"Update [EHECD_Dept] Set 
				[sName]=@sName
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
        }

        #endregion

        #region 批量删除部门

        /// <summary>
        /// 批量删除部门
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_Dept Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 根据点检员ID获取关联部门集合

        /// <summary>
        /// 根据点检员ID获取关联部门集合
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Dept> GetListByClientID(int iClientID)
        {
            return DBHelper.Query<EHECD_Dept>(string.Format(@"
                    SELECT * FROM (
                        SELECT D.*, R.iAuditState,
                        (SELECT sName FROM EHECD_UNIT WHERE bIsDeleted = 0 AND ID = R.iUnitID) sUnitName
                        FROM EHECD_CLIENTDEPTREL R INNER JOIN EHECD_DEPT D ON R.iOrganID = D.ID WHERE R.iClientID = {0} AND R.bIsDeleted = 0 AND R.iStatus = 0 AND D.bIsDeleted = 0 AND R.iAuditState < 2
                    ) T WHERE 1 = 1
                ", iClientID));
        }

        #endregion

        #region 根据单位ID获取下辖所有部门

        /// <summary>
        /// 根据单位ID获取下辖所有部门
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Dept> GetListByUnitID(int iUnitID)
        {
            return DBHelper.Query<EHECD_Dept>(string.Format("SELECT * FROM EHECD_DEPT WHERE bIsDeleted = 0 AND iUseDeptID = {0}", iUnitID));
        }

        #endregion
    }
}