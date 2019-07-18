using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 前端用户关联单位关系表 
    /// </summary>
    public class EHECD_ClientDeptRel 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 前端用户ID
		/// </summary>
        public int iClientID { set; get; }


        /// <summary>
        /// 使用单位ID
        /// </summary>
        public string sUnitName { set; get; }


        /// <summary>
        /// 证件照
        /// </summary>
        public string sCredentials { set; get; }


        /// <summary>
        /// 姓名
        /// </summary>
        public string sName { set; get; }


        /// <summary>
        /// 手机号
        /// </summary>
        public string sPhone { set; get; }


        /// <summary>
        /// 单位ID
        /// </summary>
        public int iUnitID { set; get; }

     	
		/// <summary>
		/// 部门ID
		/// </summary>
        public int iOrganID { set; get; }


        /// <summary>
        /// 审核状态[0:待审核,1:已通过,2:已拒绝]
        /// </summary>
        public int iAuditState { set; get; }


        /// <summary>
        /// 状态[0:正常,1:已冻结]
        /// </summary>
        public bool iStatus { set; get; }


        /// <summary>
        /// 操作人
        /// </summary>
        public string sOperator { set; get; }


        /// <summary>
        /// 操作时间
        /// </summary>
        public string sOperateTime { set; get; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }
    }
}