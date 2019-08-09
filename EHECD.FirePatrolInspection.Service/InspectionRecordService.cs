
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;
using Newtonsoft.Json;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 巡检记录业务逻辑
    /// </summary>
    public class InspectionRecordService
    {
        static InspectionRecordService instance = new InspectionRecordService();
        private static InspectionRecordDao Dao = InspectionRecordDao.Instance;

        private InspectionRecordService()
        {
        }

        public static InspectionRecordService Instance
        {
            get { return instance; }
        }
		
		#region 获取巡检记录数据

        /// <summary>
        /// 获取巡检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetAllTypeGridData(QueryParams param)
        {
			int iTotalRecord = 0;
            var list = Dao.GetList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取部分巡检记录数据

        /// <summary>
        /// 获取部分巡检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetAllTypePartGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetPartList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取部分维护巡检记录数据

        /// <summary>
        /// 获取部分维护巡检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetAllTypePartRepairDeptGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetPartRepairDeptList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取巡检记录数据

        /// <summary>
        /// 获取巡检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetAllTypeListGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetAllTypeList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取设备最近一次巡检记录视图

        /// <summary>
        /// 获取设备最近一次巡检记录视图
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <returns></returns>
        public EHECD_InspectionRecord GetLastRecordByDeviceID(long iDeviceID)
        {
            EHECD_InspectionRecord entity = Dao.GetLastRecordInfo(iDeviceID) ?? new EHECD_InspectionRecord();
            if (entity.ID > 0)
            {
                entity.DetailList = InspectionDetailDao.Instance.GetDetailList(entity.ID).ToList();
            }
            return entity;
        }

        #endregion

        #region 获取巡检记录详情

        /// <summary>
        /// 获取巡检记录详情
        /// </summary>
        /// <param name="iInspectionRecordID"></param>
        /// <returns></returns>
        public EHECD_InspectionRecord Get(long iInspectionRecordID)
        {
            return Dao.GetInfo(iInspectionRecordID);
        }
		
		#endregion

        #region 获取巡检记录列表

        /// <summary>
        /// 获取巡检记录列表
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [APIAttribute(name: "ins.getlist", desc: "获取巡检记录列表")]
        public ResultMessage GetInspectionRecordList(int iDeviceID, int page)
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iDeviceID", iDeviceID);
            param.condition.Add("iRecordType", 0);

            ResultMessage result = new ResultMessage();

            if (iDeviceID == 0)
            {
                result.message = "获取巡检记录列表失败";
                return result;
            }

            var list = new List<EHECD_InspectionRecord>();

            try
            {
                list = Dao.GetRecordList(param, ref iTotalRecord).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取巡检记录列表成功" : "获取巡检记录列表失败";
            return result;
        }

        #endregion

        #region 获取维修记录列表

        /// <summary>
        /// 获取维修记录列表
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [APIAttribute(name: "repair.getlist", desc: "获取维修记录列表")]
        public ResultMessage GetRepairRecordList(int iDeviceID, int page)
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iDeviceID", iDeviceID);
            param.condition.Add("iRecordType", 1);

            ResultMessage result = new ResultMessage();

            if (iDeviceID == 0)
            {
                result.message = "获取维修记录列表失败";
                return result;
            }

            var list = new List<EHECD_InspectionRecord>();

            try
            {
                list = Dao.GetRecordList(param, ref iTotalRecord).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取维修记录列表成功" : "获取维修记录列表失败";
            return result;
        }

        #endregion

        #region 获取更换记录列表

        /// <summary>
        /// 获取更换记录列表
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [APIAttribute(name: "change.getlist", desc: "获取更换记录列表")]
        public ResultMessage GetChangeRecordList(int iDeviceID, int page)
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iDeviceID", iDeviceID);
            param.condition.Add("iRecordType", 2);

            ResultMessage result = new ResultMessage();

            if (iDeviceID == 0)
            {
                result.message = "获取更换记录列表失败";
                return result;
            }

            var list = new List<EHECD_InspectionRecord>();

            try
            {
                list = Dao.GetRecordList(param, ref iTotalRecord).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取更换记录列表成功" : "获取更换记录列表失败";
            return result;
        }

        #endregion

        #region 获取巡检记录详情

        /// <summary>
        /// 获取巡检记录详情
        /// </summary>
        /// <param name="iInspectionRecordID"></param>
        /// <returns></returns>
        [APIAttribute(name: "ins.get", desc: "获取巡检记录详情")]
        public ResultMessage GetRepairRecord(long iInspectionRecordID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_InspectionRecord entity = Dao.GetInfo(iInspectionRecordID);
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取巡检记录详情成功" : "获取巡检记录详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 添加巡检记录

        /// <summary>
        /// 添加巡检记录
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="iDeviceID"></param>
        /// <param name="sParamList"></param>
        /// <param name="sDesc"></param>
        /// <returns></returns>
        [APIAttribute(name: "ins.add", desc: "添加巡检记录")]
        public ResultMessage CreateInspectionRecord(int iClientID, long iDeviceID, string sParamList, string sDesc = "")
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "添加巡检记录失败";
                return result;
            }

            List<EHECD_InspectionDetail> detailList = new List<EHECD_InspectionDetail>();
            if (!String.IsNullOrEmpty(sParamList))
            {
                detailList = JsonConvert.DeserializeObject<List<EHECD_InspectionDetail>>(sParamList);
            }

            EHECD_InspectionRecord entity = new EHECD_InspectionRecord()
            {
                iClientID = iClientID,
                iDeviceID = iDeviceID,
                sDesc = sDesc,
                sOperator = ClientDao.Instance.GetClient(iClientID).sName,
                DetailList = detailList
            };

            result.success = Dao.InsertInsRecord(entity) ? true : false;
            if (result.success)
            {
                EHECD_Device device = DeviceDao.Instance.Get(entity.iDeviceID);
                if (device.iAbnormalStatus)
                {
                    IEnumerable<EHECD_Client> list = ClientDao.Instance.GetClientByUnitID(device.iRepairDeptID);
                    SiteMsgService.Instance.SendAbnormalNotice(list.ToList(), device.sName, device.sNumber);
                }
            }
            result.message = result.success ? "添加巡检记录成功" : "添加巡检记录失败";
            return result;
        }

        #endregion

        #region 批量添加巡检记录

        /// <summary>
        /// 批量添加巡检记录
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sRecordList"></param>
        /// <returns></returns>
        [APIAttribute(name: "ins.adds", desc: "批量添加巡检记录")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage CreateInspectionRecords(int iClientID, List<EHECD_InspectionRecord> sRecordList)
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "批量添加巡检记录失败";
                return result;
            }

            EHECD_Client client = ClientDao.Instance.GetClient(iClientID);

            if (sRecordList != null && sRecordList.Count > 0)
            {
                foreach (var entity in sRecordList)
                {
                    entity.iClientID = iClientID;
                    entity.sOperator = client.sName;
                    if (entity.sDesc.Length > 200)
                    {
                        result.message = "备注描述最多200个字";
                        return result;
                    }
                }
            }

            result.success = Dao.InsertInsRecords(sRecordList) ? true : false;

            if (result.success)
            {
                foreach (var entity in sRecordList)
                {
                    EHECD_Device device = DeviceDao.Instance.Get(entity.iDeviceID);
                    if (device.iAbnormalStatus)
                    {
                        IEnumerable<EHECD_Client> list = ClientDao.Instance.GetClientByUnitID(device.iRepairDeptID);
                        SiteMsgService.Instance.SendAbnormalNotice(list.ToList(), device.sName, device.sNumber);
                    }
                }
            }

            result.message = result.success ? "批量添加巡检记录成功" : "批量添加巡检记录失败";
            return result;
        }

        #endregion

        #region 后台添加维修记录

        /// <summary>
        /// 后台添加维修记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage InsertRepairRecord(EHECD_InspectionRecord entity)
        {
            ResultMessage result = new ResultMessage();

            result.success = Dao.InsertRepairRecord(entity) ? true : false;
            result.message = result.success ? "添加维修记录成功" : "添加维修记录失败";
            return result;
        }

        #endregion

        #region 后台添加更换记录

        /// <summary>
        /// 后台添加更换记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage InsertChangeRecord(EHECD_InspectionRecord entity)
        {
            ResultMessage result = new ResultMessage();

            result.success = Dao.InsertChangeRecord(entity) ? true : false;
            result.message = result.success ? "添加更换记录成功" : "添加更换记录失败";
            return result;
        }

        #endregion

        #region 添加维修记录

        /// <summary>
        /// 添加维修记录
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="iDeviceID"></param>
        /// <param name="sDesc"></param>
        /// <returns></returns>
        [APIAttribute(name: "repair.add", desc: "添加维修记录")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage CreateRepairRecord(int iClientID, long iDeviceID, string sDesc = "")
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "添加维修记录失败";
                return result;
            }

            EHECD_InspectionRecord entity = new EHECD_InspectionRecord()
            {
                iClientID = iClientID,
                iDeviceID = iDeviceID,
                sDesc = sDesc,
                iRecordType = 1,
                sOperator = ClientDao.Instance.GetClient(iClientID).sName
            };

            result.success = Dao.InsertRepairRecord(entity) ? true : false;
            result.message = result.success ? "添加维修记录成功" : "添加维修记录失败";
            return result;
        }

        #endregion

        #region 添加更换记录

        /// <summary>
        /// 添加更换记录
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="iDeviceID"></param>
        /// <param name="iNumber"></param>
        /// <param name="sModel"></param>
        /// <param name="sSpec"></param>
        /// <param name="sInstallDate"></param>
        /// <param name="sManufacturer"></param>
        /// <param name="sProductionDate"></param>
        /// <param name="sDesc"></param>
        /// <returns></returns>
        [APIAttribute(name: "change.add", desc: "添加更换记录")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage CreateChangeRecord(int iClientID, long iDeviceID, int iNumber = 0, string sModel = "", string sSpec = "",string sInstallDate = "", string sManufacturer = "", string sProductionDate = "", string sDesc = "")
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "添加更换记录失败";
                return result;
            }

            EHECD_InspectionRecord entity = new EHECD_InspectionRecord()
            {
                iClientID = iClientID,
                iDeviceID = iDeviceID,
                sModel = sModel,
                sSpec = sSpec,
                iNumber = iNumber,
                sInstallDate = sInstallDate,
                sManufacturer = sManufacturer,
                sProductionDate = sProductionDate,
                sDesc = sDesc,
                iRecordType = 2,
                sOperator = ClientDao.Instance.GetClient(iClientID).sName
            };

            result.success = Dao.InsertChangeRecord(entity) ? true : false;
            result.message = result.success ? "添加更换记录成功" : "添加更换记录失败";
            return result;
        }

        #endregion

        #region 获取要导出的巡检记录

        /// <summary>
        /// 获取要导出的巡检记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetExportData(QueryParams param)
        {
            return Dao.GetExportData(param);
        }

        #endregion

        #region 获取要导出的部分巡检记录

        /// <summary>
        /// 获取要导出的部分巡检记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetExportPartData(QueryParams param)
        {
            return Dao.GetExportPartData(param);
        }

        #endregion

        #region 获取要导出的部分维护巡检记录

        /// <summary>
        /// 获取要导出的部分维护巡检记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetExportPartRepairDeptList(QueryParams param)
        {
            return Dao.GetExportPartRepairDeptList(param);
        }

        #endregion

        #region 获取全年各月份巡检数据统计

        /// <summary>
        /// 获取全年各月份巡检数据统计
        /// </summary>
        /// <param name="iUseDeptID"></param>
        /// <param name="sYearNumber"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetStatisticsGridData(int iUseDeptID, string sYearNumber)
        {
            return Dao.GetStatisticsGridData(iUseDeptID, sYearNumber);
        }

        #endregion
    }
}