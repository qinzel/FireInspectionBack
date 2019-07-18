using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using EHECD.Common;
using EHECD.EntityFramework.EFWork;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Service
{
    public class ModuleService
    {
        static ModuleService instance;
        private ModuleService() { }

        static public ModuleService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ModuleService();
                return instance;
            }
        }

        #region 获取模块信息

        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public EHECD_Module Get(long ID)
        {
            using (var Context = new Entities())
            {
                return Context.EHECD_Module.Find(ID);
            }
        }

        #endregion

        #region 获取所有模块

        /// <summary>
        /// 获取所有模块
        /// </summary>
        /// <returns></returns>
        public List<EHECD_Module> GetList()
        {
            using (var Context = new Entities())
            {
                List<EHECD_Module> list = Context.EHECD_Module.Where(o => o.bIsDeleted == false)
                            .OrderBy(x => x.iOrderID)
                            .ToList()
                            ;
                return list;
            }
        }

        #endregion

        #region 获取无连接的模块json数据

        /// <summary>
        /// 获取无连接的模块json数据
        /// </summary>
        /// <returns></returns>
        public string GetNoLinkForcombotree()
        {
            using (var Context = new Entities())
            {
                List<EHECD_Module> list = Context.EHECD_Module.Where(o => o.bIsDeleted == false && o.bIsLink == false)
                            .OrderBy(x => x.iOrderID)
                            .ToList()
                            ;
                List<jsontree> treeData = list.Where(o => o.iPID == 0).ToList().ConvertAll(o => new jsontree
                {
                    id = o.ID,
                    text = o.sModuleName
                });

                foreach (var item in treeData)
                {
                    var seleteChildren = list.Where(o => o.iPID == item.id).ToList();
                    if (seleteChildren.Count > 0)
                    {
                        item.children = new List<jsontree>();
                        foreach (var node in seleteChildren)
                        {
                            item.children.Add(new jsontree
                            {
                                id = node.ID,
                                text = node.sModuleName
                            });
                            list.Remove(node);
                        }
                    }
                }
                return JsonConvert.SerializeObject(treeData);
            }
        }

        #endregion

        #region 获取顶级模块

        /// <summary>
        /// 获取顶级模块
        /// </summary>
        /// <returns></returns>
        public List<EHECD_Module> GetTopList()
        {
            using (var Context = new Entities())
            {
                List<EHECD_Module> list = Context.EHECD_Module.Where(o => o.bIsDeleted == false && o.iPID == 0)
                            .OrderBy(x => x.iOrderID)
                            .ToList()
                            ;
                return list;
            }
        }

        #endregion

        #region 获取没有链接的模块

        /// <summary>
        /// 获取没有链接的模块
        /// </summary>
        /// <returns></returns>
        public List<EHECD_Module> GetNoLinkList()
        {
            using (var Context = new Entities())
            {
                List<EHECD_Module> list = Context.EHECD_Module.Where(o => o.bIsDeleted == false && o.bIsLink == false)
                            .OrderBy(x => x.iOrderID)
                            .ToList()
                            ;
                return list;
            }
        }

        #endregion

        #region 获取列表

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="rows">一页的条数</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public string GetChildrenList(int rows, int page, string sPID, string sModuleName)
        {
            using (var Context = new Entities())
            {
                JObject jResult = new JObject();
                var query = from a in Context.EHECD_Module
                            where !a.bIsDeleted && a.iPID != 0
                            //匿名类，查询出你自己需要的字段
                            select a;
                query = query.OrderBy(m => m.iOrderID);
                if (!string.IsNullOrEmpty(sPID))
                {
                    long iPID = TConvert.toInt64(sPID);
                    query =
                        query.Where(
                            m => Context.EHECD_Module.Where(x => x.iPID == iPID || x.ID == iPID).Select(n => n.ID).Contains(m.iPID));
                }
                if (!string.IsNullOrEmpty(sModuleName))
                {
                    query = query.Where(o => o.sModuleName.Contains(sModuleName));
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

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string Delete(string ids)
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
                    List<EHECD_Module> List = Context.EHECD_Module.Where(m => IDList.Contains(m.ID)).ToList();
                    foreach (EHECD_Module item in List)
                    {
                        item.bIsDeleted = true;
                    }
                    Context.SaveChanges();
                    return TCommon.setSucc(true, "删除成功");
                }
                catch (Exception ex)
                {
                    return TCommon.setSucc(false, "删除失败");
                }
            }
        }

        #endregion

        #region 添加模块

        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ModuleActionList"></param>
        /// <returns></returns>
        public string Add(EHECD_Module item, List<EHECD_ModuleAction> ModuleActionList)
        {
            using (var Context = new Entities())
            {
                try
                {
                    //添加模块
                    Context.EHECD_Module.Add(item);
                    //添加模块权限
                    foreach (EHECD_ModuleAction Action in ModuleActionList)
                    {
                        Action.iModuleID = item.ID;
                        Context.EHECD_ModuleAction.Add(Action);
                    }
                    Context.SaveChanges();
                    return TCommon.setSucc(true, "新建成功");
                }
                catch (Exception ex)
                {
                    return TCommon.setSucc(false, "新建失败");
                }
            }
        }

        #endregion

        #region 编辑模块

        /// <summary>
        /// 编辑模块
        /// </summary>
        /// <param name="item">模块</param>
        /// <param name="List">模块权限</param>
        /// <returns></returns>
        public string Edit(EHECD_Module item, List<EHECD_ModuleAction> ModuleActionList)
        {
            using (var Context = new Entities())
            {
                try
                {
                    var olditem = Context.EHECD_Module.Find(item.ID);
                    //编辑模块
                    olditem.iPID = item.iPID;
                    olditem.sModuleName = item.sModuleName;
                    olditem.sModuleUrl = item.sModuleUrl;
                    olditem.iOrderID = item.iOrderID;
                    olditem.bIsLink = item.bIsLink;
                    //编辑模块权限
                    var oldActionList = Context.EHECD_ModuleAction.Where(m => m.iModuleID == item.ID);
                    foreach (EHECD_ModuleAction oldAction in oldActionList)
                    {//删除旧的
                        Context.EHECD_ModuleAction.Remove(oldAction);
                    }
                    foreach (EHECD_ModuleAction RoleAction in ModuleActionList)
                    {//添加新的
                        RoleAction.iModuleID = item.ID;
                        Context.EHECD_ModuleAction.Add(RoleAction);
                    }

                    Context.SaveChanges();
                    return TCommon.setSucc(true, "编辑成功");
                }
                catch (Exception ex)
                {
                    return TCommon.setSucc(false, "编辑失败");
                }
            }
        }

        #endregion
    }
}