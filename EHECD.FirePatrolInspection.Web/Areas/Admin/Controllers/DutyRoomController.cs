using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using System.Collections.Generic;
using System.Linq;

namespace EHECD.FirePatrolInspection.Web.Areas.Admin.Controllers
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
            #region 绑定使用单位列表

            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(0).ToList();
            List<SelectListItem> unitselect = new List<SelectListItem>();
            unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in unitList)
            {
                unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.unitList = unitselect;

            #endregion

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

            #region 绑定使用单位列表

            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(0).ToList();
            List<SelectListItem> unitselect = new List<SelectListItem>();
            unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in unitList)
            {
                unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString(), Selected = unit.ID == entity.iUseDeptID });
            }
            ViewBag.unitList = unitselect;

            #endregion

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