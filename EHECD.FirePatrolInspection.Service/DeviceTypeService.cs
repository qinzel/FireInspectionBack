
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 设备分类业务逻辑
    /// </summary>
    public class DeviceTypeService
    {
        static DeviceTypeService instance = new DeviceTypeService();
        private static DeviceTypeDao Dao = DeviceTypeDao.Instance;

        private DeviceTypeService()
        {
        }

        public static DeviceTypeService Instance
        {
            get { return instance; }
        }
		
		#region 获取设备分类数据

        /// <summary>
        /// 获取设备分类数据
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

		#region 获取设备分类详情

        /// <summary>
        /// 获取设备分类详情
        /// </summary>
        /// <param name="iDeviceTypeID"></param>
        /// <returns></returns>
        public EHECD_DeviceType Get(int iDeviceTypeID)
        {
            return Dao.Get(iDeviceTypeID) ?? new EHECD_DeviceType();
        }
		
		#endregion
		
		#region 添加编辑设备分类

        /// <summary>
        /// 添加编辑设备分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_DeviceType entity)
        {
            ResultMessage result = new ResultMessage();
            int total = 0;
            if (entity.ID == 0)
            {
                QueryParams param = new QueryParams();
                param.condition.Add("typeName", entity.sName);
                param.condition.Add("iUseDeptID", entity.iUseDeptID);
                Dao.GetList(param, ref total);
                if (total > 0)
                {
                    result.success = false;
                    result.message = "该单位已有相同名称的设备分类";
                    return result;
                }
                //新增设备分类
                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加设备分类成功" : "添加设备分类失败";
            }
            else
            {
                QueryParams param = new QueryParams();
                param.condition.Add("typeName", entity.sName);
                param.condition.Add("iUseDeptID", entity.iUseDeptID);
                IEnumerable<EHECD_DeviceType> list = Dao.GetList(param, ref total);
                if(list != null && list.Count() > 0 && list.First().ID != entity.ID)
                {
                    result.success = false;
                    result.message = "该单位已有相同名称的设备分类";
                    return result;
                }
                //修改设备分类
                result.success = Dao.Update(entity);
                result.message = result.success ? "编辑设备分类成功" : "编辑设备分类失败";
            }
            return result;
        }
		
		#endregion
		
		#region 批量删除设备分类

        /// <summary>
        /// 批量删除设备分类
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除设备分类成功" : "删除设备分类失败";
            return result;
        }

        #endregion

        #region 获取所有设备分类

        /// <summary>
        /// 获取所有设备分类
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_DeviceType> GetAllList()
        {
            return Dao.GetAllList();
        }

        #endregion

        #region 获取使用单位或平台创建的设备分类

        /// <summary>
        /// 获取使用单位或平台创建的设备分类
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        [APIAttribute(name: "devicetype.getlist", desc: "获取设备分类")]
        public ResultMessage GetDeviceTypeByUnitID(int iUnitID)
        {
            ResultMessage result = new ResultMessage();

            if (iUnitID == 0)
            {
                result.message = "获取单位下辖设备列表失败";
                return result;
            }

            var list = new List<EHECD_DeviceType>();

            try
            {
                list = Dao.GetDeviceTypeByUnitID(iUnitID).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取设备分类成功" : "获取设备分类失败";
            return result;
        }

        #endregion
    }
}

