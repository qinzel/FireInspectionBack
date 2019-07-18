using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
{
	/// <summary>
    /// 值班室控制器
    /// </summary>
    public class DutyRoomController : Controller
    {
		#region 后台值班室列表视图
	
        /// <summary>
        /// 后台值班室列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
		
		#endregion
		
		#region 后台值班室编辑视图

        /// <summary>
        /// 后台值班室编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(int id)
		        
		{
            EHECD_DutyRoom entity = new  EHECD_DutyRoom();
					if (id != 0)
			
						
				entity = DutyRoomService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台值班室详情视图

        /// <summary>
        /// 后台值班室详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(int id)
		{
            return View(DutyRoomService.Instance.Get(id));
        }

        #endregion

        #region 后台打印值班室二维码

        /// <summary>
        /// 后台打印值班室二维码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PrintQRCode(string id)
        {
            return View(DutyRoomService.Instance.GetDataByIDs(id));
        }

        #endregion

        #region 获取值班室数据

        /// <summary>
        /// 获取值班室数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(DutyRoomService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑值班室

        /// <summary>
        /// 添加编辑值班室
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_DutyRoom entity)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            entity.iUseDeptID = user.iUnitID;
            return Json(DutyRoomService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除值班室

        /// <summary>
        /// 删除值班室
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(DutyRoomService.Instance.Delete(sIds));
        }

        #endregion
    }
}