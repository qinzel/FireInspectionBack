using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 前端用户，包含单位安全员、点检员、值班人员、维保公司维保人员、器材供应商员工。 
    /// </summary>
    public class EHECD_Client 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public int ID { set; get; }

     	
		/// <summary>
		/// 手机号
		/// </summary>
        public string sPhone { set; get; }

     	
		/// <summary>
		/// 登录密码
		/// </summary>
        public string sPwd { set; get; }

     	
		/// <summary>
		/// 姓名
		/// </summary>
        public string sName { set; get; }

     	
		/// <summary>
		/// 头像
		/// </summary>
        public string sImageSrc { set; get; }

     	
		/// <summary>
		/// 状态[0:正常,1:已冻结]
		/// </summary>
        public bool iStatus { set; get; }

     	
		/// <summary>
		/// 证件照
		/// </summary>
        public string sCredentials { set; get; }

     	
		/// <summary>
		/// 所属单位ID
		/// </summary>
        public int iUnitID { set; get; }


        /// <summary>
        /// 所属部门名称
        /// </summary>
        public string sOrganName { set; get; }


        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string sUnitName { set; get; }

     	
		/// <summary>
		/// 用户类别[0:点检员,1:消防,2:维护公司,4:值班人员]
		/// </summary>
        public int iType { set; get; }

     	
		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }


        /// <summary>
        /// 关联单位
        /// </summary>
        public List<string> sUnitIds { set; get; }


        /// <summary>
        /// 关联部门
        /// </summary>
        public List<string> sDeptIds { set; get; }


        /// <summary>
        /// 负责设备总数
        /// </summary>
        public int iDeviceCount { set; get; }


        /// <summary>
        /// 值班时长统计分钟数
        /// </summary>
        public int iTimeLength { set; get; }


        /// <summary>
        /// 所属部门集合
        /// </summary>
        public List<EHECD_Dept> DeptList { set; get; }
    }
}