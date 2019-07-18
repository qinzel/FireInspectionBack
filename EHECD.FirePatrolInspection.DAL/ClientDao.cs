using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 前端用户操作
    /// </summary>
    public class ClientDao
    {
        static ClientDao instance = new ClientDao();

        private ClientDao()
        {
        }
        public static ClientDao Instance
        {
            get
            {
                return instance;
            }
        }

        #region 获取前端用户信息（总后台）

        /// <summary>
        /// 获取前端用户信息（总后台）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetTotalList(QueryParams param, ref int iTotalRecord)
        {
            StringBuilder sb = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sb.AppendFormat(string.Format(" AND iUnitID = {0}", param.condition["iUseDeptID"]));
            }

            string sSql = @"
                    Select C.*, 
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iClientID = C.ID) iDeviceCount,
                    ISNULL(stuff((SELECT '、' + sName 
                    FROM EHECD_Unit Where bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID in (
                        SELECT iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iAuditState < 2 AND iClientID = C.ID
                    )  for XML PATH('')), 1, 1,''), '') sUnitName
                    From EHECD_Client C Where C.bIsDeleted = 0 AND C.iType = 0 AND C.ID IN (
	                    SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iAuditState = 1" + sb + @"
                    )
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (C.sName Like '%{0}%' OR C.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iType"))
            {
                sCondition.AppendFormat(string.Format(" And C.iType = {0}", param.condition["iType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And C.iStatus = {0}", param.condition["iStatus"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Client>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取前端用户信息（非维护公司和消防部门后台）

        /// <summary>
        /// 获取前端用户信息（非维护公司和消防部门后台）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetList(QueryParams param, ref int iTotalRecord)
        {
            StringBuilder sb = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sb.AppendFormat(string.Format(" AND iUnitID = {0}", param.condition["iUseDeptID"]));
            }

            string sSql = @"
                    Select C.*, 
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iClientID = C.ID) iDeviceCount,
                    ISNULL(stuff((SELECT '、' + sName 
                    FROM EHECD_Unit Where bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID in (
                        SELECT iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState < 2 AND iClientID = C.ID
                    )  for XML PATH('')), 1, 1,''), '') sUnitName
                    From EHECD_Client C Where C.bIsDeleted = 0 AND C.iType = 0 AND C.ID IN (
	                    SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1" + sb + @"
                    )
                ";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (C.sName Like '%{0}%' OR C.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iType"))
            {
                sCondition.AppendFormat(string.Format(" And C.iType = {0}", param.condition["iType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And C.iStatus = {0}", param.condition["iStatus"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Client>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取前端用户信息（维护公司和消防部门后台）

        /// <summary>
        /// 获取前端用户信息（维护公司和消防部门后台）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetUnitList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = "Select * From EHECD_Client Where bIsDeleted=0";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (sName Like '%{0}%' OR sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iType"))
            {
                sCondition.AppendFormat(string.Format(" And iType = {0}", param.condition["iType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And iUnitID = {0}", param.condition["iUseDeptID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Client>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取值班用户信息

        /// <summary>
        /// 获取值班用户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetDutyList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT * FROM (
	                    SELECT C.*, 
	                    (SELECT sName FROM EHECD_UNIT WHERE C.iUnitID = ID) sUnitName,
	                    (SELECT ISNULL(SUM(CASE WHEN sEndTime != '' THEN DATEDIFF(MINUTE, sStartTime, sEndTime) ELSE 0 END), 0) FROM EHECD_DutyRecord WHERE iClientID = C.ID) iTimeLength
	                    FROM EHECD_Client C WHERE C.iType = 4 AND C.bIsDeleted = 0
                    ) T WHERE 1 = 1
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (T.sName Like '%{0}%' OR T.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUseDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And T.iUnitID = {0}", param.condition["iUseDeptID"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Client>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 根据部门关系获取前端用户数据

        /// <summary>
        /// 根据部门关系获取前端用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetInspectorsByDeptIDList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT C.*, 
                    (SELECT sName FROM EHECD_Unit WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iType = 0 AND ID IN (
                        SELECT iUnitID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iStatus = 0 AND iOrganID = {0} AND iAuditState = 1
                    )) sUnitName,
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iClientID = C.ID) iDeviceCount
                    FROM EHECD_Client C WHERE bIsDeleted = 0 AND iStatus = 0 AND ID IN (
	                    SELECT iClientID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iStatus = 0 AND iOrganID = {0} AND iAuditState = 1
                    )
                ", Convert.ToInt32(param.condition["iOrganID"]));

            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (C.sName Like '%{0}%' OR C.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }

            param.sort = "C.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Client>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 根据部门ID集获取前端用户数据

        /// <summary>
        /// 根据部门ID集获取前端用户数据
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public int GetClientsByDeptIDs(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
            return DBHelper.QuerySingle<int>(string.Format("SELECT COUNT(0) FROM EHECD_CLIENTDEPTREL WHERE iAuditState = 1 AND bIsDeleted = 0 AND iStatus = 0 AND iOrganID IN ({0})", sIds));
        }

        #endregion

        #region 根据ID集获取前端用户数据

        /// <summary>
        /// 根据ID集获取前端用户数据
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetClientsByIDs(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";

            return DBHelper.Query<EHECD_Client>(string.Format("SELECT * FROM EHECD_Client WHERE bIsDeleted = 0 AND ID IN ({0})", sIds));
        }

        #endregion

        #region 根据手机号获取前端用户数据

        /// <summary>
        /// 根据手机号获取前端用户数据
        /// </summary>
        /// <param name="sPhones"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetClientsByPhones(string sPhones)
        {
            sPhones = "'" + string.Join("','", sPhones.Split(',')) + "'";

            return DBHelper.Query<EHECD_Client>(string.Format("SELECT * FROM EHECD_CLIENT WHERE bIsDeleted = 0 AND sPhone IN ({0})", sPhones));
        }

        #endregion

        #region 根据类型获取前端用户数据

        /// <summary>
        /// 根据类型获取前端用户数据
        /// </summary>
        /// <param name="iType">用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]</param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetClientsByType(int iType)
        {
            return DBHelper.Query<EHECD_Client>(string.Format("SELECT * FROM EHECD_CLIENT WHERE bIsDeleted = 0 AND iType = {0}", iType));
        }

        #endregion

        #region 获取前端用户详情

        /// <summary>
        /// 获取前端用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Client Get(int iClientID)
        {
            string sSql = string.Format(@"
                    Select C.*, 
                    ISNULL(stuff((SELECT '、' + sName 
                    FROM EHECD_Unit Where bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID in (
                        SELECT iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iAuditState = 1 AND iStatus = 0 AND iClientID = C.ID
                    )  for XML PATH('')), 1, 1,''), '') sUnitName
                    From EHECD_Client C Where C.bIsDeleted = 0 AND C.iType = 0 AND C.ID IN (
                        SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1
                    ) AND C.ID = {0}
                ", iClientID);
            return DBHelper.QuerySingle<EHECD_Client>(sSql);
        }

        #endregion

        #region 获取消防部门前端用户详情

        /// <summary>
        /// 获取消防部门前端用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Client GetFireDeptClient(int iClientID)
        {
            string sSql = string.Format(@"
                    Select C.*, 
                    ISNULL(stuff((SELECT '、' + sName 
                    FROM EHECD_Unit Where bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID in (
                        SELECT iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iAuditState = 1 AND iStatus = 0 AND iClientID = C.ID
                    )  for XML PATH('')), 1, 1,''), '') sUnitName
                    From EHECD_Client C Where C.bIsDeleted = 0 AND C.iType = 1 AND C.ID IN (
                        SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1
                    ) AND C.ID = {0}
                ", iClientID);
            return DBHelper.QuerySingle<EHECD_Client>(sSql);
        }

        #endregion

        #region 获取维护公司前端用户详情

        /// <summary>
        /// 获取维护公司前端用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Client GetRepairDeptClient(int iClientID)
        {
            string sSql = string.Format(@"
                    Select C.*, 
                    ISNULL(stuff((SELECT '、' + sName 
                    FROM EHECD_Unit Where bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID in (
                        SELECT iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iAuditState = 1 AND iStatus = 0 AND iClientID = C.ID
                    )  for XML PATH('')), 1, 1,''), '') sUnitName
                    From EHECD_Client C Where C.bIsDeleted = 0 AND C.iType = 2 AND C.ID IN (
                        SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1
                    ) AND C.ID = {0}
                ", iClientID);
            return DBHelper.QuerySingle<EHECD_Client>(sSql);
        }

        #endregion

        #region 获取前端用户详情

        /// <summary>
        /// 获取前端用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Client GetClientInfo(int iClientID)
        {
            return DBHelper.QuerySingle<EHECD_Client>(string.Format(@"
                    SELECT C.ID, C.sName, C.sPhone, C.iType, C.iStatus, C.bIsDeleted,
                   (SELECT sName FROM EHECD_UNIT WHERE ID = C.iUnitID) sUnitName
                   FROM EHECD_CLIENT C WHERE C.bIsDeleted = 0 AND C.iStatus = 0 AND C.ID = {0}
                ", iClientID));
        }

        #endregion

        #region 获取前端用户详情

        /// <summary>
        /// 获取前端用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Client GetClient(int iClientID)
        {
            return DBHelper.QuerySingle<EHECD_Client>(iClientID);
        }

        #endregion

        #region 获取前端待审核用户详情

        /// <summary>
        /// 获取前端待审核用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Client GetAdoptInfo(int iClientID)
        {
            string sSql = string.Format(@"
                    Select C.*, 
                    ISNULL(stuff((SELECT '、' + sName 
                    FROM EHECD_Unit Where bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID in (
                        SELECT iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iClientID = C.ID
                    )  for XML PATH('')), 1, 1,''), '') sUnitName
                    From EHECD_Client C Where C.bIsDeleted = 0 AND C.iType = 0 AND C.ID IN (
                        SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0
                    ) AND C.ID = {0}
                ", iClientID);
            return DBHelper.QuerySingle<EHECD_Client>(sSql);
        }

        #endregion

        #region 添加前端用户

        /// <summary>
        /// 添加前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public long Create(EHECD_Client entity)
        {
            return TConvert.toInt32(DBHelper.ExecuteScalar(@"
                    DECLARE @clientID INT
                    IF EXISTS (SELECT * FROM EHECD_Client WHERE bIsDeleted = 0 AND sPhone = @sPhone)
                    BEGIN
                        SELECT -2
                    END
                    ELSE
                        BEGIN
                            IF NOT EXISTS (SELECT * FROM EHECD_Client WHERE bIsDeleted = 0 AND sPhone = @sPhone)
                            BEGIN
                                INSERT INTO EHECD_Client
                                (sPhone, sPwd, sName, iUnitID, iType)
                                VALUES
                                (@sPhone, @sPwd, @sName, @iUnitID, @iType);
                                SELECT @clientID = @@IDENTITY;
                                INSERT INTO EHECD_ClientDeptRel (iClientID, iUnitID, iAuditState, sOperator, sOperateTime)
                                VALUES (@clientID, @iUnitID, 1, 'system', CONVERT(VARCHAR(20), GETDATE(), 120));
                                SELECT @clientID;
                            END
                            ELSE
                                Select -1
                        END
                ", entity));
        }

        #endregion

        #region 编辑前端用户

        /// <summary>
        /// 编辑前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Modify(EHECD_Client entity)
        {
            return DBHelper.Execute(@"
                    IF NOT EXISTS (SELECT * FROM EHECD_Client WHERE bIsDeleted = 0 AND sPhone = @sPhone AND ID != @ID)
                    BEGIN
                        Update EHECD_Client Set sPhone = @sPhone, sName = @sName Where ID = @ID
                    END
                    ELSE
                        Select -1
                ", entity) > 0;
        }

        #endregion

        #region 编辑前端用户

        /// <summary>
        /// 编辑前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_Client entity)
        {
			string sSql =
                @"Update [EHECD_Client] Set 
				[sName]=@sName,
				[sCredentials]=@sCredentials
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}

        #endregion

        #region 编辑前端用户

        /// <summary>
        /// 编辑前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdatePeople(EHECD_Client entity)
        {
            return DBHelper.Execute(@"
                    IF NOT EXISTS (SELECT * FROM EHECD_Client WHERE bIsDeleted = 0 AND sPhone = @sPhone AND ID != @ID)
                    BEGIN
                        Update EHECD_Client Set sPhone = @sPhone, sName = @sName, iUnitID = @iUnitID Where ID = @ID
                    END
                    ELSE
                        Select -1
                ", entity) > 0;
        }

        #endregion

        #region 批量删除前端用户

        /// <summary>
        /// 批量删除前端用户
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_Client Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 批量重置前端用户密码

        /// <summary>
        /// 批量重置前端用户密码
        /// </summary>
        /// <param name="sIds"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public bool Reset(string sIds, string sPwd)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";

            return DBHelper.Execute(string.Format("Update EHECD_Client Set sPwd = '{1}' Where ID In ({0})", sIds, sPwd)) > 0;
        }

        #endregion

        #region 冻结用户信息

        /// <summary>
        /// 冻结用户信息
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public bool Frozen(int iClientID)
        {
            string sSql = string.Format(@"
                IF NOT EXISTS(SELECT * FROM EHECD_Client WHERE ID = {0} AND iStatus = 1)
                BEGIN
	                UPDATE EHECD_Client Set iStatus = 1 Where ID = {0};
                    UPDATE EHECD_ClientDeptRel SET iStatus = 1 WHERE iClientID = {0} AND iAuditState = 1 AND bIsDeleted = 0;
                END
                ELSE
	                SELECT -1 ", iClientID);
            return DBHelper.Execute(sSql) > 0;
        }

        #endregion

        #region 解冻用户信息

        /// <summary>
        /// 解冻用户信息
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public bool UnFrozen(int iClientID)
        {
            return DBHelper.Execute(string.Format(@"
                    UPDATE EHECD_Client Set iStatus = 0 Where ID In ({0});
                    UPDATE EHECD_ClientDeptRel SET iStatus = 0 WHERE iClientID IN ({0}) AND iAuditState = 1 AND bIsDeleted = 0;
                ", iClientID)) > 0;
        }

        #endregion

        #region 根据电话号码获取用户信息

        /// <summary>
        /// 根据电话号码获取用户信息
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        public EHECD_Client GetByPhone(string sPhone)
        {
            string sSql = @"
                    SELECT TOP 1 C.* FROM EHECD_CLIENT C INNER JOIN EHECD_CLIENTDEPTREL R ON C.ID = R.iClientID 
                    WHERE C.bIsDeleted = 0 AND sPhone = @sPhone AND R.bIsDeleted = 0 AND R.iAuditState < 2";
            return DBHelper.QuerySingle<EHECD_Client>(sSql, new { sPhone = sPhone });
        }

        #endregion

        #region 注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EHECD_Client Register(EHECD_Client entity)
        {
            StringBuilder sb = new StringBuilder();

            if (entity.sUnitIds.Count > 0 && entity.sDeptIds.Count > 0 && entity.sUnitIds.Count == entity.sDeptIds.Count)
            {
                var unitIDs = entity.sUnitIds;
                var deptIDs = entity.sDeptIds;
                for (var i = 0; i < unitIDs.Count; i++)
                {
                    var unitID = unitIDs[i];
                    var organID = deptIDs[i];
                    sb.Append(string.Format(@"
                        IF NOT EXISTS (SELECT * FROM EHECD_ClientDeptRel WHERE iClientID = @clientId AND iUnitID = {0} AND iOrganID = {1} AND bIsDeleted = 0)
                        BEGIN
                            INSERT INTO EHECD_ClientDeptRel (iClientID, iUnitID, iOrganID) VALUES (@clientId, {0}, {1})
                        END;", unitID, organID));
                }
            }

            string sSql = @"
                        DECLARE @clientId INT
                        INSERT INTO EHECD_Client 
                        (sPhone, sPwd, sName, sImageSrc, sCredentials)
                        VALUES 
                        (@sPhone, @sPwd, @sName, @sImageSrc, @sCredentials);
                        SELECT @clientId = @@Identity;" + sb + @"
                        SELECT * FROM EHECD_Client WHERE ID = @clientId;
                    ";

            return DBHelper.QuerySingle<EHECD_Client>(sSql, entity);
        }

        #endregion

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public EHECD_Client Login(string sPhone, string sPwd)
        {
            string sSql = "SELECT TOP 1 * FROM EHECD_Client WHERE bIsDeleted = 0 AND sPhone = @sPhone AND sPwd = @sPwd";
            return DBHelper.QuerySingle<EHECD_Client>(sSql, new { sPhone = sPhone, sPwd = sPwd });
        }

        #endregion

        #region 验证是否至少有一个单位审核通过

        /// <summary>
        /// 验证是否至少有一个单位审核通过
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_ClientDeptRel> GetDeptRelByClientID(int iClientID)
        {
            return DBHelper.Query<EHECD_ClientDeptRel>(string.Format("SELECT * FROM EHECD_ClientDeptRel WHERE iClientID = {0} AND iAuditState = 1 AND bIsDeleted = 0", iClientID));
        }

        #endregion

        #region 修改资料

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateInfo(EHECD_Client entity)
        {
            string sSql =
                @"Update [EHECD_Client] Set 
				[sName]=@sName,
				[sImageSrc]=@sImageSrc
				Where ID = @ID";

            return DBHelper.Execute(sSql, entity) > 0;
        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool SetPwd(EHECD_Client entity)
        {
            return DBHelper.Execute("UPDATE EHECD_Client SET sPwd = @sPwd WHERE ID = @ID", entity) > 0;
        }

        #endregion

        #region 修改头像

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sImageSrc"></param>
        /// <returns></returns>
        public bool UpdateImage(int iClientID, string sImageSrc)
        {
            string sSql = "UPDATE EHECD_Client SET sImageSrc = @sImageSrc WHERE ID = @iClientID";
            return TConvert.toInt32(DBHelper.ExecuteScalar(sSql, new { iClientID = iClientID, sImageSrc = sImageSrc })) > 0;
        }

        #endregion

        #region 修改登录密码

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public bool EditPwd(int iClientID, string sPwd)
        {
            return DBHelper.Execute("UPDATE EHECD_Client Set sPwd = @sPwd WHERE ID = @ID", new { sPwd = sPwd, ID = iClientID }) > 0;
        }

        #endregion

        #region 修改会员头像

        /// <summary>
        /// 修改会员头像
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sImageSrc"></param>
        /// <returns></returns>
        public bool ChangeHeadImageByID(int iClientID, string sImageSrc)
        {
            return DBHelper.Execute(string.Format(@"
                Update EHECD_Client Set sImageSrc = '{1}' Where ID = {0}", iClientID, sImageSrc)) > 0;
        }

        #endregion

        #region 获取所有该分类的用户信息集合

        /// <summary>
        /// 获取所有该分类的用户信息集合
        /// </summary>
        /// <param name="iType">用户类别[0:点检员,1:消防,2:维护公司,3:使用公司,4:值班人员]</param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetListByType(int iType)
        {
            string sSql = String.Empty;
            if (iType == 0)
            {
                sSql = @"Select C.*, 
                    (SELECT sName FROM EHECD_Unit WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID = (
                        SELECT TOP 1 iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iClientID = C.ID
                    )) sUnitName
                    From EHECD_Client C Where C.bIsDeleted = 0 AND C.iType = 0 AND C.ID IN (
                        SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1
                    )";
            }
            else
            {
                sSql = string.Format("SELECT * FROM EHECD_Client WHERE bIsDeleted = 0 AND iStatus = 0 AND iType = {0}", iType);
            }
            return DBHelper.Query<EHECD_Client>(sSql);
        }

        #endregion

        #region 根据单位ID获取点检员信息

        /// <summary>
        /// 根据单位ID获取点检员信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetClientByUseDeptID(int iUnitID)
        {
            return DBHelper.Query<EHECD_Client>(string.Format(@"
                    Select C.*, 
                    (SELECT sName FROM EHECD_Unit WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID = (
                        SELECT TOP 1 iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iClientID = C.ID
                    )) sUnitName
                    From EHECD_Client C Where C.bIsDeleted = 0 AND C.iType = 0 AND C.ID IN (
                        SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iUnitID = {0}
                    )
                ", iUnitID));
        }

        #endregion

        #region 获取所有点检员账号申请待审核信息

        /// <summary>
        /// 获取所有点检员账号申请待审核信息
        /// </summary>
        /// <returns></returns>
        public int GetApplyList()
        {
            return DBHelper.QuerySingle<int>("SELECT COUNT(0) FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iAuditState = 0");
        }

        #endregion

        #region 获取使用单位点检员账号申请待审核信息

        /// <summary>
        /// 获取使用单位点检员账号申请待审核信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public int GetApplyListByUnitID(int iUnitID)
        {
            return DBHelper.QuerySingle<int>(string.Format("SELECT COUNT(0) FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iAuditState = 0 AND iUnitID = {0} AND iOrganID > 0", iUnitID));
        }

        #endregion

        #region 获取使用单位点检员账号信息

        /// <summary>
        /// 获取使用单位点检员账号信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetAdoptListByUnitID(int iUnitID)
        {
            string sSql = string.Format(@"
                SELECT * FROM (
	                SELECT R.*,  
	                (SELECT sName FROM EHECD_Client WHERE bIsDeleted = 0 AND iStatus = 0 AND ID = R.iClientID) sName, 
	                (SELECT sPhone FROM EHECD_Client WHERE bIsDeleted = 0 AND iStatus = 0 AND ID = R.iClientID) sPhone
	                FROM EHECD_ClientDeptRel R WHERE R.iUnitID = {0} AND bIsDeleted = 0 AND iStatus = 0 AND R.iAuditState = 1 AND R.iOrganID > 0
                ) T WHERE 1 = 1
            ", iUnitID);

            return DBHelper.Query<EHECD_Client>(sSql);
        }

        #endregion

        #region 根据单位ID获取值班人员信息

        /// <summary>
        /// 根据单位ID获取值班人员信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetDutyPeopleByUnitID(int iUnitID)
        {
            return DBHelper.Query<EHECD_Client>(string.Format(@"
                    SELECT C.ID, C.sPhone, C.sName, C.sImageSrc, C.iStatus, C.iUnitID, C.iType, 
                    (SELECT sName FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iAuditState = 1 AND iStatus = 0 AND ID = C.iUnitID) sUnitName
                    FROM EHECD_CLIENT C 
                    WHERE C.iTYPE = 4 AND C.iUnitID = {0} AND C.bIsDeleted = 0 AND C.ID IN (SELECT iClientID FROM 
                    EHECD_DUTYRECORD WHERE sStartTime != '' AND sEndTime = '')
                ", iUnitID));
        }

        #endregion

        #region 根据点检员ID获取关联单位值班人员信息

        /// <summary>
        /// 根据点检员ID获取关联单位值班人员信息
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetDutyPeopleByClientID(int iClientID)
        {
            return DBHelper.Query<EHECD_Client>(string.Format(@"
                    SELECT * FROM (
	                    SELECT * FROM (
		                    SELECT C.*, 
		                    (SELECT sName FROM EHECD_UNIT WHERE C.iUnitID = ID) sUnitName,
		                    (SELECT ISNULL(SUM(CASE WHEN sEndTime != '' THEN DATEDIFF(MINUTE, sStartTime, sEndTime) ELSE 0 END), 0) FROM EHECD_DutyRecord WHERE iClientID = C.ID) iTimeLength
		                    FROM EHECD_Client C WHERE C.iType = 4 AND C.bIsDeleted = 0
	                    ) A WHERE 1 = 1 AND A.iUnitID IN (
		                    SELECT iUnitID FROM EHECD_ClientDeptRel WHERE iClientID = {0} AND IAuditState = 1 AND bIsDeleted = 0 AND iStatus = 0
	                    ) AND A.ID IN (
		                    SELECT iClientID FROM EHECD_DUTYRECORD WHERE sStartTime != '' AND sEndTime = ''
	                    )
                    ) T
                ", iClientID));
        }

        #endregion

        #region 根据消防部门ID获取关联单位值班人员信息

        /// <summary>
        /// 根据消防部门ID获取关联单位值班人员信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetDutyPeopleByFireDeptID(int iUnitID)
        {
            return DBHelper.Query<EHECD_Client>(string.Format(@"
                    SELECT * FROM (
                        SELECT * FROM (
                            SELECT C.*, 
                            (SELECT sName FROM EHECD_UNIT WHERE C.iUnitID = ID) sUnitName,
                            (SELECT ISNULL(SUM(CASE WHEN sEndTime != '' THEN DATEDIFF(MINUTE, sStartTime, sEndTime) ELSE 0 END), 0) FROM EHECD_DutyRecord WHERE iClientID = C.ID) iTimeLength
                            FROM EHECD_Client C WHERE C.iType = 4 AND C.bIsDeleted = 0
                        ) A WHERE 1 = 1 AND A.iUnitID IN (
                            SELECT ID FROM EHECD_Unit WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iParentID = {0}
                        ) AND A.ID IN (
                            SELECT iClientID FROM EHECD_DUTYRECORD WHERE sStartTime != '' AND sEndTime = ''
                        )
                    ) T
                ", iUnitID));
        }

        #endregion

        #region 根据维护公司ID获取关联单位值班人员信息

        /// <summary>
        /// 根据消防部门ID获取关联单位值班人员信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetDutyPeopleByRepairDeptID(int iUnitID)
        {
            return DBHelper.Query<EHECD_Client>(string.Format(@"
                    SELECT * FROM (
                        SELECT * FROM (
                            SELECT C.*, 
                            (SELECT sName FROM EHECD_UNIT WHERE C.iUnitID = ID) sUnitName,
                            (SELECT ISNULL(SUM(CASE WHEN sEndTime != '' THEN DATEDIFF(MINUTE, sStartTime, sEndTime) ELSE 0 END), 0) FROM EHECD_DutyRecord WHERE iClientID = C.ID) iTimeLength
                            FROM EHECD_Client C WHERE C.iType = 4 AND C.bIsDeleted = 0
                        ) A WHERE 1 = 1 AND A.iUnitID IN (
                            SELECT U.ID FROM EHECD_UNIT U WHERE U.iStatus = 0 AND U.iAuditState = 1 AND U.bIsDeleted = 0 AND U.ID IN (
			                    SELECT iUseDeptID FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND ID IN (
				                    SELECT iUseDeptSettingsID FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iRepairDeptID = {0}
			                    )
		                    )
                        ) AND A.ID IN (
                            SELECT iClientID FROM EHECD_DUTYRECORD WHERE sStartTime != '' AND sEndTime = ''
                        )
                    ) T
                ", iUnitID));
        }

        #endregion

        #region 点检员申请列表

        /// <summary>
        /// 点检员申请列表
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_ClientDeptRel> GetAdoptList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                SELECT * FROM (
	                SELECT R.*,  
	                (SELECT sName FROM EHECD_Client WHERE bIsDeleted = 0 AND iStatus = 0 AND ID = R.iClientID) sName, 
	                (SELECT sPhone FROM EHECD_Client WHERE bIsDeleted = 0 AND iStatus = 0 AND ID = R.iClientID) sPhone,
	                (SELECT sCredentials FROM EHECD_Client WHERE bIsDeleted = 0 AND iStatus = 0 AND ID = R.iClientID) sCredentials,
                    ISNULL(stuff((SELECT '、' + sName 
	                FROM EHECD_Unit Where bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID in (
		                SELECT iUnitID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iUnitID = 1
	                )  for XML PATH('')), 1, 1,''), '') sUnitName
	                FROM EHECD_ClientDeptRel R WHERE R.iUnitID = {0} AND R.IOrganID > 0
                ) T WHERE 1 = 1
            ", param.condition["iUseDeptID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (sName Like '%{0}%' OR sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAuditState"))
            {
                sCondition.AppendFormat(string.Format(" And iAuditState = {0}", param.condition["iAuditState"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And dCreateTime >= CONVERT(varchar(20),'{0} :00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And dCreateTime <= CONVERT(varchar(20),'{0} :59:59', 120) ", param.condition["dEndTime"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_ClientDeptRel>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取前端用户申请详情

        /// <summary>
        /// 获取前端用户申请详情
        /// </summary>
        /// <param name="iClientDeptRelID"></param>
        /// <returns></returns>
        public EHECD_ClientDeptRel GetRel(int iClientDeptRelID)
        {
            return DBHelper.QuerySingle<EHECD_ClientDeptRel>(iClientDeptRelID);
        }

        #endregion

        #region 通过审核

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AdoptRel(EHECD_ClientDeptRel entity)
        {
            return DBHelper.Execute(@"
                    UPDATE EHECD_ClientDeptRel SET iAuditState = 1, sOperator = @sOperator, sOperateTime = CONVERT(VARCHAR(20), GETDATE(), 120) WHERE ID = @ID;
                ", entity) > 0;
        }

        #endregion

        #region 拒绝通过

        /// <summary>
        /// 拒绝通过
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool RefusedRel(EHECD_ClientDeptRel entity)
        {
            return DBHelper.Execute(@"
                    UPDATE EHECD_ClientDeptRel SET iAuditState = 2, sOperator = @sOperator, sOperateTime = CONVERT(VARCHAR(20), GETDATE(), 120) WHERE ID = @ID;
                ", entity) > 0;
        }

        #endregion

        #region 获取使用单位前端人员信息

        /// <summary>
        /// 获取使用单位前端人员信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetUseDataList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT C.*, 
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iClientID = C.ID) iDeviceCount,
                    (SELECT sName FROM EHECD_Dept WHERE bIsDeleted = 0 AND iUseDeptID = R.iUnitID AND ID = R.iOrganID) sOrganName
                    FROM EHECD_Client C INNER JOIN EHECD_ClientDeptRel R 
                    ON C.ID = R.iClientID WHERE C.bIsDeleted = 0 AND C.iType = 0 AND R.bIsDeleted = 0 AND R.iStatus = 0
                    AND R.iAuditState = 1 AND R.iUnitID = {0}
                ", param.condition["iUseDeptID"]);

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (C.sName Like '%{0}%' OR C.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iDeptID"))
            {
                sCondition.AppendFormat(string.Format(" And R.iOrganID = {0}", param.condition["iDeptID"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And C.iStatus = {0}", param.condition["iStatus"]));
            }
            param.sort = "C.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Client>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 根据使用单位和部门ID获取前端人员信息

        /// <summary>
        /// 根据使用单位和部门ID获取前端人员信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <param name="iDeptID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetClientByUnitAndDeptID(int iUnitID, int iDeptID)
        {
            return DBHelper.Query<EHECD_Client>(string.Format(@"
                    SELECT ID, sPhone, sName, sImageSrc, iType FROM EHECD_CLIENT WHERE iStatus = 0 AND bIsDeleted = 0 AND ID IN (
	                    SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE iUnitID = {0} AND iOrganID = {1} AND iAuditState = 1 AND bIsDeleted = 0 AND iStatus = 0
                    )
                ", iUnitID, iDeptID));
        }

        #endregion

        #region 获取当月未巡检的点检员

        /// <summary>
        /// 获取当月未巡检的点检员
        /// </summary>
        /// <param name="sStartTime"></param>
        /// <param name="sEndTime"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetListWithoutIns(string sStartTime, string sEndTime)
        {
            return DBHelper.Query<EHECD_Client>(string.Format(@"
                    --获取当月未巡检的点检员
                    SELECT * FROM EHECD_Client WHERE iStatus = 0 AND bIsDeleted = 0 AND iType = 0 AND ID IN (
	                    --获取当月未巡检的设备负责人ID
	                    SELECT iClientID FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND ID NOT IN (
		                    --获取当前月份已巡检过的设备ID
                            SELECT iDeviceID FROM EHECD_InspectionRecord WHERE DATEDIFF(MONTH, dCreateTime, GETDATE()) = 0 
                                AND DATEDIFF(DAY, dCreateTime, '{0}') >= 0 AND DATEDIFF(DAY, dCreateTime, '{1}') <= 0
	                    )
                    )
                ", sStartTime, sEndTime));
        }

        #endregion

        #region 修改所属使用单位/部门

        /// <summary>
        /// 修改所属使用单位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sOperator"></param>
        /// <returns></returns>
        public bool Change(EHECD_Client entity, string sOperator)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("BEGIN TRY ")
                .Append("BEGIN TRAN ")
                .Append(string.Format("UPDATE EHECD_ClientDeptRel SET bIsDeleted = 1 WHERE iClientID = {0} AND iAuditState < 2;", entity.ID));

            // 新关联使用单位及部门
            var unitIDs = entity.sUnitIds;
            var deptIDs = entity.sDeptIds;

            for (var i = 0; i < unitIDs.Count; i++)
            {
                var unitID = unitIDs[i];
                var organID = deptIDs[i];
                //sb.Append(string.Format(@"
                //        UPDATE EHECD_ClientDeptRel SET bIsDeleted = 1 WHERE iClientID = {0} AND iUnitID = {1} AND iAuditState < 2;
                //        INSERT INTO EHECD_ClientDeptRel (iClientID, iUnitID, iOrganID, iAuditState, sOperator, sOperateTime) VALUES ({0}, {1}, {2}, 1, '{3}', CONVERT(VARCHAR(20), GETDATE(), 120));
                //        ", entity.ID, unitID, organID, sOperator));
                sb.Append(string.Format(@"
                        INSERT INTO EHECD_ClientDeptRel (iClientID, iUnitID, iOrganID, iAuditState, sOperator, sOperateTime) VALUES ({0}, {1}, {2}, 1, '{3}', CONVERT(VARCHAR(20), GETDATE(), 120));
                        ", entity.ID, unitID, organID, sOperator));
            }

            sb.Append("COMMIT TRAN ")
                .Append("END TRY ")
                .Append("BEGIN CATCH ")
                .Append("ROLLBACK TRANSACTION ")
                .Append("END CATCH");

            return DBHelper.Execute(sb.ToString()) > 0;
        }

        #endregion

        #region 根据使用单位获取关联的维护公司下的所有维护人员

        /// <summary>
        /// 根据使用单位获取关联的维护公司下的所有维护人员
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetRepairClientList(int iUnitID)
        {
            return DBHelper.Query<EHECD_Client>(string.Format(@"
                    SELECT * FROM EHECD_Client WHERE bIsDeleted = 0 AND iStatus = 0 AND iUnitID IN (
	                    SELECT ID FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iType = 2 AND ID IN (
		                    SELECT iRepairDeptID FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iUseDeptSettingsID = (SELECT ID FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND iUseDeptID = {0})
	                    )
                    )
                ", iUnitID));
        }

        #endregion

        #region 根据单位ID获取前端人员信息（用于非点检人员）

        /// <summary>
        /// 根据单位ID获取前端人员信息（用于非点检人员）
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetClientByUnitID(int iUnitID)
        {
            return DBHelper.Query<EHECD_Client>(string.Format("SELECT ID, sName, sPhone FROM EHECD_CLIENT WHERE iUnitID = {0} AND iStatus = 0 AND bIsDeleted = 0", iUnitID));
        }

        #endregion
        
        #region 用户设备信息

        public bool SetClientCID(EHECD_CID cid)
        {
            string sSql = @"
                BEGIN TRY 
                    BEGIN TRAN 
                       delete from EHECD_CID where CID = @CID ;
                       insert into EHECD_CID(iClientID,CID,DeviceType) values(@iClientID , @CID , @DeviceType );
                    COMMIT TRAN 
                END TRY 
                BEGIN CATCH 
                    ROLLBACK TRANSACTION 
                END CATCH ";

            return DBHelper.Execute(sSql,cid) > 0;
        }

        public List<EHECD_CID> GetCIDByClientID(string iClientID)
        {
            List<EHECD_CID> existList = DBHelper.Query<EHECD_CID>("select * from EHECD_CID where iClientID = @iClientID", new EHECD_CID { iClientID = iClientID }).ToList();
            return existList;
        }

        #endregion
    }
}