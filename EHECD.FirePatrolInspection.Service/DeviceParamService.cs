
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
    /// 设备指标业务逻辑
    /// </summary>
    public class DeviceParamService
    {
        static DeviceParamService instance = new DeviceParamService();
        private static DeviceParamDao Dao = DeviceParamDao.Instance;

        private DeviceParamService()
        {
        }

        public static DeviceParamService Instance
        {
            get { return instance; }
        }
		
		#region 获取设备指标数据

        /// <summary>
        /// 获取设备指标数据
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

		#region 获取设备指标详情

        /// <summary>
        /// 获取设备指标详情
        /// </summary>
        /// <param name="iDeviceParamID"></param>
        /// <returns></returns>
        public EHECD_DeviceParam Get(long iDeviceParamID)
        {
            return Dao.Get(iDeviceParamID) ?? new EHECD_DeviceParam();
        }
		
		#endregion
		
		#region 添加编辑设备指标

        /// <summary>
        /// 添加编辑设备指标
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_DeviceParam entity)
        {
            ResultMessage result = new ResultMessage();
            int total = 0;
			if (entity.ID == 0)		
            {
                QueryParams param = new QueryParams();
                param.condition.Add("sName", entity.sName);
                param.condition.Add("iDeviceTypeID", entity.iDeviceTypeID);
                Dao.GetList(param, ref total);
                if (total > 0)
                {
                    result.success = false;
                    result.message = "设备指标名称重复";
                    return result;
                }
                //新增设备指标
                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加设备指标成功" : "添加设备指标失败";
            }
            else
            {
                QueryParams param = new QueryParams();
                param.condition.Add("sName", entity.sName);
                param.condition.Add("iDeviceTypeID", entity.iDeviceTypeID);
                IEnumerable<EHECD_DeviceParam> list = Dao.GetList(param, ref total);
                if (list != null && list.Count() > 0 && list.First().ID != entity.ID)
                {
                    result.success = false;
                    result.message = "设备指标名称重复";
                    return result;
                }
                //修改设备指标
                result.success = Dao.Update(entity);
                result.message = result.success ? "编辑设备指标成功" : "编辑设备指标失败";
            }
            return result;
        }
		
		#endregion
		
		#region 批量删除设备指标

        /// <summary>
        /// 批量删除设备指标
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除设备指标成功" : "删除设备指标失败";
            return result;
        }
		
		#endregion
    }
}

