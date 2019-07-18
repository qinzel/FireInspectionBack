using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 值班室 
    /// </summary>
    public class EHECD_DutyRoom 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public int ID { set; get; }

     	
		/// <summary>
		/// 所属使用单位ID
		/// </summary>
        public int iUseDeptID { set; get; }


        /// <summary>
        /// 所属使用单位名称
        /// </summary>
        public string sUnitName { set; get; }

     	
		/// <summary>
		/// 值班室名称
		/// </summary>
        public string sName { set; get; }

     	
		/// <summary>
		/// 值班室二维码地址
		/// </summary>
        public string sQRCode { set; get; }

     	
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
