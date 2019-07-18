
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using System.Linq;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 使用单位基础设置业务逻辑
    /// </summary>
    public class UseDeptSettingsService
    {
        static UseDeptSettingsService instance = new UseDeptSettingsService();
        private static UseDeptSettingsDao Dao = UseDeptSettingsDao.Instance;

        private UseDeptSettingsService()
        {
        }

        public static UseDeptSettingsService Instance
        {
            get { return instance; }
        }
		
		#region 获取使用单位基础设置数据

        /// <summary>
        /// 获取使用单位基础设置数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetGridData(QueryParams param)
        {
			int iTotalRecord = 0;
            var list = Dao.GetList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }
		
		#endregion

		#region 获取使用单位基础设置详情

        /// <summary>
        /// 获取使用单位基础设置详情
        /// </summary>
        /// <param name="iUseDeptSettingsID"></param>
        /// <returns></returns>
        public EHECD_UseDeptSettings Get(int iUseDeptSettingsID)
        {
            return Dao.Get(iUseDeptSettingsID) ?? new EHECD_UseDeptSettings();
        }
		
		#endregion
		
		#region 添加编辑使用单位基础设置

        /// <summary>
        /// 添加编辑使用单位基础设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_UseDeptSettings entity)
        {
            ResultMessage result = new ResultMessage();

            if (!string.IsNullOrWhiteSpace(entity.sRepairDeptIDs))
            {
                var sIds = entity.sRepairDeptIDs.Split(',');
                entity.DetailList = new List<EHECD_Unit>();
                foreach (var str in sIds)
                {
                    entity.DetailList.Add(new EHECD_Unit()
                    {
                        ID = Convert.ToInt32(str)
                    });
                }
            }

            if (entity.ID == 0)
            {
                //新增使用单位基础设置
                result.success = Dao.Insert(entity);
                result.message = result.success ? "基础设置成功" : "基础设置失败";
            }
            else
            {
                //修改使用单位基础设置
                result.success = Dao.Update(entity);
                result.message = result.success ? "基础设置成功" : "基础设置失败";
            }
            return result;
        }
		
		#endregion
		
		#region 批量删除使用单位基础设置

        /// <summary>
        /// 批量删除使用单位基础设置
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除使用单位基础设置成功" : "删除使用单位基础设置失败";
            return result;
        }

        #endregion

        #region 根据单位ID获取基础设置信息

        /// <summary>
        /// 根据单位ID获取基础设置信息
        /// </summary>
        /// <param name="iUseDeptID"></param>
        /// <returns></returns>
        public EHECD_UseDeptSettings GetSettings(int iUseDeptID)
        {
            return Dao.GetSettings(iUseDeptID) ?? new EHECD_UseDeptSettings();
        }

        #endregion
    }
}