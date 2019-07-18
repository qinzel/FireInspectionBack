			
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 使用单位基础设置关联维护公司业务逻辑
    /// </summary>
    public class UseDeptSettingDetailService
    {
        static UseDeptSettingDetailService instance = new UseDeptSettingDetailService();
        private static UseDeptSettingDetailDao Dao = UseDeptSettingDetailDao.Instance;

        private UseDeptSettingDetailService()
        {
        }

        public static UseDeptSettingDetailService Instance
        {
            get { return instance; }
        }
		
		#region 获取使用单位基础设置关联维护公司数据

        /// <summary>
        /// 获取使用单位基础设置关联维护公司数据
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

		#region 获取使用单位基础设置关联维护公司详情

        /// <summary>
        /// 获取使用单位基础设置关联维护公司详情
        /// </summary>
        /// <param name="iUseDeptSettingDetailID"></param>
        /// <returns></returns>
        public EHECD_UseDeptSettingDetail Get(long iUseDeptSettingDetailID)
        {
            return Dao.Get(iUseDeptSettingDetailID) ?? new EHECD_UseDeptSettingDetail();
        }
		
		#endregion
		
		#region 添加编辑使用单位基础设置关联维护公司

        /// <summary>
        /// 添加编辑使用单位基础设置关联维护公司
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_UseDeptSettingDetail entity)
        {
            ResultMessage result = new ResultMessage();
						
			if (entity.ID == 0)
						
            {
                //新增使用单位基础设置关联维护公司
                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加使用单位基础设置关联维护公司成功" : "添加使用单位基础设置关联维护公司失败";
            }
            else
            {
                //修改使用单位基础设置关联维护公司
                result.success = Dao.Update(entity);
                result.message = result.success ? "编辑使用单位基础设置关联维护公司成功" : "编辑使用单位基础设置关联维护公司失败";
            }
            return result;
        }
		
		#endregion
		
		#region 批量删除使用单位基础设置关联维护公司

        /// <summary>
        /// 批量删除使用单位基础设置关联维护公司
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除使用单位基础设置关联维护公司成功" : "删除使用单位基础设置关联维护公司失败";
            return result;
        }

        #endregion
    }
}

