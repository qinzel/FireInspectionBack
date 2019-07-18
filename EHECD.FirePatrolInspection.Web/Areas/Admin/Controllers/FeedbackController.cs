			
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
	/// <summary>
    /// 意见反馈控制器
    /// </summary>
    public class FeedbackController : Controller
    {
		#region 后台意见反馈列表视图
	
        /// <summary>
        /// 后台意见反馈列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        #endregion

        #region 后台意见反馈回复视图

        /// <summary>
        /// 后台意见反馈回复视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reply(long id)
		{
            ViewBag.ID = id;
            return View();
        }
		
		#endregion
		
		#region 后台意见反馈详情视图

        /// <summary>
        /// 后台意见反馈详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(FeedbackService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取意见反馈数据

        /// <summary>
        /// 获取意见反馈数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(FeedbackService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑意见反馈

        /// <summary>
        /// 添加编辑意见反馈
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Reply(EHECD_FeedbackReply entity)
        {
            return Json(FeedbackReplyService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除意见反馈

        /// <summary>
        /// 删除意见反馈
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(FeedbackService.Instance.Delete(sIds));
        }

		#endregion
    }
}
