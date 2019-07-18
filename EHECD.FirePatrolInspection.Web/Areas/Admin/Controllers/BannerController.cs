using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
    public class BannerController : Controller
    {
        #region 后台轮播列表视图

        /// <summary>
        /// 后台轮播列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        #endregion

        #region 后台轮播选择文章列表视图

        /// <summary>
        /// 后台轮播选择文章列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ArticleList(int id)
        {
            ViewBag.iType = id;
            return View();
        }

        #endregion

        #region 后台轮播编辑视图

        /// <summary>
        /// 后台轮播编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Set(int id)
        {
            EHECD_Banner entity = new EHECD_Banner();
            if (id != 0)
                entity = BannerService.Instance.Get(id);

            return View(entity);
        }

        #endregion

        #region 后台轮播详情视图

        /// <summary>
        /// 后台轮播详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            return View(BannerService.Instance.Get(id));
        }

        #endregion

        #region 获取轮播数据

        /// <summary>
        /// 获取轮播数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(BannerService.Instance.GetGridData(param));
        }

        #endregion

        #region 添加编辑轮播

        /// <summary>
        /// 添加编辑轮播
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_Banner entity)
        {
            return Json(BannerService.Instance.Set(entity));
        }

        #endregion

        #region 删除轮播

        /// <summary>
        /// 删除轮播
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(BannerService.Instance.Delete(sIds));
        }

        #endregion
    }
}