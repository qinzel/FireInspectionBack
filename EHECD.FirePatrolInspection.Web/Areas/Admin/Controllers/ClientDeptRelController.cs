			
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
    /// 前端用户所属单位关系表控制器
    /// </summary>
    public class ClientDeptRelController : Controller
    {
		#region 后台前端用户所属单位关系表列表视图
	
        /// <summary>
        /// 后台前端用户所属单位关系表列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台前端用户所属单位关系表编辑视图

        /// <summary>
        /// 后台前端用户所属单位关系表编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Set(long id)
		{
            EHECD_ClientDeptRel entity = new  EHECD_ClientDeptRel();
					if (id != 0)
			
						
				entity = ClientDeptRelService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台前端用户所属单位关系表详情视图

        /// <summary>
        /// 后台前端用户所属单位关系表详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Detail(long id)
		{
            return View(ClientDeptRelService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取前端用户所属单位关系表数据

        /// <summary>
        /// 获取前端用户所属单位关系表数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(ClientDeptRelService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑前端用户所属单位关系表

        /// <summary>
        /// 添加编辑前端用户所属单位关系表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_ClientDeptRel entity)
        {
            return Json(ClientDeptRelService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除前端用户所属单位关系表

        /// <summary>
        /// 删除前端用户所属单位关系表
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(ClientDeptRelService.Instance.Delete(sIds));
        }

		#endregion
    }
}