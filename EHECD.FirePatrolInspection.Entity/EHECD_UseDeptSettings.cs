using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 使用单位基础设置 
    /// </summary>
    public class EHECD_UseDeptSettings 
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
		/// 所属消防部门ID
		/// </summary>
        public int iFireDeptID { set; get; }


        /// <summary>
        /// 关联维护公司ID集
        /// </summary>
        public string sRepairDeptIDs { set; get; }

     	
		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }


        /// <summary>
        /// 关联维护公司
        /// </summary>
        public List<EHECD_Unit> DetailList { set; get; }
    }
}