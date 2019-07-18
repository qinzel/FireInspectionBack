				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 值班室操作
    /// </summary>
    public class DutyRoomDao
    {
        static DutyRoomDao instance = new DutyRoomDao();

        private DutyRoomDao()
        {
        }
        public static DutyRoomDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取值班室信息
		
        /// <summary>
        /// 获取值班室信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DutyRoom> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    Select R.*, 
                    (SELECT sName FROM EHECD_Unit WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID = R.iUseDeptID) sUnitName
                    From EHECD_DutyRoom R Where R.bIsDeleted = 0
                ";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And R.sName Like '%{0}%'", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And R.iUseDeptID = {0}", param.condition["iUseDeptID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_DutyRoom>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取值班室详情

        /// <summary>
        /// 获取值班室详情
        /// </summary>
        /// <param name="iDutyRoomID"></param>
        /// <returns></returns>
        public EHECD_DutyRoom Get(int iDutyRoomID)
        {
            return DBHelper.QuerySingle<EHECD_DutyRoom>(iDutyRoomID);
        }

        #endregion

        #region 添加值班室

        /// <summary>
        /// 添加值班室
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(EHECD_DutyRoom entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("If Not Exists(Select * From EHECD_DutyRoom Where sName = @sName AND iUseDeptID = @iUseDeptID And bIsDeleted = 0) ")
                .Append("Begin ")
                .Append("Insert Into EHECD_DutyRoom (sName, iUseDeptID) Values (@sName, @iUseDeptID) ")
                .Append("Select @@Identity ")
                .Append("End ")
                .Append("Else ")
                .Append("Select -1 ");

            return TConvert.toInt32(DBHelper.ExecuteScalar(sb.ToString(), entity));
        }
		
		#endregion
		
		#region 编辑值班室
		
        /// <summary>
        /// 编辑值班室
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_DutyRoom entity)
        {
			string sSql =
                @"Update [EHECD_DutyRoom] Set 
				[sName]=@sName,
                [iUseDeptID]=@iUseDeptID
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}

        #endregion

        #region 更新值班室二维码

        /// <summary>
        /// 更新值班室二维码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateQRCode(EHECD_DutyRoom entity)
        {
            string sSql =
                @"Update [EHECD_DutyRoom] Set 
				[sQRCode]=@sQRCode
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
        }

        #endregion

        #region 批量删除值班室

        /// <summary>
        /// 批量删除值班室
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_DutyRoom Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 获取单位下辖值班室数据

        /// <summary>
        /// 获取单位下辖值班室数据
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DutyRoom> GetListByUnitID(int iUnitID)
        {
            return DBHelper.Query<EHECD_DutyRoom>(string.Format("SELECT * FROM EHECD_DUTYROOM WHERE bIsDeleted = 0 AND iUseDeptID = {0}", iUnitID));
        }

        #endregion

        #region 根据ID集获取值班室数据

        /// <summary>
        /// 根据ID集获取值班室数据
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_DutyRoom> GetDataByIDs(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";

            return DBHelper.Query<EHECD_DutyRoom>(string.Format("SELECT * FROM EHECD_DutyRoom WHERE bIsDeleted = 0 AND ID IN ({0})", sIds));
        }

        #endregion
    }
}