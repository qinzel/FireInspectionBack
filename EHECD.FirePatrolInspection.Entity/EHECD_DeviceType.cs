using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 设备分类 
    /// </summary>
    public class EHECD_DeviceType 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public int ID { set; get; }

     	
		/// <summary>
		/// 添加单位ID（使用单位）
		/// </summary>
        public int iUseDeptID { set; get; }


        /// <summary>
        /// 添加单位名称
        /// </summary>
        public string sUnitName { set; get; }

     	
		/// <summary>
		/// 分类名称
		/// </summary>
        public string sName { set; get; }

     	
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
