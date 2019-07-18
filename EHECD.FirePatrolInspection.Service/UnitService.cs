
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 平台超管账号注册业务逻辑
    /// </summary>
    public class UnitService
    {
        static UnitService instance = new UnitService();
        private static UnitDao Dao = UnitDao.Instance;
        public static object async = new object();

        private UnitService()
        {
        }

        public static UnitService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (async)
                    {
                        if (instance == null)
                        {
                            instance = new UnitService();
                        }
                    }

                }
                return instance;
            }
        }

        #region 获取平台超管账号注册数据

        /// <summary>
        /// 获取平台超管账号注册数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取使用单位注册信息

        /// <summary>
        /// 获取使用单位注册信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetAdoptedUseDeptGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetAdoptedUseDeptList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取消防部门注册信息

        /// <summary>
        /// 获取消防部门注册信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetAdoptedFireDeptGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetAdoptedFireDeptList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取维护单位注册信息

        /// <summary>
        /// 获取维护单位注册信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetAdoptedRepairDeptGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetAdoptedRepairDeptList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取使用单位已关联的维护单位信息

        /// <summary>
        /// 获取使用单位已关联的维护单位信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="iTotalRecord"></param>
        /// <returns></returns>
        public string GetRelRepairDeptList(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetRelRepairDeptList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
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
            EHECD_Unit entity = Dao.Get(iUnitID) ?? new EHECD_Unit();
            if (entity.ID > 0)
            {
                switch (entity.iType)
                {
                    case 0:
                        entity.UnitList = Dao.GetRelRepairDeptAllList(entity.ID).ToList();
                        entity.sParentName = entity.iParentID > 0 ? Dao.Get(entity.iParentID).sName : string.Empty;
                        break;
                    case 1:
                        entity.UnitList = Dao.GetRelUnitListByFireDeptID(entity.ID).ToList();
                        break;
                    case 2:
                        entity.UnitList = Dao.GetRelUseDeptList(entity.ID).ToList();
                        break;
                }
            }
            return entity;
        }

        #endregion

        #region 编辑平台超管账号注册

        /// <summary>
        /// 编辑平台超管账号注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Unit entity)
        {
            ResultMessage result = new ResultMessage();
            if (string.IsNullOrWhiteSpace(entity.sLegalPerson))
            {
                entity.sLegalPerson = String.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sQualifications))
            {
                entity.sQualifications = String.Empty;
            }
            //修改平台超管账号注册
            result.success = Dao.Update(entity);
            result.message = result.success ? "编辑成功" : "编辑失败";
            return result;
        }

        #endregion

        #region 批量删除平台超管账号注册

        /// <summary>
        /// 批量删除平台超管账号注册
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除平台超管账号注册成功" : "删除平台超管账号注册失败";
            return result;
        }

        #endregion

        #region 平台账号注册

        /// <summary>
        /// 平台账号注册
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sValidate"></param>
        /// <param name="sPwd"></param>
        /// <param name="sName"></param>
        /// <param name="sAddress"></param>
        /// <param name="sAdminName"></param>
        /// <param name="sContact"></param>
        /// <param name="iType"></param>
        /// <param name="sCredentials"></param>
        /// <param name="sLegalPerson"></param>
        /// <param name="sQualifications"></param>
        /// <returns></returns>
        [APIAttribute(name: "unit.register", desc: "平台账号注册")]
        public ResultMessage Register(string sPhone, string sValidate, string sPwd, string sName, string sAddress,
            string sAdminName, string sContact, int iType = 0, string sCredentials = "", string sLegalPerson = "", string sQualifications = "")
        {
            ResultMessage result = new ResultMessage();
            lock (async)
            {
                var entity = Dao.GetByPhone(sPhone);
                if (entity != null)
                {
                    result.message = "该手机号码已被注册";
                    return result;
                }

                entity = Dao.GetByName(sName);
                if (entity != null)
                {
                    result.message = "该单位已经注册";
                    return result;
                }

                // 验证短信合法性
                result = PhoneMsgService.Instance.Check(sPhone, sValidate, 0);

                if (!result.success)
                {// 短信验证不通过
                    return result;
                }

                EHECD_Unit newPower = new EHECD_Unit()
                {
                    sPhone = sPhone,
                    sPwd = TCommon.Md5(sPwd),
                    sName = sName,
                    sAddress = sAddress,
                    sAdminName = sAdminName,
                    sContact = sContact,
                    iType = iType,
                    sCredentials = sCredentials,
                    sLegalPerson = sLegalPerson,
                    sQualifications = sQualifications
                };

                newPower = Dao.Register(newPower);
                result.success = true;
                if (result.success)
                {
                    PushHub hub = new PushHub();
                    hub.SendMessage("TOTALBACKSTAGE", "你有一条新的单位申请");
                }
                result.message = "注册成功";
                result.data = newPower;
                return result;
            }
        }

        #endregion

        #region 通过审核

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public ResultMessage Adopt(int iUnitID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Unit entity = Get(iUnitID);
            if (entity.iAuditState > 0)
            {
                result.message = "已审数据无法再次审核";
                return result;
            }
            entity.sOperator = UserSession.GetLogUser().sLoginName;
            result.success = Dao.Adopt(entity);
            result.message = result.success ? "通过审核成功" : "通过审核失败";
            return result;
        }

        #endregion

        #region 拒绝通过

        /// <summary>
        /// 拒绝通过
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public ResultMessage Refused(int iUnitID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Unit entity = Get(iUnitID);
            if (entity.iAuditState > 0)
            {
                result.message = "已审数据无法再次审核";
                return result;
            }
            result.success = Dao.Refused(iUnitID, UserSession.GetLogUser().sLoginName);
            result.message = result.success ? "拒绝通过成功" : "拒绝通过失败";
            return result;
        }

        #endregion

        #region 冻结单位

        /// <summary>
        /// 冻结单位
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public ResultMessage Frozen(int iUnitID)
        {
            ResultMessage result = new ResultMessage();
            result.success = Dao.Frozen(iUnitID);
            result.message = result.success ? "冻结成功" : "冻结失败";
            return result;
        }

        #endregion

        #region 解冻单位

        /// <summary>
        /// 解冻单位
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public ResultMessage UnFrozen(int iUnitID)
        {
            ResultMessage result = new ResultMessage();
            result.success = Dao.UnFrozen(iUnitID);
            result.message = result.success ? "解冻成功" : "解冻失败";
            return result;
        }

        #endregion

        #region 获取所有该分类的单位信息集合

        /// <summary>
        /// 获取所有该分类的单位信息集合
        /// </summary>
        /// <param name="iType">单位类型[0:使用单位,1:消防部门,2:维护公司]</param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetListByType(int iType)
        {
            return Dao.GetListByType(iType);
        }

        #endregion

        #region 获取所有该分类的单位信息集合(包含冻结)
        /// <summary>
        /// 获取所有该分类的单位信息集合
        /// </summary>
        /// <param name="iType">单位类型[0:使用单位,1:消防部门,2:维护公司]</param>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetListByTypeContainsFrozen(int iType)
        {
            return Dao.GetListByTypeContainsFrozen(iType);
        }
        #endregion

        #region 获取所有账号申请待审核信息

        /// <summary>
        /// 获取所有账号申请待审核信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetApplyList()
        {
            return Dao.GetApplyList();
        }

        #endregion

        #region 获取所有已审通过的单位信息

        /// <summary>
        /// 获取所有已审通过的单位信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Unit> GetAllList()
        {
            IEnumerable<EHECD_Unit> uList = Dao.GetAllList();

            if (uList != null)
            {
                foreach (EHECD_Unit unit in uList)
                {
                    unit.DeptList = DeptDao.Instance.GetListByUnitID(unit.ID).ToList();
                    if (unit.DeptList != null && unit.DeptList.Count > 0)
                    {
                        foreach (EHECD_Dept dept in unit.DeptList)
                        {
                            dept.ClientList = ClientDao.Instance.GetClientByUnitAndDeptID(unit.ID, dept.ID).ToList();
                        }
                    }
                }
            }

            return uList;
        }

        #endregion

        #region 获取单位列表

        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns></returns>
        [APIAttribute(name: "unit.getlist", desc: "获取单位列表")]
        public ResultMessage GetUnitList()
        {
            ResultMessage result = new ResultMessage();

            try
            {
                IEnumerable<EHECD_Unit> uList = Dao.GetAllList().Where(o => o.iType == 0);

                if (uList != null)
                {
                    foreach (EHECD_Unit unit in uList)
                    {
                        unit.DeptList = DeptDao.Instance.GetListByUnitID(unit.ID).ToList();
                        if (unit.DeptList != null && unit.DeptList.Count > 0)
                        {
                            foreach (EHECD_Dept dept in unit.DeptList)
                            {
                                dept.ClientList = ClientDao.Instance.GetClientByUnitAndDeptID(unit.ID, dept.ID).ToList();
                            }
                        }
                    }
                }

                result.success = true;
                result.data = uList.ToList();
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取单位列表成功" : "获取单位列表失败";
            return result;
        }

        #endregion

        #region 获取使用单位列表

        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "unit.getrellist", desc: "获取使用单位列表")]
        public ResultMessage GetUnitRelList(int iClientID)
        {
            ResultMessage result = new ResultMessage();

            EHECD_Client entity = ClientDao.Instance.GetClient(iClientID) ?? new EHECD_Client();

            if (entity.ID == 0)
            {
                result.message = "获取单位列表失败";
                return result;
            }

            List<EHECD_Unit> uList = new List<EHECD_Unit>();
            //用户类别[0:点检员,1:消防,2:维护公司,4:值班人员]
            var iType = entity.iType;

            try
            {
                switch (iType)
                {
                    case 0://点检员所属的所有使用单位
                        uList = Dao.GetClientRelUnitList(entity.ID).ToList();
                        break;
                    case 1://消防
                        uList = Dao.GetRelUnitListByFireDeptID(entity.iUnitID).ToList();
                        break;
                    case 2://维护
                        uList = Dao.GetRelUseDeptList(entity.iUnitID).ToList();
                        break;
                }
                result.success = true;
                result.data = uList;
            }
            catch { }

            result.message = result.success ? "获取单位列表成功" : "获取单位列表失败";
            return result;
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
            var list = Dao.GetFireDeptUnitsSetting(iUnitID);
            foreach (EHECD_Unit unit in list)
            {
                unit.ClientList = ClientDao.Instance.GetDutyPeopleByUnitID(unit.ID).ToList();
            }
            return list;
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
            return Dao.GetUnitByUnitID(iUnitID);
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
            var list = Dao.GetRelUnitList(iUnitID);
            foreach (EHECD_Unit unit in list)
            {
                unit.ClientList = ClientDao.Instance.GetDutyPeopleByUnitID(unit.ID).ToList();
            }
            return list;
        }

        #endregion

        #region 获取值班列表

        /// <summary>
        /// 获取值班列表
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "duty.getlist", desc: "获取值班列表")]
        public ResultMessage GetDutyRecordList(int iClientID)
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取值班列表失败";
                return result;
            }

            EHECD_Client entity = ClientDao.Instance.GetClient(iClientID);

            var list = new List<EHECD_Unit>();

            try
            {
                switch (entity.iType)
                {
                    case 0://点检员
                        list = Dao.GetClientRelUnitList(entity.ID).ToList();
                        break;
                    case 1://消防
                        list = Dao.GetRelUnitListByFireDeptID(entity.iUnitID).ToList();
                        break;
                    case 2://维护
                        list = Dao.GetRelUseDeptList(entity.iUnitID).ToList();
                        break;
                    case 4://值班
                        list.Add(Dao.Get(entity.iUnitID));
                        break;
                }
                
                foreach (EHECD_Unit unit in list)
                {
                    unit.ClientList = ClientDao.Instance.GetDutyPeopleByUnitID(unit.ID).ToList();
                    unit.DeptList = DeptDao.Instance.GetListByUnitID(unit.ID).ToList();
                    if (unit.DeptList != null && unit.DeptList.Count > 0)
                    {
                        foreach (EHECD_Dept dept in unit.DeptList)
                        {
                            dept.ClientList = ClientDao.Instance.GetClientByUnitAndDeptID(unit.ID, dept.ID).ToList();
                        }
                    }
                }
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取值班列表成功" : "获取值班列表失败";
            return result;
        }

        #endregion

        #region 获取使用单位已关联的维护公司列表

        /// <summary>
        /// 获取使用单位已关联的维护公司列表
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        [APIAttribute(name: "unit.getrprlist", desc: "获取使用单位已关联的维护公司列表")]
        public ResultMessage GetRepairDeptList(int iUnitID)
        {
            ResultMessage result = new ResultMessage();

            if (iUnitID == 0)
            {
                result.message = "获取使用单位已关联的维护公司失败";
                return result;
            }

            var list = new List<EHECD_Unit>();

            try
            {
                list = Dao.GetRelRepairDeptAllList(iUnitID).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取使用单位已关联的维护公司成功" : "获取使用单位已关联的维护公司失败";
            return result;
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
            return Dao.GetUnitListBySettingsID(iUseDeptSettingsID);
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
            return Dao.GetRelRepairDeptAllList(iUnitID);
        }

        #endregion

        #region 获取单位详情

        /// <summary>
        /// 获取单位详情
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        [APIAttribute(name: "unit.get", desc: "获取单位详情")]
        public ResultMessage GetInfo(int iUnitID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Unit entity = Dao.GetInfo(iUnitID) ?? new EHECD_Unit();
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取单位详情成功" : "获取单位详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
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
            return Dao.GetClientRelUnitList(iClientID);
        }

        #endregion
    }
}