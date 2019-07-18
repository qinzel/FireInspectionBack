
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using System.Text;
using EHECD.EntityFramework.Models;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
{
	/// <summary>
    /// 巡检记录控制器
    /// </summary>
    public class InspectionRecordController : Controller
    {
		#region 后台巡检记录列表视图
	
        /// <summary>
        /// 后台巡检记录列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult InspectionList()
        {
            #region 绑定设备类型列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().Where(o => o.iUseDeptID == user.iUnitID || o.iUseDeptID == 0).ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var type in typeList)
            {
                typeselect.Add(new SelectListItem() { Text = type.sName, Value = type.ID.ToString() });
            }
            ViewBag.typeList = typeselect;

            #endregion

            #region 绑定维护公司列表

            List<EHECD_Unit> repairList = UnitService.Instance.GetListByType(2).ToList();
            List<SelectListItem> repairselect = new List<SelectListItem>();
            repairselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in repairList)
            {
                repairselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.repairList = repairselect;

            #endregion

            return View();
        }

        #endregion

        #region 后台维修记录列表视图

        /// <summary>
        /// 后台维修记录列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult RepairList()
        {
            #region 绑定设备类型列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().Where(o => o.iUseDeptID == user.iUnitID || o.iUseDeptID == 0).ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var type in typeList)
            {
                typeselect.Add(new SelectListItem() { Text = type.sName, Value = type.ID.ToString() });
            }
            ViewBag.typeList = typeselect;

            #endregion

            #region 绑定维护公司列表

            List<EHECD_Unit> repairList = UnitService.Instance.GetListByType(2).ToList();
            List<SelectListItem> repairselect = new List<SelectListItem>();
            repairselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in repairList)
            {
                repairselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.repairList = repairselect;

            #endregion

            return View();
        }

        #endregion

        #region 后台更换记录列表视图

        /// <summary>
        /// 后台更换记录列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeList()
        {
            #region 绑定设备类型列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().Where(o => o.iUseDeptID == user.iUnitID || o.iUseDeptID == 0).ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var type in typeList)
            {
                typeselect.Add(new SelectListItem() { Text = type.sName, Value = type.ID.ToString() });
            }
            ViewBag.typeList = typeselect;

            #endregion

            #region 绑定维护公司列表

            List<EHECD_Unit> repairList = UnitService.Instance.GetListByType(2).ToList();
            List<SelectListItem> repairselect = new List<SelectListItem>();
            repairselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in repairList)
            {
                repairselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.repairList = repairselect;

            #endregion

            return View();
        }

        #endregion

        #region 后台巡检记录列表视图

        /// <summary>
        /// 后台巡检记录列表视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeviceRecordList(int id)
        {
            ViewBag.iDeviceID = id;
            return View();
        }
		
		#endregion
		
		#region 后台巡检记录详情视图

        /// <summary>
        /// 后台巡检记录详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(InspectionRecordService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取巡检记录数据

        /// <summary>
        /// 获取巡检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetInsGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            param.condition.Add("iRecordType", 0);
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(InspectionRecordService.Instance.GetAllTypeGridData(param));
        }

        #endregion

        #region 获取巡检记录数据

        /// <summary>
        /// 获取巡检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetRepairGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            param.condition.Add("iRecordType", 1);
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(InspectionRecordService.Instance.GetAllTypeGridData(param));
        }

        #endregion

        #region 获取巡检记录数据

        /// <summary>
        /// 获取巡检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetChangeGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            param.condition.Add("iRecordType", 2);
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(InspectionRecordService.Instance.GetAllTypeGridData(param));
        }

        #endregion

        #region 获取巡检记录数据

        /// <summary>
        /// 获取巡检记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetAllTypeListGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(InspectionRecordService.Instance.GetAllTypeListGridData(param));
        }

        #endregion

        #region 导出巡检记录

        public void GetInsExportData()
        {
            GetExportData(0);
        }

        #endregion

        #region 导出维修记录

        public void GetRepairExportData()
        {
            GetExportData(1);
        }

        #endregion

        #region 导出更换记录

        public void GetChangeExportData()
        {
            GetExportData(2);
        }

        #endregion

        #region 导出记录数据

        /// <summary>
        /// 导出记录数据
        /// <param name="iRecordType"></param>
        /// </summary>
        public void GetExportData(int iRecordType)
        {
            LoginUser user = AuthHelper.GetLogUseUser();

            if (user == null)
            {
                return;
            }

            bool exportSuc = false;
            try
            {
                var sKeyword = Request.QueryString["sKeyword"];
                var iStatus = Request.QueryString["iStatus"];
                var iDeviceTypeID = Request.QueryString["iDeviceTypeID"];
                var iRepairDeptID = Request.QueryString["iRepairDeptID"];
                var sOperator = Request.QueryString["sOperator"];
                var dStartTime = Request.QueryString["dStartTime"];
                var dEndTime = Request.QueryString["dEndTime"];
                var sClientPersonName = Request.QueryString["sClientPersonName"];

                QueryParams param = new QueryParams();
                param.condition.Add("sKeyword", sKeyword);
                param.condition.Add("iStatus", iStatus);
                param.condition.Add("iDeviceTypeID", iDeviceTypeID);
                param.condition.Add("iRepairDeptID", iRepairDeptID);
                param.condition.Add("iUseDeptID", user.iUnitID);
                param.condition.Add("sOperator", sOperator);
                param.condition.Add("sClientPersonName", sClientPersonName);
                param.condition.Add("dStartTime", dStartTime);
                param.condition.Add("dEndTime", dEndTime);
                param.condition.Add("iRecordType", iRecordType);

                //接收需要导出的数据
                List<EHECD_InspectionRecord> list = InspectionRecordService.Instance.GetExportData(param).ToList();

                //命名导出表格的StringBuilder变量
                StringBuilder sHtml = new StringBuilder(string.Empty);

                var sTitle = String.Empty;
                switch (iRecordType)
                {
                    case 0:
                        sTitle = "巡检";
                        break;
                    case 1:
                        sTitle = "维修";
                        break;
                    case 2:
                        sTitle = "更换";
                        break;
                }

                //打印表头
                sHtml.Append("<table border=\"1\" width=\"100%\">");
                sHtml.Append("<tr height=\"40\"><td colspan=\"10\" align=\"center\"><b style='font-size: 20px'>" + sTitle + "记录</b></td></tr>");
                sHtml.Append("<tr align=\"center\">");
                sHtml.Append("<td style='width: 200px'>设备编号</td>");
                sHtml.Append("<td style='width: 150px'>设备名称</td>");
                sHtml.Append("<td style='width: 150px'>设备类型</td>");
                sHtml.Append("<td style='width: 150px'>设备位置</td>");
                sHtml.Append("<td style='width: 150px'>责任人</td>");
                sHtml.Append("<td style='width: 150px'>所属部门</td>");
                sHtml.Append("<td style='width: 150px'>维护公司</td>");
                sHtml.Append("<td style='width: 150px'>"+ sTitle + "人</td>");
                sHtml.Append("<td style='width: 150px'>" + sTitle + "时间</td>");
                switch (iRecordType)
                {
                    case 0:
                        sHtml.Append("<td style='width: 150px'>巡检结果</td>");
                        break;
                    case 1:
                        sHtml.Append("<td style='width: 150px'>维修说明</td>");
                        break;
                    case 2:
                        sHtml.Append("<td style='width: 150px'>更换说明</td>");
                        break;
                }
                
                sHtml.Append("</tr>");

                //循环读取List集合 
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    sHtml.Append("<tr height=\"40\" align=\"center\">");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sNumber + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sDeviceTypeName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sLocation + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sClientPersonName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sOrganName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sRepairDeptName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sOperator + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.dCreateTime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                    if (iRecordType == 0)
                    {
                        sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + (item.iStatus ? "<span style='color: red'>异常</span>" : "<span style='color: cornflowerblue'>正常</span>") + "</td>");
                    }
                    else
                    {
                        sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sDesc + "</td>");
                    }
                    sHtml.Append("</tr>");
                }
                sHtml.Append("</table>");

                //调用输出Excel表的方法
                ExportToExcel("application/ms-excel", sTitle + "记录列表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", sHtml.ToString());
                exportSuc = true;
            }
            catch
            {
                exportSuc = false;
            }
            finally
            {
                ActionLogService.Instance.Add(null, user.ID, "导出记录", exportSuc ? "导出记录成功" : "导出记录失败");
            }
        }

        /// <summary>
        /// 输入HTTP头，然后把指定的流输出到指定的文件名，然后指定文件类型
        /// </summary>
        /// <param name="fileType">文件MIME</param>
        /// <param name="fileName">文件名</param>
        /// <param name="excelContent">文件内容</param>
        public void ExportToExcel(string fileType, string fileName, string excelContent)
        {
            System.Web.HttpContext.Current.Response.ContentType = fileType;
            System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            System.Web.HttpContext.Current.Response.Charset = "GB2312";

            var _fileName = fileName;

            if (Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") != -1)
            {
                _fileName = "\"" + _fileName + "\"";
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            }
            else
            {
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8).ToString());
            }

            System.Web.HttpContext.Current.Response.Write("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=utf-8\"/>" + excelContent.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }

        #endregion
    }
}
