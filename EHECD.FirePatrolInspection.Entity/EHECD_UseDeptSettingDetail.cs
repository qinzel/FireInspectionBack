using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 使用单位基础设置关联维护公司 
    /// </summary>
    public class EHECD_UseDeptSettingDetail 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 使用单位基础设置ID
		/// </summary>
        public int iUseDeptSettingsID { set; get; }

     	
		/// <summary>
		/// 关联维护公司ID
		/// </summary>
        public int iRepairDeptID { set; get; }

     	
		/// <summary>
		/// 添加时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }

    }
}
