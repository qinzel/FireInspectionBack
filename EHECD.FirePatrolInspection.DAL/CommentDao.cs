				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 帖子评论操作
    /// </summary>
    public class CommentDao
    {
        static CommentDao instance = new CommentDao();

        private CommentDao()
        {
        }
        public static CommentDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取帖子评论信息
		
        /// <summary>
        /// 获取帖子评论信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Comment> GetList(QueryParams param, ref int iTotalRecord)
        {
						
				string sSql = "Select * From EHECD_Comment Where bIsDeleted=0";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Comment>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取帖子评论详情

        /// <summary>
        /// 获取帖子评论详情
        /// </summary>
        /// <param name="iCommentID"></param>
        /// <returns></returns>
        public EHECD_Comment Get(long iCommentID)
        {
            return DBHelper.QuerySingle<EHECD_Comment>(iCommentID);
        }

        #endregion

        #region 根据帖子ID获取评论信息

        /// <summary>
        /// 根据帖子ID获取评论信息
        /// </summary>
        /// <param name="iTieziID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Comment> GetListByTieziID(long iTieziID)
        {
            return DBHelper.Query<EHECD_Comment>(string.Format("Select * From EHECD_Comment Where bIsDeleted = 0 AND iTieziID = {0} ORDER BY dCreateTime DESC", iTieziID));
        }

        #endregion

        #region 添加帖子评论

        /// <summary>
        /// 添加帖子评论
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_Comment entity)
        {
            string sSql = @"
                BEGIN TRY
                    BEGIN TRAN
                        INSERT INTO EHECD_COMMENT (iTieziID, iClientID, sName, sImageSrc, sContent, iTarClientID, sTarName)
                        VALUES (@iTieziID, @iClientID, @sName, @sImageSrc, @sContent, @iTarClientID, @sTarName);
                        UPDATE EHECD_TIEZI SET iReplyCount = iReplyCount + 1 WHERE ID = @iTieziID;
                    COMMIT TRANSACTION
                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION
                END CATCH";

            return DBHelper.Execute(sSql, entity) > 0;
        }
		
		#endregion

        #region 删除帖子评论

        /// <summary>
        /// 删除帖子评论
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(EHECD_Comment entity)
        {
            return DBHelper.Execute(string.Format(@"
                    BEGIN TRY
                        BEGIN TRAN
                            Update EHECD_Comment Set bIsDeleted = 1 Where ID = {0};
                            Update EHECD_Tiezi Set iReplyCount = iReplyCount - 1 Where ID = {1};
                        COMMIT TRANSACTION
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRANSACTION
                    END CATCH
                ", entity.ID, entity.iTieziID)) > 0;
        }
		
		#endregion
    }
}
