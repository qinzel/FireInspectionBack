using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 部门 
    /// </summary>
    public class EHECD_Dept 
    {
		/// <summary>
		/// 主键ID
		/// </summary>
        public int ID { set; get; }

     	
		/// <summary>
		/// 使用单位ID
		/// </summary>
        public int iUseDeptID { set; get; }


        /// <summary>
        /// 使用单位名称
        /// </summary>
        public string sUnitName { set; get; }


        /// <summary>
        /// 审核状态[0:待审核,1:已通过,2:已拒绝]
        /// </summary>
        public int iAuditState { set; get; }


        /// <summary>
        /// 部门名称
        /// </summary>
        public string sName { set; get; }

     	
		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }


        /// <summary>
        /// 人员集合
        /// </summary>
        public List<EHECD_Client> ClientList { set; get; }
    }
}