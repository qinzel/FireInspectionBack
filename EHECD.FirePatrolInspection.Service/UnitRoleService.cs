using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using EHECD.Common;
using EHECD.EntityFramework.EFWork;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 单位角色业务逻辑
    /// </summary>
    public class UnitRoleService
    {
        static UnitRoleService instance;
        private UnitRoleService() { }

        static public UnitRoleService Instance
        {
            get
            {
                if (instance == null)
                    instance = new UnitRoleService();
                return instance;
            }
        }

        #region 获取角色列表

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="rows">一页的条数</param>
        /// <param name="page">页码</param>
        /// <param name="sort">排序字段</param>
        /// <param name="order">排序方式</param>
        /// <param name="sRoleName">角色名查询</param>
        /// <param name="iUnitID">单位ID</param>
        /// <returns></returns>
        public string GetList(int rows, int page, string sort, string order, string sRoleName, int iUnitID)
        {
            using (var Context = new Entities())
            {
                JObject jResult = new JObject();
                var query = from a in Context.EHECD_UnitRole
                            where !a.bIsDeleted && a.iUnitID == iUnitID
                            //匿名类，查询出你自己需要的字段
                            select a;
                //根据排序方法进行排序
                if (order == "asc")
                {
                    switch (sort)
                    {
                        case "sRoleName":
                            query = query.OrderBy(m => m.sRoleName);
                            break;
                        default:
                            query = query.OrderByDescending(m => m.ID);
                            break;
                    }
                }
                //判断是升序还是降序排序
                else if (order == "desc")
                {
                    switch (sort)
                    {
                        case "sRoleName":
                            query = query.OrderByDescending(m => m.sRoleName);
                            break;
                        default:
                            //先通过字段降序排序然后再通过，Content字段降序排序，最后通过CreateDate降序排序
                            query = query.OrderByDescending(m => m.ID);
                            break;
                    }
                }
                else
                {
                    query = query.OrderByDescending(m => m.ID);
                }
                //var query = entity.Templates.OrderByDescending(m => m.ID).Skip((int)(page - 1) * rows).Take(rows);

                if (!string.IsNullOrEmpty(sRoleName))
                {//关键字查询
                    query = query.Where(m => m.sRoleName.Contains(sRoleName));
                }

                int iCount = query.Count();
                query = query.Skip((int)(page - 1) * rows).Take(rows);

                var list = query.ToList();
                //拼接符合分页控件json格式的数据
                jResult.Add(new JProperty("page", page));
                jResult.Add(new JProperty("total", iCount));
                jResult.Add(new JProperty("rows", TCommon.ItemToJson(list)));
                return jResult.ToString();
            }
        }

        #endregion

        #region 获取所有角色列表

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <returns></returns>
        public List<EHECD_UnitRole> GetAllList()
        {
            using (var Context = new Entities())
            {
                List<EHECD_UnitRole> List = Context.EHECD_UnitRole.Where(m => !m.bIsDeleted).OrderByDescending(m => m.ID).ToList();
                return List;
            }
        }

        #endregion

        #region 新建角色

        /// <summary>
        /// 新建角色
        /// </summary>
        /// <param name="item">角色</param>
        /// <param name="List">角色权限</param>
        /// <param name="user">后台登录用户</param>
        /// <returns></returns>
        public string Add(EHECD_UnitRole item, List<EHECD_UnitRoleAction> List, LoginUser user)
        {
            using (var Context = new Entities())
            {
                try
                { //重名检查
                    if (Context.EHECD_UnitRole.Any(o => !o.bIsDeleted && o.sRoleName == item.sRoleName && o.iUnitID == item.iUnitID))
                    {
                        return TCommon.setSucc(false, "角色名【" + item.sRoleName + "】已存在！请确认后重试");
                    }
                    else
                    {
                        //添加角色
                        item.dCreateTime = DateTime.Now;
                        Context.EHECD_UnitRole.Add(item);
                        Context.SaveChanges();
                        //添加角色权限
                        foreach (EHECD_UnitRoleAction RoleAction in List)
                        {
                            RoleAction.iUnitRoleID = item.ID;
                            Context.EHECD_UnitRoleAction.Add(RoleAction);
                        }
                        Context.SaveChanges();
                        string sLog = string.Format("新建角色:{0}", item.sRoleName); //日志内容
                        UnitActionLogService.Instance.Add(Context, user.ID, user.iUnitID, "新建角色", sLog);
                        return TCommon.setSucc(true, "新建角色成功");
                    }
                }
                catch (Exception ex)
                {
                    UnitActionLogService.Instance.Add(Context, user.ID, user.iUnitID, "新建角色", "新建角色失败");
                    return TCommon.setSucc(false, "新建角色失败");
                }
            }
        }

        #endregion

        #region 编辑角色

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="item">角色</param>
        /// <param name="List">角色权限</param>
        /// <param name="user">后台登录用户</param>
        /// <returns></returns>
        public string Edit(EHECD_UnitRole item, List<EHECD_UnitRoleAction> List, LoginUser user)
        {
            using (var Context = new Entities())
            {
                try
                {
                    //重名检查
                    if (Context.EHECD_UnitRole.Any(o => !o.bIsDeleted && o.ID != item.ID && o.sRoleName == item.sRoleName && o.iUnitID == item.iUnitID))
                    {
                        return TCommon.setSucc(false, "角色名【" + item.sRoleName + "】已存在！请确认后重试");
                    }
                    else
                    {
                        var olditem = Context.EHECD_UnitRole.Find(item.ID);
                        string sLog = string.Format("编辑角色:{0},角色名:{1}->{2}", olditem.sRoleName,
                            olditem.sRoleName, item.sRoleName); //日志内容

                        //编辑角色
                        olditem.sRoleName = item.sRoleName;
                        olditem.sDescription = item.sDescription;

                        //编辑角色权限
                        var oldActionList = Context.EHECD_UnitRoleAction.Where(m => m.iUnitRoleID == item.ID);
                        foreach (EHECD_UnitRoleAction oldAction in oldActionList)
                        {
                            //删除旧的
                            Context.EHECD_UnitRoleAction.Remove(oldAction);
                        }
                        foreach (EHECD_UnitRoleAction RoleAction in List)
                        {
                            //添加新的
                            Context.EHECD_UnitRoleAction.Add(RoleAction);
                        }
                        Context.SaveChanges();
                        UnitActionLogService.Instance.Add(Context, user.ID, user.iUnitID, "编辑角色", sLog);
                        return TCommon.setSucc(true, "编辑角色成功");
                    }
                }
                catch (Exception ex)
                {
                    UnitActionLogService.Instance.Add(Context, user.ID, user.iUnitID, "编辑角色", "编辑角色失败");
                    return TCommon.setSucc(false, "编辑角色失败");
                }
            }
        }

        #endregion

        #region 获取角色信息

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        public EHECD_UnitRole Get(long id)
        {
            using (var Context = new Entities())
            {
                return Context.EHECD_UnitRole.Find(id);
            }
        }

        #endregion

        #region 删除角色

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public string Delete(string ids, LoginUser user)
        {
            using (var Context = new Entities())
            {
                try
                {
                    List<long> IDList = new List<long>();
                    foreach (string sID in ids.Split(','))
                    {
                        IDList.Add(TConvert.toInt64(sID));
                    }
                    var roleNameStr = String.Empty;
                    List<EHECD_UnitRole> RoleList = Context.EHECD_UnitRole.Where(m => IDList.Contains(m.ID)).ToList();
                    foreach (EHECD_UnitRole Role in RoleList)
                    {
                        if (Context.EHECD_UnitUser.Any(o => o.iUserType == 0 && o.iUnitRoleID == Role.ID && !o.bIsDeleted))
                        {
                            return TCommon.setSucc(false, "你选择的角色下面存在用户，所以不允许删除！");
                        }
                        Role.bIsDeleted = true;
                        roleNameStr += Role.sRoleName + ",";
                    }
                    roleNameStr = roleNameStr.TrimEnd(',');

                    Context.SaveChanges();
                    string sLog = string.Format("删除角色:{0}", roleNameStr);//日志内容
                    UnitActionLogService.Instance.Add(Context, user.ID, user.iUnitID, "删除角色", sLog);
                    return TCommon.setSucc(true, "删除角色成功");
                }
                catch (Exception ex)
                {
                    return TCommon.setSucc(false, "删除角色失败");
                }
            }
        }

        #endregion
    }
}

