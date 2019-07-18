using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 总后台基础设置业务逻辑
    /// </summary>
    public class SettingsService
    {
        static SettingsService instance = new SettingsService();
        private static SettingsDao Dao = SettingsDao.Instance;

        private SettingsService()
        {
        }

        public static SettingsService Instance
        {
            get { return instance; }
        }

		#region 获取总后台基础设置详情

        /// <summary>
        /// 获取总后台基础设置详情
        /// </summary>
        /// <returns></returns>
        public EHECD_Settings Get()
        {
            return Dao.Get() ?? new EHECD_Settings();
        }
		
		#endregion
		
		#region 添加编辑总后台基础设置

        /// <summary>
        /// 添加编辑总后台基础设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Settings entity)
        {
            ResultMessage result = new ResultMessage();

            result.success = Dao.Modify(entity);
            result.message = result.success ? "编辑基础设置成功" : "编辑基础设置失败";

            return result;
        }

        #endregion

        #region 获取基础设置

        /// <summary>
        /// 获取基础设置
        /// </summary>
        /// <returns></returns>
        [APIAttribute(name: "settings.get", desc: "获取基础设置")]
        public ResultMessage GetSettings()
        {
            ResultMessage result = new ResultMessage();
            EHECD_Settings entity = Dao.Get();
            result.success = entity == null ? false : true;
            result.message = result.success ? "获取基础设置成功" : "获取基础设置失败";
            result.data = entity;
            return result;
        }

        #endregion
    }
}