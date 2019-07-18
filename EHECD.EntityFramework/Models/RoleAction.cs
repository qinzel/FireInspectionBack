using System;

namespace EHECD.EntityFramework.Models
{
    /// <summary>
    /// 用户权限实体
    /// </summary>
    [Serializable]
    public class RoleAction
    {
        /// <summary>
        /// 角色ID
        /// </summary>		
        public long iRoleID
        {
            get;
            set;
        }

        /// <summary>
        /// 模块菜单ID
        /// </summary>		
        public long iModuleID
        {
            get;
            set;
        }

        /// <summary>
        /// 操作信息ID
        /// </summary>		
        public long iActionID
        {
            get;
            set;
        }
    }
}
