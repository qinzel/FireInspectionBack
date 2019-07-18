using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.EntityFramework.EFWork;
using EHECD.EntityFramework.Models;
using EHECD.FirePatrolInspection.Web.Helper;

namespace EHECD.FirePatrolInspection.Web.Areas.FireDept.Controllers
{
	/// <summary>
    /// 控制器
    /// </summary>
    public class UnitActionLogController : Controller
    {
        #region 列表页视图

        /// <summary>
        /// 列表页视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region 获取操作类型列表

        /// <summary>
        /// 获取操作类型列表
        /// </summary>
        /// <returns></returns>
        public string GetTypeList()
        {
            LoginUser user = AuthHelper.GetLogFireUser();
            return UnitActionLogService.Instance.GetTypeList(user.iUnitID);
        }

        #endregion

        #region 获取操作日志

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList(int rows, int page, string sName, string sStartTime, string sEndTime, string sType)
        {
            LoginUser user = AuthHelper.GetLogFireUser();
            return Content(UnitActionLogService.Instance.GetList(sName, sStartTime, sEndTime, sType, user.iUnitID, page, rows));
        }

        #endregion

        #region 获取日志详情

        /// <summary>
        /// 获取日志详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(long id)
        {
            EHECD_UnitActionLog item = UnitActionLogService.Instance.Get(id);
            EHECD_UnitUser user = UnitUserService.Instance.Get(item.iUnitUserID);

            ViewBag.sName = user.sRealName;
            return View(item);
        }

        #endregion
    }
}