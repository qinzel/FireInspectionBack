using System;
using System.Collections.Generic;
using System.Linq;
using EHECD.Common;
using EHECD.EntityFramework.EFWork;
using EHECD.EntityFramework.Models;
using EHECD.FirePatrolInspection.Entity;

namespace EHECD.FirePatrolInspection.Service
{
    public class UserService
    {
        static UserService instance;
        private UserService() { }

        static public UserService Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserService();
                return instance;
            }
        }

        #region 登录

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sLoginName"></param>
        /// <param name="sPassWord"></param>
        /// <returns></returns>
        public ResultMessage Login(string sLoginName, string sPassWord)
        {
            ResultMessage msg = new ResultMessage();
            using (var Context = new Entities())
            {
                sPassWord = TCommon.Md5(sPassWord);
                long ID = 0;
                string sRealName = string.Empty;
                long iRoleID = 0;
                UserType Type = UserType.Admin;
                List<RoleAction> UserActionList = new List<RoleAction>();
                //后台用户登录
                EHECD_User User = Context.EHECD_User.FirstOrDefault(m => m.sLoginName == sLoginName && m.sPassWord == sPassWord && !m.bIsDeleted);
                if (null == User)
                {
                        msg.message = "用户名或密码错误";
                        return msg;
                }
                else
                {
                    ID = User.ID;
                    sRealName = User.sRealName;
                    iRoleID = User.iRoleID;
                    Type = (UserType)Enum.ToObject(typeof(UserType), User.iUserType);
                }

                List<EHECD_RoleAction> EHECD_RoleActionList = RoleActionService.Instance.GetRoleActionListByRoleID(iRoleID);
                List<RoleAction> RoleActionList = new List<RoleAction>();
                foreach (EHECD_RoleAction item in EHECD_RoleActionList)
                {
                    RoleAction RoleAction = new RoleAction()
                    {
                        iActionID = item.iActionID,
                        iModuleID = item.iModuleID,
                        iRoleID = item.iRoleID
                    };
                    RoleActionList.Add(RoleAction);
                }
                LoginUser loginUser = new LoginUser()
                {
                    ID = ID,
                    sLoginName = sLoginName,
                    sRealName = sRealName,
                    iRoleID = iRoleID,
                    UserActionList = RoleActionList,
                    UserType = Type
                };
                ActionLogService.Instance.Add(Context, loginUser.ID, "用户登录", User.sLoginName + " 登录成功");
                msg.success = true;
                msg.message = "登录成功";
                msg.data = loginUser;
                return msg;
            }
        }

        #endregion

        #region 通用方法

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="param">通用查询条件</param>
        /// <returns></returns>
        public PagedList GetGridData(QueryParams param, string iRoleID)
        {
            using (var Context = new Entities())
            {
                var query = from a in Context.EHECD_User
                            join r in Context.EHECD_Role on a.iRoleID equals r.ID into temp
                            from r1 in temp.Where(t => !t.bIsDeleted).DefaultIfEmpty()
                            where !a.bIsDeleted
                            //匿名类，查询出你自己需要的字段
                            select new
                            {
                                a.ID,
                                a.sLoginName,
                                a.sRealName,
                                a.iRoleID,
                                a.iUserType,
                                r1.sRoleName,
                                a.bIsDeleted,
                                a.dCreateTime
                            };
                if (!string.IsNullOrEmpty(param.keyword))
                {//关键字查询
                    query = query.Where(m => m.sLoginName.Contains(param.keyword) || m.sRealName.Contains(param.keyword));
                }
                if (!string.IsNullOrEmpty(iRoleID))
                {//关键字查询
                    long iroleID = TConvert.toInt64(iRoleID);
                    query = query.Where(m => m.iRoleID == iroleID);
                }
                query = query.Where(m => m.iUserType != 1);
                if (string.IsNullOrEmpty(param.order))
                {
                    param.order = "desc";
                }
                //排序处理  升/降 序  
                switch (param.sort)
                {
                    //修改位置
                    case "sLoginName":
                        query = param.order.Equals("asc") ? query.OrderBy(o => o.sLoginName) : query.OrderByDescending(o => o.sLoginName);
                        break;
                    case "sRealName":
                        query = param.order.Equals("asc") ? query.OrderBy(o => o.sRealName) : query.OrderByDescending(o => o.sRealName);
                        break;
                    case "iUserType":
                        query = param.order.Equals("asc") ? query.OrderBy(o => o.iUserType) : query.OrderByDescending(o => o.iUserType);
                        break;
                    case "sRoleName":
                        query = param.order.Equals("asc") ? query.OrderBy(o => o.sRoleName) : query.OrderByDescending(o => o.sRoleName);
                        break;
                    default:
                        query = param.order.Equals("asc") ? query.OrderBy(o => o.ID) : query.OrderByDescending(o => o.ID);
                        break;
                }

                PagedList PagedList = query.EHECDAsPagedList(param.page, param.rows);//分页后分页后的对象,如果要返回json字符串,请使用EHECDAsPagedString

                return PagedList;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public EHECD_User Get(long id)
        {
            using (var Context = new Entities())
            {
                return Context.EHECD_User.Find(id);
            }
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ResultMessage Add(EHECD_User item)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                using (var Context = new Entities())
                {
                    if (Context.EHECD_User.Any(m => m.sLoginName == item.sLoginName && !m.bIsDeleted))
                    {//登录名重复 
                        //result.success = false; (ResultMessage.success 默认是false，不用重新赋值)
                        result.message = "用户名已存在!";
                    }
                    else
                    {
                        item.iUserType = 0;
                        Context.EHECD_User.Add(item);
                        Context.SaveChanges();
                        var user = UserSession.GetLogUser();
                        ActionLogService.Instance.Add(Context, user.ID, "新增后台用户",
                            string.Format("新增后台用户:{0},登录账号:{1}", item.sRealName, item.sLoginName));
                        result.success = true;
                        result.message = "新建用户成功";
                    }
                }
            }
            catch (Exception ex)
            {
                result.message = "新建用户失败";

            }

            return result;
        }

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="item">用户</param>
        /// <returns></returns>
        public ResultMessage Edit(EHECD_User item)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                using (var Context = new Entities())
                {
                    var olditem = Context.EHECD_User.Find(item.ID);
                    if (Context.EHECD_User.Count(m => m.sLoginName == item.sLoginName && m.ID != item.ID && !m.bIsDeleted) > 0)
                    {//登录名重复 
                        result.message = "登录账号已存在";
                    }
                    else
                    {
                        string sLog = string.Format("编辑后台用户:{0},登录账号:{1}->{2},真实姓名:{3}->{4}", olditem.sRealName, olditem.sLoginName, item.sLoginName, olditem.sRealName, item.sRealName);//日志内容
                        //编辑用户
                        olditem.sLoginName = item.sLoginName;
                        olditem.sRealName = item.sRealName;
                        olditem.iRoleID = item.iRoleID;
                        olditem.iUserType = item.iUserType;
                        Context.SaveChanges();
                        var user = UserSession.GetLogUser();
                        ActionLogService.Instance.Add(Context, user.ID, "编辑后台用户", sLog);

                        result.success = true;
                        result.message = "编辑用户成功";
                    }
                }
            }
            catch (Exception ex)
            {
                result.message = "编辑用户失败";
            }
            return result;
        }

        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultMessage Delete(string ids)
        {
            ResultMessage result = new ResultMessage();

            try
            {
                using (var Context = new Entities())
                {
                    List<long> IDList = new List<long>();
                    foreach (string sID in ids.Split(','))
                    {
                        IDList.Add(TConvert.toInt64(sID));
                    }
                    var userNameStr = String.Empty;
                    List<EHECD_User> UserList = Context.EHECD_User.Where(m => IDList.Contains(m.ID)).ToList();
                    foreach (EHECD_User User in UserList)
                    {
                        User.bIsDeleted = true;
                        userNameStr += User.sLoginName + ",";
                    }
                    userNameStr = userNameStr.TrimEnd(',');
                    Context.SaveChanges();
                    string sLog = string.Format("删除后台用户:{0}", userNameStr);//日志内容
                    var user = UserSession.GetLogUser();
                    ActionLogService.Instance.Add(Context, user.ID, "删除角色", sLog);

                    result.success = true;
                    result.message = "删除会员成功";
                }
            }
            catch (Exception ex)
            {
                result.message = "删除会员失败";
            }
            return result;
        }

        #endregion

        #region 重置密码

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMessage ResetPwd(EHECD_User item)
        {
            ResultMessage result = new ResultMessage();

            try
            {
                using (var Context = new Entities())
                {
                    EHECD_User olditem = Context.EHECD_User.Find(item.ID);
                    olditem.sPassWord = item.sPassWord;
                    Context.SaveChanges();
                    string sLog = string.Format("重置密码:{0}", item.ID);//日志内容
                    var user = UserSession.GetLogUser();
                    ActionLogService.Instance.Add(Context, user.ID, "重置密码", sLog);

                    result.success = true;
                    result.message = "重置密码成功";
                }
            }
            catch (Exception ex)
            {
                result.message = "重置密码失败";
            }
            return result;
        }

        #endregion

        #region 修改当前用户登录密码

        /// <summary>
        /// 修改当前用户登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="sPwd"></param>
        /// <returns></returns>
        public ResultMessage EditPwd(long iUserID,string sOldPwd, string sPwd)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                using (var Context = new Entities())
                {
                    EHECD_User user = Context.EHECD_User.Find(iUserID);
                    if (user == null)
                    {
                        result.message = "该用户不存在";
                        return result;
                    }
                    if (user.sPassWord.ToLower() != TCommon.Md5(sOldPwd).ToLower())
                    {
                        result.message = "旧密码不正确";
                        return result;
                    }

                    user.sPassWord = TCommon.Md5(sPwd);
                    Context.SaveChanges();

                    result.success = true;
                    result.message = "密码修改成功";

                }
            }
            catch (Exception ex)
            {
                result.message = "密码修改失败";
            }
            return result;
        }

        #endregion

        #region 获取总后台用户数

        /// <summary>
        /// 获取总后台用户数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            using (var Context = new Entities())
            {
                var query = from a in Context.EHECD_User
                            where !a.bIsDeleted
                            //匿名类，查询出你自己需要的字段
                            select new
                            {
                                a.ID,
                                a.sLoginName,
                                a.sRealName,
                                a.iRoleID,
                                a.iUserType,
                                a.bIsDeleted,
                                a.dCreateTime
                            };

                return query.Count();
            }
        }

        #endregion
    }
}