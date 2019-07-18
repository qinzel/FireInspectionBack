				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 值班记录操作
    /// </summary>
    public class DutyRecordDao
    {
        static DutyRecordDao instance = new DutyRecordDao();

        private DutyRecordDao()
        {
        }
        public static DutyRecordDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取值班记录信息
		
        /// <summary>
        /// 获取值班记录信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DutyRecord> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
                        SELECT DR.*, 
                        (SELECT sPhone FROM EHECD_Client WHERE ID = DR.iClientID) sPhone,
                        (SELECT sName FROM EHECD_Client WHERE ID = DR.iClientID) sClientName,
                        (SELECT sName FROM EHECD_DutyRoom WHERE ID = DR.iDutyRoomID) sDutyRoomName,
                        (SELECT U.sName FROM EHECD_Client C INNER JOIN EHECD_Unit U ON C.iUnitID = U.ID WHERE C.bIsDeleted = 0 AND U.bIsDeleted = 0 AND C.iStatus = 0 AND C.ID = DR.iClientID) sUnitName,
                        (SELECT U.ID FROM EHECD_Client C INNER JOIN EHECD_Unit U ON C.iUnitID = U.ID WHERE C.bIsDeleted = 0 AND U.bIsDeleted = 0 AND C.iStatus = 0 AND C.ID = DR.iClientID) iUseDeptID
                        FROM EHECD_DutyRecord DR
                    ) T WHERE 1 = 1
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sClientName Like '%{0}%' OR T.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDutyRoomID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDutyRoomID = {0}", param.condition["iDutyRoomID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dSStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sStartTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dSStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dSEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sStartTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dSEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sEndTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dEStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sEndTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iClientID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iClientID = {0}", param.condition["iClientID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sStartTime >= CONVERT(varchar(20),'{0}', 120) ", param.condition["sStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sEndTime <= CONVERT(varchar(20),'{0}', 120) ", param.condition["sEndTime"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_DutyRecord>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取值班记录详情

        /// <summary>
        /// 获取值班记录详情
        /// </summary>
        /// <param name="iDutyRecordID"></param>
        /// <returns></returns>
        public EHECD_DutyRecord Get(long iDutyRecordID)
        {
            return DBHelper.QuerySingle<EHECD_DutyRecord>(iDutyRecordID);
        }
		
		#endregion
		
		#region 添加值班记录

        /// <summary>
        /// 添加值班记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_DutyRecord entity)
        {
            return DBHelper.Execute(@"
                    INSERT INTO EHECD_DutyRecord
                    (iClientID, iDutyRoomID, sStartTime, sEndTime, iTimeLength, sDesc, sImageSrc)
                    VALUES (@iClientID, @iDutyRoomID, CONVERT(VARCHAR(20), GETDATE(), 120), '', 0, '', '')
                ", entity) > 0;
        }

        #endregion

        #region 获取未签退记录

        /// <summary>
        /// 获取未签退记录
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="iDutyRoomID"></param>
        /// <returns></returns>
        public EHECD_DutyRecord GetUnFinishRecord(int iClientID, int iDutyRoomID)
        {
            return DBHelper.QuerySingle<EHECD_DutyRecord>(string.Format("SELECT TOP 1 * FROM EHECD_DUTYRECORD WHERE iClientID = {0} AND iDutyRoomID = {1} AND sEndTime = '' ORDER BY dCreateTime DESC", iClientID, iDutyRoomID));
        }

        #endregion

        #region 编辑值班记录

        /// <summary>
        /// 编辑值班记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_DutyRecord entity)
        {
			string sSql =
                @"Update [EHECD_DutyRecord] Set 
				[sEndTime]= CONVERT(VARCHAR(20), GETDATE(), 120),
				[iTimeLength]=DATEDIFF(MINUTE, sStartTime, GETDATE()),
				[sDesc]=@sDesc,
				[sImageSrc]=@sImageSrc
                Where ID = @ID";

            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除值班记录

        /// <summary>
        /// 批量删除值班记录
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
            return DBHelper.Execute(string.Format("Delete EHECD_DutyRecord Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 获取要导出的值班记录

        /// <summary>
        /// 获取要导出的值班记录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DutyRecord> GetExportData(QueryParams param)
        {
            string sSql = @"
                    SELECT * FROM (
	                    SELECT DR.*, 
	                    (SELECT sPhone FROM EHECD_Client WHERE ID = DR.iClientID) sPhone,
	                    (SELECT sName FROM EHECD_Client WHERE ID = DR.iClientID) sClientName,
	                    (SELECT sName FROM EHECD_DutyRoom WHERE ID = DR.iDutyRoomID) sDutyRoomName,
	                    (SELECT U.sName FROM EHECD_DutyRoom R INNER JOIN EHECD_Unit U ON R.iUseDeptID = U.ID WHERE R.bIsDeleted = 0 AND U.bIsDeleted = 0 AND R.ID = DR.iDutyRoomID) sUnitName,
	                    (SELECT U.ID FROM EHECD_Client R INNER JOIN EHECD_Unit U ON R.iUnitID = U.ID WHERE R.bIsDeleted = 0 AND U.bIsDeleted = 0 AND R.ID = DR.iClientID) iUseDeptID
	                    FROM EHECD_DutyRecord DR
                    ) T WHERE 1 = 1
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sClientName Like '%{0}%' OR T.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDutyRoomID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDutyRoomID = {0}", param.condition["iDutyRoomID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dSStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sStartTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dSStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dSEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sStartTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dSEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sEndTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dEStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And T.sEndTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEEndTime"]));
            }

            sCondition.Append(" ORDER BY T.ID DESC");

            return DBHelper.Query<EHECD_DutyRecord>(sSql + sCondition);
        }

        #endregion

        #region 查询当前值班室该值班员是否签到签退状态

        /// <summary>
        /// 查询当前值班室该值班员是否签到签退状态
        /// </summary>
        /// <param name="iDutyRoomID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_DutyRecord GetDutyPeopleStatus(int iDutyRoomID, int iClientID)
        {
            return DBHelper.QuerySingle<EHECD_DutyRecord>(string.Format(@"
                    SELECT TOP 1 * FROM EHECD_DUTYRECORD 
                    WHERE iCLientID = {1} AND iDutyRoomID = {0}
                    ORDER BY dCreateTime DESC
                ", iDutyRoomID, iClientID));
        }

        #endregion
    }
}
