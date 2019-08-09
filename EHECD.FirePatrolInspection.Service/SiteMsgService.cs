
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;
using MongoDB.Driver;
using EHECD.Core.Push;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 站内信业务逻辑
    /// </summary>
    public class SiteMsgService
    {
        static SiteMsgService instance = new SiteMsgService();
        private static SiteMsgDao Dao = SiteMsgDao.Instance;

        private SiteMsgService()
        {
        }

        public static SiteMsgService Instance
        {
            get { return instance; }
        }
		
		#region 获取站内信数据

        /// <summary>
        /// 获取站内信数据
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

		#region 获取站内信详情

        /// <summary>
        /// 获取站内信详情
        /// </summary>
        /// <param name="iSiteMsgID"></param>
        /// <returns></returns>
        public EHECD_SiteMsg Get(int iSiteMsgID)
        {
            return Dao.Get(iSiteMsgID) ?? new EHECD_SiteMsg();
        }
		
		#endregion
		
		#region 添加编辑站内信

        /// <summary>
        /// 添加编辑站内信
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_SiteMsg entity)
        {
            ResultMessage result = new ResultMessage();

            if (entity.iType == 1 && string.IsNullOrEmpty(entity.sReceiveClient))
            {
                result.message = "请填写收信人账号（多个收信人请用英文“,”分隔）";
                return result;
            }

            int newId = Dao.Insert(entity);
            result.success = newId > 0;

            if (result.success)
            {
                //推送列表
                List<PushModel> pushs = new List<PushModel>();
                if (string.IsNullOrWhiteSpace(entity.sReceiveClient))
                {
                    if (entity.sReceiveDept.Contains("0"))
                    {// 点检员
                        List<EHECD_SiteMsgClientRelation> m_rels = new List<EHECD_SiteMsgClientRelation>();

                        foreach (var client in ClientDao.Instance.GetClientsByType(0))
                        {
                            m_rels.Add(new EHECD_SiteMsgClientRelation()
                            {
                                iSiteMsgID = newId,
                                iClientID = client.ID,
                                bIsReaded = true
                            });

                            pushs.Add(new PushModel
                            {
                                iClientID = client.ID.ToString(),
                                Content = new { sTitle = entity.sTitle, sContent = entity.sContent },
                                Type = 1
                            });
                        }

                        MongoDBHelper<EHECD_SiteMsgClientRelation>.BatchInsert(m_rels);
                    }
                    if (entity.sReceiveDept.Contains("1"))
                    {// 使用单位
                        List<EHECD_SiteMsgUnitRelation> use_rels = new List<EHECD_SiteMsgUnitRelation>();

                        foreach (var unit in UnitDao.Instance.GetUnitsByType(0))
                        {
                            use_rels.Add(new EHECD_SiteMsgUnitRelation()
                            {
                                iSiteMsgID = newId,
                                iUnitID = unit.ID,
                                bIsReaded = true
                            });
                        }

                        MongoDBHelper<EHECD_SiteMsgUnitRelation>.BatchInsert(use_rels);
                    }
                    if (entity.sReceiveDept.Contains("2"))
                    {// 消防部门
                        #region 消防后台

                        List<EHECD_SiteMsgUnitRelation> use_rels = new List<EHECD_SiteMsgUnitRelation>();

                        foreach (var unit in UnitDao.Instance.GetUnitsByType(1))
                        {
                            use_rels.Add(new EHECD_SiteMsgUnitRelation()
                            {
                                iSiteMsgID = newId,
                                iUnitID = unit.ID,
                                bIsReaded = true
                            });
                        }

                        MongoDBHelper<EHECD_SiteMsgUnitRelation>.BatchInsert(use_rels);

                        #endregion

                        #region 消防前端

                        List<EHECD_SiteMsgClientRelation> fire_rels = new List<EHECD_SiteMsgClientRelation>();

                        foreach (var clt in ClientDao.Instance.GetClientsByType(1))
                        {
                            fire_rels.Add(new EHECD_SiteMsgClientRelation()
                            {
                                iSiteMsgID = newId,
                                iClientID = clt.ID,
                                bIsReaded = true
                            });

                            pushs.Add(new PushModel
                            {
                                iClientID = clt.ID.ToString(),
                                Content = new { sTitle = entity.sTitle, sContent = entity.sContent },
                                Type = 1
                            });
                        }

                        MongoDBHelper<EHECD_SiteMsgClientRelation>.BatchInsert(fire_rels);

                        #endregion
                    }
                    if (entity.sReceiveDept.Contains("3"))
                    {// 维护公司
                        #region 公司后台

                        List<EHECD_SiteMsgUnitRelation> use_rels = new List<EHECD_SiteMsgUnitRelation>();

                        foreach (var unit in UnitDao.Instance.GetUnitsByType(2))
                        {
                            use_rels.Add(new EHECD_SiteMsgUnitRelation()
                            {
                                iSiteMsgID = newId,
                                iUnitID = unit.ID,
                                bIsReaded = true
                            });
                        }

                        MongoDBHelper<EHECD_SiteMsgUnitRelation>.BatchInsert(use_rels);

                        #endregion

                        #region 公司前端

                        List<EHECD_SiteMsgClientRelation> rpr_rels = new List<EHECD_SiteMsgClientRelation>();

                        foreach (var clt in ClientDao.Instance.GetClientsByType(2))
                        {
                            rpr_rels.Add(new EHECD_SiteMsgClientRelation()
                            {
                                iSiteMsgID = newId,
                                iClientID = clt.ID,
                                bIsReaded = true
                            });

                            pushs.Add(new PushModel
                            {
                                iClientID = clt.ID.ToString(),
                                Content = new { sTitle = entity.sTitle, sContent = entity.sContent },
                                Type = 1
                            });
                        }

                        MongoDBHelper<EHECD_SiteMsgClientRelation>.BatchInsert(rpr_rels);

                        #endregion
                    }
                    if (entity.sReceiveDept.Contains("4"))
                    {// 值班人员
                        List<EHECD_SiteMsgClientRelation> m_rels = new List<EHECD_SiteMsgClientRelation>();

                        foreach (var client in ClientDao.Instance.GetClientsByType(4))
                        {
                            m_rels.Add(new EHECD_SiteMsgClientRelation()
                            {
                                iSiteMsgID = newId,
                                iClientID = client.ID,
                                bIsReaded = true
                            });

                            pushs.Add(new PushModel
                            {
                                iClientID = client.ID.ToString(),
                                Content = new { sTitle = entity.sTitle, sContent = entity.sContent },
                                Type = 1
                            });
                        }

                        MongoDBHelper<EHECD_SiteMsgClientRelation>.BatchInsert(m_rels);
                    }
                }
                else
                {
                    List<EHECD_SiteMsgClientRelation> m_rels = new List<EHECD_SiteMsgClientRelation>();
                    // 发送指定工作人员
                    foreach (var member in ClientService.Instance.GetClientsByPhones(entity.sReceiveClient))
                    {
                        m_rels.Add(new EHECD_SiteMsgClientRelation()
                        {
                            iSiteMsgID = newId,
                            iClientID = member.ID,
                            bIsReaded = true
                        });

                        pushs.Add(new PushModel
                        {
                            iClientID = member.ID.ToString(),
                            Content = new { sTitle = entity.sTitle, sContent = entity.sContent },
                            Type = 1
                        });
                    }

                    MongoDBHelper<EHECD_SiteMsgClientRelation>.BatchInsert(m_rels);
                }

                PushService.Instance.PushMsgToBatchAsync(pushs);
            }

            result.message = result.success ? "发送站内信成功" : "发送站内信失败";
            return result;
        }
		
		#endregion
		
		#region 批量删除站内信

        /// <summary>
        /// 批量删除站内信
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            if (result.success)
            {// 删除关系
                var sIdAry = sIds.Split(',');
                foreach (var id in sIdAry)
                {
                    MongoDBHelper<EHECD_SiteMsgClientRelation>.Delete(o => Convert.ToInt32(id) == o.iSiteMsgID);
                    MongoDBHelper<EHECD_SiteMsgUnitRelation>.Delete(o => Convert.ToInt32(id) == o.iSiteMsgID);
                }
            }
            result.message = result.success ? "删除站内信成功" : "删除站内信失败";
            return result;
        }

        #endregion

        #region 获取用户站内信列表

        /// <summary>
        /// 获取用户站内信列表
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [APIAttribute(name: "msg.getlist", desc: "获取用户站内信列表")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage GetMemberMessageList(int iClientID, int page)
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iClientID", iClientID);

            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取用户站内信列表失败";
                return result;
            }
            EHECD_Client client = ClientDao.Instance.GetClient(iClientID);

            var list = new List<EHECD_SiteMsg>();

            var iType = 0;
            switch (client.iType)
            {
                case 0:
                    iType = 0;
                    break;
                case 1:
                    iType = 2;
                    break;
                case 2:
                    iType = 3;
                    break;
                case 4:
                    iType = 4;
                    break;
            }

            try
            {
                param.condition.Add("sPhone", client.sPhone);
                param.condition.Add("iType", iType);
                param.condition.Add("dClientCreateTime", client.dCreateTime);
                IEnumerable<EHECD_SiteMsg> msgList = Dao.GetSiteMsgList(param, ref iTotalRecord);
                if(msgList != null)
                {
                    list = PrepareList(msgList.ToList(), iClientID);
                }else
                {
                    list = new List<EHECD_SiteMsg>();
                }
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取用户站内信列表成功" : "获取用户站内信列表失败";
            return result;
        }

        #endregion

        #region 获取单位未读站内信列表

        /// <summary>
        /// 获取单位未读站内信列表
        /// </summary>
        /// <param name="iUnitID"></param>
        /// <param name="iUnitType"></param>
        /// <returns></returns>
        public List<EHECD_SiteMsg> GetUnitMessageList(int iUnitID, int iUnitType)
        {
            var list = new List<EHECD_SiteMsg>();
            list = Dao.GetUnitSiteMsgList(iUnitType).ToList();
            list = UnitPrepareList(list, iUnitID);
            return list;
        }

        #endregion

        #region 构造站内信已读未读

        /// <summary>
        /// 构造站内信已读未读
        /// </summary>
        /// <param name="list"></param>
        /// <param name="iClientID">s</param>
        List<EHECD_SiteMsg> PrepareList(List<EHECD_SiteMsg> list, int iClientID)
        {
            var result = new List<EHECD_SiteMsg>();

            var m_rels = MongoDBHelper<EHECD_SiteMsgClientRelation>.Query(
                   o => o.iClientID == iClientID);

            for (var i = 0; i < list.Count; i++)
            {
                var relation = m_rels.FirstOrDefault(o => o.iSiteMsgID == list[i].ID);
                if (relation != null)
                {
                    list[i].bIsReaded = relation.bIsReaded;
                    result.Add(list[i]);
                }
            }

            return result;
        }

        #endregion

        #region 构造单位站内信已读未读

        /// <summary>
        /// 构造单位站内信已读未读
        /// </summary>
        /// <param name="list"></param>
        /// <param name="iUnitID">s</param>
        List<EHECD_SiteMsg> UnitPrepareList(List<EHECD_SiteMsg> list, int iUnitID)
        {
            var result = new List<EHECD_SiteMsg>();
         
            var m_rels = MongoDBHelper<EHECD_SiteMsgUnitRelation>.Query(
                   o => o.iUnitID == iUnitID);

            for (var i = 0; i < list.Count; i++)
            {
                var relation = m_rels.FirstOrDefault(o => o.iSiteMsgID == list[i].ID && o.bIsReaded);
                if (relation != null)
                {
                    list[i].bIsReaded = relation.bIsReaded;
                    result.Add(list[i]);
                }
            }

            return result;
        }

        #endregion

        #region 获取站内信详情

        /// <summary>
        /// 获取站内信详情
        /// </summary>
        /// <param name="iSiteMsgID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "msg.get", desc: "获取站内信详情")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage GetSiteMsgInfo(int iSiteMsgID, int iClientID)
        {
            ResultMessage result = new ResultMessage();
            SetReaded(iSiteMsgID, iClientID);
            EHECD_SiteMsg entity = Dao.Get(iSiteMsgID) ?? new EHECD_SiteMsg();
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取站内信详情成功" : "获取站内信详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 获取站内信详情

        /// <summary>
        /// 获取站内信详情
        /// </summary>
        /// <param name="iSiteMsgID"></param>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public EHECD_SiteMsg GetUnitSiteMsgInfo(int iSiteMsgID, int iUnitID)
        {
            SetUnitReaded(iSiteMsgID, iUnitID);
            EHECD_SiteMsg entity = Dao.Get(iSiteMsgID) ?? new EHECD_SiteMsg();
            return entity;
        }

        #endregion

        #region 设置站内信已读

        /// <summary>
        /// 设置站内信已读
        /// </summary>
        /// <param name="iSiteMsgID"></param>
        /// <param name="iClientID"></param>
        public void SetReaded(int iSiteMsgID, int iClientID)
        {
            var m_update = Builders<EHECD_SiteMsgClientRelation>.Update.Set("bIsReaded", false);
            MongoDBHelper<EHECD_SiteMsgClientRelation>.Update(o => o.iClientID == iClientID && o.iSiteMsgID == iSiteMsgID, m_update);
        }

        #endregion

        #region 设置站内信已读

        /// <summary>
        /// 设置站内信已读
        /// </summary>
        /// <param name="iSiteMsgID"></param>
        /// <param name="iUnitID"></param>
        public void SetUnitReaded(int iSiteMsgID, int iUnitID)
        {
            var m_update = Builders<EHECD_SiteMsgUnitRelation>.Update.Set("bIsReaded", false);
            MongoDBHelper<EHECD_SiteMsgUnitRelation>.Update(o => o.iUnitID == iUnitID && o.iSiteMsgID == iSiteMsgID, m_update);
        }

        #endregion

        #region 获取用户未读的站内信条数

        /// <summary>
        /// 获取用户未读的站内信条数
        /// </summary>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "msg.unreadcount", desc: "获取用户未读的站内信条数")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage GetMemberMessageCount(int iClientID)
        {
            ResultMessage result = new ResultMessage();
            if (iClientID == 0)
            {
                result.message = "获取用户未读的站内信条数";
                return result;
            }
            try
            {
                result.success = true;
                result.data = MongoDBHelper<EHECD_SiteMsgClientRelation>.QueryCount(
                    o => o.iClientID == iClientID && o.bIsReaded);
            }
            catch
            {
                result.success = false;
            }
            result.message = result.success ? "获取用户未读的站内信条数成功" : "获取用户未读的站内信条数失败";
            return result;
        }

        #endregion

        #region 前端删除站内信

        /// <summary>
        /// 前端删除站内信
        /// </summary>
        /// <param name="iSiteMsgID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        public ResultMessage ClientDelete(int iSiteMsgID, int iClientID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_SiteMsg entity = Dao.Get(iSiteMsgID);
            result.success = entity != null ? true : false;
            result.message = result.success ? "删除站内信成功" : "删除站内信失败";

            if (result.success)
            {// 删除关系
                MongoDBHelper<EHECD_SiteMsgClientRelation>.Delete(o => iSiteMsgID == o.iSiteMsgID && o.iClientID == iClientID);
            }

            return result;
        }

        #endregion

        #region 单位删除站内信

        /// <summary>
        /// 单位删除站内信
        /// </summary>
        /// <param name="sIds"></param>
        /// <param name="iUnitID"></param>
        /// <returns></returns>
        public ResultMessage UnitDelete(string sIds, int iUnitID)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                // 删除关系
                var sIdsAry = sIds.Split(',');
                foreach (string id in sIdsAry)
                {
                    MongoDBHelper<EHECD_SiteMsgUnitRelation>.Delete(o => Convert.ToInt32(id) == o.iSiteMsgID && o.iUnitID == iUnitID);
                }
                result.success = true;
                result.message = "删除成功";
            }
            catch (Exception ex)
            {
                TTracer.WriteLog("delete unit sitemsg error:" + ex.Message);
                result.message = "删除失败";
            }

            return result;
        }

        #endregion

        #region 删除站内信

        /// <summary>
        /// 删除站内信
        /// </summary>
        /// <param name="iSiteMsgID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "msg.delete", desc: "删除站内信")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage DeleteFeedback(long iSiteMsgID, int iClientID)
        {
            ResultMessage result = new ResultMessage();

            try
            {
                MongoDBHelper<EHECD_SiteMsgClientRelation>.Delete(o => iSiteMsgID == o.iSiteMsgID && o.iClientID == iClientID);
                result.success = true;
            }
            catch { }

            result.message = result.success ? "删除站内信成功" : "删除站内信失败";
            return result;
        }

        #endregion

        #region 发送站内信

        /// <summary>
        /// 发送站内信
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Send(EHECD_SiteMsg entity)
        {
            ResultMessage result = new ResultMessage();

            var clientAry = new List<string>();
            IEnumerable<EHECD_Client> cList = ClientDao.Instance.GetClientsByIDs(entity.sReceiveClient);
            if (cList != null && cList.Count() > 0)
            {
                foreach (EHECD_Client client in cList)
                {
                    clientAry.Add(client.sPhone);
                }
            }
            entity.sReceiveClient = string.Join(",", clientAry);

            int newId = Dao.Insert(entity);
            result.success = newId > 0;

            if (result.success)
            {
                List<EHECD_SiteMsgClientRelation> m_rels = new List<EHECD_SiteMsgClientRelation>();
                // 发送指定工作人员
                foreach (var client in cList)
                {
                    m_rels.Add(new EHECD_SiteMsgClientRelation()
                    {
                        iSiteMsgID = newId,
                        iClientID = client.ID,
                        bIsReaded = true
                    });

                    PushService.Instance.PushMsgToSingleAsync(1, client.ID.ToString(), new {
                        sTitle = entity.sTitle,
                        sContent = entity.sContent
                    });
                }

                MongoDBHelper<EHECD_SiteMsgClientRelation>.BatchInsert(m_rels);
               
            }

            result.message = result.success ? "发送站内信成功" : "发送站内信失败";
            return result;
        }

        #endregion

        #region 获取当月巡检通知数据

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_SiteMsg> GetCurrentMonthNotices()
        {
            return Dao.GetCurrentMonthNotices();
        }

        #endregion

        #region 发送巡检通知

        /// <summary>
        /// 发送巡检通知
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public void SendNotice(List<EHECD_Client> list)
        {
            try
            {
                #region OLD

                //foreach (var entity in list)
                //{
                //    int newId = Dao.SendNotice(entity);
                //    if (newId > 0)
                //    {
                //        MongoDBHelper<EHECD_SiteMsgClientRelation>.Insert(new EHECD_SiteMsgClientRelation()
                //        {
                //            iSiteMsgID = newId,
                //            iClientID = entity.ID,
                //            bIsReaded = true
                //        });
                //    }
                //}

                #endregion

                #region NEW

                List<EHECD_SiteMsgClientRelation> m_rels = new List<EHECD_SiteMsgClientRelation>();
                List<PushModel> pushs = new List<PushModel>();
                string msg = "工作人员你好，你当月还有设备未巡检，请及时巡检";
                foreach (var entity in list)
                {
                    int newId = Dao.SendNotice(entity);
                    if (newId > 0)
                    {
                        m_rels.Add(new EHECD_SiteMsgClientRelation()
                        {
                            iSiteMsgID = newId,
                            iClientID = entity.ID,
                            bIsReaded = true
                        });
                    }
                    
                    object obj = new {
                        sTitle = "巡检通知",
                        sContent = msg
                    };
                    pushs.Add(new PushModel
                    {
                        Content = obj,
                        Type = 2,
                        iClientID = entity.ID.ToString()
                    });
                    AliSmsService.Instance.SendInsMonthlyMessageAsync(entity.sPhone);
                }

                MongoDBHelper<EHECD_SiteMsgClientRelation>.BatchInsert(m_rels);
                PushService.Instance.PushMsgToBatchAsync(pushs);
                #endregion
            }
            catch (Exception ex)
            {
                TTracer.WriteLog("send notice error:" + ex.Message);
            }
        }

        #endregion

        #region 发送异常通知

        /// <summary>
        /// 发送异常通知
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sDeviceName"></param>
        /// <param name="sDeviceNumber"></param>
        /// <returns></returns>
        public void SendAbnormalNotice(List<EHECD_Client> list, string sDeviceName, string sDeviceNumber)
        {
            try
            {
                #region OLD

                //foreach (var entity in list)
                //{
                //    entity.sCredentials = sDeviceName;
                //    int newId = Dao.SendAbnormalNotice(entity);
                //    if (newId > 0)
                //    {
                //        MongoDBHelper<EHECD_SiteMsgClientRelation>.Insert(new EHECD_SiteMsgClientRelation()
                //        {
                //            iSiteMsgID = newId,
                //            iClientID = entity.ID,
                //            bIsReaded = true
                //        });
                //    }
                //}

                #endregion

                #region NEW

                List<EHECD_SiteMsgClientRelation> m_rels = new List<EHECD_SiteMsgClientRelation>();
                List<PushModel> pushs = new List<PushModel>();
                string sTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                string msg = "设备【{0}】，编号{1}于" + sTime + @"被检查出异常，请查看设备详情并及时维修或更换。";
                foreach (var entity in list)
                {
                    entity.sCredentials = sDeviceName;
                    entity.sOrganName = sDeviceNumber;
                    int newId = Dao.SendAbnormalNotice(entity);
                    if (newId > 0)
                    {
                        m_rels.Add(new EHECD_SiteMsgClientRelation()
                        {
                            iSiteMsgID = newId,
                            iClientID = entity.ID,
                            bIsReaded = true
                        });
                    }

                    object obj = new {
                        sTitle = "设备异常通知",
                        sContent = string.Format(msg, sDeviceName, sDeviceNumber)
                    };
                    pushs.Add(new PushModel
                    {
                        Content = obj,
                        Type = 3,
                        iClientID = entity.ID.ToString()
                    });
                    AliSmsService.Instance.SendDeviceAbnormalMessageAsync(entity.sPhone, sDeviceName, sDeviceNumber, sTime);
                }

                MongoDBHelper<EHECD_SiteMsgClientRelation>.BatchInsert(m_rels);
                PushService.Instance.PushMsgToBatchAsync(pushs);
                #endregion

            }
            catch (Exception ex)
            {
                TTracer.WriteLog("send notice error:" + ex.Message);
            }
        }

        #endregion
    }
}