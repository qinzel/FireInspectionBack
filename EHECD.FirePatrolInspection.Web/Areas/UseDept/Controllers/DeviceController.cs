
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EHECD.FirePatrolInspection.Service;
using EHECD.FirePatrolInspection.Entity;
using EHECD.FirePatrolInspection.Web.Helper;
using EHECD.EntityFramework.Models;
using System.Text;
using System.IO;
using EHECD.Common;
using System.Data;

namespace EHECD.FirePatrolInspection.Web.Areas.UseDept.Controllers
{
	/// <summary>
    /// 设备控制器
    /// </summary>
    public class DeviceController : Controller
    {
		#region 后台设备列表视图
	
        /// <summary>
        /// 后台设备列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
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

            List<EHECD_Unit> repairList = UnitService.Instance.GetRelRepairDeptAllList(user.iUnitID).ToList();
            List<SelectListItem> repairselect = new List<SelectListItem>();
            repairselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in repairList)
            {
                repairselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.repairList = repairselect;

            #endregion

            ViewBag.iUseDeptID = user.iUnitID;

            return View();
        }

        #endregion

        #region 后台设备列表视图

        /// <summary>
        /// 后台设备列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult RelDeviceList(long id)
        {
            ViewBag.currentID = id;
            EHECD_Device entity = DeviceService.Instance.Get(id) ?? new EHECD_Device();
            if (entity.ID > 0)
            {
                ViewBag.sDeviceIDs = entity.sDeviceIDs;
            }
            else
            {
                ViewBag.sDeviceIDs = String.Empty;
            }

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

            return View();
        }

        #endregion

        #region 后台设备列表视图

        /// <summary>
        /// 后台设备列表视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeviceList(int id)
        {
            ViewBag.iClientID = id;
            LoginUser user = AuthHelper.GetLogUseUser();
            ViewBag.iUseDeptID = user.iUnitID;

            #region 绑定设备类型列表

            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().Where(o => o.iUseDeptID == user.iUnitID || o.iUseDeptID == 0).ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var type in typeList)
            {
                typeselect.Add(new SelectListItem() { Text = type.sName, Value = type.ID.ToString() });
            }
            ViewBag.typeList = typeselect;

            #endregion

            return View();
        }

        #endregion

        #region 后台设备编辑视图

        /// <summary>
        /// 后台设备编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Set(long id)
		{
            EHECD_Device entity = new  EHECD_Device();
            if (id != 0)
				entity = DeviceService.Instance.Get(id);

            #region 绑定设备类型列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().Where(o => o.iUseDeptID == user.iUnitID || o.iUseDeptID == 0).ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var type in typeList)
            {
                typeselect.Add(new SelectListItem() { Text = type.sName, Value = type.ID.ToString(), Selected = type.ID == entity.iDeviceTypeID });
            }
            ViewBag.typeList = typeselect;

            #endregion

            #region 绑定维护公司列表

            List<EHECD_Unit> repairList = UnitService.Instance.GetRelRepairDeptAllList(user.iUnitID).ToList();
            List<SelectListItem> repairselect = new List<SelectListItem>();
            repairselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in repairList)
            {
                repairselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString(), Selected = unit.ID == entity.iUseDeptID });
            }
            ViewBag.repairList = repairselect;

            #endregion

            #region 绑定点检员列表

            List<EHECD_Client> clientList = ClientService.Instance.GetClientByUseDeptID(user.iUnitID).ToList();
            List<SelectListItem> clientselect = new List<SelectListItem>();
            clientselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var client in clientList)
            {
                clientselect.Add(new SelectListItem() { Text = client.sName, Value = client.ID.ToString(), Selected = client.ID == entity.iClientID });
            }
            ViewBag.clientList = clientselect;

            #endregion

            return View(entity);
        }
		
		#endregion
		
		#region 后台设备详情视图

        /// <summary>
        /// 后台设备详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult Detail(long id)
		{
            return View(DeviceService.Instance.Get(id));
        }

        #endregion

        #region 后台设备更改责任人视图

        /// <summary>
        /// 后台设备更改责任人视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ChangeClient(string id)
        {
            ViewBag.sIds = id;

            #region 绑定点检员列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_Client> clientList = ClientService.Instance.GetClientByUseDeptID(user.iUnitID).ToList();
            List<SelectListItem> clientselect = new List<SelectListItem>();
            clientselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var client in clientList)
            {
                clientselect.Add(new SelectListItem() { Text = client.sName, Value = client.ID.ToString() });
            }
            ViewBag.clientList = clientselect;

            #endregion

            return View();
        }

        #endregion

        #region 设备更改责任人

        /// <summary>
        /// 设备更改责任人
        /// </summary>
        /// <param name="sIds"></param>
        /// <param name="iClientID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeClient(string sIds, int iClientID)
        {
            return Json(DeviceService.Instance.ChangeClient(sIds, iClientID));
        }

        #endregion

        #region 后台设备更改维护公司视图

        /// <summary>
        /// 后台设备更改维护公司视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult ChangeRepair(string id)
        {
            ViewBag.sIds = id;

            #region 绑定维护公司列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_Unit> repairList = UnitService.Instance.GetRelRepairDeptAllList(user.iUnitID).ToList();
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

        #region 设备更改维护公司

        /// <summary>
        /// 设备更改维护公司
        /// </summary>
        /// <param name="sIds"></param>
        /// <param name="iRepairDeptID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangeRepair(string sIds, int iRepairDeptID)
        {
            return Json(DeviceService.Instance.ChangeRepair(sIds, iRepairDeptID));
        }

        #endregion

        #region 后台打印设备二维码

        /// <summary>
        /// 后台打印设备二维码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PrintQRCode(string id)
        {
            return View(DeviceService.Instance.GetDataByIDs(id));
        }

        #endregion

        #region 后台设备最近一次巡检记录视图

        /// <summary>
        /// 后台设备最近一次巡检记录视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public ActionResult LastRecordDetail(long id)
        {
            return View(InspectionRecordService.Instance.GetLastRecordByDeviceID(id));
        }

        #endregion

        #region 获取设备数据

        /// <summary>
        /// 获取设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(DeviceService.Instance.GetGridData(param));
        }

        #endregion
		
		#region 添加编辑设备

        /// <summary>
        /// 添加编辑设备
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_Device entity)
        {
            if (string.IsNullOrWhiteSpace(entity.sModel))
            {
                entity.sModel = String.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sSpec))
            {
                entity.sSpec = String.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sInstallDate))
            {
                entity.sInstallDate = String.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sManufacturer))
            {
                entity.sManufacturer = String.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sProductionDate))
            {
                entity.sProductionDate = String.Empty;
            }
            if (string.IsNullOrWhiteSpace(entity.sDeviceIDs))
            {
                entity.sDeviceIDs = String.Empty;
            }
            LoginUser user = AuthHelper.GetLogUseUser();
            entity.iUseDeptID = user.iUnitID;
            entity.iCreateUnitID = user.iUnitID;
            return Json(DeviceService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除设备

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(DeviceService.Instance.Delete(sIds));
        }

        #endregion

        #region 获取条件下设备数量

        /// <summary>
        /// 获取条件下设备数量
        /// </summary>
        /// <returns></returns>
        public int GetDeviceCount()
        {
            var sKeyword = Request.Form["sKeyword"];
            var iAbnormalStatus = Request.Form["iAbnormalStatus"];
            var iDeviceTypeID = Request.Form["iDeviceTypeID"];
            var sClientName = Request.Form["sClientName"];
            var dStartTime = Request.Form["dStartTime"];
            var dEndTime = Request.Form["dEndTime"];
            var dExpStartTime = Request.Form["dExpStartTime"];
            var dExpEndTime = Request.Form["dExpEndTime"];
            var dFocStartTime = Request.Form["dFocStartTime"];
            var dFocEndTime = Request.Form["dFocEndTime"];

            LoginUser user = AuthHelper.GetLogUseUser();
            QueryParams param = new QueryParams();
            param.condition.Add("sKeyword", sKeyword);
            param.condition.Add("iAbnormalStatus", iAbnormalStatus);
            param.condition.Add("iDeviceTypeID", iDeviceTypeID);
            param.condition.Add("iUseDeptID", user.iUnitID);
            param.condition.Add("sClientName", sClientName);
            param.condition.Add("dStartTime", dStartTime);
            param.condition.Add("dEndTime", dEndTime);
            param.condition.Add("dExpStartTime", dExpStartTime);
            param.condition.Add("dExpEndTime", dExpEndTime);
            param.condition.Add("dFocStartTime", dFocStartTime);
            param.condition.Add("dFocEndTime", dFocEndTime);

            return DeviceService.Instance.GetDeviceCount(param);
        }

        #endregion

        #region 导出设备数据

        /// <summary>
        /// 导出设备数据
        /// </summary>
        public void GetExportData()
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
                var iAbnormalStatus = Request.QueryString["iAbnormalStatus"];
                var iDeviceTypeID = Request.QueryString["iDeviceTypeID"];
                var sClientName = Request.QueryString["sClientName"];
                var dStartTime = Request.QueryString["dStartTime"];
                var dEndTime = Request.QueryString["dEndTime"];
                var dExpStartTime = Request.QueryString["dExpStartTime"];
                var dExpEndTime = Request.QueryString["dExpEndTime"];
                var dFocStartTime = Request.QueryString["dFocStartTime"];
                var dFocEndTime = Request.QueryString["dFocEndTime"];

                QueryParams param = new QueryParams();
                param.condition.Add("sKeyword", sKeyword);
                param.condition.Add("iAbnormalStatus", iAbnormalStatus);
                param.condition.Add("iDeviceTypeID", iDeviceTypeID);
                param.condition.Add("iUseDeptID", user.iUnitID);
                param.condition.Add("sClientName", sClientName);
                param.condition.Add("dStartTime", dStartTime);
                param.condition.Add("dEndTime", dEndTime);
                param.condition.Add("dExpStartTime", dExpStartTime);
                param.condition.Add("dExpEndTime", dExpEndTime);
                param.condition.Add("dFocStartTime", dFocStartTime);
                param.condition.Add("dFocEndTime", dFocEndTime);

                //接收需要导出的数据
                List<EHECD_Device> list = DeviceService.Instance.GetExportData(param).ToList();

                //命名导出表格的StringBuilder变量
                StringBuilder sHtml = new StringBuilder(string.Empty);

                //打印表头
                sHtml.Append("<table border=\"1\" width=\"100%\">");
                sHtml.Append("<tr height=\"40\"><td colspan=\"15\" align=\"center\"><b style='font-size: 20px'>设备信息</b></td></tr>");
                sHtml.Append("<tr align=\"center\">");
                sHtml.Append("<td style='width: 200px'>设备编号</td>");
                sHtml.Append("<td style='width: 150px'>设备名称</td>");
                sHtml.Append("<td style='width: 150px'>设备类型</td>");
                sHtml.Append("<td style='width: 150px'>设备状态</td>");
                sHtml.Append("<td style='width: 150px'>设备位置</td>");
                sHtml.Append("<td style='width: 150px'>安装日期</td>");
                sHtml.Append("<td style='width: 150px'>生产日期</td>");
                sHtml.Append("<td style='width: 150px'>过期日期</td>");
                sHtml.Append("<td style='width: 150px'>强制报废日期</td>");
                sHtml.Append("<td style='width: 150px'>责任人</td>");
                sHtml.Append("<td style='width: 150px'>所属部门</td>");
                sHtml.Append("<td style='width: 150px'>维护公司</td>");
                sHtml.Append("<td style='width: 150px'>生产厂家</td>");
                sHtml.Append("<td style='width: 150px'>最近维修日期</td>");
                sHtml.Append("<td style='width: 150px'>设备二维码</td>");
                sHtml.Append("</tr>");

                //循环读取List集合 
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    sHtml.Append("<tr height=\"40\" align=\"center\">");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sNumber + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sDeviceTypeName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + (item.iAbnormalStatus ? "异常" : "正常") + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sLocation + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sInstallDate + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + (string.IsNullOrWhiteSpace(item.sProductionDate) ? "-" : item.sProductionDate) + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + (string.IsNullOrWhiteSpace(item.sProductionDate) ? "-" : DateTime.Parse(item.sProductionDate).AddYears(item.iExpiredYears).ToString("yyyy-MM-dd")) + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + (string.IsNullOrWhiteSpace(item.sProductionDate) ? "-" : DateTime.Parse(item.sProductionDate).AddYears(item.iForciblyScrappedYears).ToString("yyyy-MM-dd")) + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sClientName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sOrganName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sRepairDeptName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sManufacturer + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sLastRepairDate + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'><img src='http://" + Request.Url.Host + ":" + Request.Url.Port + item.sQRCode + "' width='40' height='40' /></td>");
                    sHtml.Append("</tr>");
                }
                sHtml.Append("</table>");

                //调用输出Excel表的方法
                ExportToExcel("application/ms-excel", "设备列表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", sHtml.ToString());
                exportSuc = true;
            }
            catch
            {
                exportSuc = false;
            }
            finally
            {
                ActionLogService.Instance.Add(null, user.ID, "导出设备", exportSuc ? "导出设备成功" : "导出设备失败");
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

        #region 导入设备数据

        /// <summary>
        /// 导入设备数据
        /// </summary>
        /// <returns></returns>
        public JsonResult Upload()
        {
            HttpPostedFileBase file = Request.Files[0];
            int _failCount = 0;
            String _log = String.Empty;
            int _deviceCount = 0;
            string _msg = "";
            try
            {
                // 文件存放路径
                string filePath = Request.MapPath("~/SaveFile");
                string fileName = Path.Combine(filePath, Path.GetFileName(file.FileName));
                string fileExtend = Path.GetExtension(fileName);

                if (fileExtend != ".xlsx" && fileExtend != ".xls")
                {
                    return Json(TCommon.setSucc(false, "请选择电子表格格式文件"));
                }

                // 如果文件目录不存在，则创建目录
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                // 保存文件
                file.SaveAs(fileName);

                // 读取Excel文档，并填充DataTable
                DataTable dtData = ExcelHelper.Import(fileName, 1);

                // 设备信息
                var deviceList = DeviceService.Instance.GetAllList();
                _deviceCount = dtData.Rows.Count;
                LoginUser user = AuthHelper.GetLogUseUser();
                List<EHECD_DeviceType> deviceTypeList = DeviceTypeService.Instance.GetAllList().Where(x=>x.iUseDeptID == user.iUnitID || x.iUseDeptID == 0).ToList();
                foreach (DataRow row in dtData.Rows)
                {
                    if (string.IsNullOrEmpty(row[0].ToString()))
                    {
                        break;
                    }

                    try
                    {
                        EHECD_Device entity = new EHECD_Device();

                        entity.sNumber = row[0].ToString();
                        entity.sName = row[1].ToString();
                        entity.sDeviceTypeName = row[2].ToString();
                        entity.iNumber = string.IsNullOrWhiteSpace(row[3].ToString()) ? 0 : Convert.ToInt32(row[3].ToString());
                        entity.sLocation = row[4].ToString();
                        entity.sModel = row[5].ToString();
                        entity.sSpec = row[6].ToString();
                        entity.sInstallDate = row[7].ToString();
                        entity.sManufacturer = row[8].ToString();
                        entity.sProductionDate = row[9].ToString();
                        entity.iExpiredYears = string.IsNullOrWhiteSpace(row[10].ToString()) ? 0 : Convert.ToInt32(row[10].ToString());
                        entity.iForciblyScrappedYears = string.IsNullOrWhiteSpace(row[11].ToString()) ? 0 : Convert.ToInt32(row[11].ToString());
                        EHECD_DeviceType type = deviceTypeList.Find(x => x.sName == entity.sDeviceTypeName);
                        entity.iDeviceTypeID = type == null ? 0: type.ID;
                        entity.iRepairDeptID = 0;
                        entity.iClientID = 0;
                        entity.sQRCode = string.Empty;
                        entity.iUseDeptID = user.iUnitID;
                        entity.iCreateUnitID = user.iUnitID;

                        var member = deviceList.Where(o => o.sNumber == entity.sNumber);
                        if (member.Count() == 0)
                        {
                            DeviceService.Instance.Import(entity);
                        }
                        else if (string.IsNullOrWhiteSpace(entity.sNumber))
                        {
                            continue;
                        }
                        else
                        {
                            _failCount++;
                            _log += "设备编号「" + entity.sNumber + "」已存在，将不再增加\r\n";
                            _msg += "设备编号「" + entity.sNumber + "」已存在，将不再增加;</br>";
                        }
                    }
                    catch (Exception e)
                    {
                        _failCount++;
                        _log += "设备编号[" + row[0].ToString() + "]导入失败\r\n";
                        _msg += "设备编号「" + row[0].ToString() + "」导入失败;</br>";
                    }
                }


                if (_failCount > 0)
                {
                    TTracer.WriteLog("导入完成，失败「" + _failCount + "」条，详细信息：\r\n" + _log);
                    return Json(TCommon.setSucc(true, "导入成功，但有" + _failCount + "条失败</br>" + _msg));
                }
                else
                {
                    return Json(TCommon.setSucc(true, "导入成功"));
                }
            }
            catch
            {
                //导入失败
                TTracer.WriteLog("导入完成，失败「" + _failCount + "」条，详细信息：\r\n" + _log);
                return Json(TCommon.setSucc(false, "导入失败"));
            }
        }

        #endregion

        #region 获取当月已巡检的设备数据

        /// <summary>
        /// 获取当月已巡检的设备数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetInspectedGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            return Content(DeviceService.Instance.GetInspectedGridData(param));
        }

        #endregion

        #region 复制数据

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <returns></returns>
        public string GetDeviceInfo()
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            return Newtonsoft.Json.JsonConvert.SerializeObject(DeviceService.Instance.GetLatelyDeviceByUnitID(user.iUnitID));
        }

        #endregion
    }
}
