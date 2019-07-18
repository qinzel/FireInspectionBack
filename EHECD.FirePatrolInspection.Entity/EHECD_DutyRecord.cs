using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 值班记录 
    /// </summary>
    public class EHECD_DutyRecord 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 值班人员ID
		/// </summary>
        public int iClientID { set; get; }


        /// <summary>
        /// 值班人员账号/电话
        /// </summary>
        public string sPhone { set; get; }


        /// <summary>
        /// 值班人员姓名
        /// </summary>
        public string sClientName { set; get; }

     	
		/// <summary>
		/// 值班室ID
		/// </summary>
        public int iDutyRoomID { set; get; }


        /// <summary>
        /// 值班室名称
        /// </summary>
        public string sDutyRoomName { set; get; }


        /// <summary>
        /// 使用单位名称
        /// </summary>
        public string sUnitName { set; get; }

     	
		/// <summary>
		/// 值班开始时间
		/// </summary>
        public string sStartTime { set; get; }

     	
		/// <summary>
		/// 值班结束时间
		/// </summary>
        public string sEndTime { set; get; }

     	
		/// <summary>
		/// 值班时长
		/// </summary>
        public int iTimeLength { set; get; }

     	
		/// <summary>
		/// 值班情况
		/// </summary>
        public string sDesc { set; get; }

     	
		/// <summary>
		/// 值班图片
		/// </summary>
        public string sImageSrc { set; get; }


        /// <summary>
        /// 签到时间（创建记录时间）
        /// </summary>
        public DateTime dCreateTime { set; get; }

    }
}