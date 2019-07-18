				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 设备指标操作
    /// </summary>
    public class DeviceParamDao
    {
        static DeviceParamDao instance = new DeviceParamDao();

        private DeviceParamDao()
        {
        }
        public static DeviceParamDao Instance
        {
            get
            {
                return instance;
            }
        }

        #region 获取设备指标信息

        /// <summary>
        /// 获取设备指标信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DeviceParam> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
                        SELECT DP.*,
                        (SELECT sName FROM EHECD_Unit WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID = DP.iUseDeptID) sUnitName,
                        (SELECT sName FROM EHECD_DeviceType WHERE bIsDeleted = 0 AND ID = DP.iDeviceTypeID) sDeviceTypeName
                        FROM EHECD_DeviceParam DP WHERE DP.bIsDeleted = 0
                    ) T WHERE 1 = 1
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sName Like '%{0}%'", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeviceTypeID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iDeviceTypeID = {0}", param.condition["iDeviceTypeID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUnitDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And (T.iUseDeptID = {0} OR T.iUseDeptID = 0)", param.condition["iUnitDeptID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_DeviceParam>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        /// <summary>
        /// 获取所有未删除的设备指标
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_DeviceParam> GetAll()
        {
            string sql = "SELECT * FROM EHECD_DeviceParam  WHERE bIsDeleted = 0";
            return DBHelper.Query<EHECD_DeviceParam>(sql) ;
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
            return DBHelper.QuerySingle<EHECD_DeviceParam>(iDeviceParamID);
        }

        #endregion

        #region 添加设备指标

        /// <summary>
        /// 添加设备指标
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_DeviceParam entity)
        {
            return DBHelper.Execute("INSERT INTO EHECD_DeviceParam (iUseDeptID, sName, iDeviceTypeID) VALUES (@iUseDeptID, @sName, @iDeviceTypeID)", entity) > 0;
        }

        #endregion

        #region 编辑设备指标

        /// <summary>
        /// 编辑设备指标
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_DeviceParam entity)
        {
            string sSql =
                @"Update [EHECD_DeviceParam] Set 
				[sName]=@sName,
				[iDeviceTypeID]=@iDeviceTypeID
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
        }

        #endregion

        #region 批量删除设备指标

        /// <summary>
        /// 批量删除设备指标
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";

            return DBHelper.Execute(string.Format("Update EHECD_DeviceParam Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 根据设备类别ID获取设备指标集合

        /// <summary>
        /// 根据设备类别ID获取设备指标集合
        /// </summary>
        /// <param name="iDeviceTypeID"></param>
        /// <param name="iUseDeptID"></param>
        /// <param name="iDeviceUseDeptID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DeviceParam> GetListByTypeID(int iDeviceTypeID, int iUseDeptID, int iDeviceUseDeptID)
        {
            return DBHelper.Query<EHECD_DeviceParam>(string.Format(@"
                    SELECT * FROM EHECD_DeviceParam WHERE bIsDeleted = 0 AND iDeviceTypeID = {0} AND (iUseDeptID = 0 OR iUseDeptID = {1} OR iUseDeptID = {2})
                ", iDeviceTypeID, iUseDeptID, iDeviceUseDeptID));
        }

        #endregion
    }
}
