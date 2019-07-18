
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;
using System.Linq;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 使用单位基础设置操作
    /// </summary>
    public class UseDeptSettingsDao
    {
        static UseDeptSettingsDao instance = new UseDeptSettingsDao();

        private UseDeptSettingsDao()
        {
        }
        public static UseDeptSettingsDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取使用单位基础设置信息
		
        /// <summary>
        /// 获取使用单位基础设置信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_UseDeptSettings> GetList(QueryParams param, ref int iTotalRecord)
        {
						
				string sSql = "Select * From EHECD_UseDeptSettings Where bIsDeleted=0";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_UseDeptSettings>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }
		
		#endregion
		
		#region 获取使用单位基础设置详情

        /// <summary>
        /// 获取使用单位基础设置详情
        /// </summary>
        /// <param name="iUseDeptSettingsID"></param>
        /// <returns></returns>
        public EHECD_UseDeptSettings Get(int iUseDeptSettingsID)
        {
            return DBHelper.QuerySingle<EHECD_UseDeptSettings>(iUseDeptSettingsID);
        }
		
		#endregion
		
		#region 添加使用单位基础设置

        /// <summary>
        /// 添加使用单位基础设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_UseDeptSettings entity)
        {
            StringBuilder sb = new StringBuilder();
            if (entity.DetailList != null && entity.DetailList.Count > 0)
            {
                foreach (EHECD_Unit detail in entity.DetailList)
                {
                    sb.Append(string.Format(@"
                        IF NOT EXISTS (SELECT * FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iUseDeptSettingsID = @SID AND iRepairDeptID = {0})
	                    BEGIN
		                    INSERT INTO EHECD_UseDeptSettingDetail (iUseDeptSettingsID, iRepairDeptID) VALUES (@SID, {0});
	                    END;
                    ", detail.ID));
                }
            }
            return DBHelper.Execute(@"
                    IF NOT EXISTS (SELECT * FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND iUseDeptID = @iUseDeptID)
                    BEGIN
	                    DECLARE @SID INT;
                        UPDATE EHECD_UNIT SET iParentID = @iFireDeptID WHERE ID = @iUseDeptID;
	                    INSERT INTO EHECD_UseDeptSettings (iUseDeptID, iFireDeptID) VALUES (@iUseDeptID, @iFireDeptID);
	                    SELECT @SID = @@IDENTITY;
	                    " + sb + @"
                        SELECT @SID;
                    END
                    ELSE
	                    SELECT -1
                ", entity) > 0;
        }
		
		#endregion
		
		#region 编辑使用单位基础设置
		
        /// <summary>
        /// 编辑使用单位基础设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_UseDeptSettings entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("BEGIN TRY ")
                .Append("BEGIN TRAN ")
                .Append(string.Format("Update EHECD_UseDeptSettings Set iFireDeptID = {1} Where ID = {0};", entity.ID, entity.iFireDeptID))
                .Append(string.Format("UPDATE EHECD_UNIT SET iParentID = {0} WHERE ID = {1};", entity.iFireDeptID, entity.iUseDeptID));

            // 旧关联维护公司
            IEnumerable<EHECD_Unit> unitList = UnitDao.Instance.GetRelRepairDeptAllList(entity.iUseDeptID);

            // 旧关联维护公司
            foreach (EHECD_Unit unit in unitList)
            {
                var hasItem = entity.DetailList.Where(o => o.ID == unit.ID).FirstOrDefault();
                if (hasItem == null)
                {
                    sb.Append(string.Format(@"
                        Update EHECD_UseDeptSettingDetail Set bIsDeleted = 1 Where iUseDeptSettingsID = {0} AND iRepairDeptID = {1};
                        Update EHECD_Device Set iRepairDeptID = 0 WHERE iRepairDeptID = {1};
                    ", entity.ID, unit.ID));
                }
            }

            // 新关联维护公司
            foreach (EHECD_Unit unit in entity.DetailList)
            {
                var newItem = unitList.Where(o => o.ID == unit.ID).FirstOrDefault();
                if (newItem == null)
                {
                    sb.Append(string.Format("INSERT INTO EHECD_UseDeptSettingDetail (iUseDeptSettingsID, iRepairDeptID) VALUES ({0}, {1});", entity.ID, unit.ID));
                }
                else
                {
                    var removeItem = entity.DetailList.Where(o => o.ID == newItem.ID).FirstOrDefault();
                    if (removeItem == null)
                    {
                        sb.Append(string.Format(@"
                            Update EHECD_UseDeptSettingDetail Set bIsDeleted = 1 Where iUseDeptSettingsID = {0} AND iRepairDeptID = {1};
                            Update EHECD_Device Set iRepairDeptID = 0 WHERE iRepairDeptID = {1};
                        ", entity.ID, unit.ID));
                    }
                }
            }

            sb.Append("COMMIT TRAN ")
                .Append("END TRY ")
                .Append("BEGIN CATCH ")
                .Append("ROLLBACK TRANSACTION ")
                .Append("END CATCH");

            return DBHelper.Execute(sb.ToString()) > 0;
		}
		
		#endregion
		
		#region 批量删除使用单位基础设置

        /// <summary>
        /// 批量删除使用单位基础设置
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_UseDeptSettings Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
					}

        #endregion

        #region 根据单位ID获取基础设置信息

        /// <summary>
        /// 根据单位ID获取基础设置信息
        /// </summary>
        /// <param name="iUseDeptID"></param>
        /// <returns></returns>
        public EHECD_UseDeptSettings GetSettings(int iUseDeptID)
        {
            return DBHelper.QuerySingle<EHECD_UseDeptSettings>(string.Format("SELECT * FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND iUseDeptID = {0}", iUseDeptID));
        }

        #endregion
    }
}
