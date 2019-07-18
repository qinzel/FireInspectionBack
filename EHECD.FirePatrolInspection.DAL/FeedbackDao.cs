				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 意见反馈操作
    /// </summary>
    public class FeedbackDao
    {
        static FeedbackDao instance = new FeedbackDao();

        private FeedbackDao()
        {
        }
        public static FeedbackDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取意见反馈信息
		
        /// <summary>
        /// 获取意见反馈信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Feedback> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
                        SELECT F.*,
                        (SELECT sPhone FROM EHECD_Client WHERE ID = F.iClientID) sClientName,
                        (SELECT iType FROM EHECD_Client WHERE ID = F.iClientID) iClientType
                        FROM EHECD_Feedback F WHERE F.bIsDeleted = 0
                    ) T WHERE 1 = 1
                ";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sTitle Like '%{0}%' OR T.sClientName Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientID = {0}", param.condition["iClientID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientType"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientType = {0}", param.condition["iClientType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsReplyStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.bIsReplyStatus = {0}", param.condition["bIsReplyStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Feedback>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取各单位意见反馈信息

        /// <summary>
        /// 获取各单位意见反馈信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Feedback> GetFeedBackList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT T.* FROM (
                        SELECT T.*,
                        (SELECT sPhone FROM EHECD_Client WHERE ID = T.iClientID) sClientName,
                        (SELECT iType FROM EHECD_Client WHERE ID = T.iClientID) iClientType
                        FROM EHECD_Feedback T WHERE T.bIsDeleted = 0
                    ) T WHERE 1 = 1 AND (T.iUnitID = 0 OR T.iUnitID = {0}) AND T.iClientID IN (
	                    SELECT ID FROM EHECD_CLIENT WHERE bIsDeleted = 0 AND iStatus = 0 AND iUnitID = {0}
	                    UNION
	                    SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iUnitID = {0}
                    )
                ", param.condition["iUnitID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sTitle Like '%{0}%' OR T.sClientName Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsReplyStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.bIsReplyStatus = {0}", param.condition["bIsReplyStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            param.sort = "T.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Feedback>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取意见反馈详情

        /// <summary>
        /// 获取意见反馈详情
        /// </summary>
        /// <param name="iFeedbackID"></param>
        /// <returns></returns>
        public EHECD_Feedback Get(long iFeedbackID)
        {
            return DBHelper.QuerySingle<EHECD_Feedback>(string.Format(@"
                    SELECT T.*,
                    (SELECT sPhone FROM EHECD_Client WHERE ID = T.iClientID) sClientName
                    FROM EHECD_Feedback T WHERE T.bIsDeleted = 0 AND T.ID = {0}
                ", iFeedbackID));
        }
		
		#endregion
		
		#region 添加意见反馈

        /// <summary>
        /// 添加意见反馈
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_Feedback entity)
        {
            return DBHelper.Execute("INSERT INTO EHECD_Feedback (sTitle, sContent, sImageSrc, iClientID, iUnitID) VALUES (@sTitle, @sContent, @sImageSrc, @iClientID, @iUnitID)", entity) > 0;
        }
		
		#endregion
		
		#region 批量删除意见反馈

        /// <summary>
        /// 批量删除意见反馈
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_Feedback Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 删除意见反馈

        /// <summary>
        /// 删除意见反馈
        /// </summary>
        /// <param name="iFeedbackID"></param>
        /// <returns></returns>
        public bool DeleteFeedback(long iFeedbackID)
        {
            return DBHelper.Execute(string.Format("Update EHECD_Feedback Set bIsDeleted=1 Where ID = {0}", iFeedbackID)) > 0;
        }

        #endregion
    }
}
