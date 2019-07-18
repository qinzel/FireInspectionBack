using System.Collections.Generic;
using System.Linq;
using EHECD.EntityFramework.EFWork;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 单位角色操作信息业务逻辑
    /// </summary>
    public class UnitRoleActionService
    {
        static UnitRoleActionService instance;
        private UnitRoleActionService() { }

        static public UnitRoleActionService Instance
        {
            get
            {
                if (instance == null)
                    instance = new UnitRoleActionService();
                return instance;
            }
        }

        #region 根据角色id获取角色权限

        /// <summary>
        /// 根据角色id获取角色权限
        /// </summary>
        /// <returns></returns>
        public List<EHECD_UnitRoleAction> GetRoleActionListByRoleID(long iRoleID)
        {
            using (var Context = new Entities())
            {
                return Context.EHECD_UnitRoleAction.Where(m => m.iUnitRoleID == iRoleID).ToList();
            }
        }

        #endregion
    }
}

