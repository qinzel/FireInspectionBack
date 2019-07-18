using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 单位角色 
    /// </summary>
    public class EHECD_UnitRole 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 角色名称
		/// </summary>
        public string sRoleName { set; get; }

     	
		/// <summary>
		/// 描述信息
		/// </summary>
        public string sDescription { set; get; }

     	
		/// <summary>
		/// 所属单位ID
		/// </summary>
        public int iUnitID { set; get; }

     	
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
