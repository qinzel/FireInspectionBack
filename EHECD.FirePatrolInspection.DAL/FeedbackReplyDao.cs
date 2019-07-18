				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 反馈回复操作
    /// </summary>
    public class FeedbackReplyDao
    {
        static FeedbackReplyDao instance = new FeedbackReplyDao();

        private FeedbackReplyDao()
        {
        }
        public static FeedbackReplyDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取反馈回复信息
		
        /// <summary>
        /// 获取反馈回复信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_FeedbackReply> GetList(QueryParams param, ref int iTotalRecord)
        {
						
				string sSql = "Select * From EHECD_FeedbackReply Where bIsDeleted=0";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_FeedbackReply>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取反馈回复详情

        /// <summary>
        /// 获取反馈回复详情
        /// </summary>
        /// <param name="iFeedbackReplyID"></param>
        /// <returns></returns>
        public EHECD_FeedbackReply Get(long iFeedbackReplyID)
        {
            return DBHelper.QuerySingle<EHECD_FeedbackReply>(iFeedbackReplyID);
        }
		
		#endregion
		
		#region 添加反馈回复

        /// <summary>
        /// 添加反馈回复
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_FeedbackReply entity)
        {
            return DBHelper.Execute(@"
                BEGIN TRY
                    BEGIN TRAN
                        INSERT INTO EHECD_FeedbackReply (iFeedbackID, sContent) VALUES (@iFeedbackID, @sContent);
                        Update EHECD_Feedback Set bIsReplyStatus = 1 Where ID = @iFeedbackID;
                    COMMIT TRANSACTION
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION
                END CATCH
            ", entity) > 0;
        }
		
		#endregion
		
		#region 编辑反馈回复
		
        /// <summary>
        /// 编辑反馈回复
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_FeedbackReply entity)
        {
			string sSql =
                @"Update [EHECD_FeedbackReply] Set 
															
				[iFeedbackID]=@iFeedbackID,
												
				[sContent]=@sContent,
																						
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除反馈回复

        /// <summary>
        /// 批量删除反馈回复
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_FeedbackReply Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 根据反馈ID获取回复信息

        /// <summary>
        /// 根据反馈ID获取回复信息
        /// </summary>
        /// <param name="iFeedbackID"></param>
        /// <returns></returns>
        public EHECD_FeedbackReply GetByFeedbackID(long iFeedbackID)
        {
            return DBHelper.QuerySingle<EHECD_FeedbackReply>(string.Format("SELECT * FROM EHECD_FeedbackReply WHERE bIsDeleted = 0 AND iFeedbackID = {0}", iFeedbackID));
        }

        #endregion
    }
}
