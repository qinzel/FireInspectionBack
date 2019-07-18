
using System;
using System.Collections.Generic;
using System.Text;
using EHECD.Common;
using EHECD.FirePatrolInspection.DAL;
using EHECD.FirePatrolInspection.Entity;
using EHECD.WebApi.Attributes;
using System.Linq;
using EHECD.Core.APIHelper;

namespace EHECD.FirePatrolInspection.Service
{
    /// <summary>
    /// 文章业务逻辑
    /// </summary>
    public class ArticleService
    {
        static ArticleService instance = new ArticleService();
        private static ArticleDao Dao = ArticleDao.Instance;

        private ArticleService()
        {
        }

        public static ArticleService Instance
        {
            get { return instance; }
        }
		
		#region 获取文章数据

        /// <summary>
        /// 获取文章数据
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

		#region 获取文章详情

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="iArticleID"></param>
        /// <returns></returns>
        public EHECD_Article Get(int iArticleID)
        {
            return Dao.Get(iArticleID) ?? new EHECD_Article();
        }
		
		#endregion
		
		#region 添加编辑文章

        /// <summary>
        /// 添加编辑文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage Set(EHECD_Article entity)
        {
            ResultMessage result = new ResultMessage();
            if(entity.iType == 4)
            {
                IEnumerable<EHECD_Article> aList = Dao.GetAllList();
                List<EHECD_Article> tempList = aList.Where(o => o.iType == entity.iType && o.ID != entity.ID).ToList();

                if (tempList != null && tempList.Count > 0)
                {
                    result.message = "已存在关于我们";
                    return result;
                }
            }
            
            if (entity.ID == 0)
            {
                //新增文章
                result.success = Dao.Insert(entity);
                result.message = result.success ? "添加文章成功" : "添加文章失败";
            }
            else
            {
                //修改文章
                result.success = Dao.Update(entity);
                result.message = result.success ? "编辑文章成功" : "编辑文章失败";
            }
            return result;
        }

        #endregion

        #region 添加编辑关于我们

        /// <summary>
        /// 添加编辑关于我们
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultMessage About(EHECD_Article entity)
        {
            ResultMessage result = new ResultMessage();
            entity.iType = 4;
            if (string.IsNullOrWhiteSpace(entity.sImageSrc))
            {
                entity.sImageSrc = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sTitle))
            {
                entity.sTitle = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sSortNumber))
            {
                entity.sSortNumber = string.Empty;
            }
            IEnumerable<EHECD_Article> aList = Dao.GetAllList();
            List<EHECD_Article> tempList = aList.Where(o => o.iType == entity.iType && entity.iType == 4 && o.ID != entity.ID).ToList();

            if (tempList != null && tempList.Count > 0)
            {
                result.message = "已存在关于我们";
                return result;
            }

            if (entity.ID == 0)
            {
                //新增文章
                result.success = Dao.Insert(entity);
                result.message = result.success ? "编辑文章成功" : "编辑文章失败";
            }
            else
            {
                //修改文章
                result.success = Dao.Update(entity);
                result.message = result.success ? "编辑文章成功" : "编辑文章失败";
            }
            return result;
        }

        #endregion

        #region 批量删除文章

        /// <summary>
        /// 批量删除文章
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public ResultMessage Delete(string sIds)
        {
            ResultMessage result = new ResultMessage()
            {
                success = Dao.Delete(sIds)
            };
            result.message = result.success ? "删除文章成功" : "删除文章失败";
            return result;
        }

        #endregion

        #region 获取文章列表

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="page"></param>
        /// <param name="sTitle"></param>
        /// <returns></returns>
        [APIAttribute(name: "article.getlist", desc: "获取文章列表")]
        public ResultMessage GetArticleList(int iType, int page, string sTitle = "")
        {
            var iTotalRecord = 0;

            QueryParams param = new QueryParams();
            param.rows = 15;
            param.page = page;
            param.condition.Add("iType", iType);
            param.condition.Add("sTitle", sTitle);

            ResultMessage result = new ResultMessage();

            var list = new List<EHECD_Article>();

            try
            {
                list = Dao.GetList(param, ref iTotalRecord).ToList();
                result.success = true;
                result.data = list;
            }
            catch
            {
                result.success = false;
            }

            result.message = result.success ? "获取文章列表成功" : "获取文章列表失败";
            return result;
        }

        #endregion

        #region 移动端获取文章

        /// <summary>
        /// 移动端获取文章列表(不包含文章内容)
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="page"></param>
        /// <param name="sTitle"></param>
        /// <returns></returns>
        [APIAttribute(name: "article.client.getlist", desc: "获取文章列表")]
        [ClientAPI]
        public ResultMessage ClientGetArticleList(string lm)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                ClientMessage msg = new ClientMessage();
                DateTime now = DateTime.Now;
      
                DateTime? lastModified = null;
                DateTime parseTime;
                if (!string.IsNullOrEmpty(lm) && DateTime.TryParse(lm, out parseTime))
                {
                    lastModified = parseTime;
                }
                List<EHECD_Article> articles  = Dao.ClientGetList(lastModified).ToList();

                msg.IsModified = articles.Count > 0;
                msg.IsAll = lastModified == null || lastModified.Value.Year < 2019 ? true : false;
                msg.Data = articles;
                msg.LastModifyTime = now.ToString();
                result.success = true;
                result.data = msg;
            }
            catch(Exception e)
            {
                result.success = false;
            }

            result.message = result.success ? "获取文章列表成功" : "获取文章列表失败";
            return result;
        }

        #endregion

        #region 获取文章详情

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="iArticleID"></param>
        /// <returns></returns>
        [APIAttribute(name: "article.get", desc: "获取文章详情")]
        public ResultMessage GetArticle(int iArticleID)
        {
            ResultMessage result = new ResultMessage();
            EHECD_Article entity = Dao.Get(iArticleID) ?? new EHECD_Article();
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取文章详情成功" : "获取文章详情失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion

        #region 获取关于我们

        /// <summary>
        /// 获取关于我们
        /// </summary>
        /// <returns></returns>
        public EHECD_Article GetAboutUs()
        {
            return Dao.GetAbout() ?? new EHECD_Article();
        }

        #endregion

        #region 获取关于我们

        /// <summary>
        /// 获取关于我们
        /// </summary>
        /// <returns></returns>
        [APIAttribute(name: "article.about", desc: "获取关于我们")]
        public ResultMessage GetAbout()
        {
            ResultMessage result = new ResultMessage();
            EHECD_Article entity = Dao.GetAbout() ?? new EHECD_Article();
            result.success = entity.ID == 0 ? false : true;
            result.message = result.success ? "获取关于我们成功" : "获取关于我们失败";
            result.data = entity.ID == 0 ? null : entity;
            return result;
        }

        #endregion
    }
}