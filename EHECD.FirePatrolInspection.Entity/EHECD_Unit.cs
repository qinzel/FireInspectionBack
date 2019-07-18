using System;
using System.Collections.Generic;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 包含消防部门、使用单位和维护公司超管账号注册 
    /// </summary>
    public class EHECD_Unit 
    {
		/// <summary>
		/// 主键ID
		/// </summary>
        public int ID { set; get; }

     	
		/// <summary>
		/// 上级单位ID
		/// </summary>
        public int iParentID { set; get; }

     	
		/// <summary>
		/// 注册手机号
		/// </summary>
        public string sPhone { set; get; }

     	
		/// <summary>
		/// 登录密码
		/// </summary>
        public string sPwd { set; get; }

     	
		/// <summary>
		/// 单位名称
		/// </summary>
        public string sName { set; get; }

     	
		/// <summary>
		/// 单位地址
		/// </summary>
        public string sAddress { set; get; }

     	
		/// <summary>
		/// 平台管理员
		/// </summary>
        public string sAdminName { set; get; }

     	
		/// <summary>
		/// 联系电话
		/// </summary>
        public string sContact { set; get; }

     	
		/// <summary>
		/// 法人
		/// </summary>
        public string sLegalPerson { set; get; }

     	
		/// <summary>
		/// 资质
		/// </summary>
        public string sQualifications { set; get; }

     	
		/// <summary>
		/// 单位类型[0:使用单位,1:消防部门,2:维护公司]
		/// </summary>
        public int iType { set; get; }

     	
		/// <summary>
		/// 资格证明（图片路径）
		/// </summary>
        public string sCredentials { set; get; }

     	
		/// <summary>
		/// 状态[0:正常,1:已冻结]
		/// </summary>
        public bool iStatus { set; get; }

     	
		/// <summary>
		/// 审核状态[0:待审核,1:已通过,2:已拒绝]
		/// </summary>
        public int iAuditState { set; get; }


        /// <summary>
        /// 审核操作人
        /// </summary>
        public string sOperator { set; get; }

     	
        /// <summary>
        /// 审核时间
        /// </summary>
        public string sAuditTime { set; get; }


		/// <summary>
		/// 创建时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }


        /// <summary>
        /// 所属上级单位名称
        /// </summary>
        public string sParentName { set; get; }


        /// <summary>
        /// 下级关联单位数
        /// </summary>
        public int iDeptCount { set; get; }


        /// <summary>
        /// 设备数
        /// </summary>
        public int iDeviceCount { set; get; }


        /// <summary>
        /// 点检员数量
        /// </summary>
        public int iClientCount { set; get; }


        /// <summary>
        /// 值班人员数量
        /// </summary>
        public int iDutyPeopleCount { set; get; }


        /// <summary>
        /// 单位下辖部门
        /// </summary>
        public List<EHECD_Dept> DeptList { set; get; }


        /// <summary>
        /// 前端登录用户集合
        /// </summary>
        public List<EHECD_Client> ClientList { set; get; }


        /// <summary>
        /// 关联单位集合
        /// </summary>
        public List<EHECD_Unit> UnitList { set; get; }
    }
}