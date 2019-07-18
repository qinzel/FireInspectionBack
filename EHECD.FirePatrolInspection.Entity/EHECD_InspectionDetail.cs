using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 巡检明细 
    /// </summary>
    public class EHECD_InspectionDetail 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 巡检记录ID
		/// </summary>
        public long iInspectionRecordID { set; get; }

     	
		/// <summary>
		/// 设备指标ID
		/// </summary>
        public long iDeviceParamID { set; get; }


        /// <summary>
        /// 指标状态[0:正常,1:异常]
        /// </summary>
        public bool iStatus { set; get; }


        /// <summary>
        /// 指标名称
        /// </summary>
        public string sName { set; get; }

    }
}