using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 站内信 
    /// </summary>
    public class EHECD_SiteMsg 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public int ID { set; get; }

     	
		/// <summary>
		/// 标题
		/// </summary>
        public string sTitle { set; get; }

     	
		/// <summary>
		/// 内容
		/// </summary>
        public string sContent { set; get; }


        /// <summary>
        /// 前端用户ID
        /// </summary>
        public int iClientID { set; get; }


        /// <summary>
        /// 发送方式[0:单位,1:个人,2:巡检通知,3:巡检通知]
        /// </summary>
        public int iType { set; get; }

     	
		/// <summary>
		/// 收信人身份
		/// </summary>
        public string sReceiveDept { set; get; }

     	
		/// <summary>
		/// 收信人账号
		/// </summary>
        public string sReceiveClient { set; get; }

     	
		/// <summary>
		/// 发送人
		/// </summary>
        public string sOperator { set; get; }

     	
		/// <summary>
		/// 创建时间
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
    }
}