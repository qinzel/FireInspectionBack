				
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.FirePatrolInspection.Entity;
using EHECD.Common;

namespace EHECD.FirePatrolInspection.DAL
{
    /// <summary>
    /// 文章操作
    /// </summary>
    public class ArticleDao
    {
        static ArticleDao instance = new ArticleDao();

        private ArticleDao()
        {
        }
        public static ArticleDao Instance
        {
            get
            {
                return instance;
            }
        }
		
		#region 获取文章信息
		
        /// <summary>
        /// 获取文章信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Article> GetList(QueryParams param, ref int iTotalRecord)
        {
            string sSql = "Select * From EHECD_Article Where bIsDeleted=0";
						
            StringBuilder sCondition = new StringBuilder();
			if (TDictionary.IsExitsAndNotEmpty(param.condition, "sName"))
            {
                sCondition.AppendFormat(string.Format(" And sTitle Like '%{0}%'", param.condition["sName"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "sTitle"))
            {
                sCondition.AppendFormat(string.Format(" And sTitle Like '%{0}%'", param.condition["sTitle"]));
            }
            if (TDictionary.IsExitsAndNotEmpty(param.condition, "iType"))
            {
                sCondition.AppendFormat(string.Format(" And iType = {0}", param.condition["iType"]));
            }

            param.sort = "CASE WHEN sSortNumber = '' THEN CONVERT(INT, '9999999') ELSE CONVERT(INT, sSortNumber) END";
            param.order = "ASC";

            return DBHelper.QueryRunSqlByPager<EHECD_Article>(sSql + sCondition, param.page, param.rows,
                ref iTotalRecord, param.sort + " " + param.order);
        }

        #endregion

        #region 移动端查询

        /// <summary>
        /// 获取文章信息,不包含内容
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<EHECD_Article> ClientGetList(DateTime? lm)
        {
            string sSql = "Select ID,sTitle,sSortNumber,sImageSrc,iType,dCreateTime,bIsDeleted,lastModifyTime From EHECD_Article where 1 = 1";
            QueryParams param = new QueryParams();
            param.sort = "CASE WHEN sSortNumber = '' THEN CONVERT(INT, '9999999') ELSE CONVERT(INT, sSortNumber) END";
            param.order = "ASC";
            if (lm.HasValue)
            {
                sSql += string.Format(@" and lastModifyTime > '{0}'", lm.Value.ToString());
            }else
            {
                sSql += " and bIsDeleted = 0";
            }
            return DBHelper.Query<EHECD_Article>(sSql, param);
        }

  
        #endregion

        #region 查询所有可用文章信息

        /// <summary>
        /// 查询所有可用文章信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EHECD_Article> GetAllList()
        {
            string sSql = "Select * From EHECD_Article Where bIsDeleted = 0";
            return DBHelper.Query<EHECD_Article>(sSql);
        }

        #endregion

        #region 获取文章详情

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="iArticleID"></param>
        /// <returns></returns>
        public EHECD_Article Get(int iArticleID)
        {
            return DBHelper.QuerySingle<EHECD_Article>(iArticleID);
        }

        #endregion

        #region 获取关于我们

        /// <summary>
        /// 获取关于我们
        /// </summary>
        /// <returns></returns>
        public EHECD_Article GetAbout()
        {
            return DBHelper.QuerySingle<EHECD_Article>("SELECT * FROM EHECD_ARTICLE WHERE bIsDeleted = 0 AND iType = 4");
        }
		
		#endregion
		
		#region 添加文章

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(EHECD_Article entity)
        {
            entity.lastModifyTime = DateTime.Now;
            return DBHelper.Insert<EHECD_Article>(entity) > 0;
        }
		
		#endregion
		
		#region 编辑文章
		
        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(EHECD_Article entity)
        {
            entity.lastModifyTime = DateTime.Now;
			string sSql =
                @"Update [EHECD_Article] Set 
				[sTitle]=@sTitle,
				[sContent]=@sContent,
				[sSortNumber]=@sSortNumber,
				[sImageSrc]=@sImageSrc,
                [lastModifyTime]=@lastModifyTime
				Where ID = @ID";
            return DBHelper.Execute(sSql, entity) > 0;
		}
		
		#endregion
		
		#region 批量删除文章

        /// <summary>
        /// 批量删除文章
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public bool Delete(string sIds)
        {
            sIds = "'" + string.Join("','", sIds.Split(',')) + "'";
            string sSql =
                @"Update EHECD_Article Set bIsDeleted=1, lastModifyTime = @lastModifyTime Where ID In (" + sIds+ ")";
			return DBHelper.Execute(sSql, new EHECD_Article { lastModifyTime = DateTime.Now }) > 0;
        }
		
		#endregion
    }
}