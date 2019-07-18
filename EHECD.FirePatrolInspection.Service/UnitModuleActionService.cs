using System.Collections.Generic;
using System.Linq;
using EHECD.Common;
using EHECD.EntityFramework.EFWork;

namespace EHECD.FirePatrolInspection.Service
{
    public class UnitModuleActionService
    {
        static UnitModuleActionService instance;
        private UnitModuleActionService() { }

        static public UnitModuleActionService Instance
        {
            get
            {
                if (instance == null)
                    instance = new UnitModuleActionService();
                return instance;
            }
        }

        #region 获取所有模块权限

        /// <summary>
        /// 获取所有模块权限
        /// </summary>
        /// <returns></returns>
        public List<EHECD_UnitModuleAction> GetList()
        {
			using (var Context = new Entities())
			{
				List<EHECD_UnitModuleAction> list = Context.EHECD_UnitModuleAction.Where(o => o.bIsDeleted == false)
							.OrderBy(x => x.iOrder)
							.ToList()
							;
			
            return list; 
			}
        }

        #endregion

        #region 获取模块权限

        /// <summary>
        /// 获取模块权限
        /// </summary>
        /// <param name="id">模块id</param>
        /// <returns></returns>
        public string GetModuleAction(long id)
        {
			using (var Context = new Entities())
			{
				List<EHECD_UnitModuleAction> list = Context.EHECD_UnitModuleAction.Where(o => o.bIsDeleted == false && o.iModuleID == id)
							.OrderBy(x => x.iOrder)
							.ToList()
							;
				return TCommon.ItemToJson(list).ToString();
			}
        }

        #endregion
    }
}