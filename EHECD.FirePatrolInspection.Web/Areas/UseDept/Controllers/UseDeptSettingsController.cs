using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;
using System.Collections.Generic;
using System.Linq;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
{
	/// <summary>
    /// 使用单位基础设置控制器
    /// </summary>
    public class UseDeptSettingsController : Controller
    {
        #region 后台使用单位基础设置列表视图

        /// <summary>
        /// 后台使用单位基础设置列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region 绑定消防部门列表

            LoginUser user = AuthHelper.GetLogUseUser();
            EHECD_UseDeptSettings settings = UseDeptSettingsService.Instance.GetSettings(user.iUnitID);
            List<EHECD_Unit> fireList = UnitService.Instance.GetListByType(1).ToList();
            List<SelectListItem> fireselect = new List<SelectListItem>();
            fireselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in fireList)
            {
                fireselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString(), Selected = unit.ID == settings.iFireDeptID });
            }
            ViewBag.fireList = fireselect;

            #endregion

            #region 获取已绑定的维护公司

            List<EHECD_Unit> unitList = UnitService.Instance.GetUnitListBySettingsID(settings.ID).ToList();
            List<EHECD_Unit> allList = UnitService.Instance.GetListByType(2).ToList();
            for (var i = 0; i < allList.Count; i++)
            {
                var unit = allList[i];
                for (var j = 0; j < unitList.Count; j++)
                {
                    var u = unitList[j];
                    if (u.ID == unit.ID)
                    {
                        unit.iStatus = true;
                    }
                }
            }

            #endregion

            ViewBag.unit = UnitService.Instance.Get(user.iUnitID);
            ViewBag.repairList = allList;
            return View(settings);
        }

        #endregion
		
		#region 添加编辑使用单位基础设置

        /// <summary>
        /// 添加编辑使用单位基础设置
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_UseDeptSettings entity)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            entity.iUseDeptID = user.iUnitID;
            return Json(UseDeptSettingsService.Instance.Set(entity));
        }

		#endregion
    }
}
