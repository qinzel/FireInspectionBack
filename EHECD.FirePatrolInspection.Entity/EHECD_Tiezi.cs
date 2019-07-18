using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 帖子 
    /// </summary>
    public class EHECD_Tiezi 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 发帖人ID
		/// </summary>
        public int iClientID { set; get; }


        /// <summary>
        /// 发帖人姓名
        /// </summary>
        public string sClientName { set; get; }


        /// <summary>
        /// 部门名称
        /// </summary>
        public string sOrganName { set; get; }


        /// <summary>
        /// 发帖人头像
        /// </summary>
        public string sClientImageSrc { set; get; }


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
		/// 评论回复数
		/// </summary>
        public int iReplyCount { set; get; }

     	
		/// <summary>
		/// 发帖时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }


        /// <summary>
        /// 评论
        /// </summary>
        public List<EHECD_Comment> CommentList { set; get; }
    }
}
