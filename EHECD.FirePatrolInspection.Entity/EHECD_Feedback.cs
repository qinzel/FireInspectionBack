using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 意见反馈 
    /// </summary>
    public class EHECD_Feedback 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 标题
		/// </summary>
        public string sTitle { set; get; }

     	
		/// <summary>
		/// 内容
		/// </summary>
        public string sContent { set; get; }

     	
		/// <summary>
		/// 图片
		/// </summary>
        public string sImageSrc { set; get; }


        /// <summary>
        /// 反馈人ID
        /// </summary>
        public int iClientID { set; get; }


        /// <summary>
        /// 单位ID
        /// </summary>
        public int iUnitID { set; get; }


        /// <summary>
        /// 反馈人账号
        /// </summary>
        public string sClientName { set; get; }


        /// <summary>
        /// 用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]
        /// </summary>
        public int iClientType { set; get; }

     	
		/// <summary>
		/// 回复状态[0:未回复,1:已回复]
		/// </summary>
        public bool bIsReplyStatus { set; get; }

     	
		/// <summary>
		/// 反馈时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }


        /// <summary>
        /// 阅读标识[true:未读,false:已读]
        /// </summary>
        public bool bIsReaded { set; get; }


        /// <summary>
        /// 回复信息
        /// </summary>
        public EHECD_FeedbackReply FeedbackReply { set; get; }
    }
}