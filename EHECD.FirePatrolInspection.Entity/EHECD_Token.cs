using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// Token记录
    /// </summary>
    public class EHECD_Token
    {	
		/// <summary>
		/// 用户ID
		/// </summary>
        public string iClientID { set; get; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        public string SystemType { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }
    }
}