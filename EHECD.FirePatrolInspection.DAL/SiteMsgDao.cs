				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 站内信操作
    /// </summary>
    public class SiteMsgDao
    {
        static SiteMsgDao instance = new SiteMsgDao();

        private SiteMsgDao()
        {
        }
        public static SiteMsgDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取站内信信息
		
        /// <summary>
        /// 获取站内信信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_SiteMsg> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = "Select * From EHECD_SiteMsg Where bIsDeleted = 0 AND (iType = 0 OR iType = 1)";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sName Like '%{0}%'", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dStartTime"))
            {
                sCondition.AppendFormat(string.Format(" And dCreateTime >= CONVERT(varchar(20),'{0} 00:00:00', 120) ", param.condition["dStartTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "dEndTime"))
            {
                sCondition.AppendFormat(string.Format(" And dCreateTime <= CONVERT(varchar(20),'{0} 23:59:59', 120) ", param.condition["dEndTime"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iUnitType"))
            {
                /**
                 0 点检员
                 1 使用单位
                 2 消防部门
                 3 维护公司
                 4 值班人员
                 */

                sCondition.AppendFormat(
                    string.Format(
                        " AND iType = 0 AND ',' + sReceiveDept + ',' like '%,{0},%' ",
                        param.condition["iUnitType"]));
            }

            return DBHelper.QueryRunSqlByPager<EHECD_SiteMsg>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
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
            return DBHelper.QuerySingle<EHECD_SiteMsg>(iSiteMsgID);
        }

        #endregion

        #region 添加站内信

        /// <summary>
        /// 添加站内信
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Insert(EHECD_SiteMsg entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" INSERT INTO EHECD_SiteMsg (sTitle, sContent, iType, sReceiveDept, sReceiveClient, sOperator) VALUES (@sTitle, @sContent, @iType, @sReceiveDept, @sReceiveClient, @sOperator) Select @@Identity ");

            return Convert.ToInt32(DBHelper.ExecuteScalar(sb.ToString(), entity));
        }

        #endregion

        #region 批量删除站内信

        /// <summary>
        /// 批量删除站内信
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
						
			return DBHelper.Execute(string.Format("Update EHECD_SiteMsg Set bIsDeleted=1 Where ID In ({0})", sIds)) > 0;
        }

        #endregion

        #region 获取前台站内信数据

        /// <summary>
        /// 获取前台站内信数据
        /// </summary>
        /// <param name="param"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_SiteMsg> GetSiteMsgList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = string.Format(@"
                    SELECT S.* FROM EHECD_SITEMSG S WHERE bIsDeleted = 0 AND (
                        (iType = 3 AND S.sReceiveClient = '{0}') OR 
                        (iType = 2 AND S.sReceiveClient = '{0}') OR 
                        (iType = 1 AND ',' + S.sReceiveClient + ',' like '%,{0},%') OR 
                        (iType = 0 AND ',' + S.sReceiveDept + ',' like '%,{1},%')
                    ) AND DATEDIFF(SECOND, '{2}', dCreateTime) >= 0", param.condition["sPhone"], param.condition["iType"], param.condition["dClientCreateTime"]);

            param.sort = "dCreateTime";

            return DBHelper.QueryRunSqlByPager<EHECD_SiteMsg>(sSql, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 获取单位站内信数据

        /// <summary>
        /// 获取单位站内信数据
        /// </summary>
        /// <param name="iUnitType"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_SiteMsg> GetUnitSiteMsgList(int iUnitType)
        {
            string sSql = string.Format("Select * From EHECD_SiteMsg Where bIsDeleted = 0 AND iType = 0 AND ',' + sReceiveDept + ',' like '%,{0},%'", iUnitType);

            return DBHelper.Query<EHECD_SiteMsg>(sSql);
        }

        #endregion

        #region 获取当月巡检通知数据

        /// <summary>
        /// 获取当月巡检通知数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_SiteMsg> GetCurrentMonthNotices()
        {
            return DBHelper.Query<EHECD_SiteMsg>(string.Format(@"
                    SELECT * FROM EHECD_SITEMSG WHERE bIsDeleted = 0 AND iType = 2 AND DATEDIFF(MONTH, dCreateTime, GETDATE()) = 0 AND sTitle = '巡检通知'
                "));
        }

        #endregion

        #region 发送巡检通知

        /// <summary>
        /// 发送巡检通知
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SendNotice(EHECD_Client entity)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar(string.Format(@"
                    IF NOT EXISTS (SELECT * FROM EHECD_SITEMSG WHERE bIsDeleted = 0 AND iType = 2 AND DATEDIFF(MONTH, dCreateTime, GETDATE()) = 0 AND sTitle = '巡检通知' AND sReceiveClient = '{0}')
                    BEGIN
                        INSERT INTO EHECD_SiteMsg (sTitle, sContent, iType, sReceiveDept, sReceiveClient, sOperator) VALUES ('巡检通知', '工作人员你好，你当月还有设备未巡检，请及时巡检。', 2, '', '{0}', 'system');
                        Select @@Identity;
                    END; ", entity.sPhone)));
        }

        #endregion

        #region 发送异常通知

        /// <summary>
        /// 发送异常通知
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SendAbnormalNotice(EHECD_Client entity)
        {
            return Convert.ToInt32(DBHelper.ExecuteScalar(string.Format(@"
                    INSERT INTO EHECD_SiteMsg 
                    (sTitle, sContent, iType, sReceiveDept, sReceiveClient, sOperator) 
                    VALUES 
                    ('设备异常通知', '设备【{0}】，编号{1}于" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + @"被检查出异常，请查看设备详情并及时维修或更换。', 3, '', '{2}', 'system');
                    Select @@Identity;", entity.sCredentials, entity.sOrganName, entity.sPhone)));
        }

        #endregion
    }
}
