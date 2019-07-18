using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Web.Areas.RepairDept.Controllers
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

        #region 后台站内信详情视图

        /// <summary>
        /// 后台站内信详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Detail(int id)
		{
            LoginUser user = AuthHelper.GetLogRepairUser();
            return View(SiteMsgService.Instance.GetUnitSiteMsgInfo(id, user.iUnitID));
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
            param.condition.Add("iUnitType", 3);
            return Content(SiteMsgService.Instance.GetGridData(param));
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
            LoginUser user = AuthHelper.GetLogRepairUser();
            return Json(SiteMsgService.Instance.UnitDelete(sIds, user.iUnitID));
        }

		#endregion
    }
}
