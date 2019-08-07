				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;
using System.Linq;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 设备操作
    /// </summary>
    public class DeviceDao
    {
        static DeviceDao instance = new DeviceDao();

        private DeviceDao()
        {
        }
        public static DeviceDao Instance
        {
            get
            {
                return instance;
            }
        }

        #region 总平台获取设备信息

        /// <summary>
        /// 总平台获取设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetTotalList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                SELECT * FROM (
	                SELECT D.*, 
	                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
                    (SELECT sPhone FROM EHECD_Client WHERE ID = D.ICLIENTID) sClientPhone,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
	                (SELECT iParentID FROM EHECD_Unit WHERE ID = D.iUseDeptID) iParentID,
	                ISNULL((SELECT sName FROM (
		                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                        WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
	                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
	                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
	                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
	                FROM EHECD_Device D WHERE D.bIsDeleted = 0
                ) T WHERE 1 = 1
            ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUnitID"))
            {
                sCondition.AppendFormat(string.Format(" AND T.iParentID = {0}", param.condition["iUnitID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sSubKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sSubKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iSubDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iSubUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iCurrentDeviceID"))
            {
                var iDeviceID = Convert.ToInt32(param.condition["iCurrentDeviceID"]);
                if (iDeviceID > 0)
                {
                    sCondition.AppendFormat(string.Format(" And T.ID != {0}", iDeviceID));
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsRecorded"))
            {
                var bIsRecorded = Convert.ToInt32(param.condition["bIsRecorded"]);
                switch (bIsRecorded)
                {
                    case 0://未巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) < 0");
                        break;
                    case 1://已巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) = 0");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsExpired"))
            {
                var bIsExpired = Convert.ToInt32(param.condition["bIsExpired"]);
                switch (bIsExpired)
                {
                    case 0://未超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) > 0 OR T.sProductionDate = '')");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) < 0)");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                var iStatus = Convert.ToInt32(param.condition["iStatus"]);
                switch (iStatus)
                {
                    case 0://未超期
                        sCondition.Append(" AND T.iAbnormalStatus = 0");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND T.iAbnormalStatus = 1");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientID = {0}", param.condition["iClientID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region
        /// <summary>
        /// 获取可选关联设备列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetRelDeptList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                SELECT * FROM (
	                SELECT D.*, 
	                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
                    (SELECT sPhone FROM EHECD_Client WHERE ID = D.ICLIENTID) sClientPhone,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
	                (SELECT iParentID FROM EHECD_Unit WHERE ID = D.iUseDeptID) iParentID,
	                ISNULL((SELECT sName FROM (
		                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                        WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
	                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
	                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
	                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
	                FROM EHECD_Device D WHERE D.bIsDeleted = 0
                ) T WHERE 1 = 1  AND T.sProductionDate != ''
            ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUnitID"))
            {
                sCondition.AppendFormat(string.Format(" AND T.iParentID = {0}", param.condition["iUnitID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sSubKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sSubKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iSubDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iSubUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iCurrentDeviceID"))
            {
                var iDeviceID = Convert.ToInt32(param.condition["iCurrentDeviceID"]);
                if (iDeviceID > 0)
                {
                    sCondition.AppendFormat(string.Format(" And T.ID != {0}", iDeviceID));
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsRecorded"))
            {
                var bIsRecorded = Convert.ToInt32(param.condition["bIsRecorded"]);
                switch (bIsRecorded)
                {
                    case 0://未巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) < 0");
                        break;
                    case 1://已巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) = 0");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsExpired"))
            {
                var bIsExpired = Convert.ToInt32(param.condition["bIsExpired"]);
                switch (bIsExpired)
                {
                    case 0://未超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) > 0 OR T.sProductionDate = '')");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) < 0)");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                var iStatus = Convert.ToInt32(param.condition["iStatus"]);
                switch (iStatus)
                {
                    case 0://未超期
                        sCondition.Append(" AND T.iAbnormalStatus = 0");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND T.iAbnormalStatus = 1");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientID = {0}", param.condition["iClientID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
        #endregion

        #region 获取设备信息

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                SELECT * FROM (
	                SELECT D.*, 
	                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
	                (SELECT iParentID FROM EHECD_Unit WHERE ID = D.iUseDeptID) iParentID,
	                ISNULL((SELECT sName FROM (
		                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                        WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
	                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
	                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
	                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
	                FROM EHECD_Device D INNER JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0 AND U.iStatus = 0
                ) T WHERE 1 = 1
            ";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUnitID"))
            {
                sCondition.AppendFormat(string.Format(" AND T.iParentID = {0}", param.condition["iUnitID"]));
            }
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sSubKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sSubKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iSubDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iSubUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iCurrentDeviceID"))
            {
                var iDeviceID = Convert.ToInt32(param.condition["iCurrentDeviceID"]);
                if (iDeviceID > 0)
                {
                    sCondition.AppendFormat(string.Format(" And T.ID != {0}", iDeviceID));
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsRecorded"))
            {
                var bIsRecorded = Convert.ToInt32(param.condition["bIsRecorded"]);
                switch (bIsRecorded)
                {
                    case 0://未巡检
                        sCondition.Append(" AND (T.dCreateTime IS NULL OR DATEDIFF(MONTH, GETDATE(), T.dCreateTime) < 0)");
                        break;
                    case 1://已巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) = 0");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsExpired"))
            {
                var bIsExpired = Convert.ToInt32(param.condition["bIsExpired"]);
                switch (bIsExpired)
                {
                    case 0://未超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) > 0 OR T.sProductionDate = '')");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) < 0)");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                var iStatus = Convert.ToInt32(param.condition["iStatus"]);
                switch (iStatus)
                {
                    case 0://未超期
                        sCondition.Append(" AND T.iAbnormalStatus = 0");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND T.iAbnormalStatus = 1");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientID = {0}", param.condition["iClientID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取维护公司负责的设备信息

        /// <summary>
        /// 获取维护公司负责的设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetRepairList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                SELECT * FROM (
	                SELECT A.* FROM (
		                SELECT D.*, 
		                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
		                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
		                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
		                ISNULL((SELECT sName FROM (
			                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                            WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
		                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
		                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
		                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
		                FROM EHECD_Device D WHERE D.bIsDeleted = 0
	                ) A INNER JOIN EHECD_UNIT U ON A.iUseDeptID = U.ID WHERE A.bIsDeleted = 0 AND U.iStatus = 0
		                AND A.iRepairDeptID = {0} AND A.iUseDeptID IN (
		                SELECT S.iUseDeptID FROM EHECD_UseDeptSettings S INNER JOIN EHECD_UseDeptSettingDetail D ON S.ID = D.iUseDeptSettingsID WHERE D.iRepairDeptID = {0}
	                )
                ) T WHERE 1 = 1
            ", param.condition["iUnitID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sSubKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sSubKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iSubDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iSubUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iCurrentDeviceID"))
            {
                var iDeviceID = Convert.ToInt32(param.condition["iCurrentDeviceID"]);
                if (iDeviceID > 0)
                {
                    sCondition.AppendFormat(string.Format(" And T.ID != {0}", iDeviceID));
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsRecorded"))
            {
                var bIsRecorded = Convert.ToInt32(param.condition["bIsRecorded"]);
                switch (bIsRecorded)
                {
                    case 0://未巡检
                        sCondition.Append(" AND (T.dCreateTime IS NULL OR DATEDIFF(MONTH, GETDATE(), T.dCreateTime) < 0)");
                        break;
                    case 1://已巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) = 0");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsExpired"))
            {
                var bIsExpired = Convert.ToInt32(param.condition["bIsExpired"]);
                switch (bIsExpired)
                {
                    case 0://未超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) > 0)");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) < 0)");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                var iStatus = Convert.ToInt32(param.condition["iStatus"]);
                switch (iStatus)
                {
                    case 0://未超期
                        sCondition.Append(" AND T.iAbnormalStatus = 0");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND T.iAbnormalStatus = 1");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientID = {0}", param.condition["iClientID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取部分设备信息

        /// <summary>
        /// 获取部分设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetPartDataList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                SELECT * FROM (
	                SELECT A.* FROM (
		                SELECT D.*, 
		                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
		                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
		                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
		                ISNULL((SELECT sName FROM (
			                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                            WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
		                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
		                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
		                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
		                FROM EHECD_Device D WHERE D.bIsDeleted = 0
	                ) A 
	                INNER JOIN EHECD_UNIT U ON A.iUseDeptID = U.ID AND U.iStatus = 0 AND U.iParentID = {0}
                ) T WHERE 1 = 1
            ", param.condition["iUnitID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" AND (T.sName LIKE '%{0}%' OR T.sNumber LIKE '%{0}%' OR T.sClientName LIKE '%{0}%' OR T.sOrganName LIKE '%{0}%')", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sSubKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sSubKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iSubDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iSubUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iSubUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iCurrentDeviceID"))
            {
                var iDeviceID = Convert.ToInt32(param.condition["iCurrentDeviceID"]);
                if (iDeviceID > 0)
                {
                    sCondition.AppendFormat(string.Format(" And T.ID != {0}", iDeviceID));
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsRecorded"))
            {
                var bIsRecorded = Convert.ToInt32(param.condition["bIsRecorded"]);
                switch (bIsRecorded)
                {
                    case 0://未巡检
                        sCondition.Append(" AND (T.dCreateTime IS NULL OR DATEDIFF(MONTH, GETDATE(), T.dCreateTime) < 0)");
                        break;
                    case 1://已巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) = 0");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsExpired"))
            {
                var bIsExpired = Convert.ToInt32(param.condition["bIsExpired"]);
                switch (bIsExpired)
                {
                    case 0://未超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) > 0)");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) < 0)");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                var iStatus = Convert.ToInt32(param.condition["iStatus"]);
                switch (iStatus)
                {
                    case 0://未超期
                        sCondition.Append(" AND T.iAbnormalStatus = 0");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND T.iAbnormalStatus = 1");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sUnitID"))
            {
                var iUnitID = Convert.ToInt32(param.condition["sUnitID"]);
                sCondition.AppendFormat(string.Format(" AND T.iUseDeptID = {0}", iUnitID));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientID = {0}", param.condition["iClientID"]));
            }

            param.sort = "T.sInstallDate";

            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取当月已巡检的设备数据

        /// <summary>
        /// 获取当月已巡检的设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetInspectedList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                SELECT * FROM (
                    SELECT D.*, 
                    (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
                    (SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID) sDeviceTypeName
                    FROM EHECD_Device D WHERE D.bIsDeleted = 0
                ) T WHERE 1 = 1 AND T.ID IN (
	                SELECT iDeviceID FROM EHECD_InspectionRecord WHERE 1 = 1 
	                AND DATEPART(YEAR, dCreateTime) = {0}
	                AND DATEPART(MONTH, dCreateTime) = {1}
                ) AND T.iUseDeptID = {2}
            ", param.condition["sYearNumber"], param.condition["iMonth"], param.condition["iUseDeptID"]);

            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取设备信息

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetDeviceList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
	                    SELECT D.ID, D.sNumber, D.sName, D.sLocation, D.sProductionDate, D.iClientID, D.iAbnormalStatus, D.iUseDeptID, D.iExpiredYears, D.sInstallDate,
	                    (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName, 
                        ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
	                    (SELECT sName FROM (SELECT TOP 1 R.iUnitID FROM EHECD_CLIENT C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID WHERE C.ID = D.iClientID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1) A INNER JOIN EHECD_Unit U ON A.iUnitID = U.ID) sUnitName,
	                    ISNULL((SELECT sName FROM (SELECT TOP 1 R.iOrganID FROM EHECD_CLIENT C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1) B INNER JOIN EHECD_Dept D ON B.iOrganID = D.ID), '') sOrganName,
	                    (SELECT TOP 1 dCreateTime FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID ORDER BY dCreateTime DESC) dCreateTime
	                    FROM EHECD_Device D INNER JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0 AND U.iStatus = 0
                    ) T WHERE 1 = 1
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" AND T.iAbnormalStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" AND (T.sName LIKE '%{0}%' OR T.sNumber LIKE '%{0}%' OR T.sClientName LIKE '%{0}%' OR T.sOrganName LIKE '%{0}%')", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sUnitID"))
            {
                var iUnitID = Convert.ToInt32(param.condition["sUnitID"]);
                sCondition.AppendFormat(string.Format(" AND T.iUseDeptID = {0}", iUnitID));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsRecorded"))
            {
                var bIsRecorded = Convert.ToInt32(param.condition["bIsRecorded"]);
                switch (bIsRecorded)
                {
                    case 0://未巡检
                        sCondition.Append(" AND (T.dCreateTime IS NULL OR DATEDIFF(MONTH, GETDATE(), T.dCreateTime) < 0)");
                        break;
                    case 1://已巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) = 0");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsExpired"))
            {
                var bIsExpired = Convert.ToInt32(param.condition["bIsExpired"]);
                switch (bIsExpired)
                {
                    case 0://未超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) > 0)");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) < 0)");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                var iStatus = Convert.ToInt32(param.condition["iStatus"]);
                switch (iStatus)
                {
                    case 0://未超期
                        sCondition.Append(" AND T.iAbnormalStatus = 0");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND T.iAbnormalStatus = 1");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" AND T.iClientID = {0}", param.condition["iClientID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUnitID"))
            {
                //sCondition.AppendFormat(string.Format(" AND T.iUseDeptID = {0} AND sProductionDate != ''", param.condition["iUnitID"]));
                sCondition.AppendFormat(string.Format(" AND T.iUseDeptID = {0}", param.condition["iUnitID"]));
            }

            param.sort = "T.sInstallDate";

            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取单位下辖设备信息

        /// <summary>
        /// 获取单位下辖设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetUnitDeviceList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
	                    SELECT D.ID, D.sNumber, D.sName, D.sLocation, D.sProductionDate, D.iClientID, D.iAbnormalStatus, D.iUseDeptID, D.iExpiredYears, D.iRepairDeptID, D.sInstallDate,
	                    (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName, 
	                    (SELECT sName FROM EHECD_UNIT WHERE ID = D.iUseDeptID) sUnitName,
                        ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
	                    ISNULL((SELECT sName FROM (SELECT TOP 1 R.iOrganID FROM EHECD_CLIENT C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1) B INNER JOIN EHECD_Dept D ON B.iOrganID = D.ID), '') sOrganName,
	                    (SELECT TOP 1 dCreateTime FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID ORDER BY dCreateTime DESC) dCreateTime
	                    FROM EHECD_Device D INNER JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0 AND U.iStatus = 0
                    ) T WHERE 1 = 1
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" AND T.iAbnormalStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" AND (T.sName LIKE '%{0}%' OR T.sNumber LIKE '%{0}%' OR T.sClientName LIKE '%{0}%' OR T.sOrganName LIKE '%{0}%')", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sUnitID"))
            {
                var iUnitID = Convert.ToInt32(param.condition["sUnitID"]);
                sCondition.AppendFormat(string.Format(" AND T.iUseDeptID = {0}", iUnitID));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsRecorded"))
            {
                var bIsRecorded = Convert.ToInt32(param.condition["bIsRecorded"]);
                switch (bIsRecorded)
                {
                    case 0://未巡检
                        sCondition.Append(" AND (T.dCreateTime IS NULL OR DATEDIFF(MONTH, GETDATE(), T.dCreateTime) < 0)");
                        break;
                    case 1://已巡检
                        sCondition.Append(" AND DATEDIFF(MONTH, GETDATE(), T.dCreateTime) = 0");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bIsExpired"))
            {
                var bIsExpired = Convert.ToInt32(param.condition["bIsExpired"]);
                switch (bIsExpired)
                {
                    case 0://未超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) > 0)");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND (T.sProductionDate != '' AND DATEDIFF(DAY, GETDATE(), DATEADD(YEAR, T.iExpiredYears, T.sProductionDate)) < 0)");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                var iStatus = Convert.ToInt32(param.condition["iStatus"]);
                switch (iStatus)
                {
                    case 0://未超期
                        sCondition.Append(" AND T.iAbnormalStatus = 0");
                        break;
                    case 1://已超期
                        sCondition.Append(" AND T.iAbnormalStatus = 1");
                        break;
                }
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" AND T.iClientID = {0}", param.condition["iClientID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" AND T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUnitID"))
            {
                //sCondition.AppendFormat(string.Format(" AND T.iUseDeptID = {0} AND sProductionDate != ''", param.condition["iUnitID"]));
                sCondition.AppendFormat(string.Format(" AND T.iUseDeptID = {0}", param.condition["iUnitID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "bHasProductDate"))
            {
                sCondition.AppendFormat(string.Format(" AND T.sProductionDate != ''", param.condition["bHasProductDate"]));
            }

            param.sort = "T.ID";
            param.order = "asc";
            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取所有设备

        /// <summary>
        /// 获取所有设备
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetAllDeviceList()
        {
            string sSql = @"
                    SELECT D.*,
	                (SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID) sDeviceTypeName, 
	                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName, 
	                (SELECT sName FROM (SELECT TOP 1 R.iUnitID FROM EHECD_CLIENT C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID WHERE C.ID = D.iClientID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1) A INNER JOIN EHECD_Unit U ON A.iUnitID = U.ID) sUnitName,
	                (SELECT sName FROM (SELECT TOP 1 R.iOrganID FROM EHECD_CLIENT C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1) B INNER JOIN EHECD_Dept D ON B.iOrganID = D.ID) sOrganName,
	                (SELECT TOP 1 dCreateTime FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID ORDER BY dCreateTime DESC) dCreateTime
	                FROM EHECD_Device D INNER JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0 AND U.iStatus = 0
                ";
            
            return DBHelper.Query<EHECD_Device>(sSql);
        }

        #endregion

        #region 移动端获取设备

        /// <summary>
        /// 获取设备
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> ClientGetDeviceList(EHECD_Device filter,DateTime? lm)
        {
            string sSql = @"
                    SELECT A.*,D.sName sDeviceTypeName,E.sName sClientName ,C.sName sUnitName,F.sName sOrganName,
                    (SELECT TOP 1 dCreateTime FROM EHECD_InspectionRecord WHERE iDeviceID = A.ID ORDER BY dCreateTime DESC) sLastInspectionDate
                    FROM EHECD_Device A
                    INNER JOIN EHECD_Unit C ON A.iUseDeptID = C.ID
                    LEFT JOIN EHECD_DeviceType D ON D.ID = A.iDeviceTypeID
                    LEFT JOIN EHECD_Client E ON E.ID = A.iClientID
                    LEFT JOIN EHECD_ClientDeptRel B ON B.iClientID = A.iClientID AND B.iUnitID = A.iUseDeptID AND B.iAuditState = 1 AND B.iStatus = 0
                    LEFT JOIN EHECD_Dept F ON F.ID = B.iOrganID 
                    WHERE  C.iStatus = 0
                ";
            if(filter.iClientID > 0)
            {
                //点检员为条件
                sSql += " AND A.iClientID = @iClientID";
            }
            if(filter.iUseDeptID > 0)
            {
                //设备所属单位为条件
                sSql += " AND A.iUseDeptID = @iUseDeptID ";
            }
            if(filter.iRepairDeptID > 0)
            {
                //维护单位为条件
                sSql += " AND A.iRepairDeptID = @iRepairDeptID";
            }
            if (lm != null)
            {
                //最后更新时间为条件
                sSql += " AND A.lastModifyTime >= @LastModifyTime";
                filter.LastModifyTime = lm.Value;
            }
            else
            {
                sSql += " AND A.bIsDeleted = 0";
            }
            return DBHelper.Query<EHECD_Device>(sSql,filter);
        }

        /// <summary>
        /// 获取责任人发生更换的设备
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetDeviceListChangeClient(EHECD_Device filter, DateTime lm)
        {
            string sSql = @"
                    select B.ID,B.iClientID,1 as bIsDeleted
                    from EHECD_DeviceHis A
                    left join EHECD_Device B on A.iDeviceID = B.ID
                    where A.lastClientID = @iClientID AND B.iClientID <> @iClientID AND A.lastModifyTime >= @LastModifyTime
                ";
         
            //最后更新时间为条件
            filter.LastModifyTime = lm;
            return DBHelper.Query<EHECD_Device>(sSql, filter);
        }


        /// <summary>
        /// 根据消防部门获取设备
        /// </summary>
        /// <param name="iUnitID">消防部门ID</param>
        /// <param name="lm"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> ClientGetDeviceListByFireUnit(int iUnitID, DateTime? lm)
        {
            string sSql = @"
                    SELECT A.*, C.sName sClientName,B.sName sUnitName,D.sName sRepairDeptName,F.sName sOrganName,G.sName sDeviceTypeName,
                        (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = A.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
                    FROM EHECD_Device A
                    INNER JOIN EHECD_UNIT B ON A.iUseDeptID = B.ID 
                    LEFT JOIN EHECD_Client C ON C.ID = A.iClientID
                    LEFT JOIN EHECD_Unit D ON A.iRepairDeptID = D.ID
                    LEFT JOIN EHECD_ClientDeptRel E ON E.iClientID = A.iClientID AND E.iUnitID = A.iUseDeptID AND E.bIsDeleted = 0 AND E.iAuditState = 1 
                    LEFT JOIN EHECD_Dept F ON F.ID = E.iOrganID AND F.bIsDeleted = 0 
                    LEFT JOIN EHECD_DeviceType G ON G.ID = A.iDeviceTypeID
                    WHERE B.iStatus = 0  AND B.iParentID = @iUnitID
                    ";

            if (lm != null)
            {
                sSql += " AND A.lastModifyTime >= @LastModifyTime";
                return DBHelper.Query<EHECD_Device>(sSql, new { iUnitID = iUnitID , LastModifyTime  = lm.Value});
            }
            else
            {
                sSql += " AND A.bIsDeleted = 0" ;
                return   DBHelper.Query<EHECD_Device>(sSql, new { iUnitID = iUnitID });
            }
        }

        #endregion

        #region 判断设备是否存在

        public bool IsExist(EHECD_Device entity)
        {
            string sSql = @"SELECT * FROM EHECD_Device WHERE sNumber=@sNumber AND iUseDeptID = @iUseDeptID AND bIsDeleted = 0";
            List<EHECD_Device> list = DBHelper.Query<EHECD_Device>(sSql, entity).ToList();
            if(list.Count > 0 && entity.ID == 0)
            {
                return true;
            }
            else if(list.Count > 0 && entity.ID != list[0].ID)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 根据点检员ID获取设备数据

        /// <summary>
        /// 根据点检员ID获取设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetDevicesByClientIDList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT * FROM (
                        SELECT D.*, 
	                    ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName, 
                        (SELECT sName FROM EHECD_Client WHERE bIsDeleted = 0 AND ID = D.iClientID) sClientName,
                        (SELECT sName FROM EHECD_Unit WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iType = 0 AND ID = D.iUseDeptID) sUnitName,
                        ISNULL((SELECT sName FROM (
		                    SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
		                    WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
	                    ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName
                        FROM EHECD_Device D INNER JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0 AND U.iStatus = 0 AND D.iClientID = {0}
                    ) T WHERE 1 = 1
                ", Convert.ToInt32(param.condition["iClientID"]));

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sNumber Like '%{0}%' OR T.sName Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }

            param.sort = "T.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Device>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
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
            return DBHelper.QuerySingle<EHECD_Device>(iDeviceID);
        }

        #endregion

        #region 获取设备详情

        /// <summary>
        /// 获取设备详情
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <returns></returns>
        public EHECD_Device GetInfo(long iDeviceID)
        {
            string sSql = string.Format(@"
                    SELECT D.ID, D.sNumber, D.iDeviceTypeID, D.iUseDeptID, D.iRepairDeptID, D.iClientID, D.sDeviceIDs,
                    ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName, 
                    D.iNumber, D.sName, D.sLocation, D.sModel, D.sSpec, D.sInstallDate, D.sManufacturer, D.sQRCode, 
                    (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName, 
                    (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName, 
                    ISNULL((SELECT sName FROM (
                        SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                        WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
                    ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
                    (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName, 
                    (SELECT sPhone FROM EHECD_Client WHERE ID = D.iClientID) sClientPhone, 
                    sProductionDate, iExpiredYears, iForciblyScrappedYears, iAbnormalStatus,
                    (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 0 ORDER BY dCreateTime DESC) sLastInspectionDate,
                    (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate,
                    (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 2 ORDER BY dCreateTime DESC) sLastChangeDate
                    FROM EHECD_Device D LEFT JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0 AND D.ID = {0}
                ", iDeviceID);
            return DBHelper.QuerySingle<EHECD_Device>(sSql);
        }

        #endregion

        #region 添加设备

        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Int32 Insert(EHECD_Device entity)
        {
            StringBuilder sParam = new StringBuilder();
            entity.LastModifyTime = DateTime.Now;
            if (!string.IsNullOrEmpty(entity.sDeviceIDs))
            {
                var sRelDeviceIDParam = entity.sDeviceIDs.Split(',');

                foreach (var deviceId in sRelDeviceIDParam)
                {
                    sParam.Append(string.Format(@"
                        IF NOT EXISTS (SELECT * FROM EHECD_DeviceRel WHERE iDeviceID = @deviceID AND iRelDeviceID = {0} AND bIsDeleted = 0)
                        BEGIN
                            INSERT INTO EHECD_DeviceRel (iDeviceID, iRelDeviceID) VALUES (@deviceID, {0})
                        END;", deviceId));
                }
            }

            entity.sDeviceIDs = entity.sDeviceIDs == null ? "" : entity.sDeviceIDs;
            string sSql = @"
                    DECLARE @deviceID BIGINT
                    If Not Exists(Select * From EHECD_Device Where sNumber = @sNumber And iUseDeptID = @iUseDeptID And bIsDeleted = 0) 
                    BEGIN
                        INSERT INTO EHECD_Device
                        (sNumber, iDeviceTypeID, iNumber, sName, sLocation, sModel, sSpec, 
                        sInstallDate, sManufacturer, iRepairDeptID, iUseDeptID, iClientID, 
                        sProductionDate, iExpiredYears, iForciblyScrappedYears, iAbnormalStatus, sDeviceIDs, iCreateUnitID,LastModifyTime)
                        VALUES
                        (@sNumber, @iDeviceTypeID, @iNumber, @sName, @sLocation, @sModel, @sSpec, 
                        @sInstallDate, @sManufacturer, @iRepairDeptID, @iUseDeptID, @iClientID, 
                        @sProductionDate, @iExpiredYears, @iForciblyScrappedYears, @iAbnormalStatus, @sDeviceIDs, @iCreateUnitID,@LastModifyTime);
                        Select @deviceID = @@Identity;" + sParam + @"
                        Select @deviceID;
                    END
                    ELSE
                        Select -1
                ";
            return Convert.ToInt32(DBHelper.ExecuteScalar(sSql, entity));
        }
		
		#endregion
		
		#region 编辑设备
		
        /// <summary>
        /// 编辑设备
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_Device entity)
        {
            StringBuilder sParam = new StringBuilder();
            entity.LastModifyTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(entity.sDeviceIDs))
            {
                var sRelDeviceIDParam = entity.sDeviceIDs.Split(',');

                foreach (var deviceId in sRelDeviceIDParam)
                {
                    sParam.Append(string.Format(@"
                        IF NOT EXISTS (SELECT * FROM EHECD_DeviceRel WHERE iDeviceID = {1} AND iRelDeviceID = {0} AND bIsDeleted = 0)
                        BEGIN
                            INSERT INTO EHECD_DeviceRel (iDeviceID, iRelDeviceID) VALUES ({1}, {0})
                        END;", deviceId, entity.ID));
                }
            }

            string sSql = @"
                    BEGIN TRY
                        BEGIN TRAN
                            UPDATE EHECD_DeviceRel SET bIsDeleted = 1 WHERE iDeviceID = @ID;
                            Update EHECD_Device Set sNumber = @sNumber, iDeviceTypeID = @iDeviceTypeID, sName = @sName, sLocation = @sLocation, sModel = @sModel, iNumber = @iNumber,
                            sSpec = @sSpec, sInstallDate = @sInstallDate, sManufacturer = @sManufacturer, iUseDeptID = @iUseDeptID, sDeviceIDs = @sDeviceIDs, iRepairDeptID = @iRepairDeptID, iClientID = @iClientID,
                            sProductionDate = @sProductionDate, iExpiredYears = @iExpiredYears, iForciblyScrappedYears = @iForciblyScrappedYears,LastModifyTime = @LastModifyTime Where ID = @ID;
                    " + sParam + @"
                        COMMIT TRANSACTION
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRANSACTION
                    END CATCH";

            return DBHelper.Execute(sSql, entity) > 0;
		}

        #endregion

        #region 获取当前单位某个编号的设备

        /// <summary>
        /// 获取当前单位某个编号的设备
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetDeviceCountByNumber(EHECD_Device entity)
        {
            return DBHelper.QuerySingle<int>("Select COUNT(0) From EHECD_Device Where sNumber = @sNumber And iUseDeptID = @iUseDeptID And bIsDeleted = 0 AND ID != @ID", entity);
        }

        #endregion

        #region 更新设备二维码

        /// <summary>
        /// 更新设备二维码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateQRCode(EHECD_Device entity)
        {
            string sSql =
                @"Update [EHECD_Device] Set 
				[sQRCode]=@sQRCode
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
        }

        #endregion

        #region 批量删除设备

        /// <summary>
        /// 批量删除设备
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
            string sSql =
                @"Update EHECD_Device Set bIsDeleted=1,lastModifyTime = @LastModifyTime Where ID In (" + sIds + ")";
            return DBHelper.Execute(sSql, new EHECD_Device { LastModifyTime = DateTime.Now }) > 0;
        }

        #endregion

        #region 根据设备ID获取关联设备数据

        /// <summary>
        /// 根据设备ID获取关联设备数据
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetRelDeviceListByID(long iDeviceID)
        {
            return DBHelper.Query<EHECD_Device>(string.Format(@"
                    DECLARE @CHAR VARCHAR(5000)
                    SELECT @CHAR = sDeviceIDs FROM EHECD_DEVICE WHERE ID = {0};
                    IF(@CHAR = '')
                    BEGIN
	                    EXEC('SELECT D.*,(SELECT sName FROM EHECD_DEVICETYPE WHERE ID = D.iDeviceTypeID) sDeviceTypeName FROM EHECD_DEVICE D WHERE D.bIsDeleted = 0 AND D.ID IN (0)')
                    END
                    ELSE
	                    EXEC('SELECT D.*,(SELECT sName FROM EHECD_DEVICETYPE WHERE ID = D.iDeviceTypeID) sDeviceTypeName FROM EHECD_DEVICE D WHERE D.bIsDeleted = 0 AND D.ID IN ('+@CHAR+')')
                ", iDeviceID));
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
            string sSql = @"
                SELECT COUNT(0) iNumber FROM (
	                SELECT D.*, 
	                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID AND bIsDeleted = 0 AND iStatus = 0) sClientName
	                FROM EHECD_Device D INNER JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0
                ) T WHERE 1 = 1
            ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }

            return DBHelper.QuerySingle<int>(sSql + sCondition);
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
            string sSql = string.Format(@"
                SELECT COUNT(0) FROM (
	                SELECT A.* FROM (
		                SELECT D.*, 
		                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID AND iStatus = 0 AND bIsDeleted = 0) sClientName 
                        FROM EHECD_Device D WHERE D.bIsDeleted = 0
	                ) A 
	                INNER JOIN EHECD_UNIT U ON A.iUseDeptID = U.ID AND U.iStatus = 0 AND U.bIsDeleted = 0 AND U.iParentID = {0}
                ) T
                WHERE 1 = 1
            ", param.condition["iUnitID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }

            return DBHelper.QuerySingle<int>(sSql + sCondition);
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
            string sSql = string.Format(@"
                SELECT COUNT(0) FROM (
	                SELECT A.* FROM (
		                SELECT D.*, 
		                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID AND iStatus = 0 AND bIsDeleted = 0) sClientName 
                        FROM EHECD_Device D WHERE D.bIsDeleted = 0
	                ) A INNER JOIN EHECD_UNIT U ON A.iUseDeptID = U.ID AND U.iStatus = 0 AND U.bIsDeleted = 0 WHERE A.iRepairDeptID = {0} AND A.iUseDeptID IN (
		                SELECT S.iUseDeptID FROM EHECD_UseDeptSettings S INNER JOIN EHECD_UseDeptSettingDetail D ON S.ID = D.iUseDeptSettingsID WHERE D.iRepairDeptID = {0}
	                )
                ) T WHERE 1 = 1
            ", param.condition["iUnitID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }

            return DBHelper.QuerySingle<int>(sSql + sCondition);
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
            string sSql = @"
                SELECT * FROM (
	                SELECT D.*, 
	                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
	                ISNULL((SELECT sName FROM (
		                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                        WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
	                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
	                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
	                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
	                FROM EHECD_Device D INNER JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0
                ) T WHERE 1 = 1
            ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }

            sCondition.Append(" ORDER BY T.ID DESC");
            return DBHelper.Query<EHECD_Device>(sSql + sCondition);
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
            string sSql = @"
                SELECT * FROM (
	                SELECT D.*, 
	                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
	                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
	                ISNULL((SELECT sName FROM (
		                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                        WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
	                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
	                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
	                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
	                FROM EHECD_Device D INNER JOIN EHECD_UNIT U ON D.iUseDeptID = U.ID WHERE D.bIsDeleted = 0 AND U.iStatus = 0
                ) T WHERE 1 = 1
            ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }

            sCondition.Append(" ORDER BY T.ID DESC");
            return DBHelper.Query<EHECD_Device>(sSql + sCondition);
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
            string sSql = string.Format(@"
                SELECT * FROM (
	                SELECT A.* FROM (
		                SELECT D.*, 
		                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
		                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
		                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
		                ISNULL((SELECT sName FROM (
			                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                            WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
		                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
		                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
		                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
		                FROM EHECD_Device D WHERE D.bIsDeleted = 0
	                ) A 
	                INNER JOIN EHECD_UNIT U ON A.iUseDeptID = U.ID AND U.bIsDeleted = 0 AND U.iStatus = 0 AND U.iParentID = {0}
                ) T WHERE 1 = 1
            ", param.condition["iUnitID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }

            return DBHelper.Query<EHECD_Device>(sSql + sCondition);
        }

        #endregion

        #region 获取要导出的维护部分设备信息

        /// <summary>
        /// 获取要导出的维护部分设备信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetExportPartRepairData(QueryParams param)
        {
            string sSql = string.Format(@"
                SELECT * FROM (
	                SELECT A.* FROM (
		                SELECT D.*, 
		                (SELECT sName FROM EHECD_Client WHERE ID = D.iClientID) sClientName,
		                (SELECT sName FROM EHECD_Unit WHERE ID = D.iUseDeptID) sUnitName,
		                (SELECT sName FROM EHECD_Unit WHERE ID = D.iRepairDeptID) sRepairDeptName,
		                ISNULL((SELECT sName FROM (
			                SELECT TOP 1 R.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R ON C.ID = R.iClientID 
                            WHERE C.ID = D.iClientID AND R.iUnitID = D.iUseDeptID AND R.bIsDeleted = 0 AND R.iStatus = 0 AND R.iAuditState = 1
		                ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
		                ISNULL((SELECT sName FROM EHECD_DeviceType WHERE ID = D.iDeviceTypeID), '') sDeviceTypeName,
		                (SELECT TOP 1 CONVERT(VARCHAR(20), dCreateTime, 120) FROM EHECD_InspectionRecord WHERE iDeviceID = D.ID AND iRecordType = 1 ORDER BY dCreateTime DESC) sLastRepairDate
		                FROM EHECD_Device D WHERE D.bIsDeleted = 0
	                ) A INNER JOIN EHECD_UNIT U ON A.iUseDeptID = U.ID AND U.iStatus = 0 AND U.bIsDeleted = 0 WHERE A.iRepairDeptID = {0} AND A.iUseDeptID IN (
		                SELECT S.iUseDeptID FROM EHECD_UseDeptSettings S INNER JOIN EHECD_UseDeptSettingDetail D ON S.ID = D.iUseDeptSettingsID WHERE D.iRepairDeptID = {0}
	                )
                ) T WHERE 1 = 1
            ", param.condition["iUnitID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sNumber Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAbnormalStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iAbnormalStatus = {0}", param.condition["iAbnormalStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sInstallDate <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dExpStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dExpEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iExpiredYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dExpEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) >= CONVERT(varchar(20),'{0} 00:00:00', 120)) ", param.condition["dFocStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dFocEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sProductionDate != '' AND DATEADD(YEAR, T.iForciblyScrappedYears, T.sProductionDate) <= CONVERT(varchar(20),'{0} 23:59:59', 120)) ", param.condition["dFocEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientName Like '%{0}%'", param.condition["sClientName"]));
            }

            return DBHelper.Query<EHECD_Device>(sSql + sCondition);
        }

        #endregion

        #region 获取所有设备信息

        /// <summary>
        /// 获取所有设备信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Device> GetAllList()
        {
            return DBHelper.Query<EHECD_Device>("SELECT * FROM EHECD_Device WHERE bIsDeleted = 0");
        }

        #endregion

        #region 导入设备信息

        /// <summary>
        /// 导入设备信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Import(EHECD_Device entity)
        {
            entity.LastModifyTime = DateTime.Now;
            string sSql = @"
                    IF NOT EXISTS(SELECT * FROM EHECD_Device WHERE sNumber = @sNumber AND iUseDeptID = @iUseDeptID AND bIsDeleted = 0) 
                    BEGIN
                        INSERT INTO EHECD_Device (sNumber, iDeviceTypeID, iNumber, sName, 
                        sLocation, sModel, sSpec, sInstallDate, sManufacturer, sQRCode, iCreateUnitID,
                        iRepairDeptID, iUseDeptID, iClientID, sProductionDate, iExpiredYears, iForciblyScrappedYears, LastModifyTime)
                        VALUES (@sNumber, @iDeviceTypeID, @iNumber, @sName, 
                        @sLocation, @sModel, @sSpec, @sInstallDate, @sManufacturer, @sQRCode, @iCreateUnitID,
                        @iRepairDeptID, @iUseDeptID, @iClientID, @sProductionDate, @iExpiredYears, @iForciblyScrappedYears,@LastModifyTime);
                        SELECT @@Identity;
                    END
                    ELSE
                        SELECT -1
                ";

            return TConvert.toInt32(DBHelper.ExecuteScalar(sSql, entity));
        }

        #endregion

        #region 设备更改责任人

        /// <summary>
        /// 设备更改责任人
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public bool ChangeClient(long iDeviceID, int iClientID)
        {
            string sSql = @"Update EHECD_Device Set iClientID = @iClientID,LastModifyTime = @LastModifyTime Where ID = @ID";
            EHECD_Device entity = new EHECD_Device { ID = iDeviceID, iClientID = iClientID, LastModifyTime = DateTime.Now };
            return DBHelper.Execute(sSql,entity) > 0;
        }

        /// <summary>
        /// 设备更改责任人历史记录
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public bool ChangeClientHis(long iDeviceID, long newClientID,long lastClientID)
        {
            string sSql = @"INSERT INTO EHECD_DeviceHIS(iDeviceID,lastClientID,newClientID,lastModifyTime) values(@iDeviceID,@lastClientID,@newClientID,@lastModifyTime)";
            EHECD_DeviceHis hist = new EHECD_DeviceHis
            {
                iDeviceID = iDeviceID,
                lastClientID = lastClientID,
                LastModifyTime = DateTime.Now,
                newClientID = newClientID
            };
            return DBHelper.Execute(sSql, hist) > 0;
        }

        #endregion

        #region 设备更改维护公司

        /// <summary>
        /// 设备更改维护公司
        /// </summary>
        /// <param name="sIds"></param>
        /// <param name="iRepairDeptID"></param>
        /// <returns></returns>
        public bool ChangeRepair(string sIds, int iRepairDeptID)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
            string sSql = @"Update EHECD_Device Set iRepairDeptID = @iRepairDeptID ,LastModifyTime = @LastModifyTime, lastiRepairDeptID = iRepairDeptID  Where ID In (" + sIds + ")";
            EHECD_Device entity = new EHECD_Device { iRepairDeptID = iRepairDeptID, LastModifyTime = DateTime.Now };
            return DBHelper.Execute(sSql, entity) > 0;
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
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";

            return DBHelper.Query<EHECD_Device>(string.Format("SELECT * FROM EHECD_Device WHERE bIsDeleted = 0 AND ID IN ({0})", sIds));
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
            return DBHelper.Query<EHECD_Device>(string.Format(@"
                    SELECT * FROM EHECD_Device WHERE bIsDeleted = 0 AND iUseDeptID = {0}
                ", iUseDeptID));
        }

        #endregion

        #region 根据点检员ID查询负责设备数据

        /// <summary>
        /// 根据点检员ID查询负责设备数据
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public int GetDeviceCountByClientID(int iClientID)
        {
            return DBHelper.QuerySingle<int>(string.Format("SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iClientID = {0}", iClientID));
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
            EHECD_Device device = DBHelper.QuerySingle<EHECD_Device>(string.Format("SELECT TOP 1 * FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iCreateUnitID = {0} ORDER BY dCreateTime DESC", iUnitID));
            List<EHECD_Device> relatedDevice = DBHelper.Query<EHECD_Device>(string.Format(@" select b.* from EHECD_DeviceRel a
                    inner join EHECD_Device b on a.iRelDeviceID = b.ID
                    where a.iDeviceID = '{0}'", device.ID)).ToList();
            device.DeviceList = relatedDevice;
            return device;
        }

        #endregion

        #region 根据点检员ID获取最近一条添加的设备

        /// <summary>
        /// 根据点检员ID获取最近一条添加的设备
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Device GetLatelyDeviceByClientID(int iClientID)
        {
            return DBHelper.QuerySingle<EHECD_Device>(string.Format("SELECT TOP 1 * FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iClientID = {0} ORDER BY dCreateTime DESC", iClientID));
        }

        #endregion
    }
}