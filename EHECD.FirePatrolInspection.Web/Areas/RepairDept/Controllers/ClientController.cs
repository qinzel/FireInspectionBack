using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Web.Areas.RepairDept.Controllers
{
	/// <summary>
    /// 前端用户控制器
    /// </summary>
    public class ClientController : Controller
    {
        #region 后台人员列表视图

        /// <summary>
        /// 后台人员列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }

        #endregion

        #region 后台前端用户编辑视图

        /// <summary>
        /// 后台前端用户编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Set(int id)
        {
            EHECD_Client entity = new EHECD_Client();
            if (id != 0)

                entity = ClientService.Instance.GetRepairDeptClient(id);

            return View(entity);
        }

        #endregion

        #region 编辑前端用户

        /// <summary>
        /// 编辑前端用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_Client entity)
        {
            if (entity.ID == 0)
            {
                LoginUser user = AuthHelper.GetLogRepairUser();
                entity.iUnitID = user.iUnitID;
                entity.iType = 2;
            }
            return Json(ClientService.Instance.Set(entity));
        }

        #endregion

        #region 后台前端用户详情视图

        /// <summary>
        /// 后台前端用户详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            return View(ClientService.Instance.GetClient(id));
        }

        #endregion

        #region 获取前端用户数据

        /// <summary>
        /// 获取前端用户数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogRepairUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(ClientService.Instance.GetUnitGridData(param));
        }

        #endregion

        #region 删除前端用户

        /// <summary>
        /// 删除前端用户
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(ClientService.Instance.Delete(sIds));
        }

        #endregion

        #region 重置前端用户密码

        /// <summary>
        /// 重置前端用户密码
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Reset(string sIds)
        {
            return Json(ClientService.Instance.Reset(sIds));
        }

        #endregion
    }
}