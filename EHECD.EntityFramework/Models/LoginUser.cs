using System;
using System.Collections.Generic;
using EHECD.Common;

namespace EHECD.EntityFramework.Models
{
    /// <summary>
    /// 后台登录用户
    /// </summary>
    [Serializable]
    public class LoginUser
    {
        /// <summary>
        /// 后台会员ID
        /// </summary>		
        public long ID
        {
            get;
            set;
        }

        /// <summary>
        /// 登录账号
        /// </summary>		
        public string sLoginName
        {
            get;
            set;
        }

        /// <summary>
        /// 用户真实姓名
        /// </summary>		
        public string sRealName
        {
            get;
            set;
        }

        /// <summary>
        /// 角色ID
        /// </summary>		
        public long iRoleID
        {
            get;
            set;
        }

        /// <summary>
        /// 状态[0:正常,1:已冻结]
        /// </summary>		
        public bool iStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 用户类型 1普通用户 2管理员 
        /// </summary>		
        public UserType UserType
        {
            get;
            set;
        }

        /// <summary>
        /// 后台账号归属单位（含使用单位、维护公司、消防部门）ID
        /// </summary>
        public int iUnitID
        {
            get;
            set;
        }

        /// <summary>
        /// 所属单位部门类别[0:使用单位,1:维护公司,2:消防部门]
        /// </summary>
        public int iUserUnitType
        {
            get;
            set;
        }

        /// <summary>
        /// 登录用户所拥有的权限
        /// </summary>
        public List<RoleAction> UserActionList
        {
            get;
            set;
        }
    }
}
