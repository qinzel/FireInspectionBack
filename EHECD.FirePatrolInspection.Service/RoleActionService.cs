using System.Collections.Generic;
using System.Linq;
using EHECD.EntityFramework.EFWork;

namespace EHECD.FirePatrolInspection.Service
{
    public class RoleActionService
    {
         static RoleActionService instance;
         private RoleActionService() { }

         static public RoleActionService Instance
        {
            get
            {
                if (instance == null)
                    instance = new RoleActionService();
                return instance;
            }
        }

        #region 根据角色id获取角色权限

        /// <summary>
        /// 根据角色id获取角色权限
        /// </summary>
        /// <returns></returns>
        public List<EHECD_RoleAction> GetRoleActionListByRoleID(long iRoleID)
        {
            using (var Context = new Entities())
            {
                return Context.EHECD_RoleAction.Where(m => m.iRoleID == iRoleID).ToList();
            }
        }

        #endregion
    }
}