			
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
    /// 站内信控制器
    /// </summary>
    public class SiteMsgController : Controller
    {
		#region 后台站内信列表视图
	
        /// <summary>
        /// 后台站内信列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台站内信编辑视图

        /// <summary>
        /// 后台站内信编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Set(int id)
		{
            EHECD_SiteMsg entity = new  EHECD_SiteMsg();
            if (id != 0)
				entity = SiteMsgService.Instance.Get(id);
					
            return View(entity);
        }

        #endregion

        #region 后台站内信编辑视图

        /// <summary>
        /// 后台站内信编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Send(string id)
        {
            ViewBag.sIds = id;
            return View();
        }

        #endregion

        #region 后台站内信详情视图

        /// <summary>
        /// 后台站内信详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Detail(int id)
		{
            return View(SiteMsgService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取站内信数据

        /// <summary>
        /// 获取站内信数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(SiteMsgService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑站内信

        /// <summary>
        /// 添加编辑站内信
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Set(EHECD_SiteMsg entity)
        {
            if (string.IsNullOrWhiteSpace(entity.sOperator))
            {
                entity.sOperator = UserSession.GetLogUser().sLoginName;
            }
            if (string.IsNullOrWhiteSpace(entity.sReceiveClient))
            {
                entity.sReceiveClient = String.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sReceiveDept))
            {
                entity.sReceiveDept = String.Empty;
            }
            if (entity.iType == 0)
            {
                entity.sReceiveClient = String.Empty;
            }
            else
            {
                entity.sReceiveDept = String.Empty;
            }
            return Json(SiteMsgService.Instance.Set(entity));
        }

        #endregion

        #region 发送站内信

        /// <summary>
        /// 发送站内信
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Send(EHECD_SiteMsg entity)
        {
            if (string.IsNullOrWhiteSpace(entity.sOperator))
            {
                entity.sOperator = UserSession.GetLogUser().sLoginName;
            }
            if (string.IsNullOrWhiteSpace(entity.sReceiveDept))
            {
                entity.sReceiveDept = String.Empty;
            }
            return Json(SiteMsgService.Instance.Send(entity));
        }

        #endregion

        #region 删除站内信

        /// <summary>
        /// 删除站内信
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(SiteMsgService.Instance.Delete(sIds));
        }

		#endregion
    }
}
