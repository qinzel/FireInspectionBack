
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;
using System.Linq;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 巡检记录操作
    /// </summary>
    public class InspectionRecordDao
    {
        static InspectionRecordDao instance = new InspectionRecordDao();

        private InspectionRecordDao()
        {
        }
        public static InspectionRecordDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取巡检记录信息
		
        /// <summary>
        /// 获取巡检记录信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
                        SELECT R.*, 
                        (SELECT sName FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sName,
                        (SELECT sNumber FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sNumber,
                        (SELECT sLocation FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sLocation,
                        (SELECT DT.ID FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID) iDeviceTypeID,
                        ISNULL((SELECT DT.sName FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID), '') sDeviceTypeName,
                        (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sUnitName,
                        (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iUseDeptID,
                        (SELECT C.sName FROM EHECD_Client C INNER JOIN EHECD_DEVICE D ON C.ID = D.iClientID WHERE D.ID = R.iDeviceID) sClientPersonName,
                        ISNULL((SELECT sName FROM (
                            SELECT TOP 1 CR.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel CR ON C.ID = CR.iClientID 
                            INNER JOIN EHECD_DEVICE D ON D.iClientID = C.ID
                            WHERE D.ID = R.iDeviceID AND CR.iUnitID = D.iUseDeptID AND CR.iStatus = 0 AND CR.bIsDeleted = 0
                        ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
                        (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sRepairDeptName,
                        (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iRepairDeptID
                        FROM EHECD_InspectionRecord R INNER JOIN EHECD_DEVICE V ON R.iDeviceID = V.ID WHERE V.bIsDeleted = 0
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
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRecordType"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRecordType = {0}", param.condition["iRecordType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientPersonName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientPersonName Like '%{0}%'", param.condition["sClientPersonName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sOperator"))
            {
                sCondition.AppendFormat(string.Format(" And T.sOperator Like '%{0}%'", param.condition["sOperator"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceID = {0}", param.condition["iDeviceID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_InspectionRecord>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取部分巡检记录信息

        /// <summary>
        /// 获取部分巡检记录信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetPartList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT * FROM (
	                    SELECT A.* FROM (
		                    SELECT R.*, 
		                    (SELECT sName FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sName,
		                    (SELECT sNumber FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sNumber,
		                    (SELECT sLocation FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sLocation,
		                    (SELECT DT.ID FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID) iDeviceTypeID,
		                    ISNULL((SELECT DT.sName FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID), '') sDeviceTypeName,
		                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sUnitName,
		                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iUseDeptID,
                            (SELECT C.sName FROM EHECD_Client C INNER JOIN EHECD_DEVICE D ON C.ID = D.iClientID WHERE D.ID = R.iDeviceID) sClientPersonName,
                            ISNULL((SELECT sName FROM (
                                SELECT TOP 1 CR.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel CR ON C.ID = CR.iClientID 
                                INNER JOIN EHECD_DEVICE D ON D.iClientID = C.ID
                                WHERE D.ID = R.iDeviceID AND CR.iUnitID = D.iUseDeptID AND CR.iStatus = 0 AND CR.bIsDeleted = 0
                            ) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
		                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sRepairDeptName,
		                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iRepairDeptID
		                    FROM EHECD_InspectionRecord R INNER JOIN EHECD_DEVICE V ON R.iDeviceID = V.ID WHERE V.bIsDeleted = 0
	                    ) A INNER JOIN EHECD_UNIT U ON A.iUseDeptID = U.ID AND U.iParentID = {0}
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
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRecordType"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRecordType = {0}", param.condition["iRecordType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientPersonName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientPersonName Like '%{0}%'", param.condition["sClientPersonName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sOperator"))
            {
                sCondition.AppendFormat(string.Format(" And T.sOperator Like '%{0}%'", param.condition["sOperator"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceID = {0}", param.condition["iDeviceID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_InspectionRecord>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取部分维护巡检记录信息

        /// <summary>
        /// 获取部分维护巡检记录信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetPartRepairDeptList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT * FROM (
	                    SELECT A.* FROM (
		                    SELECT R.*, 
		                    (SELECT sName FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sName,
		                    (SELECT sNumber FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sNumber,
		                    (SELECT sLocation FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sLocation,
		                    (SELECT DT.ID FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID) iDeviceTypeID,
		                    ISNULL((SELECT DT.sName FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID), '') sDeviceTypeName,
		                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sUnitName,
		                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iUseDeptID,
		                    (SELECT C.sName FROM EHECD_Client C INNER JOIN EHECD_DEVICE D ON C.ID = D.iClientID WHERE D.ID = R.iDeviceID) sClientPersonName,
							ISNULL((SELECT sName FROM (
								SELECT TOP 1 CR.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel CR ON C.ID = CR.iClientID 
								INNER JOIN EHECD_DEVICE D ON D.iClientID = C.ID
								WHERE D.ID = R.iDeviceID AND CR.iUnitID = D.iUseDeptID AND CR.iStatus = 0 AND CR.bIsDeleted = 0
							) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
		                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sRepairDeptName,
		                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iRepairDeptID
		                    FROM EHECD_InspectionRecord R INNER JOIN EHECD_DEVICE V ON R.iDeviceID = V.ID WHERE V.bIsDeleted = 0
	                    ) A WHERE A.iRepairDeptID = {0} AND A.iUseDeptID IN (
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
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRecordType"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRecordType = {0}", param.condition["iRecordType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientPersonName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientPersonName Like '%{0}%'", param.condition["sClientPersonName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sOperator"))
            {
                sCondition.AppendFormat(string.Format(" And T.sOperator Like '%{0}%'", param.condition["sOperator"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceID = {0}", param.condition["iDeviceID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_InspectionRecord>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取巡检记录信息

        /// <summary>
        /// 获取巡检记录信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetAllTypeList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT * FROM (
	                    SELECT I.*, 
	                    (SELECT sNumber FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = I.iDeviceID) sNumber, 
	                    (SELECT sName FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = I.iDeviceID) sName
	                    FROM EHECD_InspectionRecord I Where 1 = 1 AND I.iDeviceID = {0}
                    ) T WHERE 1 = 1
                ", Convert.ToInt32(param.condition["iDeviceID"]));

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sOperator"))
            {
                sCondition.AppendFormat(string.Format(" And T.sOperator Like '%{0}%'", param.condition["sOperator"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEndTime"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_InspectionRecord>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取巡检记录信息
		
        /// <summary>
        /// 获取巡检记录信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionRecord> GetRecordList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT R.ID, R.iDeviceID, R.sDesc, R.dCreateTime, R.sOperator, R.iStatus,R.iRecordType
                    FROM EHECD_InspectionRecord R WHERE 1 = 1
                ";

            StringBuilder sCondition = new StringBuilder();

            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceID"))
            {
                sCondition.AppendFormat(string.Format(" And R.iDeviceID = {0}", param.condition["iDeviceID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRecordType"))
            {
                sCondition.AppendFormat(string.Format(" And R.iRecordType = {0}", param.condition["iRecordType"]));
            }

            param.sort = "R.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_InspectionRecord>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
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
            return DBHelper.QuerySingle<EHECD_InspectionRecord>(iInspectionRecordID);
        }

        #endregion

        #region 获取巡检记录详情

        /// <summary>
        /// 获取巡检记录详情
        /// </summary>
        /// <param name="iInspectionRecordID"></param>
        /// <returns></returns>
        public EHECD_InspectionRecord GetInfo(long iInspectionRecordID)
        {
            var record = DBHelper.QuerySingle<EHECD_InspectionRecord>(iInspectionRecordID);
            if (record != null)
            {
                record.DetailList = InspectionDetailDao.Instance.GetDetailList(iInspectionRecordID).ToList();
            }
            return record ?? new EHECD_InspectionRecord();
        }

        #endregion

        #region 获取设备最近一次巡检记录视图

        /// <summary>
        /// 获取设备最近一次巡检记录视图
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <returns></returns>
        public EHECD_InspectionRecord GetLastRecordInfo(long iDeviceID)
        {
            return DBHelper.QuerySingle<EHECD_InspectionRecord>(string.Format("SELECT TOP 1 * FROM EHECD_InspectionRecord WHERE iDeviceID = {0} ORDER BY dCreateTime DESC", iDeviceID));
        }

        #endregion

        #region 添加巡检记录

        /// <summary>
        /// 添加巡检记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertInsRecord(EHECD_InspectionRecord entity)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbDetail = new StringBuilder();
            var ary = new List<int>();

            foreach (EHECD_InspectionDetail item in entity.DetailList)
            {
                ary.Add(item.iStatus ? 1 : 0);
                sbDetail.Append("INSERT INTO EHECD_InspectionDetail (iInspectionRecordID, iDeviceParamID, iStatus) ")
                    .Append(string.Format("VALUES (@recID, {0}, {1}); ", item.iDeviceParamID, (item.iStatus ? 1 : 0)));
            }

            sb.Append("DECLARE @recID BIGINT ")
                .Append("BEGIN TRY ")
                .Append("BEGIN TRAN ")
                .Append("INSERT INTO EHECD_InspectionRecord (iDeviceID, iClientID, sDesc, iStatus, sOperator) ")
                .Append(string.Format("VALUES ({0}, {1}, '{2}', {3}, '{4}'); ", entity.iDeviceID, entity.iClientID, entity.sDesc, (ary.Contains(1) ? 1 : 0), entity.sOperator))
                .Append(string.Format("UPDATE EHECD_Device SET iAbnormalStatus = "+ (ary.Contains(1) ? 1 : 0) + " WHERE ID = {0}; ", entity.iDeviceID))
                .Append("SELECT @recID = @@IDENTITY; ");

            string sSql = @"
                COMMIT TRAN 
                END TRY 
                BEGIN CATCH 
                ROLLBACK TRANSACTION 
                END CATCH ";
            
            return DBHelper.Execute(sb.ToString() + sbDetail + sSql) > 0;
        }

        #endregion

        #region 批量添加巡检记录

        /// <summary>
        /// 批量添加巡检记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertInsRecords(List<EHECD_InspectionRecord> sDeviceList)
        {
            var sqlList = new List<string>();
            var i = 0;

            foreach (var entity in sDeviceList)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sbDetail = new StringBuilder();
                var ary = new List<int>();


                if(entity.DetailList != null)
                {
                    foreach (EHECD_InspectionDetail item in entity.DetailList)
                    {
                        ary.Add(item.iStatus ? 1 : 0);
                        sbDetail.Append("INSERT INTO EHECD_InspectionDetail (iInspectionRecordID, iDeviceParamID, iStatus) ")
                            .Append(string.Format("VALUES (@recID" + i + @", {0}, {1}); ", item.iDeviceParamID, (item.iStatus ? 1 : 0)));
                    }
                }

                sb.Append("DECLARE @recID" + i + @" BIGINT ")
                    .Append("BEGIN TRY ")
                    .Append("BEGIN TRAN ")
                    .Append("INSERT INTO EHECD_InspectionRecord (iDeviceID, iClientID, sDesc, iStatus, sOperator) ")
                    .Append(string.Format("VALUES ({0}, {1}, '{2}', {3}, '{4}'); ", entity.iDeviceID, entity.iClientID, entity.sDesc, (ary.Contains(1) ? 1 : 0), entity.sOperator))
                    .Append(string.Format("UPDATE EHECD_Device SET iAbnormalStatus = " + (ary.Contains(1) ? 1 : 0) + " WHERE ID = {0}; ", entity.iDeviceID))
                    .Append("SELECT @recID" + i + @" = @@IDENTITY; ");

                string sSql = @"
                    COMMIT TRAN 
                    END TRY 
                    BEGIN CATCH 
                    ROLLBACK TRANSACTION 
                    END CATCH ";

                sqlList.Add(sb.ToString() + sbDetail + sSql);
                i++;
            }
            
            return DBHelper.Execute(string.Join(";", sqlList)) > 0;
        }

        #endregion

        #region 添加维修记录

        /// <summary>
        /// 添加维修记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertRepairRecord(EHECD_InspectionRecord entity)
        {
            string sSql = string.Format(@"
                BEGIN TRY 
                    BEGIN TRAN 
                        INSERT INTO EHECD_InspectionRecord (iDeviceID, iClientID, sDesc, iRecordType, iSource, sOperator) VALUES ({0}, {1}, '{2}', {3}, {4}, '{5}');
                        UPDATE EHECD_Device SET iAbnormalStatus = 0 WHERE ID = {0};
                    COMMIT TRAN 
                END TRY 
                BEGIN CATCH 
                    ROLLBACK TRANSACTION 
                END CATCH ", entity.iDeviceID, entity.iClientID, entity.sDesc, 1, entity.iSource, entity.sOperator);

            return DBHelper.Execute(sSql) > 0;
        }

        #endregion

        #region 添加更换记录

        /// <summary>
        /// 添加更换记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertChangeRecord(EHECD_InspectionRecord entity)
        {
            string sSql = @"
                BEGIN TRY 
                    BEGIN TRAN 
                        INSERT INTO EHECD_InspectionRecord (iDeviceID, iRecordType, iClientID, sDesc, iNumber, sModel, sSpec, sInstallDate, sManufacturer, sProductionDate, iSource, sOperator) 
                        VALUES (@iDeviceID, @iRecordType, @iClientID, @sDesc, @iNumber, @sModel, @sSpec, @sInstallDate, @sManufacturer, @sProductionDate, @iSource, @sOperator);
                        UPDATE EHECD_Device SET iNumber = @iNumber, sModel = @sModel, sSpec = @sSpec, sInstallDate = @sInstallDate, 
                        sManufacturer = @sManufacturer, sProductionDate = @sProductionDate, iAbnormalStatus = 0 WHERE ID = @iDeviceID;
                    COMMIT TRAN 
                END TRY 
                BEGIN CATCH 
                    ROLLBACK TRANSACTION 
                END CATCH ";

            return DBHelper.Execute(sSql, entity) > 0;
        }

        #endregion

        #region 批量删除巡检记录

        /// <summary>
        /// 批量删除巡检记录
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
            return DBHelper.Execute(string.Format("Delete EHECD_InspectionRecord Where ID In ({0})", sIds)) > 0;
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
            string sSql = @"
                SELECT * FROM (
	                    SELECT R.*, 
	                    (SELECT sName FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sName,
	                    (SELECT sNumber FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sNumber,
	                    (SELECT sLocation FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sLocation,
	                    (SELECT DT.ID FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID) iDeviceTypeID,
	                    ISNULL((SELECT DT.sName FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID), '') sDeviceTypeName,
	                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sUnitName,
	                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iUseDeptID,
	                    (SELECT C.sName FROM EHECD_Client C INNER JOIN EHECD_DEVICE D ON C.ID = D.iClientID WHERE D.ID = R.iDeviceID) sClientPersonName,
						ISNULL((SELECT sName FROM (
							SELECT TOP 1 CR.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel CR ON C.ID = CR.iClientID 
							INNER JOIN EHECD_DEVICE D ON D.iClientID = C.ID
							WHERE D.ID = R.iDeviceID AND CR.iUnitID = D.iUseDeptID AND CR.iStatus = 0 AND CR.bIsDeleted = 0
						) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
	                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sRepairDeptName,
	                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iRepairDeptID
	                    FROM EHECD_InspectionRecord R INNER JOIN EHECD_DEVICE V ON R.iDeviceID = V.ID WHERE V.bIsDeleted = 0
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
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRecordType"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRecordType = {0}", param.condition["iRecordType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sOperator"))
            {
                sCondition.AppendFormat(string.Format(" And T.sOperator Like '%{0}%'", param.condition["sOperator"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientPersonName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientPersonName Like '%{0}%'", param.condition["sClientPersonName"]));
            }

            sCondition.Append(" ORDER BY T.ID DESC");

            return DBHelper.Query<EHECD_InspectionRecord>(sSql + sCondition);
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
            string sSql = string.Format(@"
                    SELECT * FROM (
	                    SELECT A.* FROM (
		                    SELECT R.*, 
		                    (SELECT sName FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sName,
		                    (SELECT sNumber FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sNumber,
		                    (SELECT sLocation FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sLocation,
		                    (SELECT DT.ID FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID) iDeviceTypeID,
		                    ISNULL((SELECT DT.sName FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID), '') sDeviceTypeName,
		                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sUnitName,
		                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iUseDeptID,
		                    (SELECT C.sName FROM EHECD_Client C INNER JOIN EHECD_DEVICE D ON C.ID = D.iClientID WHERE D.ID = R.iDeviceID) sClientPersonName,
							ISNULL((SELECT sName FROM (
								SELECT TOP 1 CR.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel CR ON C.ID = CR.iClientID 
								INNER JOIN EHECD_DEVICE D ON D.iClientID = C.ID
								WHERE D.ID = R.iDeviceID AND CR.iUnitID = D.iUseDeptID AND CR.iStatus = 0 AND CR.bIsDeleted = 0
							) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
		                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sRepairDeptName,
		                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iRepairDeptID
		                    FROM EHECD_InspectionRecord R INNER JOIN EHECD_DEVICE V ON R.iDeviceID = V.ID WHERE V.bIsDeleted = 0
	                    ) A INNER JOIN EHECD_UNIT U ON A.iUseDeptID = U.ID AND U.iParentID = {0}
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
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRecordType"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRecordType = {0}", param.condition["iRecordType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sOperator"))
            {
                sCondition.AppendFormat(string.Format(" And T.sOperator Like '%{0}%'", param.condition["sOperator"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientPersonName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientPersonName Like '%{0}%'", param.condition["sClientPersonName"]));
            }

            sCondition.Append(" ORDER BY T.ID DESC");

            return DBHelper.Query<EHECD_InspectionRecord>(sSql + sCondition);
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
            string sSql = string.Format(@"
                    SELECT * FROM (
	                    SELECT A.* FROM (
		                    SELECT R.*, 
		                    (SELECT sName FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sName,
		                    (SELECT sNumber FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sNumber,
		                    (SELECT sLocation FROM EHECD_Device WHERE bIsDeleted = 0 AND ID = R.iDeviceID) sLocation,
		                    (SELECT DT.ID FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID) iDeviceTypeID,
		                    ISNULL((SELECT DT.sName FROM EHECD_DeviceType DT INNER JOIN EHECD_Device D ON D.iDeviceTypeID = DT.ID WHERE D.bIsDeleted = 0 AND DT.bIsDeleted = 0 AND D.ID = R.iDeviceID), '') sDeviceTypeName,
		                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sUnitName,
		                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iUseDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iUseDeptID,
		                    (SELECT C.sName FROM EHECD_Client C INNER JOIN EHECD_DEVICE D ON C.ID = D.iClientID WHERE D.ID = R.iDeviceID) sClientPersonName,
							ISNULL((SELECT sName FROM (
								SELECT TOP 1 CR.iOrganID FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel CR ON C.ID = CR.iClientID 
								INNER JOIN EHECD_DEVICE D ON D.iClientID = C.ID
								WHERE D.ID = R.iDeviceID AND CR.iUnitID = D.iUseDeptID AND CR.iStatus = 0 AND CR.bIsDeleted = 0
							) B INNER JOIN EHECD_Dept DT ON B.iOrganID = DT.ID), '') sOrganName,
		                    (SELECT U.sName FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) sRepairDeptName,
		                    (SELECT U.ID FROM EHECD_Unit U INNER JOIN EHECD_Device D ON U.ID = D.iRepairDeptID WHERE D.bIsDeleted = 0 AND D.ID = R.iDeviceID) iRepairDeptID
		                    FROM EHECD_InspectionRecord R INNER JOIN EHECD_DEVICE V ON R.iDeviceID = V.ID WHERE V.bIsDeleted = 0
	                    ) A WHERE A.iRepairDeptID = {0} AND A.iUseDeptID IN (
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
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRecordType"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRecordType = {0}", param.condition["iRecordType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And T.iStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.dCreateTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iRepairDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iRepairDeptID = {0}", param.condition["iRepairDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sOperator"))
            {
                sCondition.AppendFormat(string.Format(" And T.sOperator Like '%{0}%'", param.condition["sOperator"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sClientPersonName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sClientPersonName Like '%{0}%'", param.condition["sClientPersonName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceID = {0}", param.condition["iDeviceID"]));
            }

            sCondition.Append(" ORDER BY T.ID DESC");

            return DBHelper.Query<EHECD_InspectionRecord>(sSql + sCondition);
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
            return DBHelper.Query<EHECD_InspectionRecord>(string.Format(@"
                    SELECT * FROM (
	                    SELECT R.*, 
	                    (SELECT iUseDeptID FROM EHECD_Device WHERE ID = R.iDeviceID AND bIsDeleted = 0) iUseDeptID
	                    FROM EHECD_InspectionRecord R
                    ) T WHERE T.iRecordType = 0 AND T.iUseDeptID = {0} AND T.dCreateTime >= '{1}-01-01 00:00:00' AND T.dCreateTime <= '{1}-12-31 23:59:59'
                ", iUseDeptID, sYearNumber));
        }

        #endregion
    }
}