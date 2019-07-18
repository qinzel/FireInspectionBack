using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using System;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
	/// <summary>
    /// 文章控制器
    /// </summary>
    public class ArticleController : Controller
    {
        #region 后台法律知识列表视图

        /// <summary>
        /// 后台法律知识列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult LegalKnowledgeList()
        {
            return View();
        }

        #endregion

        #region 后台消防知识列表视图

        /// <summary>
        /// 后台消防知识列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FireProtectionKnowledgeList()
        {
            return View();
        }

        #endregion

        #region 后台资讯列表视图

        /// <summary>
        /// 后台资讯列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult NewsList()
        {
            return View();
        }

        #endregion

        #region 后台帮助中心列表视图

        /// <summary>
        /// 后台帮助中心列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult HelpList()
        {
            return View();
        }

        #endregion

        #region 后台文章编辑视图

        /// <summary>
        /// 后台文章编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public ActionResult Set(int id, string iType = "")
		{
            EHECD_Article entity = new  EHECD_Article();
            if (id != 0)
				entity = ArticleService.Instance.Get(id);
            if (id == 0 && !string.IsNullOrWhiteSpace(iType))
            {
                entity.iType = Convert.ToInt32(iType);
            }
					
            return View(entity);
        }

        #endregion

        #region 后台帮助中心文章编辑视图

        /// <summary>
        /// 后台帮助中心文章编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public ActionResult SetHelp(int id, string iType = "")
        {
            EHECD_Article entity = new EHECD_Article();
            if (id != 0)
                entity = ArticleService.Instance.Get(id);
            if (id == 0 && !string.IsNullOrWhiteSpace(iType))
            {
                entity.iType = Convert.ToInt32(iType);
            }

            return View(entity);
        }

        #endregion

        #region 后台消防知识文章编辑视图

        /// <summary>
        /// 后台消防知识文章编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public ActionResult SetFireProtectionKnowledge(int id, string iType = "")
        {
            EHECD_Article entity = new EHECD_Article();
            if (id != 0)
                entity = ArticleService.Instance.Get(id);
            if (id == 0 && !string.IsNullOrWhiteSpace(iType))
            {
                entity.iType = Convert.ToInt32(iType);
            }

            return View(entity);
        }

        #endregion

        #region 后台法律知识文章编辑视图

        /// <summary>
        /// 后台法律知识文章编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public ActionResult SetLegalKnowledge(int id, string iType = "")
        {
            EHECD_Article entity = new EHECD_Article();
            if (id != 0)
                entity = ArticleService.Instance.Get(id);
            if (id == 0 && !string.IsNullOrWhiteSpace(iType))
            {
                entity.iType = Convert.ToInt32(iType);
            }

            return View(entity);
        }

        #endregion

        #region 后台资讯文章编辑视图

        /// <summary>
        /// 后台资讯文章编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public ActionResult SetNews(int id, string iType = "")
        {
            EHECD_Article entity = new EHECD_Article();
            if (id != 0)
                entity = ArticleService.Instance.Get(id);
            if (id == 0 && !string.IsNullOrWhiteSpace(iType))
            {
                entity.iType = Convert.ToInt32(iType);
            }

            return View(entity);
        }

        #endregion

        #region 后台关于我们编辑视图

        /// <summary>
        /// 后台关于我们编辑视图
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
		{
            EHECD_Article entity = ArticleService.Instance.GetAboutUs();
            return View(entity);
        }
		
		#endregion
		
		#region 后台文章详情视图

        /// <summary>
        /// 后台文章详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(int id)
		{
            return View(ArticleService.Instance.Get(id));
        }

        #endregion

        #region 后台帮助中心文章详情视图

        /// <summary>
        /// 后台帮助中心文章详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DetailHelp(int id)
        {
            return View(ArticleService.Instance.Get(id));
        }

        #endregion

        #region 后台资讯文章详情视图

        /// <summary>
        /// 后台资讯文章详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DetailNews(int id)
        {
            return View(ArticleService.Instance.Get(id));
        }

        #endregion

        #region 后台消防知识详情视图

        /// <summary>
        /// 后台消防知识详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DetailFireProtectionKnowledge(int id)
        {
            return View(ArticleService.Instance.Get(id));
        }

        #endregion

        #region 后台法律知识详情视图

        /// <summary>
        /// 后台法律知识详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DetailLegalKnowledge(int id)
        {
            return View(ArticleService.Instance.Get(id));
        }

        #endregion

        #region 获取消防知识文章数据

        /// <summary>
        /// 获取消防知识文章数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(ArticleService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取法律知识文章数据

        /// <summary>
        /// 获取法律知识文章数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetLegalGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
            param.condition.Add("iType", 0);
            return Content(ArticleService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取消防知识文章数据

        /// <summary>
        /// 获取消防知识文章数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetFireGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
            param.condition.Add("iType", 1);
            return Content(ArticleService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取资讯文章数据

        /// <summary>
        /// 获取资讯文章数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetNewsGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
            param.condition.Add("iType", 2);
            return Content(ArticleService.Instance.GetGridData(param));
        }

        #endregion

        #region 获取帮助中心文章数据

        /// <summary>
        /// 获取帮助中心文章数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetHelpGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            //文章类别[0:法律知识,1:消防知识,2:资讯,3:帮助,4:关于我们]
            param.condition.Add("iType", 3);
            return Content(ArticleService.Instance.GetGridData(param));
        }

        #endregion

        #region 添加编辑文章

        /// <summary>
        /// 添加编辑文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Set(EHECD_Article entity)
        {
            if (string.IsNullOrWhiteSpace(entity.sImageSrc))
            {
                entity.sImageSrc = string.Empty;
            }
            return Json(ArticleService.Instance.Set(entity));
        }

        #endregion

        #region 添加编辑文章

        /// <summary>
        /// 添加编辑文章
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult About(EHECD_Article entity)
        {
            return Json(ArticleService.Instance.About(entity));
        }

        #endregion

        #region 删除文章

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(ArticleService.Instance.Delete(sIds));
        }

        #endregion

        #region 删除消防知识

        /// <summary>
        /// 删除消防知识
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult DeleteFireProtectionKnowledge(string sIds)
        {
            return Json(ArticleService.Instance.Delete(sIds));
        }

        #endregion

        #region 删除资讯

        /// <summary>
        /// 删除资讯
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult DeleteNews(string sIds)
        {
            return Json(ArticleService.Instance.Delete(sIds));
        }

        #endregion

        #region 删除帮助中心文章

        /// <summary>
        /// 删除帮助中心文章
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult DeleteHelp(string sIds)
        {
            return Json(ArticleService.Instance.Delete(sIds));
        }

        #endregion

        #region 删除法律知识

        /// <summary>
        /// 删除法律知识
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult DeleteLegalKnowledge(string sIds)
        {
            return Json(ArticleService.Instance.Delete(sIds));
        }

        #endregion
    }
}
