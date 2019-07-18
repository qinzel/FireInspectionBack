using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 设备责任人变更历史
    /// </summary>
    public class EHECD_DeviceHis
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public long iDeviceID { get; set; }

        /// <summary>
        /// 新责任人ID
        /// </summary>
        public long newClientID { get; set; }

        /// <summary>
        /// 旧责任人ID
        /// </summary>
        public long lastClientID { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime LastModifyTime { get; set; }
    }
}
