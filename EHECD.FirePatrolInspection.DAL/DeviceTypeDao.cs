				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 设备分类操作
    /// </summary>
    public class DeviceTypeDao
    {
        static DeviceTypeDao instance = new DeviceTypeDao();

        private DeviceTypeDao()
        {
        }
        public static DeviceTypeDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取设备分类信息
		
        /// <summary>
        /// 获取设备分类信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DeviceType> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
                        SELECT DT.*,
                        (SELECT sName FROM EHECD_Unit WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID = DT.iUseDeptID) sUnitName
                        FROM EHECD_DeviceType DT WHERE DT.bIsDeleted = 0
                    ) T WHERE 1 = 1
                ";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And T.sName Like '%{0}%'", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUnitDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And (T.iUseDeptID = {0} OR T.iUseDeptID = 0)", param.condition["iUnitDeptID"]));
            }

            param.sort = "T.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_DeviceType>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取所有设备分类

        /// <summary>
        /// 获取所有设备分类
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_DeviceType> GetAllList()
        {
            return DBHelper.Query<EHECD_DeviceType>(@"Select a.*,case when b.sName IS null then '总平台' else b.sName end sUnitName From EHECD_DeviceType a
                left join EHECD_unit b on a.iUseDeptID = b.ID
                Where a.bIsDeleted = 0");
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
            return DBHelper.QuerySingle<EHECD_DeviceType>(iDeviceTypeID);
        }
		
		#endregion
		
		#region 添加设备分类

        /// <summary>
        /// 添加设备分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_DeviceType entity)
        {
            return DBHelper.Execute("INSERT INTO EHECD_DeviceType (iUseDeptID, sName) VALUES (@iUseDeptID, @sName)", entity) > 0;
        }
		
		#endregion
		
		#region 编辑设备分类
		
        /// <summary>
        /// 编辑设备分类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_DeviceType entity)
        {
			string sSql =
                @"Update [EHECD_DeviceType] Set 
				[sName]=@sName
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除设备分类

        /// <summary>
        /// 批量删除设备分类
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_DeviceType Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 获取点检员关联单位创建的设备分类

        /// <summary>
        /// 获取点检员关联单位创建的设备分类
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DeviceType> GetRelDeviceTypeByClientID(int iClientID)
        {
            return DBHelper.Query<EHECD_DeviceType>(string.Format(@"
                    SELECT * FROM EHECD_DEVICETYPE WHERE bIsDeleted = 0 AND (iUseDeptID = 0 OR iUseDeptID IN (
	                    SELECT iUnitID FROM EHECD_ClientDeptRel WHERE iClientID = {0} AND iAuditState = 1 AND bIsDeleted = 0 AND iStatus = 0
                    ))
                ", iClientID));
        }

        #endregion

        #region 获取使用单位或平台创建的设备分类

        /// <summary>
        /// 获取使用单位或平台创建的设备分类
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DeviceType> GetDeviceTypeByUnitID(int iUnitID)
        {
            return DBHelper.Query<EHECD_DeviceType>(string.Format(@"
                    SELECT * FROM EHECD_DEVICETYPE WHERE bIsDeleted = 0 AND (iUseDeptID = 0 OR iUseDeptID = {0})
                ", iUnitID));
        }

        #endregion
    }
}