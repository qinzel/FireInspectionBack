
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;
using System.Dynamic;
using System.IO;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 设备业务逻辑
    /// </summary>
    public class DeviceService
    {
        static DeviceService instance = new DeviceService();
        private static DeviceDao Dao = DeviceDao.Instance;
        private static DeviceParamDao paramDao = DeviceParamDao.Instance;
        public static object async = new object();

        private DeviceService()
        {
        }

        public static DeviceService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (async)
                    {
                        if (instance == null)
                        {
                            instance = new DeviceService();
                        }
                    }

                }
                return instance;
            }
        }

        #region 获取设备数据

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetTotalGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetTotalList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取设备数据

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetRelGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            param.sort = "ID";
            param.order = "asc"; 
            var list = Dao.GetRelDeptList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取设备数据

        /// <summary>
        /// 获取设备数据
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

        #region 获取维护公司负责设备数据

        /// <summary>
        /// 获取维护公司负责设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetRepairGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetRepairList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取当月已巡检的设备数据

        /// <summary>
        /// 获取当月已巡检的设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetInspectedGridData(QueryParams param)
        {
			int iTotalRecord = 0;
            var list = Dao.GetInspectedList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取设备详情

        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <returns></returns>
        public EHECD_Device Get(long iDeviceID)
        {
            EHECD_Device entity = Dao.GetInfo(iDeviceID) ?? new EHECD_Device();
            if (entity.ID > 0)
            {
                entity.DeviceList = Dao.GetRelDeviceListByID(entity.ID).ToList();
            }
            return entity;
        }
		
		#endregion
		
		#region 添加编辑设备

        /// <summary>
        /// 添加编辑设备
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Device entity)
        {
            ResultMessage result = new ResultMessage();
            int total = 0;
            lock (async)
            {
                if (entity.iUseDeptID == 0)
                {
                    result.message = "请选择使用单位";
                    return result;
                }
                if (entity.iRepairDeptID == 0)
                {
                    result.message = "请选择维护公司";
                    return result;
                }
                if (entity.iClientID == 0)
                {
                    result.message = "请选择责任人";
                    return result;
                }

                if (entity.ID == 0)
                {
                    //判断设备是否存在
                    if (Dao.IsExist(entity))
                    {
                        result.success = false;
                        result.message = "该单位已存在同编号的设备";
                        return result;
                    }

                    var iVal = Dao.Insert(entity);
                    //新增值班室
                    result.success = iVal > 0 ? true : false;
                    if (result.success)
                    {
                        // 生成值班室二维码（iQRCodeType：0为设备，1为值班室）
                        string qrText = "{ \"iQRCodeType\": 0, \"sQRCodeName\": \"" + entity.sName + "\",\"sNumber\":\"" + entity.sNumber + "\", \"iClientID\": " + entity.iClientID + ", \"iTargetID\": " + iVal + " }";
                        EHECD_Device device = Dao.Get(iVal);
                        device.sQRCode = QrCodeHelper.CreateQrCode(qrText, "Device");
                        Dao.UpdateQRCode(device);
                    }
                    result.message = result.success ? "添加设备成功" : "添加设备失败：服务器错误";
                }
                else
                {
                    //修改值班室
                    var iCount = Dao.GetDeviceCountByNumber(entity);
                    if (iCount > 0)
                    {
                        result.message = "该单位已存在同编号的设备";
                        return result;
                    }
                    result.success = Dao.Update(entity);
                    result.message = result.success ? "编辑设备成功" : "编辑设备失败";
                }
                return result;
            }
        }

        #endregion

        public void SetQRCode()
        {
            IEnumerable<EHECD_Device> list = DeviceService.Instance.GetAllList();
            foreach (EHECD_Device item in list)
            {
                lock (async)
                {
                    // 生成值班室二维码（iQRCodeType：0为设备，1为值班室）
                    string qrText = "{ \"iQRCodeType\": 0, \"sQRCodeName\": \"" + item.sName + "\",\"sNumber\":\"" + item.sNumber + "\", \"iClientID\": " + item.iClientID + ", \"iTargetID\": " + item.ID + " }";
                    item.sQRCode = QrCodeHelper.CreateQrCode(qrText, "Device");
                    Dao.UpdateQRCode(item);
                }
            }
        }
		
		#region 批量删除设备

        /// <summary>
        /// 批量删除设备
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除设备成功" : "删除设备失败";
            return result;
        }

        #endregion

        #region 添加设备

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sNumber"></param>
        /// <param name="iDeviceTypeID"></param>
        /// <param name="iNumber"></param>
        /// <param name="sName"></param>
        /// <param name="sLocation"></param>
        /// <param name="sModel"></param>
        /// <param name="sSpec"></param>
        /// <param name="sInstallDate"></param>
        /// <param name="sManufacturer"></param>
        /// <param name="iRepairDeptID"></param>
        /// <param name="iUseDeptID"></param>
        /// <param name="iCreateUnitID"></param>
        /// <param name="sProductionDate"></param>
        /// <param name="iExpiredYears"></param>
        /// <param name="iForciblyScrappedYears"></param>
        /// <param name="sRelDeviceIDs"></param>
        /// <returns></returns>
        [APIAttribute(name: "device.add", desc: "添加设备")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage CreateDevice(int iClientID, string sNumber, int iDeviceTypeID, string sName, string sLocation, 
            int iRepairDeptID, int iUseDeptID, int iNumber = 0, string sModel = "", string sSpec = "", string sInstallDate = "", string sManufacturer = "",
            int iCreateUnitID = 0, string sProductionDate = "", int iExpiredYears = 0, int iForciblyScrappedYears = 0, string sRelDeviceIDs = "")
        {
            lock (async)
            {
                ResultMessage result = new ResultMessage();

                if (iUseDeptID == 0)
                {
                    result.message = "请选择使用单位";
                    return result;
                }
                if (iRepairDeptID == 0)
                {
                    result.message = "请选择维护公司";
                    return result;
                }
                if (iClientID == 0)
                {
                    result.message = "请选择责任人";
                    return result;
                }

                EHECD_Device entity = new EHECD_Device()
                {
                    iClientID = iClientID,
                    sNumber = sNumber,
                    iDeviceTypeID = iDeviceTypeID,
                    iNumber = iNumber,
                    sName = sName,
                    sLocation = sLocation,
                    sModel = sModel == null ? "":sModel,
                    sSpec = sSpec == null ? "":sSpec,
                    sInstallDate = sInstallDate,
                    sManufacturer = sManufacturer == null ? "":sManufacturer,
                    iRepairDeptID = iRepairDeptID,
                    iUseDeptID = iUseDeptID,
                    iCreateUnitID = iCreateUnitID,
                    sProductionDate = sProductionDate,
                    iExpiredYears = iExpiredYears,
                    iForciblyScrappedYears = iForciblyScrappedYears,
                    sDeviceIDs = null
                };

                int iVal = Dao.Insert(entity);
                if(iVal < 0)
                {
                    result.success = false;
                    result.message = "该单位已存在同编号的设备";
                    return result;
                }else if(iVal == 0)
                {
                    result.success = false;
                    result.message = "设备新增失败：服务器错误";
                    return result;
                }
                
                // 生成设备二维码（iQRCodeType：0为设备，1为值班室）
                string qrText = "{ \"iQRCodeType\": 0, \"sQRCodeName\": \"" + entity.sName + "\", \"iClientID\": " + entity.iClientID + ",\"sNumber\":\"" + entity.sNumber + "\", \"iTargetID\": " + iVal + " }";
                EHECD_Device device = Dao.Get(iVal);
                device.sQRCode = QrCodeHelper.CreateQrCode(qrText, "Device");
                Dao.UpdateQRCode(device);
                result.success = true;
                result.message = "添加设备成功";
                return result;
            }
        }

        #endregion

        #region 获取设备列表

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="page"></param>
        /// <param name="sUnitID"></param>
        /// <param name="iStatus"></param>
        /// <param name="bIsRecorded"></param>
        /// <param name="bIsExpired"></param>
        /// <param name="sName"></param>
        /// <returns></returns>
        [APIAttribute(name: "device.getlist", desc: "获取设备列表")]
        public ResultMessage GetDeviceList(int iClientID, int page, string sUnitID = "", string iStatus = "", string bIsRecorded = "", string bIsExpired = "", string sName = "")
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("sUnitID", sUnitID);
            param.condition.Add("iStatus", iStatus);
            param.condition.Add("bIsRecorded", bIsRecorded);
            param.condition.Add("bIsExpired", bIsExpired);
            param.condition.Add("sName", sName);

            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取设备列表失败";
                return result;
            }

            EHECD_Client entity = ClientDao.Instance.GetClient(iClientID);

            var list = new List<EHECD_Device>();

            try
            {
                switch (entity.iType)
                {
                    case 0:
                        param.condition.Add("iClientID", iClientID);
                        list = Dao.GetDeviceList(param, ref iTotalRecord).ToList();
                        break;
                    case 1:
                        param.condition.Add("iUnitID", entity.iUnitID);
                        list = Dao.GetPartDataList(param, ref iTotalRecord).ToList();
                        break;
                    case 2:
                        param.condition.Add("iRepairDeptID", entity.iUnitID);
                        list = Dao.GetUnitDeviceList(param, ref iTotalRecord).ToList();
                        break;
                }
                

                result.success = true;
                result.data = list;
            }
            catch(Exception ex)
            {
                result.success = false;
            }

            result.message = result.success ? "获取设备列表成功" : "获取设备列表失败";
            return result;
        }

        #endregion

        #region 获取所有设备

        /// <summary>
        /// 获取所有设备
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="page"></param>
        /// <param name="sUnitID"></param>
        /// <param name="iStatus"></param>
        /// <param name="bIsRecorded"></param>
        /// <param name="bIsExpired"></param>
        /// <returns></returns>
        [APIAttribute(name: "device.getalllist", desc: "获取所有设备")]
        public ResultMessage GetAllDeviceList()
        {
            ResultMessage result = new ResultMessage();

            var list = new List<EHECD_Device>();

            try
            {
                list = Dao.GetAllDeviceList().ToList();
                foreach (var entity in list)
                {
                    var recList = InspectionDetailDao.Instance.GetLastDetailList(entity.ID).ToList();
                    if (recList.Count > 0)
                    {
                        entity.DetailList = recList;
                    }
                    else
                    {
                        List<EHECD_InspectionDetail> detailList = new List<EHECD_InspectionDetail>();
                        List<EHECD_DeviceParam> paramList = DeviceParamDao.Instance.GetListByTypeID(entity.iDeviceTypeID, entity.iUseDeptID, entity.iUseDeptID).ToList();
                        foreach (var param in paramList)
                        {
                            detailList.Add(new EHECD_InspectionDetail()
                            {
                                iDeviceParamID = param.ID,
                                sName = param.sName
                            });
                        }
                        entity.DetailList = detailList;
                    }

                    EHECD_DeviceType type = DeviceTypeDao.Instance.Get(entity.iDeviceTypeID);
                    if (type != null)
                    {
                        entity.ParamList = DeviceParamDao.Instance.GetListByTypeID(type.ID, type.iUseDeptID, entity.iUseDeptID).ToList();
                    }
                    entity.DeviceList = Dao.GetRelDeviceListByID(entity.ID).ToList();
                    if (entity.DeviceList != null && entity.DeviceList.Count > 0)
                    {
                        foreach (var device in entity.DeviceList)
                        {
                            EHECD_DeviceType device_type = DeviceTypeDao.Instance.Get(device.iDeviceTypeID);
                            device.ParamList = DeviceParamDao.Instance.GetListByTypeID(device_type.ID, device_type.iUseDeptID, device.iUseDeptID).ToList();
                        }
                    }
                }
                result.success = true;
                result.data = list;
            }
            catch(Exception ex)
            {
                result.success = false;
            }

            result.message = result.success ? "获取所有设备成功" : "获取所有设备失败";
            return result;
        }

        #endregion

        #region 客户端设备数据

        [APIAttribute(name: "device.client.getlist", desc: "获取设备列表")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage GetUpdatedDeviceList(int iClientID,string lm)
        {
            ResultMessage result = new ResultMessage();
            ClientMessage msg = new ClientMessage();
            if (iClientID == 0)
            {
                result.message = "获取设备列表失败";
                return result;
            }

            EHECD_Client entity = ClientDao.Instance.GetClient(iClientID);

            var list = new List<EHECD_Device>();
            //责任人发生变更的设备列表
            var delList = new List<EHECD_Device>();
            List<DeviceClientModel> clientList = new List<DeviceClientModel>();
            DateTime? lastModified = null;
            DateTime parseTime;
            if (!string.IsNullOrEmpty(lm) && DateTime.TryParse(lm, out parseTime))
            {
                lastModified = parseTime;
            }
            DateTime now = DateTime.Now;
            try
            {
                switch (entity.iType)
                {
                    case 0:
                        //点检员获取设备列表
                        list = Dao.ClientGetDeviceList(new EHECD_Device { iClientID = iClientID }, lastModified).ToList();
                        if(lastModified != null && lastModified.Value.Year >= 2019)
                        {
                            delList = Dao.GetDeviceListChangeClient(new EHECD_Device { iClientID = iClientID }, lastModified.Value).ToList();
                        }
                        break;
                    case 1:
                        //消防人员获取设备列表
                        lastModified = null;
                        list = Dao.ClientGetDeviceListByFireUnit(entity.iUnitID, lastModified).ToList();
                        break;
                    case 2:
                        //维护公司人员获取设备列表
                        lastModified = null;
                        list = Dao.ClientGetDeviceList(new EHECD_Device { iRepairDeptID = entity.iUnitID }, lastModified).ToList();
                        break;
                }
                if(list != null)
                {
                    foreach(EHECD_Device device in list)
                    {
                        clientList.Add(new DeviceClientModel
                        {
                            ID = device.ID,
                            iAbnormalStatus = device.iAbnormalStatus ? 1 : 0,
                            iClientID = device.iClientID,
                            iDeviceTypeID = device.iDeviceTypeID,
                            iNumber = device.iNumber,
                            iRepairDeptID = device.iRepairDeptID,
                            iUseDeptID = device.iUseDeptID,
                            LastModifyTime = device.LastModifyTime,
                            sClientName = device.sClientName,
                            sClientPhone = device.sClientPhone,
                            sDeviceTypeName = device.sDeviceTypeName,
                            sLastChangeDate = device.sLastChangeDate,
                            sLastInspectionDate = device.sLastInspectionDate,
                            sLastRepairDate = device.sLastRepairDate,
                            sLocation = device.sLocation,
                            sManufacturer = device.sManufacturer,
                            sModel = device.sModel,
                            sName = device.sName,
                            sNumber = device.sNumber,
                            sRepairDeptName = device.sRepairDeptName,
                            sUnitName = device.sUnitName,
                            bIsDeleted = device.bIsDeleted
                        });
                    }

                    foreach(EHECD_Device device in delList)
                    {
                        clientList.Add(new DeviceClientModel
                        {
                            ID = device.ID,
                            bIsDeleted = 1
                        });
                    }
                }

                result.success = true;
                msg.Data = clientList;
                msg.IsModified = clientList.Count > 0;
                msg.IsAll = lastModified == null || lastModified.Value.Year < 2019 ? true : false;
                msg.LastModifyTime = now.ToString();
                result.data = msg;
            }
            catch (Exception ex)
            {
                result.success = false;
            }

            result.message = result.success ? "获取设备列表成功" : "获取设备列表失败";
            return result;
        }

        [APIAttribute(name: "device.client.getDeviceParam", desc: "查询设备指标")]
        [ClientAPI]
        public ResultMessage GetUpdatedDeviceParam(int iClientID, string lm)
        {
            ResultMessage result = new ResultMessage();
            ClientMessage msg = new ClientMessage();
            DateTime? lastModified = null;
            DateTime parseTime;
            if (!string.IsNullOrEmpty(lm) && DateTime.TryParse(lm, out parseTime))
            {
                lastModified = parseTime;
            }
            DateTime now = DateTime.Now;
            try
            {
                List<EHECD_DeviceParam> list = paramDao.GetAll().ToList();

                result.success = true;
                msg.Data = list;
                msg.IsModified = true;
                msg.IsAll = true;
                msg.LastModifyTime = now.ToString();
                result.data = msg;
            }
            catch (Exception ex)
            {
                result.success = false;
            }

            result.message = result.success ? "成功" : "获取失败";
            return result;
        }

        #endregion

        #region 获取单位下辖设备列表

        /// <summary>
        /// 获取单位下辖设备列表
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <param name="page"></param>
        /// <param name="iStatus"></param>
        /// <param name="bIsRecorded"></param>
        /// <param name="bIsExpired"></param>
        /// <param name="sName"></param>
        /// <param name="bHasProductDate"></param>
        /// <returns></returns>
        [APIAttribute(name: "device.getlistbyunit", desc: "获取单位下辖设备列表")]
        public ResultMessage GetDeviceListByUnitID(int iUnitID, int page, string iStatus = "", string bIsRecorded = "", string bIsExpired = "", string sName = "", string bHasProductDate = "")
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iUnitID", iUnitID);
            param.condition.Add("iStatus", iStatus);
            param.condition.Add("bIsRecorded", bIsRecorded);
            param.condition.Add("bIsExpired", bIsExpired);
            param.condition.Add("bHasProductDate", bHasProductDate);
            param.condition.Add("sName", sName);

            ResultMessage result = new ResultMessage();

            if (iUnitID == 0)
            {
                result.message = "获取单位下辖设备列表失败";
                return result;
            }

            var list = new List<EHECD_Device>();

            try
            {
                list = Dao.GetUnitDeviceList(param, ref iTotalRecord).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取设备列表成功" : "获取设备列表失败";
            return result;
        }

        #endregion

        #region 获取设备详情

        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <returns></returns>
        [APIAttribute(name: "device.get", desc: "获取设备详情")]
        public ResultMessage GetRepairRecord(long iDeviceID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Device entity = Dao.GetInfo(iDeviceID) ?? new EHECD_Device();
            if (entity.ID > 0)
            {
                var list = InspectionDetailDao.Instance.GetLastDetailList(iDeviceID).ToList();
                if (list.Count > 0)
                {
                    entity.DetailList = list;
                }
                else
                {
                    List<EHECD_InspectionDetail> detailList = new List<EHECD_InspectionDetail>();
                    List<EHECD_DeviceParam> paramList = DeviceParamDao.Instance.GetListByTypeID(entity.iDeviceTypeID, entity.iUseDeptID, entity.iUseDeptID).ToList();
                    foreach (var param in paramList)
                    {
                        detailList.Add(new EHECD_InspectionDetail()
                        {
                            iDeviceParamID = param.ID,
                            sName = param.sName
                        });
                    }
                    entity.DetailList = detailList;
                }
                
                EHECD_DeviceType type = DeviceTypeDao.Instance.Get(entity.iDeviceTypeID);
                if (type != null)
                {
                    entity.ParamList = DeviceParamDao.Instance.GetListByTypeID(type.ID, type.iUseDeptID, entity.iUseDeptID).ToList();
                }
                entity.DeviceList = Dao.GetRelDeviceListByID(entity.ID).ToList();
                if (entity.DeviceList != null && entity.DeviceList.Count > 0)
                {
                    foreach (var device in entity.DeviceList)
                    {
                        EHECD_DeviceType device_type = DeviceTypeDao.Instance.Get(device.iDeviceTypeID);
                        device.ParamList = DeviceParamDao.Instance.GetListByTypeID(device_type.ID, device_type.iUseDeptID, device.iUseDeptID).ToList();
                    }
                }
            }
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取设备详情成功" : "获取设备详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 根据点检员ID获取设备数据

        /// <summary>
        /// 根据点检员ID获取设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetDevicesByClientIDGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetDevicesByClientIDList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取条件下设备数量

        /// <summary>
        /// 获取条件下设备数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetDeviceCount(QueryParams param)
        {
            return Dao.GetDeviceCount(param);
        }

        #endregion

        #region 获取条件下部分设备数量

        /// <summary>
        /// 获取条件下部分设备数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetPartDeviceCount(QueryParams param)
        {
            return Dao.GetPartDeviceCount(param);
        }

        #endregion

        #region 获取条件下部分维护设备数量

        /// <summary>
        /// 获取条件下部分维护设备数量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public int GetPartDeviceRepairCount(QueryParams param)
        {
            return Dao.GetPartDeviceRepairCount(param);
        }

        #endregion

        #region 获取总后台要导出的设备信息

        /// <summary>
        /// 获取总后台要导出的设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetTotalExportData(QueryParams param)
        {
            return Dao.GetTotalExportData(param);
        }

        #endregion

        #region 获取要导出的设备信息

        /// <summary>
        /// 获取要导出的设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetExportData(QueryParams param)
        {
            return Dao.GetExportData(param);
        }

        #endregion

        #region 获取要导出的部分设备信息

        /// <summary>
        /// 获取要导出的部分设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetExportPartData(QueryParams param)
        {
            return Dao.GetExportPartData(param);
        }

        #endregion

        #region 获取要导出的部分维护设备信息

        /// <summary>
        /// 获取要导出的部分维护设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetExportPartRepairData(QueryParams param)
        {
            return Dao.GetExportPartRepairData(param);
        }

        #endregion

        #region 获取所有设备信息

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetAllList()
        {
            return Dao.GetAllList();
        }

        #endregion

        #region 导入设备信息

        /// <summary>
        /// 导入设备信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Import(EHECD_Device entity)
        {
            lock (async)
            {
                ResultMessage result = new ResultMessage();

                if (entity.ID == 0)
                {
                    var iVal = Dao.Import(entity);
                    result.success = iVal > 0 ? true : false;
                    if (result.success)
                    {
                        // 生成设备二维码（iQRCodeType：0为设备，1为值班室）
                        string qrText = "{ \"iQRCodeType\": 0, \"sQRCodeName\": \"" + entity.sName + "\",\"sNumber\":\"" + entity.sNumber + "\", \"iClientID\": 0, \"iTargetID\": " + iVal + " }";
                        EHECD_Device device = Dao.Get(iVal);
                        device.sQRCode = QrCodeHelper.CreateQrCode(qrText, "Device");
                        Dao.UpdateQRCode(device);
                    }
                    result.message = result.success ? "导入设备信息成功" : "导入设备信息失败";
                }
                return result;
            }
        }

        #endregion

        #region 设备更改责任人

        /// <summary>
        /// 设备更改责任人
        /// </summary>
        /// <param name="sIds"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public ResultMessage ChangeClient(string sIds, int iClientID)
        {
            lock (async)
            {
                ResultMessage result = new ResultMessage();

                var list = Dao.GetDataByIDs(sIds);

                try
                {
                    foreach (var entity in list)
                    {

                        if (Dao.ChangeClient(entity.ID, iClientID))
                        {
                            string oldQR = entity.sQRCode;
                            // 生成设备二维码（iQRCodeType：0为设备，1为值班室）
                            string qrText = "{ \"iQRCodeType\": 0, \"sQRCodeName\": \"" + entity.sName + "\",\"sNumber\":\""+entity.sNumber+"\", \"iClientID\": " + iClientID + ", \"iTargetID\": " + entity.ID + " }";
                            entity.sQRCode = QrCodeHelper.CreateQrCode(qrText, "Device");
                            if (Dao.UpdateQRCode(entity))
                            {
                                TIO.deleteFile(System.Web.HttpContext.Current.Server.MapPath(oldQR));
                            }

                            //记录历史
                            Dao.ChangeClientHis(entity.ID, iClientID, entity.iClientID);
                        }
                    }
                    result.success = true;
                }
                catch(Exception ex)
                {
                    result.success = false;
                }
                result.message = result.success ? "更改责任人成功" : "更改责任人失败";
                return result;
            }
        }

        #endregion

        #region 设备更改维护公司

        /// <summary>
        /// 设备更改维护公司
        /// </summary>
        /// <param name="sIds"></param>
        /// <param name="iRepairDeptID"></param>
        /// <returns></returns>
        public ResultMessage ChangeRepair(string sIds, int iRepairDeptID)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.ChangeRepair(sIds, iRepairDeptID)
            };
            result.message = result.success ? "更改维护公司成功" : "更改维护公司失败";
            return result;
        }

        #endregion

        #region 根据ID集获取设备数据

        /// <summary>
        /// 根据ID集获取设备数据
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetDataByIDs(string sIds)
        {
            return Dao.GetDataByIDs(sIds);
        }

        #endregion

        #region 获取各使用单位设备数据

        /// <summary>
        /// 获取各使用单位设备数据
        /// </summary>
        /// <param name="iUseDeptID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetDeviceList(int iUseDeptID)
        {
            return Dao.GetDeviceList(iUseDeptID);
        }

        #endregion

        #region 获取新增设备需要的参数

        /// <summary>
        /// 获取新增设备需要的参数
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="page"></param>
        /// <param name="iStatus"></param>
        /// <param name="bIsRecorded"></param>
        /// <param name="bIsExpired"></param>
        /// <returns></returns>
        [APIAttribute(name: "device.param", desc: "获取新增设备需要的参数")]
        public ResultMessage GetParam(int iClientID)
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取新增设备需要的参数失败";
                return result;
            }

            dynamic obj = new ExpandoObject();
            // 默认前端为点检员，用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]
            var iClientType = 0;
            //1.设备类型
            //2.维护公司
            //3.使用单位
            EHECD_Client entity = ClientDao.Instance.GetClient(iClientID) ?? new EHECD_Client();
            if (entity.ID > 0)
            {
                iClientType = entity.iType;
            }

            try
            {
                switch (iClientType)
                {
                    case 0:// 点检员
                        // 该点检员所属的使用单位
                        obj.UseDeptList = UnitDao.Instance.GetClientRelUnitList(iClientID);
                        break;
                    case 1:// 消防
                        // 该消防部门关联的使用单位
                        obj.UseDeptList = UnitDao.Instance.GetRelUnitListByFireDeptID(entity.iUnitID);
                        break;
                    case 2:// 维护公司
                        // 该维护公司关联的所有使用单位
                        obj.UseDeptList = UnitDao.Instance.GetRelUseDeptList(entity.iUnitID);
                        break;
                }
                result.success = true;
                result.data = obj;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取新增设备需要的参数成功" : "获取新增设备需要的参数失败";
            return result;
        }

        #endregion

        #region 根据单位ID获取最近一条添加的设备

        /// <summary>
        /// 根据单位ID获取最近一条添加的设备
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public EHECD_Device GetLatelyDeviceByUnitID(int iUnitID)
        {
            return Dao.GetLatelyDeviceByUnitID(iUnitID);
        }

        #endregion

        #region 根据单位ID获取最近一条添加的设备

        /// <summary>
        /// 根据单位ID获取最近一条添加的设备
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        [APIAttribute(name: "device.ulately", desc: "获取单位最近添加的设备详情")]
        public ResultMessage GetUnitAddLately(int iUnitID)
        {
            ResultMessage result = new ResultMessage();
            if (iUnitID == 0)
            {
                result.message = "获取设备详情失败";
                return result;
            }
            EHECD_Device entity = Dao.GetLatelyDeviceByUnitID(iUnitID) ?? new EHECD_Device();
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取设备详情成功" : "获取设备详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 根据点检员ID获取最近一条添加的设备

        /// <summary>
        /// 根据点检员ID获取最近一条添加的设备
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "device.clately", desc: "获取点检员最近添加的设备详情")]
        [ClientAPI(LoginCheck =true)]
        public ResultMessage GetClientAddLately(int iClientID)
        {
            ResultMessage result = new ResultMessage();
            if (iClientID == 0)
            {
                result.message = "获取设备详情失败";
                return result;
            }
            EHECD_Device entity = Dao.GetLatelyDeviceByClientID(iClientID) ?? new EHECD_Device();
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取设备详情成功" : "获取设备详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion
    }
}