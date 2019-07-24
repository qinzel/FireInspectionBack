
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 帖子评论业务逻辑
    /// </summary>
    public class CommentService
    {
        static CommentService instance = new CommentService();
        private static CommentDao Dao = CommentDao.Instance;

        private CommentService()
        {
        }

        public static CommentService Instance
        {
            get { return instance; }
        }
		
		#region 获取帖子评论数据

        /// <summary>
        /// 获取帖子评论数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetGridData(QueryParams param)
        {
			int iTotalRecord = 0;
            var list = Dao.GetList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
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
            return Dao.Get(iCommentID) ?? new EHECD_Comment();
        }
		
		#endregion
		
		#region 添加编辑帖子评论

        /// <summary>
        /// 添加编辑帖子评论
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Comment entity)
        {
            ResultMessage result = new ResultMessage();

            //新增帖子评论
            result.success = Dao.Insert(entity);
            result.message = result.success ? "添加帖子评论成功" : "添加帖子评论失败";
            return result;
        }

        #endregion

        #region 删除帖子评论

        /// <summary>
        /// 删除帖子评论
        /// </summary>
        /// <param name="iCommentID"></param>
        /// <returns></returns>
        public ResultMessage Delete(long iCommentID)
        {
            EHECD_Comment entity = Get(iCommentID);
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(entity)
            };
            result.message = result.success ? "删除评论成功" : "删除评论失败";
            return result;
        }

        #endregion

        #region 发布评论

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name="iTieziID"></param>
        /// <param name="iClientID"></param>
        /// <param name="sName"></param>
        /// <param name="sContent"></param>
        /// <param name="iTarClientID"></param>
        /// <param name="sTarName"></param>
        /// <param name="sImageSrc"></param>
        /// <returns></returns>
        [APIAttribute(name: "cmt.add", desc: "发布评论")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage CreateComment(long iTieziID, int iClientID, string sName, string sContent, int iTarClientID = 0, string sTarName = "", string sImageSrc = "")
        {
            ResultMessage result = new ResultMessage();

            if (iTieziID == 0 || iClientID == 0)
            {
                result.message = "发表评论失败";
                return result;
            }

            EHECD_Comment entity = new EHECD_Comment()
            {
                iTieziID = iTieziID,
                iClientID = iClientID,
                sName = sName,
                sImageSrc = sImageSrc,
                sContent = sContent,
                iTarClientID = iTarClientID,
                sTarName = sTarName
            };

            result.success = Dao.Insert(entity) ? true : false;
            result.message = result.success ? "评论成功" : "评论失败";
            return result;
        }

        #endregion
    }
}