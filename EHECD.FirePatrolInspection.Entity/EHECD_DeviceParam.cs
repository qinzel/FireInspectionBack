using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 设备指标 
    /// </summary>
    public class EHECD_DeviceParam 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 指标名称
		/// </summary>
        public string sName { set; get; }

     	
		/// <summary>
		/// 所属分类ID
		/// </summary>
        public int iDeviceTypeID { set; get; }


        /// <summary>
        /// 所属分类名称
        /// </summary>
        public string sDeviceTypeName { set; get; }

     	
		/// <summary>
		/// 添加单位ID（使用单位）
		/// </summary>
        public int iUseDeptID { set; get; }


        /// <summary>
        ///添加单位名称
        /// </summary>
        public string sUnitName { set; get; }

     	
		/// <summary>
		/// 添加时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }


        /// <summary>
        /// 前端用
        /// </summary>
        public int bIsSelected { set; get; }

    }
}