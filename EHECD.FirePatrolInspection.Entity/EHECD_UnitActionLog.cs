using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 单位操作日志 
    /// </summary>
    public class EHECD_UnitActionLog 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 操作人员ID
		/// </summary>
        public long iUnitUserID { set; get; }

     	
		/// <summary>
		/// 操作类型
		/// </summary>
        public string sType { set; get; }

     	
		/// <summary>
		/// IP地址
		/// </summary>
        public string sIpAddress { set; get; }

     	
		/// <summary>
		/// 操作内容
		/// </summary>
        public string sContent { set; get; }

     	
		/// <summary>
		/// 所属单位ID
		/// </summary>
        public int iUnitID { set; get; }

     	
		/// <summary>
		/// 写入时间
		/// </summary>
        public DateTime dInsertTime { set; get; }

         }
}
