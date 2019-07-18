				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 巡检明细操作
    /// </summary>
    public class InspectionDetailDao
    {
        static InspectionDetailDao instance = new InspectionDetailDao();

        private InspectionDetailDao()
        {
        }
        public static InspectionDetailDao Instance
        {
            get
            {
                return instance;
            }
        }

        #region 获取巡检明细信息

        /// <summary>
        /// 获取巡检明细信息
        /// </summary>
        /// <param name="iInspectionRecordID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionDetail> GetDetailList(long iInspectionRecordID)
        {
            string sSql = string.Format(@"
                    Select *, 
                    (Select sName From EHECD_DeviceParam Where ID = D.iDeviceParamID) sName
                    From EHECD_InspectionDetail D Where D.iInspectionRecordID = {0}
                ", iInspectionRecordID);

            return DBHelper.Query<EHECD_InspectionDetail>(sSql);
        }
		
		#endregion

        #region 获取巡检明细信息

        /// <summary>
        /// 获取巡检明细信息
        /// </summary>
        /// <param name="iDeviceID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_InspectionDetail> GetLastDetailList(long iDeviceID)
        {
            string sSql = string.Format(@"
                    Select *, 
                    (Select sName From EHECD_DeviceParam Where ID = D.iDeviceParamID) sName
                    From EHECD_InspectionDetail D Where D.iInspectionRecordID = (
                        SELECT TOP 1 ID FROM EHECD_InspectionRecord WHERE iDeviceID = {0} ORDER BY dCreateTime DESC
                    )
                ", iDeviceID);

            return DBHelper.Query<EHECD_InspectionDetail>(sSql);
        }
		
		#endregion
		
		#region 添加巡检明细

        /// <summary>
        /// 添加巡检明细
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_InspectionDetail entity)
        {
            return DBHelper.Insert<EHECD_InspectionDetail>(entity) > 0;
        }
		
		#endregion
    }
}
