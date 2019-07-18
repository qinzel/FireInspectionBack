using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.EntityFramework.EFWork;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
{
	/// <summary>
    /// 控制器
    /// </summary>
    public class ActionLogController : Controller
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
            return ActionLogService.Instance.GetTypeList();
        }

        #endregion

        #region 获取操作日志

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList(int rows, int page, string sName, string sStartTime, string sEndTime, string sType)
        {
            return Content(ActionLogService.Instance.GetList(sName, sStartTime, sEndTime, sType, page, rows));
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
            EHECD_ActionLog item = ActionLogService.Instance.Get(id);
            EHECD_User user = UserService.Instance.Get(item.iUserID);

            ViewBag.sName = user.sRealName;
            return View(item);
        }

        #endregion
    }
}