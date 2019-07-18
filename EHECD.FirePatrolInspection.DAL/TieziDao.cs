				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 帖子操作
    /// </summary>
    public class TieziDao
    {
        static TieziDao instance = new TieziDao();

        private TieziDao()
        {
        }
        public static TieziDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取帖子信息
		
        /// <summary>
        /// 获取帖子信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Tiezi> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
                        SELECT T.*,
                        (SELECT sPhone FROM EHECD_Client WHERE ID = T.iClientID) sClientName,
                        (SELECT sImageSrc FROM EHECD_Client WHERE ID = T.iClientID) sClientImageSrc
                        FROM EHECD_Tiezi T WHERE T.bIsDeleted = 0
                    ) T WHERE 1 = 1
                ";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sClientName Like '%{0}%' OR T.sTitle Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientID = {0}", param.condition["iClientID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Tiezi>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取帖子详情

        /// <summary>
        /// 获取帖子详情
        /// </summary>
        /// <param name="iTieziID"></param>
        /// <returns></returns>
        public EHECD_Tiezi Get(long iTieziID)
        {
            return DBHelper.QuerySingle<EHECD_Tiezi>(string.Format(@"
                    SELECT T.*,
                    (SELECT sName FROM EHECD_Client WHERE ID = T.iClientID) sClientName,
                    (SELECT sImageSrc FROM EHECD_Client WHERE ID = T.iClientID) sClientImageSrc
                    FROM EHECD_Tiezi T WHERE T.bIsDeleted = 0 AND T.ID = {0}
                ", iTieziID));
        }
		
		#endregion
		
		#region 添加帖子

        /// <summary>
        /// 添加帖子
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_Tiezi entity)
        {
            return DBHelper.Execute("INSERT INTO EHECD_Tiezi (iClientID, sTitle, sContent, sImageSrc) VALUES (@iClientID, @sTitle, @sContent, @sImageSrc)", entity) > 0;
        }
		
		#endregion
		
		#region 批量删除帖子

        /// <summary>
        /// 批量删除帖子
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format(@"
                BEGIN TRY
                    BEGIN TRAN
                        Update EHECD_Tiezi Set bIsDeleted=1 Where ID In ({0});
                        Update EHECD_Comment Set bIsDeleted = 1 Where iTieziID In ({0});
                    COMMIT TRANSACTION
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION
                END CATCH
            ", sIds)) > 0;
        }

        #endregion

        #region 获取朋友圈帖子信息

        /// <summary>
        /// 获取朋友圈帖子信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Tiezi> GetTieziList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT * FROM (
				        SELECT TZ.*, 
				        (SELECT sName FROM EHECD_Client WHERE ID = TZ.iClientID) sClientName,
                        ISNULL((SELECT sName FROM (
                            SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                            WHERE C.ID = TZ.iClientID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
                        ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
                        (SELECT sImageSrc FROM EHECD_Client WHERE ID = TZ.iClientID) sClientImageSrc
				        FROM EHECD_Tiezi TZ WHERE TZ.bIsDeleted = 0 AND TZ.iClientID IN (
					        SELECT iClientID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iAuditState = 1 AND iUnitID IN (
						        SELECT iUnitID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iClientID = {0}
					        )
				        )
			        ) T WHERE 1 = 1
                ", param.condition["iClientID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sClientName Like '%{0}%' OR T.sTitle Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Tiezi>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取单位朋友圈帖子信息

        /// <summary>
        /// 获取朋友圈帖子信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Tiezi> GetUnitTieziList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT * FROM (
                        SELECT TZ.*, 
                        (SELECT sName FROM EHECD_Client WHERE ID = TZ.iClientID) sClientName,
                        (SELECT sImageSrc FROM EHECD_Client WHERE ID = TZ.iClientID) sClientImageSrc
                        FROM EHECD_Tiezi TZ WHERE TZ.bIsDeleted = 0 AND TZ.iClientID IN (
                            SELECT iClientID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iUnitID IN (
	                            SELECT iUnitID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iUnitID = {0}
                            )
                        )
                    ) T WHERE 1 = 1
                ", param.condition["iUnitID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sClientName Like '%{0}%' OR T.sTitle Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Tiezi>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 删除我的帖子

        /// <summary>
        /// 删除我的帖子
        /// </summary>
        /// <param name="iTieziID"></param>
        /// <returns></returns>
        public bool DeleteTiezi(int iTieziID)
        {
            return DBHelper.Execute(string.Format("UPDATE EHECD_TIEZI SET bIsDeleted = 1 WHERE ID = {0}", iTieziID)) > 0;
        }

        #endregion
    }
}