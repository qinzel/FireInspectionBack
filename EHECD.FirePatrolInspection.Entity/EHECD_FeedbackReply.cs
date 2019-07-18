using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 反馈回复 
    /// </summary>
    public class EHECD_FeedbackReply 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 反馈ID
		/// </summary>
        public long iFeedbackID { set; get; }

     	
		/// <summary>
		/// 回复内容
		/// </summary>
        public string sContent { set; get; }

     	
		/// <summary>
		/// 回复时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }

         }
}
