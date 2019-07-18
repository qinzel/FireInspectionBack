			
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
    /// 帖子评论控制器
    /// </summary>
    public class CommentController : Controller
    {
		#region 后台帖子评论列表视图
	
        /// <summary>
        /// 后台帖子评论列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台帖子评论编辑视图

        /// <summary>
        /// 后台帖子评论编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(long id)
		        
		{
            EHECD_Comment entity = new  EHECD_Comment();
					if (id != 0)
			
						
				entity = CommentService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台帖子评论详情视图

        /// <summary>
        /// 后台帖子评论详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(CommentService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取帖子评论数据

        /// <summary>
        /// 获取帖子评论数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(CommentService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑帖子评论

        /// <summary>
        /// 添加编辑帖子评论
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_Comment entity)
        {
            return Json(CommentService.Instance.Set(entity));
        }

        #endregion

        #region 删除帖子评论

        /// <summary>
        /// 删除帖子评论
        /// </summary>
        /// <param name="iCommentID"></param>
        /// <returns></returns>
        public JsonResult Delete(long iCommentID)
        {
            return Json(CommentService.Instance.Delete(iCommentID));
        }

		#endregion
    }
}
