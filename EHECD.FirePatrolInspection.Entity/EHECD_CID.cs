using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 记录前端用户的设备ID
    /// </summary>
    public class EHECD_CID
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public int ID { set; get; }

     	
		/// <summary>
		/// 用户ID
		/// </summary>
        public string iClientID { set; get; }

     	
		/// <summary>
		/// 设备ID
		/// </summary>
        public string CID { set; get; }

     	
		/// <summary>
		/// 设备类型
		/// </summary>
        public string DeviceType { set; get; }
    }
}