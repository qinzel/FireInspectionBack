			
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;

namespace EHECD.FirePatrolInspection.Web.Areas.FireDept.Controllers
{
	/// <summary>
    /// 各平台超管账号注册控制器
    /// </summary>
    public class UnitController : Controller
    {
        #region 后台各平台超管账号注册详情视图

        /// <summary>
        /// 后台各平台超管账号注册详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Detail(int id)
		{
            EHECD_Unit entity = UnitService.Instance.Get(id) ?? new EHECD_Unit();
            if (entity.ID > 0)
            {
                entity.UnitList = UnitService.Instance.GetFireDeptUnitsSetting(entity.ID).ToList();
            }
            return View(entity);
        }

        #endregion
    }
}