				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 平台超管账号注册操作
    /// </summary>
    public class UnitDao
    {
        static UnitDao instance = new UnitDao();

        private UnitDao()
        {
        }
        public static UnitDao Instance
        {
            get
            {
                return instance;
            }
        }

        #region 获取平台超管账号注册信息

        /// <summary>
        /// 获取平台超管账号注册信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = "Select * From EHECD_Unit Where bIsDeleted = 0";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (sName Like '%{0}%' OR sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iType"))
            {
                sCondition.AppendFormat(string.Format(" And iType = {0}", param.condition["iType"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And dCreateTime >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And dCreateTime <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iAuditState"))
            {
                sCondition.AppendFormat(string.Format(" And iAuditState = {0}", param.condition["iAuditState"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_Unit>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取使用单位注册信息

        /// <summary>
        /// 获取使用单位注册信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetAdoptedUseDeptList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT U.*, 
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iUseDeptID = U.ID) iDeviceCount,
                    (SELECT COUNT(0) FROM EHECD_CLIENT WHERE ID IN (
	                    SELECT iClientID FROM EHECD_CLIENTDEPTREL WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iUnitID = U.ID
                    ) AND iType = 0 AND bIsDeleted = 0) iClientCount,
                    (SELECT COUNT(0) FROM EHECD_CLIENT WHERE bIsDeleted = 0 AND iStatus = 0 AND iType = 4 AND iUnitID = U.ID) iDutyPeopleCount,
                    ISNULL((SELECT sName FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iAuditState = 1 AND iStatus = 0 AND ID = U.iParentID), '') sParentName,
                    (SELECT COUNT(0) FROM EHECD_USEDEPTSETTINGDETAIL D INNER JOIN EHECD_USEDEPTSETTINGS S ON S.ID = D.iUseDeptSettingsID WHERE D.bIsDeleted = 0 AND S.iUseDeptID = U.ID) iDeptCount
                    FROM EHECD_UNIT U WHERE U.bIsDeleted = 0 AND U.iAuditState = 1 AND U.iType = 0
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (U.sName Like '%{0}%' OR U.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And U.iStatus = {0}", param.condition["iStatus"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iParentID"))
            {
                sCondition.AppendFormat(string.Format(" And U.iParentID = {0}", param.condition["iParentID"]));
            }

            param.sort = "U.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Unit>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取消防部门注册信息

        /// <summary>
        /// 获取消防部门注册信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetAdoptedFireDeptList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT U.*, 
                    (SELECT COUNT(0) FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iType = 0 AND iParentID = U.ID) iDeptCount,
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iUseDeptID IN (
						SELECT ID FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iType = 0 AND iParentID = U.ID
                    )) iDeviceCount
                    FROM EHECD_UNIT U WHERE U.bIsDeleted = 0 AND U.iAuditState = 1 AND U.iType = 1
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (U.sName Like '%{0}%' OR U.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And U.iStatus = {0}", param.condition["iStatus"]));
            }

            param.sort = "U.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Unit>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取维护单位注册信息

        /// <summary>
        /// 获取维护单位注册信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetAdoptedRepairDeptList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = @"
                    SELECT U.*, 
                    (SELECT COUNT(A.ID) FROM EHECD_UNIT A WHERE A.iStatus = 0 AND A.iAuditState = 1 AND A.bIsDeleted = 0 AND A.ID IN (
                        SELECT iUseDeptID FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND ID IN (
                            SELECT iUseDeptSettingsID FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iRepairDeptID = U.ID
                        )
                    )) iDeptCount,
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iRepairDeptID = U.ID AND iUseDeptID IN (
	                    SELECT iUseDeptID FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND ID IN (
		                    SELECT iUseDeptSettingsID FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iRepairDeptID = U.ID
	                    )
                    )) iDeviceCount
                    FROM EHECD_UNIT U WHERE U.bIsDeleted = 0 AND U.iAuditState = 1 AND U.iType = 2
                ";

            StringBuilder sCondition = new StringBuilder();
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sKeyword"))
            {
                sCondition.AppendFormat(string.Format(" And (U.sName Like '%{0}%' OR U.sPhone Like '%{0}%')", param.condition["sKeyword"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iStatus"))
            {
                sCondition.AppendFormat(string.Format(" And U.iStatus = {0}", param.condition["iStatus"]));
            }

            param.sort = "U.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Unit>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 按类型获取单位信息

        /// <summary>
        /// 按类型获取单位信息
        /// </summary>
        /// <param name="iType"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetUnitsByType(int iType)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format("SELECT * FROM EHECD_UNIT U WHERE bIsDeleted = 0 AND iAuditState = 1 AND iType = {0}", iType));
        }

        #endregion

        #region 获取平台超管账号注册详情

        /// <summary>
        /// 获取平台超管账号注册详情
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public EHECD_Unit Get(int iUnitID)
        {
            return DBHelper.QuerySingle<EHECD_Unit>(iUnitID);
        }

        #endregion

        #region 获取平台超管账号注册详情

        /// <summary>
        /// 获取平台超管账号注册详情
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public EHECD_Unit GetInfo(int iUnitID)
        {
            return DBHelper.QuerySingle<EHECD_Unit>(string.Format(@"
                    SELECT ID, sPhone, sName, sAddress, sAdminName, sContact, sLegalPerson, sQualifications, iType, sCredentials FROM EHECD_UNIT WHERE ID = {0}
                ", iUnitID));
        }

        #endregion

        #region 添加平台超管账号注册

        /// <summary>
        /// 添加平台超管账号注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_Unit entity)
        {
            return DBHelper.Insert<EHECD_Unit>(entity) > 0;
        }

        #endregion

        #region 编辑平台超管账号注册

        /// <summary>
        /// 编辑平台超管账号注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_Unit entity)
        {
            string sSql = @"
                    BEGIN TRY
                        BEGIN TRAN
                            Update EHECD_Unit Set sName = @sName, sAddress = @sAddress, sAdminName = @sAdminName, sContact = @sContact,
                            sLegalPerson = @sLegalPerson, sQualifications = @sQualifications, sCredentials = @sCredentials Where ID = @ID;
                            Update EHECD_UnitUser Set sRealName = @sAdminName WHERE iUnitID = @ID AND iUserType = 1;
                        COMMIT TRANSACTION
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRANSACTION
                    END CATCH
                ";
            return DBHelper.Execute(sSql, entity) > 0;
        }

        #endregion

        #region 批量删除平台超管账号注册

        /// <summary>
        /// 批量删除平台超管账号注册
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";

            return DBHelper.Execute(string.Format("Update EHECD_Unit Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 根据电话号码获取账号信息

        /// <summary>
        /// 根据电话号码获取账号信息
        /// </summary>
        /// <param name="sPhone"></param>
        /// <returns></returns>
        public EHECD_Unit GetByPhone(string sPhone)
        {
            string sSql = "SELECT TOP 1 * FROM EHECD_Unit WHERE bIsDeleted = 0 AND iAuditState < 2 AND sPhone = @sPhone";
            return DBHelper.QuerySingle<EHECD_Unit>(sSql, new { sPhone = sPhone });
        }

        #endregion

        #region 根据单位名称获取账号信息

        /// <summary>
        /// 根据单位名称获取账号信息
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public EHECD_Unit GetByName(string sName)
        {
            string sSql = "SELECT TOP 1 * FROM EHECD_Unit WHERE bIsDeleted = 0 AND iAuditState = 1 AND sName = @sName";
            return DBHelper.QuerySingle<EHECD_Unit>(sSql, new { sName = sName });
        }

        #endregion

        #region 注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EHECD_Unit Register(EHECD_Unit entity)
        {
            string sSql = @"
                        DECLARE @clientId INT
                        IF NOT EXISTS (SELECT * FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iAuditState = 1 AND (sName = @sName OR sPhone = @sPhone))
                        BEGIN
                            INSERT INTO EHECD_Unit 
                            (sPhone, sPwd, sName, sAddress, sAdminName, sContact, sLegalPerson, sQualifications, iType, sCredentials, iStatus, iAuditState)
                            VALUES 
                            (@sPhone, @sPwd, @sName, @sAddress, @sAdminName, @sContact, @sLegalPerson, @sQualifications, @iType, @sCredentials, @iStatus, @iAuditState);
                            SELECT @clientId = @@Identity;
                        END
                        SELECT * FROM EHECD_Unit WHERE ID = @clientId;
                    ";

            return DBHelper.QuerySingle<EHECD_Unit>(sSql, entity);
        }

        #endregion

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public EHECD_Unit Login(string sPhone, string sPwd)
        {
            string sSql = "SELECT TOP 1 * FROM EHECD_Unit WHERE bIsDeleted = 0 AND sPhone = @sPhone AND sPwd = @sPwd";
            return DBHelper.QuerySingle<EHECD_Unit>(sSql, new { sPhone = sPhone, sPwd = sPwd });
        }

        #endregion

        #region 修改资料

        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateInfo(EHECD_Unit entity)
        {
            string sSql =
                @"Update [EHECD_Unit] Set 
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
        public bool SetPwd(EHECD_Unit entity)
        {
            return DBHelper.Execute("UPDATE EHECD_Unit SET sPwd = @sPwd WHERE ID = @ID", entity) > 0;
        }

        #endregion

        #region 通过审核

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Adopt(EHECD_Unit entity)
        {
            var iType = 0;

            switch (entity.iType)
            {
                case 1:
                    iType = 2;
                    break;
                case 2:
                    iType = 1;
                    break;
            }

            return DBHelper.Execute(@"
                    BEGIN TRY
                        BEGIN TRAN
                            UPDATE EHECD_Unit SET iAuditState = 1, sOperator = @sOperator, sAuditTime = CONVERT(VARCHAR(20), GETDATE(), 120) WHERE ID = @ID;
                            INSERT INTO EHECD_UnitUser (sLoginName, sPassWord, sRealName, iUserType, iUserUnitType, iUnitID) VALUES (@sPhone, @sPwd, @sAdminName, 1, " + iType + @", @ID);
                        COMMIT TRANSACTION
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRANSACTION
                    END CATCH
                ", entity) > 0;
        }

        #endregion

        #region 拒绝通过

        /// <summary>
        /// 拒绝通过
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <param name="sOperator"></param>
        /// <returns></returns>
        public bool Refused(int iUnitID, string sOperator)
        {
            return DBHelper.Execute(string.Format("UPDATE EHECD_Unit SET iAuditState = 2, sOperator = '{1}', sAuditTime = CONVERT(VARCHAR(20), GETDATE(), 120) WHERE ID = {0}", iUnitID, sOperator)) > 0;
        }

        #endregion

        #region 冻结单位

        /// <summary>
        /// 冻结单位
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <remarks>判断该点检员是否存在多个使用单位，存在则冻结当前单位关系，不存在则直接冻结该点检员</remarks>
        /// <returns></returns>
        public bool Frozen(int iUnitID)
        {
            return DBHelper.Execute(string.Format(@"
                    BEGIN TRY
                        BEGIN TRAN
                            UPDATE EHECD_Unit SET iStatus = 1 WHERE ID = {0};
                            UPDATE EHECD_Client SET iStatus = 1 WHERE iUnitID = {0} AND bIsDeleted = 0 AND iStatus = 0;


                            declare @temp_temp int
                            declare @temp_count int
                            --------------------------------- 创建游标
                            declare clientID cursor for SELECT iClientID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iAuditState = 1 AND iUnitID = {0}
                            ----------------------------------- 打开游标
                            open clientID
                            ----------------------------------- 遍历和获取游标
                            fetch next from clientID into @temp_temp
                            while @@fetch_status = 0
                            begin
                              --查询点检员关系数量
                              select @temp_count = count(0) from EHECD_ClientDeptRel where iClientID = @temp_temp AND bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1
                              if(@temp_count = 1)
                              begin
	                            UPDATE EHECD_Client SET iStatus = 1 WHERE ID = @temp_temp AND bIsDeleted = 0 AND iStatus = 0;
                              end
                              UPDATE EHECD_ClientDeptRel Set iStatus = 1 WHERE iUnitID = {0} AND iClientID = @temp_temp AND bIsDeleted = 0 AND iAuditState = 1 AND iStatus = 0;
                              fetch next from clientID into @temp_temp;  -- 取值赋给变量
                            end
                            ----------------------------------- 关闭游标
                            close clientID;
                            ----------------------------------- 删除游标
                            deallocate clientID;


                            UPDATE EHECD_UnitUser SET iStatus = 1 WHERE iUnitID = {0} AND bIsDeleted = 0;
                        COMMIT TRANSACTION
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRANSACTION
                    END CATCH
                ", iUnitID)) > 0;
        }

        #endregion

        #region 解冻单位

        /// <summary>
        /// 解冻单位
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <remarks>解冻点检员与本单位的关系并解冻点检员</remarks>
        /// <returns></returns>
        public bool UnFrozen(int iUnitID)
        {
            return DBHelper.Execute(string.Format(@"
                    BEGIN TRY
                        BEGIN TRAN
                            UPDATE EHECD_Unit SET iStatus = 0 WHERE ID = {0};
                            UPDATE EHECD_Client SET iStatus = 0 WHERE iUnitID = {0} AND bIsDeleted = 0 AND iStatus = 1;
                            

                            declare @temp_temp int
                            declare @temp_count int
                            --------------------------------- 创建游标
                            declare clientID cursor for SELECT iClientID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iAuditState = 1 AND iUnitID = {0}
                            ----------------------------------- 打开游标
                            open clientID
                            ----------------------------------- 遍历和获取游标
                            fetch next from clientID into @temp_temp
                            while @@fetch_status = 0
                            begin
                              UPDATE EHECD_Client SET iStatus = 0 WHERE ID = @temp_temp AND bIsDeleted = 0 AND iStatus = 1;
                              UPDATE EHECD_ClientDeptRel Set iStatus = 0 WHERE iUnitID = {0} AND iClientID = @temp_temp AND bIsDeleted = 0 AND iAuditState = 1 AND iStatus = 1;
                              fetch next from clientID into @temp_temp;  -- 取值赋给变量
                            end
                            ----------------------------------- 关闭游标
                            close clientID;
                            ----------------------------------- 删除游标
                            deallocate clientID;


                            UPDATE EHECD_UnitUser SET iStatus = 0 WHERE iUnitID = {0} AND bIsDeleted = 0;
                        COMMIT TRANSACTION
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRANSACTION
                    END CATCH
                ", iUnitID)) > 0;
        }

        #endregion

        #region 获取所有该分类的单位信息集合

        /// <summary>
        /// 获取所有该分类的单位信息集合
        /// </summary>
        /// <param name="iType"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetListByType(int iType)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format("SELECT ID, iParentID, sPhone, sName, sAddress, sAdminName, sContact, sLegalPerson, sQualifications, iType, sCredentials FROM EHECD_Unit WHERE bIsDeleted = 0 AND iAuditState = 1 AND iStatus = 0 AND iType = {0}", iType));
        }

        #endregion

        #region 获取所有该分类的单位信息集合(包含冻结)

        /// <summary>
        /// 获取所有该分类的单位信息集合
        /// </summary>
        /// <param name="iType"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetListByTypeContainsFrozen(int iType)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format("SELECT ID, iParentID, sPhone, sName,iStatus, sAddress, sAdminName, sContact, sLegalPerson, sQualifications, iType, sCredentials FROM EHECD_Unit WHERE bIsDeleted = 0 AND iAuditState = 1  AND iType = {0}", iType));
        }

        #endregion

        #region 获取所有账号申请待审核信息

        /// <summary>
        /// 获取所有账号申请待审核信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetApplyList()
        {
            return DBHelper.Query<EHECD_Unit>("SELECT * FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iAuditState = 0");
        }

        #endregion

        #region 获取所有已审通过的单位信息

        /// <summary>
        /// 获取所有已审通过的单位信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetAllList()
        {
            return DBHelper.Query<EHECD_Unit>("SELECT ID, sName, sAddress, iType FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1");
        }

        #endregion

        #region 获取消防部门基础设置单位信息

        /// <summary>
        /// 获取消防部门基础设置单位信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetFireDeptUnitsSetting(int iUnitID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                    SELECT U.ID, U.sName, U.sAdminName, U.sPhone, U.sContact,
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE iUseDeptID = U.ID AND bIsDeleted = 0) iDeviceCount
                    FROM EHECD_UNIT U WHERE U.iParentID = {0} AND U.iType = 0
                ", iUnitID));
        }

        #endregion

        #region 获取某单位的关联单位集合

        /// <summary>
        /// 获取某单位的关联单位集合
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetUnitByUnitID(int iUnitID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format("SELECT * FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iParentID = {0}", iUnitID));
        }

        #endregion

        #region 获取某单位的关联使用单位集合

        /// <summary>
        /// 获取某单位的关联使用单位集合
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetRelUnitList(int iUnitID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                SELECT U.ID, U.sPhone, U.sName, U.sAddress, U.sAdminName, U.sContact, U.sLegalPerson, U.sQualifications, U.iType, U.sCredentials, U.iStatus, U.iAuditState, 
                (SELECT COUNT(0) FROM EHECD_DEVICE WHERE iUseDeptID = U.ID AND bIsDeleted = 0) iDeviceCount
                FROM EHECD_Unit U WHERE U.bIsDeleted = 0 AND U.iStatus = 0 AND U.iAuditState = 1 AND U.ID IN (
                    SELECT S.iUseDeptID FROM EHECD_UseDeptSettings S INNER JOIN EHECD_UseDeptSettingDetail D ON S.ID = D.iUseDeptSettingsID WHERE D.iRepairDeptID = {0}
                )
            ", iUnitID));
        }

        #endregion

        #region 获取点检员所属所有使用单位集合

        /// <summary>
        /// 获取点检员所属所有使用单位集合
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetClientRelUnitList(int iClientID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                SELECT ID, sPhone, sName, sAddress, sAdminName, sContact, sLegalPerson, sQualifications, iType, sCredentials, iStatus, iAuditState 
                    FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID IN (
                    SELECT iUnitID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iClientID = {0}
                )
            ", iClientID));
        }

        #endregion


        #region 获取点检员所属所有使用单位集合(包含已冻结的)

        /// <summary>
        /// 获取点检员所属所有使用单位集合
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetClientRelUnitListContainsFreeze(int iClientID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                SELECT ID, sPhone, sName, sAddress, sAdminName, sContact, sLegalPerson, sQualifications, iType, sCredentials, iStatus, iAuditState 
                    FROM EHECD_UNIT WHERE bIsDeleted = 0  AND iAuditState = 1 AND ID IN (
                    SELECT iUnitID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iClientID = {0}
                )
            ", iClientID));
        }

        #endregion

        #region 获取未审核点检员所属所有使用单位集合

        /// <summary>
        /// 获取未审核点检员所属所有使用单位集合
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetUnAdoptClientRelUnitList(int iClientID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                SELECT ID, sPhone, sName, sAddress, sAdminName, sContact, sLegalPerson, sQualifications, iType, sCredentials, iStatus, iAuditState 
                    FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND ID IN (
                    SELECT iUnitID FROM EHECD_ClientDeptRel WHERE bIsDeleted = 0 AND iStatus = 0 AND iClientID = {0}
                )
            ", iClientID));
        }

        #endregion

        #region 获取消防部门关联的使用单位集合

        /// <summary>
        /// 获取消防部门关联的使用单位集合
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetRelUnitListByFireDeptID(int iUnitID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                SELECT U.ID, U.iParentID, U.sName, U.sAddress, U.sAdminName, U.sContact, U.sLegalPerson, U.sQualifications, U.iType, U.sCredentials,
                (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iUseDeptID = U.ID) iDeviceCount
                FROM EHECD_Unit U WHERE U.bIsDeleted = 0 AND U.iStatus = 0 AND U.iAuditState = 1 AND U.iParentID = {0}
            ", iUnitID));
        }

        #endregion

        #region 根据使用单位基础设置ID获取关联单位

        /// <summary>
        /// 根据使用单位基础设置ID获取关联单位
        /// </summary>
        /// <param name="iUseDeptSettingsID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetUnitListBySettingsID(int iUseDeptSettingsID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                    SELECT ID, sPhone, sName, sAddress, sAdminName, sContact, sLegalPerson, sQualifications, iType, sCredentials 
                        FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND IAuditState = 1 AND ID IN (
	                    SELECT iRepairDeptID FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iUseDeptSettingsID = {0}
                    )
                ", iUseDeptSettingsID));
        }

        #endregion

        #region 获取使用单位已关联的维护单位信息

        /// <summary>
        /// 获取使用单位已关联的维护单位信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetRelRepairDeptList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT U.*, 
                    (SELECT COUNT(0) FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iType = 0 AND U.iParentID = ID) iDeptCount,
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iUseDeptID = {0} AND iRepairDeptID = U.ID) iDeviceCount
                    FROM EHECD_UNIT U WHERE U.bIsDeleted = 0 AND U.iAuditState = 1 AND U.iType = 2 AND U.ID IN (
	                    SELECT iRepairDeptID FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iUseDeptSettingsID = (SELECT ID FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND iUseDeptID = {0})
                    )
                ", param.condition["iUseDeptID"]);

            param.sort = "U.ID";

            return DBHelper.QueryRunSqlByPager<EHECD_Unit>(sSql, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取使用单位已关联的维护单位数据

        /// <summary>
        /// 获取使用单位已关联的维护单位数据
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetRelRepairDeptAllList(int iUnitID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                    SELECT U.*, 
                    (SELECT COUNT(0) FROM EHECD_UNIT WHERE bIsDeleted = 0 AND iStatus = 0 AND iAuditState = 1 AND iType = 0 AND U.iParentID = ID) iDeptCount,
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iUseDeptID = {0} AND iRepairDeptID = U.ID) iDeviceCount
                    FROM EHECD_UNIT U WHERE U.bIsDeleted = 0 AND U.iStatus = 0 AND U.iAuditState = 1 AND U.iType = 2 AND U.ID IN (
                        SELECT iRepairDeptID FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iUseDeptSettingsID = (SELECT ID FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND iUseDeptID = {0})
                    )
                ", iUnitID));
        }

        #endregion

        #region 获取关联了该维护公司的使用单位

        /// <summary>
        /// 获取关联了该维护公司的使用单位
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetRelUseDeptList(int iUnitID)
        {
            return DBHelper.Query<EHECD_Unit>(string.Format(@"
                    SELECT U.ID, U.iParentID, U.sName, U.sAddress, U.sAdminName, U.sContact, U.sLegalPerson, U.sQualifications, U.iType, U.sCredentials,
                    (SELECT COUNT(0) FROM EHECD_DEVICE WHERE bIsDeleted = 0 AND iUseDeptID = U.ID AND iRepairDeptID = {0}) iDeviceCount 
                    FROM EHECD_UNIT U WHERE U.iStatus = 0 AND U.iAuditState = 1 AND U.bIsDeleted = 0 AND U.ID IN (
                        SELECT iUseDeptID FROM EHECD_UseDeptSettings WHERE bIsDeleted = 0 AND ID IN (
                            SELECT iUseDeptSettingsID FROM EHECD_UseDeptSettingDetail WHERE bIsDeleted = 0 AND iRepairDeptID = {0}
                        )
                    )
                ", iUnitID));
        }

        #endregion
    }
}