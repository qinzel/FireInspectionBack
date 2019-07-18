using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 帖子评论 
    /// </summary>
    public class EHECD_Comment 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 帖子ID
		/// </summary>
        public long iTieziID { set; get; }

     	
		/// <summary>
		/// 评论人ID
		/// </summary>
        public int iClientID { set; get; }


        /// <summary>
        /// 评论人名称
        /// </summary>
        public string sName { set; get; }


        /// <summary>
        /// 评论人头像
        /// </summary>
        public string sImageSrc { set; get; }

     	
		/// <summary>
		/// 评论人目标名称
		/// </summary>
        public string sTarName { set; get; }

     	
		/// <summary>
		/// 评论内容
		/// </summary>
        public string sContent { set; get; }

     	
		/// <summary>
		/// 评论人目标ID
		/// </summary>
        public int iTarClientID { set; get; }

     	
		/// <summary>
		/// 评论时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }

    }
}
