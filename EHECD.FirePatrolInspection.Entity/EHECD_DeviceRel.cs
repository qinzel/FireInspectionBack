using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 关联设备
    /// </summary>
    public class EHECD_DeviceRel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { set; get; }


        /// <summary>
        /// 设备ID
        /// </summary>
        public long iDeviceID { set; get; }


        /// <summary>
        /// 关联设备ID
        /// </summary>
        public long iRelDeviceID { set; get; }


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