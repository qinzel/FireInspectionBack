
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using System.Web;
using System.Linq;
using EHECD.WebApi.Attributes;
using EHECD.EntityFramework.Models;
using Microsoft.AspNet.SignalR;
using EHECD.Core.APIHelper;
using EHECD.Core.Push;
using System.Threading;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 前端用户业务逻辑
    /// </summary>
    public class ClientService
    {
        static ClientService instance = new ClientService();
        private static ClientDao Dao = ClientDao.Instance;
        public static object async = new object();

        private ClientService()
        {
        }

        public static ClientService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (async)
                    {
                        if (instance == null)
                        {
                            instance = new ClientService();
                        }
                    }

                }
                return instance;
            }
        }

        #region 总后台获取前端用户数据

        /// <summary>
        /// 总后台获取前端用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetTotalGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetTotalList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取前端用户数据

        /// <summary>
        /// 获取前端用户数据
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

        #region 获取前端用户数据

        /// <summary>
        /// 获取前端用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetUnitGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetUnitList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 添加编辑人员

        /// <summary>
        /// 添加编辑人员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Client entity)
        {
            ResultMessage result = new ResultMessage();

            if (entity.ID == 0)
            {
                //新增人员
                entity.sPwd = TCommon.Md5(entity.sPwd);
                var iVal = Dao.Create(entity);
                switch (iVal)
                {
                    case -1:
                        result.message = "添加失败";
                        break;
                    case -2:
                        result.message = "账号/电话已经存在";
                        break;
                }
                result.success = iVal > 0 ? true : false;
                result.message = result.success ? "添加成功" : result.message;
            }
            else
            {
                //修改人员
                result.success = Dao.Modify(entity);
                result.message = result.success ? "编辑成功" : "账号/电话已经存在";
            }
            return result;
        }

        #endregion

        #region 获取值班用户数据

        /// <summary>
        /// 获取值班用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetDutyGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetDutyList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 根据部门关系获取前端用户数据

        /// <summary>
        /// 根据部门关系获取前端用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetInspectorsByDeptIDGridData(QueryParams param)
        {
			int iTotalRecord = 0;
            var list = Dao.GetInspectorsByDeptIDList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
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
            return Dao.GetClientsByPhones(sPhones);
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
            EHECD_Client entity = Dao.GetFireDeptClient(iClientID) ?? new EHECD_Client();
            if (entity.ID > 0)
            {
                entity.DeptList = DeptDao.Instance.GetListByClientID(entity.ID).ToList();
            }
            return entity;
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
            EHECD_Client entity = Dao.GetRepairDeptClient(iClientID) ?? new EHECD_Client();
            if (entity.ID > 0)
            {
                entity.DeptList = DeptDao.Instance.GetListByClientID(entity.ID).ToList();
            }
            return entity;
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
            EHECD_Client entity = Dao.Get(iClientID) ?? new EHECD_Client();
            if (entity.ID > 0)
            {
                entity.DeptList = DeptDao.Instance.GetListByClientID(entity.ID).ToList();
            }
            return entity;
        }

        #endregion

        #region 获取前端用户详情（非点检员）

        /// <summary>
        /// 获取前端用户详情（非点检员）
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Client GetClient(int iClientID)
        {
            return Dao.GetClient(iClientID) ?? new EHECD_Client();
        }

        #endregion

        #region 获取前端待审核用户详情

        /// <summary>
        /// 获取前端用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public EHECD_Client GetAdoptInfo(int iClientID)
        {
            EHECD_Client entity = Dao.GetAdoptInfo(iClientID) ?? new EHECD_Client();
            if (entity.ID > 0)
            {
                entity.DeptList = DeptDao.Instance.GetListByClientID(entity.ID).ToList();
            }
            return entity;
        }

        #endregion

        #region 编辑前端用户

        /// <summary>
        /// 编辑前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Update(EHECD_Client entity)
        {
            ResultMessage result = new ResultMessage();

            //修改前端用户
            result.success = Dao.Update(entity);
            result.message = result.success ? "编辑成功" : "编辑失败";
            return result;
        }

        #endregion

        #region 添加编辑前端用户

        /// <summary>
        /// 添加编辑前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage SetPeople(EHECD_Client entity)
        {
            ResultMessage result = new ResultMessage();

            if (entity.ID == 0)
            {
                //添加前端用户
                entity.iType = 4;
                entity.sPwd = TCommon.Md5(entity.sPwd);
                var iVal = Dao.Create(entity);
                switch (iVal)
                {
                    case -1:
                        result.message = "添加失败";
                        break;
                    case -2:
                        result.message = "账号/电话已经存在";
                        break;
                }
                result.success = iVal > 0 ? true : false;
                result.message = result.success ? "添加成功" : result.message;
            }
            else
            {
                //修改前端用户
                result.success = Dao.UpdatePeople(entity);
                result.message = result.success ? "编辑成功" : "账号/电话已经存在";
            }
            return result;
        }

        #endregion

        #region 批量删除前端用户

        /// <summary>
        /// 批量删除前端用户
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除前端用户成功" : "删除前端用户失败";
            return result;
        }

        #endregion

        #region 批量重置前端用户密码

        /// <summary>
        /// 批量重置前端用户密码
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Reset(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Reset(sIds, TCommon.Md5("123456"))
            };
            result.message = result.success ? "重置密码成功" : "重置密码失败";
            return result;
        }

        #endregion

        #region 冻结用户信息

        /// <summary>
        /// 冻结用户信息
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public ResultMessage Frozen(int iClientID)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Frozen(iClientID)
            };
            result.message = result.success ? "冻结成功" : "冻结失败";
            return result;
        }

        #endregion

        #region 解冻用户信息

        /// <summary>
        /// 解冻用户信息
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public ResultMessage UnFrozen(int iClientID)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.UnFrozen(iClientID)
            };
            result.message = result.success ? "解冻成功" : "解冻失败";
            return result;
        }

        #endregion

        #region 用户注册

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sValidate"></param>
        /// <param name="sPwd"></param>
        /// <param name="sName"></param>
        /// <param name="sUnitIds"></param>
        /// <param name="sDeptIds"></param>
        /// <param name="sCredentials"></param>
        /// <param name="sImageSrc"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.register", desc: "用户注册")]
        public ResultMessage Register(string sPhone, string sValidate, string sPwd, string sName, string sUnitIds = "", string sDeptIds = "", string sCredentials = "", string sImageSrc = "")
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

                if (string.IsNullOrWhiteSpace(sUnitIds) || string.IsNullOrWhiteSpace(sDeptIds))
                {
                    result.message = "所属单位和部门不能空";
                    return result;
                }
                if (!string.IsNullOrWhiteSpace(sUnitIds) && !string.IsNullOrWhiteSpace(sDeptIds))
                {
                    var units = sUnitIds.Split(',');
                    var depts = sDeptIds.Split(',');
                    if (units.Length != depts.Length)
                    {
                        result.message = "单位或部门选择未完成";
                        return result;
                    }
                }

                // 验证短信合法性
                result = PhoneMsgService.Instance.Check(sPhone, sValidate, 0);

                if (!result.success)
                {// 短信验证不通过
                    return result;
                }

                EHECD_Client newClient = new EHECD_Client()
                {
                    sPhone = sPhone,
                    sPwd = TCommon.Md5(sPwd),
                    sName = sName,
                    sCredentials = sCredentials,
                    sImageSrc = sImageSrc,
                    sUnitIds = sUnitIds.Split(',').ToList(),
                    sDeptIds = sDeptIds.Split(',').ToList()
                };

                newClient = Dao.Register(newClient);

                HttpCookie cookie = new HttpCookie("ClientID", newClient.ID.ToString());
                cookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
                result.success = true;
                if (result.success)
                {
                    PushHub hub = new PushHub();
                    IHubContext chat = GlobalHost.ConnectionManager.GetHubContext<PushHub>();
                    if (newClient.iType == 0)
                    {
                        var list = UnitDao.Instance.GetUnAdoptClientRelUnitList(newClient.ID);
                        if (list != null)
                        {
                            foreach (EHECD_Unit unit in list)
                            {
                                hub.SendMessage(unit.ID + "." + unit.sName, "你有一条新的申请");
                            }
                        }
                    }
                    else
                    {
                        EHECD_Unit unit = UnitDao.Instance.Get(newClient.iUnitID);
                        hub.SendMessage(unit.ID + "." + unit.sName, "你有一条新的申请");
                    }
                }
                result.message = "注册成功";
                result.data = newClient;
                return result;
            }
        }

        #endregion

        #region 用户登录

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.login", desc: "用户登录")]
        [ClientAPI]
        public ResultMessage Login(string sPhone, string sPwd)
        {
            ResultMessage result = new ResultMessage();
            lock (async)
            {
                var entity = Dao.GetByPhone(sPhone);
                if (entity == null)
                {
                    result.message = "手机号不存在";
                    return result;
                }

                entity = Dao.Login(sPhone, TCommon.Md5(sPwd));
                if (entity == null)
                {
                    result.message = "密码错误";
                    return result;
                }
                if (entity.iStatus)
                {
                    // 冻结
                    result.message = "账号已被冻结，无法登录";
                    return result;
                }
                if (entity.iType == 0)
                {
                    // 验证是否至少有一个使用单位通过审核 iStatus
                    //IEnumerable<EHECD_ClientDeptRel> relList = Dao.GetDeptRelByClientID(entity.ID);
                    //if (relList.Count() == 0)
                    //{
                    //    result.message = "还没有任何单位通过申请，请等待";
                    //    return result;
                    //}

                    //验证是否至少有一个未冻结的使用单位通过审核 iStatus
                    IEnumerable<EHECD_Unit> relList = UnitDao.Instance.GetClientRelUnitListContainsFreeze(entity.ID);
                    if (relList.Count() == 0)
                    {
                        result.message = "还没有任何单位通过申请，请等待";
                        return result;
                    }else if(relList.First(x=>!x.iStatus) == null)
                    {
                        result.message = "使用单位已被冻结，无法登陆";
                        return result;
                    }
                }
                else
                {
                    entity.sUnitName = UnitDao.Instance.Get(entity.iUnitID).sName;
                }

                HttpCookie cookie = new HttpCookie("ClientID", entity.ID.ToString());
                cookie.Expires = DateTime.Now.AddDays(30);
                HttpContext.Current.Response.Cookies.Add(cookie);
                result.success = true;
                result.message = "登录成功";
                entity.sPwd = string.Empty;
                result.data = entity;
                return result;
            }
        }

        #endregion

        #region 获取前端用户详情

        /// <summary>
        /// 获取前端用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.get", desc: "获取前端用户详情")]
        public ResultMessage GetClientInfo(int iClientID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Client entity = new EHECD_Client();
            try
            {
                entity = Dao.GetClientInfo(iClientID);
                result.success = true;
            }
            catch { }
            result.message = result.success ? "获取前端用户详情成功" : "获取前端用户详情失败";
            result.data = entity;
            return result;
        }

        #endregion

        #region 获取前端用户详情

        /// <summary>
        /// 获取前端用户详情
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.self", desc: "获取前端用户详情")]
        public ResultMessage GetClientSelfInfo(int iClientID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Client entity = new EHECD_Client();
            try
            {
                entity = Dao.Get(iClientID);
                result.success = true;
            }
            catch { }
            result.message = result.success ? "获取前端用户详情成功" : "获取前端用户详情失败";
            result.data = entity;
            return result;
        }

        #endregion

        #region 找回密码

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sValidate"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.findpwd", desc: "找回密码")]
        public ResultMessage FindPwd(string sPhone, string sValidate, string sPwd)
        {
            ResultMessage result = new ResultMessage();
            lock (async)
            {
                var entity = Dao.GetByPhone(sPhone);
                if (entity == null)
                {
                    result.message = "账户不存在";
                    return result;
                }

                // 验证短信合法性
                result = PhoneMsgService.Instance.Check(sPhone, sValidate, 1);

                if (!result.success)
                {// 短信验证不通过
                    return result;
                }

                entity.sPwd = TCommon.Md5(sPwd);
                result.success = Dao.SetPwd(entity);
                result.message = result.success ? "密码修改成功，请重新登录" : "密码找回失败";
                return result;
            }
        }

        #endregion

        #region 修改登录密码

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sOldPwd"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.editpwd", desc: "修改登录密码")]
        public ResultMessage EditPwd(int iClientID, string sOldPwd, string sPwd)
        {
            ResultMessage result = new ResultMessage();
            var entity = Dao.GetClient(iClientID);
            if (entity == null)
            {
                result.message = "用户不存在";
                return result;
            }
            if (entity != null && entity.iStatus)
            {
                result.message = "账号已被冻结";
                return result;
            }
            if (!entity.sPwd.Equals(TCommon.Md5(sOldPwd)))
            {
                result.message = "旧密码错误，请确认后重试";
                return result;
            }
            result.success = Dao.EditPwd(iClientID, TCommon.Md5(sPwd));
            result.message = result.success ? "密码修改成功" : "密码修改失败";
            return result;
        }

        #endregion

        #region 修改会员头像

        /// <summary>
        /// 修改会员头像
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sImageSrc"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.changeimage", desc: "修改会员头像")]
        public ResultMessage ChangeHeadImageByID(int iClientID, string sImageSrc)
        {
            ResultMessage result = new ResultMessage();
            result.success = Dao.ChangeHeadImageByID(iClientID, sImageSrc);
            result.message = result.success ? "修改头像成功" : "修改头像失败";
            return result;
        }

        #endregion

        #region 根据使用单位ID获取点检员信息

        /// <summary>
        /// 根据使用单位ID获取点检员信息
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.getlist", desc: "获取点检员列表")]
        public ResultMessage GetParam(int iUnitID)
        {
            ResultMessage result = new ResultMessage();

            if (iUnitID == 0)
            {
                result.message = "获取点检员列表失败";
                return result;
            }

            var list = new List<EHECD_Client>();
            try
            {
                list = Dao.GetAdoptListByUnitID(iUnitID).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取点检员列表成功" : "获取点检员列表失败";
            return result;
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
            return Dao.GetListByType(iType);
        }

        #endregion

        #region 获取所有点检员账号申请待审核信息

        /// <summary>
        /// 获取所有点检员账号申请待审核信息
        /// </summary>
        /// <returns></returns>
        public int GetApplyList()
        {
            return Dao.GetApplyList();
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
            return Dao.GetApplyListByUnitID(iUnitID);
        }

        #endregion

        #region 点检员申请列表

        /// <summary>
        /// 点检员申请列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetAdoptList(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetAdoptList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 通过审核

        /// <summary>
        /// 通过审核
        /// </summary>
        /// <param name="iClientDeptRelID"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Adopt(int iClientDeptRelID, LoginUser user)
        {
            ResultMessage result = new ResultMessage();
            EHECD_ClientDeptRel entity = Dao.GetRel(iClientDeptRelID);
            EHECD_Client client = Dao.GetClient(entity.iClientID);
            if (entity.iAuditState > 0)
            {
                result.message = "已审数据无法再次审核";
                return result;
            }
            entity.sOperator = user.sLoginName;
            result.success = Dao.AdoptRel(entity);
            result.message = result.success ? "通过审核成功" : "通过审核失败";
            AliSmsService.Instance.SendApplyAdoptMessage(client.sPhone);
            return result;
        }

        #endregion

        #region 拒绝通过

        /// <summary>
        /// 拒绝通过
        /// </summary>
        /// <param name="iClientDeptRelID"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResultMessage Refused(int iClientDeptRelID, LoginUser user)
        {
            ResultMessage result = new ResultMessage();
            EHECD_ClientDeptRel entity = Dao.GetRel(iClientDeptRelID);
            EHECD_Client client = Dao.GetClient(entity.iClientID);
            if (entity.iAuditState > 0)
            {
                result.message = "已审数据无法再次审核";
                return result;
            }
            entity.sOperator = user.sLoginName;
            result.success = Dao.RefusedRel(entity);
            result.message = result.success ? "拒绝通过成功" : "拒绝通过失败";
            AliSmsService.Instance.SendApplyRefusedMessage(client.sPhone, user.sRealName);
            return result;
        }

        #endregion

        #region 获取使用单位前端人员信息

        /// <summary>
        /// 获取使用单位前端人员信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetUseDataList(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetUseDataList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 根据使用单位ID获取点检员信息

        /// <summary>
        /// 获取值班列表
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "client.getdutylist", desc: "获取值班列表")]
        public ResultMessage GetDutyList(int iClientID)
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取值班列表失败";
                return result;
            }

            EHECD_Client entity = GetClient(iClientID);

            var list = new List<EHECD_Client>();
            try
            {
                switch (entity.iType)
                {
                    case 0://点检员
                        list = Dao.GetDutyPeopleByClientID(entity.ID).ToList();
                        break;
                    case 1://消防
                        list = Dao.GetDutyPeopleByFireDeptID(entity.iUnitID).ToList();
                        break;
                    case 2://维护
                        list = Dao.GetDutyPeopleByRepairDeptID(entity.iUnitID).ToList();
                        break;
                    case 4://值班
                        list = Dao.GetDutyPeopleByUnitID(entity.iUnitID).ToList();
                        break;
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

        #region 获取当月未巡检的点检员

        /// <summary>
        /// 获取当月未巡检的点检员
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Client> GetListWithoutIns()
        {
            var startTime = DateTime.Now.ToString("yyyy-MM-01 00:00:00");
            var endTime = DateTime.Now.ToString("yyyy-MM-24 23:59:59");
            return Dao.GetListWithoutIns(startTime, endTime);
        }

        #endregion

        #region 修改所属使用单位/部门

        /// <summary>
        /// 修改所属使用单位
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="sOperator"></param>
        /// <returns></returns>
        public ResultMessage Change(EHECD_Client entity, string sOperator)
        {
            ResultMessage result = new ResultMessage();

            int iDeviceCount = DeviceDao.Instance.GetDeviceCountByClientID(entity.ID);
            if (iDeviceCount > 0)
            {
                result.message = "当前点检员已有负责设备，无法修改所属单位/部门";
                return result;
            }

            if (entity.sUnitIds.Count == 0 || entity.sDeptIds.Count == 0 || (entity.sUnitIds.Count != entity.sDeptIds.Count))
            {
                result.message = "单位或部门选择未完成";
                return result;
            }
            result.success = Dao.Change(entity, sOperator);
            result.message = result.success ? "修改成功" : "修改失败";
            return result;
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
            return Dao.GetClientByUseDeptID(iUnitID);
        }

        #endregion

        #region 用户设备ID信息

        [APIAttribute(name: "client.setcid", desc: "设置用户设备号")]
        [ClientAPI]

        public ResultMessage SetClientCID(string iClientID,string cid, string deviceType)
        {
            ResultMessage result = new ResultMessage()
            {
                success = true,
                message = ""
            };

            try
            {
                List<EHECD_CID> list = Dao.GetCIDByClientID(iClientID);
                if (list.Find(x => x.CID == cid) == null)
                {
                    result.success = Dao.SetClientCID(new EHECD_CID { CID = cid, DeviceType = deviceType, iClientID = iClientID });
                }
            }
            catch(Exception e)
            {
                result.success = false;
                result.message = "服务器错误";
            }
            
            return result;
        }

        #endregion
    }
}