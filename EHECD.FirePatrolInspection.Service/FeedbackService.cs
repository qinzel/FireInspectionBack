
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;
using MongoDB.Driver;
using Microsoft.AspNet.SignalR;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 意见反馈业务逻辑
    /// </summary>
    public class FeedbackService
    {
        static FeedbackService instance = new FeedbackService();
        private static FeedbackDao Dao = FeedbackDao.Instance;

        private FeedbackService()
        {
        }

        public static FeedbackService Instance
        {
            get { return instance; }
        }
		
		#region 获取意见反馈数据

        /// <summary>
        /// 获取意见反馈数据
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

        #region 获取各单位意见反馈数据

        /// <summary>
        /// 获取各单位意见反馈数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetFeedbackGridData(QueryParams param)
        {
            int iTotalRecord = 0;
            var list = Dao.GetFeedBackList(param, ref iTotalRecord);

            return list.EHECDAsPagedString(iTotalRecord);
        }

        #endregion

        #region 获取意见反馈详情

        /// <summary>
        /// 获取意见反馈详情
        /// </summary>
        /// <param name="iFeedbackID"></param>
        /// <returns></returns>
        public EHECD_Feedback Get(long iFeedbackID)
        {
            EHECD_Feedback entity = Dao.Get(iFeedbackID) ?? new EHECD_Feedback();
            if (entity.ID > 0 && entity.bIsReplyStatus)
            {
                entity.FeedbackReply = FeedbackReplyDao.Instance.GetByFeedbackID(iFeedbackID);
            }
            return entity;
        }
		
		#endregion
		
		#region 批量删除意见反馈

        /// <summary>
        /// 批量删除意见反馈
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除意见反馈成功" : "删除意见反馈失败";
            return result;
        }

        #endregion

        #region 提交意见反馈

        /// <summary>
        /// 提交意见反馈
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="sTitle"></param>
        /// <param name="sContent"></param>
        /// <param name="iUnitID"></param>
        /// <param name="sImageSrc"></param>
        /// <returns></returns>
        [APIAttribute(name: "fb.add", desc: "提交意见反馈")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage CreateFeedback(int iClientID, string sTitle, string sContent, int iUnitID = 0, string sImageSrc = "")
        {
            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "提交意见反馈失败";
                return result;
            }

            EHECD_Feedback entity = new EHECD_Feedback()
            {
                iClientID = iClientID,
                sTitle = sTitle,
                sContent = sContent,
                sImageSrc = sImageSrc,
                iUnitID = iUnitID
            };

            result.success = Dao.Insert(entity) ? true : false;
            if (result.success)
            {
                PushHub hub = new PushHub();
                EHECD_Client client = ClientDao.Instance.GetClient(iClientID);
                if (client.iType == 0)
                {
                    var list = UnitDao.Instance.GetClientRelUnitList(iClientID);
                    if (list != null)
                    {
                        foreach (EHECD_Unit unit in list)
                        {
                            hub.SendMessage(unit.ID + "." + unit.sName, "你有一条新的反馈");
                            hub.SendMessage("TOTALBACKSTAGE", "你有一条新的反馈");
                        }
                    }
                }
                else
                {
                    EHECD_Unit unit = UnitDao.Instance.Get(client.iUnitID);
                    hub.SendMessage(unit.ID + "." + unit.sName, "你有一条新的反馈");
                    hub.SendMessage("TOTALBACKSTAGE", "你有一条新的反馈");
                }
            }
            result.message = result.success ? "提交意见反馈成功" : "提交意见反馈失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 获取反馈列表

        /// <summary>
        /// 获取反馈列表
        /// </summary>
        /// <param name="iClientID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [APIAttribute(name: "fb.getlist", desc: "获取反馈列表")]
        public ResultMessage GetFeedbackList(int iClientID, int page)
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iClientID", iClientID);

            ResultMessage result = new ResultMessage();

            if (iClientID == 0)
            {
                result.message = "获取反馈列表";
                return result;
            }

            var list = new List<EHECD_Feedback>();

            try
            {
                list = Dao.GetList(param, ref iTotalRecord).ToList();
                list = PrepareList(list, iClientID);
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取反馈列表成功" : "获取反馈列表失败";
            return result;
        }

        #endregion

        #region 获取反馈详情

        /// <summary>
        /// 获取反馈详情
        /// </summary>
        /// <param name="iFeedbackID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "fb.get", desc: "获取反馈详情")]
        public ResultMessage GetFeedback(int iFeedbackID, int iClientID)
        {
            ResultMessage result = new ResultMessage();
            SetReaded(iFeedbackID, iClientID);
            EHECD_Feedback entity = Dao.Get(iFeedbackID) ?? new EHECD_Feedback();
            if (entity.ID > 0)
            {
                entity.FeedbackReply = FeedbackReplyDao.Instance.GetByFeedbackID(entity.ID);
            }
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取反馈详情成功" : "获取反馈详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 构造意见反馈已读未读

        /// <summary>
        /// 构造意见反馈已读未读
        /// </summary>
        /// <param name="list"></param>
        /// <param name="iClientID">s</param>
        List<EHECD_Feedback> PrepareList(List<EHECD_Feedback> list, int iClientID)
        {
            var result = new List<EHECD_Feedback>();

            var m_rels = MongoDBHelper<EHECD_FeedbackClientRelation>.Query(
                   o => o.iClientID == iClientID);

            for (var i = 0; i < list.Count; i++)
            {
                var relation = m_rels.FirstOrDefault(o => o.iFeedbackID == list[i].ID);
                if (relation != null)
                {
                    list[i].bIsReaded = relation.bIsReaded;
                }
                else
                {
                    list[i].bIsReaded = false;
                }
                result.Add(list[i]);
            }

            return result;
        }

        #endregion

        #region 设置反馈详情已读

        /// <summary>
        /// 设置反馈详情已读
        /// </summary>
        /// <param name="iFeedbackID"></param>
        /// <param name="iClientID"></param>
        public void SetReaded(long iFeedbackID, int iClientID)
        {
            var m_update = Builders<EHECD_FeedbackClientRelation>.Update.Set("bIsReaded", false);
            MongoDBHelper<EHECD_FeedbackClientRelation>.Update(o => o.iClientID == iClientID && o.iFeedbackID == iFeedbackID, m_update);
        }

        #endregion

        #region 删除反馈

        /// <summary>
        /// 删除反馈
        /// </summary>
        /// <param name="iFeedbackID"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [APIAttribute(name: "fb.delete", desc: "删除反馈")]
        [ClientAPI(LoginCheck = true)]
        public ResultMessage DeleteFeedback(long iFeedbackID, int iClientID)
        {
            ResultMessage result = new ResultMessage();

            result.success = Dao.DeleteFeedback(iFeedbackID);
            if (result.success)
            {
                MongoDBHelper<EHECD_FeedbackClientRelation>.Delete(o => iFeedbackID == o.iFeedbackID && o.iClientID == iClientID);
            }
            result.message = result.success ? "删除反馈成功" : "删除反馈失败";
            return result;
        }

        #endregion
    }
}