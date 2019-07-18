using EHECD.Common;
using EHECD.EntityFramework.EFWork;
using Newtonsoft.Json.Linq;
using System;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 日志管理
    /// </summary>
    public class ActionLogService
    {
        static ActionLogService instance;

        private ActionLogService() { }

        static public ActionLogService Instance
        {
            get
            {
                if (instance == null)
                    instance = new ActionLogService();
                return instance;
            }
        }

        #region 获取操作日志信息

        /// <summary>
        /// 获取操作日志信息
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sStartTime"></param>
        /// <param name="sEndTime"></param>
        /// <param name="sType"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public string GetList(string sName, string sStartTime, string sEndTime, string sType, int iPageIndex, int iPageSize)
        {
            using (var Context = new Entities())
            {
                var query = from a in Context.EHECD_ActionLog
                            join b in Context.EHECD_User
                            on a.iUserID equals b.ID
                            select new
                            {
                                a.ID,
                                a.sType,
                                a.dInsertTime,
                                a.sContent,
                                a.sIpAddress,
                                b.sLoginName,
                                b.sRealName
                            };
                if (!string.IsNullOrEmpty(sName))
                    query = query.Where(o => o.sLoginName.Contains(sName) || o.sRealName.Contains(sName));

                if (!string.IsNullOrEmpty(sStartTime))
                    query = query.Where(o => SqlFunctions.DateDiff("d", o.dInsertTime, sStartTime) <= 0);

                if (!string.IsNullOrEmpty(sEndTime))
                    query = query.Where(o => SqlFunctions.DateDiff("d", o.dInsertTime, sEndTime) >= 0);

                if (sType != "全部类型" && !string.IsNullOrEmpty(sType))
                    query = query.Where(o => o.sType == sType);

                JObject jResult = new JObject();
                int iCount = query.Count();

                var list = iPageSize != 0 ? query.OrderByDescending(o => o.dInsertTime).Skip((int)(iPageIndex - 1) * iPageSize).Take(iPageSize).ToList() : query.ToList();
                jResult.Add(new JProperty("page", iPageIndex));
                jResult.Add(new JProperty("total", iCount));
                jResult.Add(new JProperty("rows", TCommon.ItemToJson(list)));
                return jResult.ToString();
            }
        }

        #endregion

        #region 获取操作类型

        /// <summary>
        /// 获取操作类型
        /// </summary>
        /// <returns></returns>
        public string GetTypeList()
        {
            using (var Context = new Entities())
            {
                var query = (from c in Context.EHECD_ActionLog  select new { id = c.sType, text = c.sType }).Distinct();
                var list = query.ToList();
                list.Insert(0, new { id = "全部类型", text = "全部类型" });
                return TCommon.ItemToJson(list).ToString();
            }
        }

        #endregion

        #region 添加操作日志

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="iUserID"></param>
        /// <param name="sType"></param>
        /// <param name="sContent"></param>
        public void Add(Entities Context, long iUserID, string sType, string sContent)
        {
            var flag = false;
            if (Context == null)
            {
                Context = new Entities();
                flag = true;
            }
            EHECD_ActionLog item = new EHECD_ActionLog()
            {
                iUserID = iUserID,
                sType = sType,
                sContent = sContent,
                dInsertTime = DateTime.Now,
                sIpAddress = new TClientIpAddress().NetIP()
            };
            Context.EHECD_ActionLog.Add(item);
            Context.SaveChanges();
            if (flag)
                Context.Dispose();
        }

        #endregion

        #region 获取日志详情

        /// <summary>
        /// 获取日志详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EHECD_ActionLog Get(long id)
        {
            using (var Context = new Entities())
            {
                EHECD_ActionLog item = Context.EHECD_ActionLog.Find(id);
                return item;
            }
        }

        #endregion
    }
}