using EHECD.EntityFramework.Models;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EHECD.FirePatrolInspection.Web.Areas.FireDept.Controllers
{
    public class StatisticsController : Controller
    {
        #region 后台运维统计视图

        /// <summary>
        /// 后台运维统计视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            LoginUser user = AuthHelper.GetLogFireUser();

            #region 绑定使用单位列表

            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(0).Where(o => o.iParentID == user.iUnitID).ToList();
            List<SelectListItem> unitselect = new List<SelectListItem>();
            foreach (var unit in unitList)
            {
                unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.unitList = unitselect;

            #endregion

            #region 绑定年份列表

            // 系统上线年份
            var iStartYear = 2018;
            var nowYear = DateTime.Now.Year;
            var iSubYear = nowYear - iStartYear;
            iSubYear = (iSubYear < 5 ? 5 : iSubYear + 5);

            List<SelectListItem> yearselect = new List<SelectListItem>();
            for (var i = 0; i < iSubYear; i++)
            {
                var sStartYear = (iStartYear + i).ToString();
                yearselect.Add(new SelectListItem() { Text = sStartYear, Value = sStartYear, Selected = nowYear == iSubYear });
            }
            ViewBag.yearList = yearselect;

            #endregion

            #region 账号申请信息

            var uuCount = UnitUserService.Instance.GetUnitUserCount(user.iUnitID);

            IEnumerable<EHECD_Client> uList = ClientService.Instance.GetListByType(1);
            if (uList != null && uList.Count() > 0)
            {
                ViewBag.clientCount = uList.Where(o => o.iType == 1 && o.iUnitID == user.iUnitID).Count() + uuCount;
            }
            else
            {
                ViewBag.clientCount = uuCount;
            }

            #endregion

            #region 关联使用单位

            ViewBag.dutyList = UnitService.Instance.GetFireDeptUnitsSetting(user.iUnitID);

            #endregion

            #region 单位站内信

            ViewBag.msgList = SiteMsgService.Instance.GetUnitMessageList(user.iUnitID, 2);

            #endregion

            ViewBag.unit = UnitService.Instance.Get(user.iUnitID);
            ViewBag.user = user;

            return View();
        }

        #endregion

        #region 获取设备巡检数据统计

        /// <summary>
        /// 获取设备巡检数据统计
        /// </summary>
        /// <param name="iUseDeptID"></param>
        /// <param name="sYearNumber"></param>
        /// <returns></returns>
        public JsonResult GetRecordGridData(int iUseDeptID, string sYearNumber)
        {
            IEnumerable<EHECD_InspectionRecord> recList = InspectionRecordService.Instance.GetStatisticsGridData(iUseDeptID, sYearNumber);
            // 记录排倒序并去重
            recList = recList.OrderByDescending(o => o.dCreateTime).GroupBy(o => new { o.iDeviceID }).Select(o => o.First()).ToList<EHECD_InspectionRecord>();

            var recAry = new List<int>();

            if (recList != null && recList.Count() > 0)
            {
                for (var i = 1; i <= 12; i++)
                {
                    IEnumerable<EHECD_InspectionRecord> partList = recList.Where(o => o.dCreateTime.Month == i);
                    if (partList != null && partList.Count() > 0)
                    {
                        recAry.Add(partList.Count());
                    }
                    else
                    {
                        recAry.Add(0);
                    }
                }
            }
            else
            {
                for (var i = 0; i < 12; i++)
                {
                    recAry.Add(0);
                }
            }

            return Json(recAry);
        }

        #endregion

        #region 获取设备巡检数据统计

        /// <summary>
        /// 获取设备巡检数据统计
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sYearNumber"></param>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public ActionResult InspectionRecordList(int id, string sYearNumber, int iMonth = 0)
        {
            ViewBag.iUseDeptID = id;
            ViewBag.sYearNumber = sYearNumber;
            ViewBag.iMonth = iMonth + 1;

            return View();
        }

        #endregion

        #region 获取各使用单位正常/异常设备数据

        /// <summary>
        /// 获取各使用单位正常/异常设备数据
        /// </summary>
        /// <param name="iUseDeptID"></param>
        /// <returns></returns>
        public JsonResult GetDeviceStatusGridData(int iUseDeptID)
        {
            IEnumerable<EHECD_Device> devList = DeviceService.Instance.GetDeviceList(iUseDeptID);
            var devAry = new List<string>();

            if (devList != null && devList.Count() > 0)
            {
                IEnumerable<EHECD_Device> list1 = devList.Where(o => o.iAbnormalStatus);
                IEnumerable<EHECD_Device> list0 = devList.Where(o => !o.iAbnormalStatus);
                if (list1 != null && list1.Count() > 0)
                {
                    devAry.Add("{ \"value\": " + list1.Count() + ", \"name\": \"异常占比\", \"istatus\": 1 }");
                }
                else
                {
                    devAry.Add("{ \"value\": 0, \"name\": \"异常占比\", \"istatus\": 1 }");
                }
                if (list0 != null && list0.Count() > 0)
                {
                    devAry.Add("{ \"value\": " + list0.Count() + ", \"name\": \"正常占比\", \"istatus\": 0 }");
                }
                else
                {
                    devAry.Add("{ \"value\": 0, \"name\": \"正常占比\", \"istatus\": 0 }");
                }
            }
            else
            {
                devAry.Add("{ \"value\": 0, \"name\": \"异常占比\", \"istatus\": 1 }");
                devAry.Add("{ \"value\": 0, \"name\": \"正常占比\", \"istatus\": 0 }");
            }

            return Json(devAry);
        }

        #endregion

        #region 获取各使用单位过期/未过期设备数据

        /// <summary>
        /// 获取各使用单位过期/未过期设备数据
        /// </summary>
        /// <param name="iUseDeptID"></param>
        /// <returns></returns>
        public JsonResult GetDeviceExpiredGridData(int iUseDeptID)
        {
            IEnumerable<EHECD_Device> devList = DeviceService.Instance.GetDeviceList(iUseDeptID);
            var devAry = new List<string>();

            if (devList != null && devList.Count() > 0)
            {
                IEnumerable<EHECD_Device> list1 = devList.Where(o => o.sProductionDate != "" && DateTime.Parse(o.sProductionDate).AddYears(o.iExpiredYears) < DateTime.Now);
                IEnumerable<EHECD_Device> list0 = devList.Where(o => o.sProductionDate == "" || DateTime.Parse(o.sProductionDate).AddYears(o.iExpiredYears) >= DateTime.Now);
                if (list1 != null && list1.Count() > 0)
                {
                    devAry.Add("{ \"value\": " + list1.Count() + ", \"name\": \"过期占比\", \"istatus\": 1 }");
                }
                else
                {
                    devAry.Add("{ \"value\": 0, \"name\": \"过期占比\", \"istatus\": 1 }");
                }
                if (list0 != null && list0.Count() > 0)
                {
                    devAry.Add("{ \"value\": " + list0.Count() + ", \"name\": \"未过期占比\", \"istatus\": 0 }");
                }
                else
                {
                    devAry.Add("{ \"value\": 0, \"name\": \"未过期占比\", \"istatus\": 0 }");
                }
            }
            else
            {
                devAry.Add("{ \"value\": 0, \"name\": \"过期占比\", \"istatus\": 1 }");
                devAry.Add("{ \"value\": 0, \"name\": \"未过期占比\", \"istatus\": 0 }");
            }

            return Json(devAry);
        }

        #endregion

        #region 后台部门点检员下辖设备列表视图

        /// <summary>
        /// 后台部门点检员下辖设备列表视图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iStatus"></param>
        /// <returns></returns>
        public ActionResult DeviceStatusList(int id, int iStatus)
        {
            ViewBag.iUseDeptID = id;
            ViewBag.iAbnormalStatus = iStatus;
            return View();
        }

        #endregion

        #region 后台部门点检员下辖设备列表视图

        /// <summary>
        /// 后台部门点检员下辖设备列表视图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iExpired"></param>
        /// <returns></returns>
        public ActionResult DeviceExpiredList(int id, int iExpired)
        {
            ViewBag.iUseDeptID = id;
            ViewBag.bIsExpired = iExpired;
            return View();
        }

        #endregion
    }
}