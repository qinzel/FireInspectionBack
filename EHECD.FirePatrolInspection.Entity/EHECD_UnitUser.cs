using System;

namespace EHECD.FirePatrolInspection.Entity
{
    /// <summary>
    /// 单位后台用户 
    /// </summary>
    public class EHECD_UnitUser 
    {
     	
		/// <summary>
		/// 主键ID
		/// </summary>
        public long ID { set; get; }

     	
		/// <summary>
		/// 登录账号
		/// </summary>
        public string sLoginName { set; get; }

     	
		/// <summary>
		/// 登录密码
		/// </summary>
        public string sPassWord { set; get; }

     	
		/// <summary>
		/// 真实姓名
		/// </summary>
        public string sRealName { set; get; }

     	
		/// <summary>
		/// 角色ID
		/// </summary>
        public long iUnitRoleID { set; get; }

     	
		/// <summary>
		/// 用户类别[1:普通用户,2:管理员]
		/// </summary>
        public int iUserType { set; get; }

     	
		/// <summary>
		/// 所属单位ID
		/// </summary>
        public int iUnitID { set; get; }

     	
		/// <summary>
		/// 添加时间
		/// </summary>
        public DateTime dCreateTime { set; get; }

     	
		/// <summary>
		/// 删除标识[0:未删除,1:已删除]
		/// </summary>
        public bool bIsDeleted { set; get; }

         }
}
