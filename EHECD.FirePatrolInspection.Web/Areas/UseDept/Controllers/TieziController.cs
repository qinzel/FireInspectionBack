
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
{
	/// <summary>
    /// 帖子控制器
    /// </summary>
    public class TieziController : Controller
    {
		#region 后台帖子列表视图
	
        /// <summary>
        /// 后台帖子列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台帖子编辑视图

        /// <summary>
        /// 后台帖子编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(long id)
		        
		{
            EHECD_Tiezi entity = new  EHECD_Tiezi();
					if (id != 0)
			
						
				entity = TieziService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台帖子详情视图

        /// <summary>
        /// 后台帖子详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(TieziService.Instance.Get(id));
        }

        #endregion

        #region 获取单位帖子数据

        /// <summary>
        /// 获取单位帖子数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUnitID", user.iUnitID);
            return Content(TieziService.Instance.GetUnitGridData(param));
        }
		
		#endregion
		
		#region 添加编辑帖子

        /// <summary>
        /// 添加编辑帖子
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_Tiezi entity)
        {
            return Json(TieziService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除帖子

        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(TieziService.Instance.Delete(sIds));
        }

		#endregion
    }
}
