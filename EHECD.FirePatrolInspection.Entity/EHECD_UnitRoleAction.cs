using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 单位角色操作信息 
    /// </summary>
    public class EHECD_UnitRoleAction 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 角色ID
		/// </summary>
        public long iUnitRoleID { set; get; }

     	
		/// <summary>
		/// 模块菜单ID
		/// </summary>
        public long iModuleID { set; get; }

     	
		/// <summary>
		/// 操作信息ID
		/// </summary>
        public long iActionID { set; get; }

         }
}
