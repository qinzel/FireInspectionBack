			
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 反馈回复业务逻辑
    /// </summary>
    public class FeedbackReplyService
    {
        static FeedbackReplyService instance = new FeedbackReplyService();
        private static FeedbackReplyDao Dao = FeedbackReplyDao.Instance;

        private FeedbackReplyService()
        {
        }

        public static FeedbackReplyService Instance
        {
            get { return instance; }
        }
		
		#region 添加编辑反馈回复

        /// <summary>
        /// 添加编辑反馈回复
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_FeedbackReply entity)
        {
            ResultMessage result = new ResultMessage();

            EHECD_Feedback fb = FeedbackDao.Instance.Get(entity.iFeedbackID);
            if (fb.bIsReplyStatus)
            {
                result.message = "该反馈已回复，请勿重复提交";
                return result;
            }

            //新增反馈回复
            result.success = Dao.Insert(entity);
            if (result.success)
            {
                List<EHECD_FeedbackClientRelation> m_rels = new List<EHECD_FeedbackClientRelation>();
                // 构造MongoDB反馈回复关系
                MongoDBHelper<EHECD_FeedbackClientRelation>.Insert(new EHECD_FeedbackClientRelation()
                {
                    iFeedbackID = (int)entity.iFeedbackID,
                    iClientID = fb.iClientID,
                    bIsReaded = true
                });
            }
            result.message = result.success ? "回复成功" : "回复失败";
            return result;
        }
		
		#endregion
    }
}

