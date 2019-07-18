
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

namespace EHECD.FirePatrolInspection.Web.Areas.FireDept.Controllers
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
            #region 绑定使用单位列表

            LoginUser user = AuthHelper.GetLogFireUser();
            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(0).Where(o => o.iParentID == user.iUnitID).ToList();
            List<SelectListItem> unitselect = new List<SelectListItem>();
            unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in unitList)
            {
                unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.unitList = unitselect;

            #endregion

            #region 绑定设备类型列表

            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });

            var query = from x in typeList
                        where (from y in unitList where x.iUseDeptID == y.ID || x.iUseDeptID == 0 select y).Any()
                        select x;

            foreach (var type in query)
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

            #region 绑定使用单位列表

            LoginUser user = AuthHelper.GetLogFireUser();
            List<EHECD_Unit> unitList = UnitService.Instance.GetListByType(0).Where(o => o.iParentID == user.iUnitID).ToList();
            List<SelectListItem> unitselect = new List<SelectListItem>();
            unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in unitList)
            {
                unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.unitList = unitselect;

            #endregion

            #region 绑定设备类型列表
            
            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });

            var query = from x in typeList
                        where (from y in unitList where x.iUseDeptID == y.ID || x.iUseDeptID == 0 select y).Any()
                        select x;

            foreach (var type in query)
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

            #region 绑定使用单位列表
            
            LoginUser user = AuthHelper.GetLogFireUser();
            List<EHECD_Unit> unitList = new List<EHECD_Unit>();

            if (id == 0)
            {
                unitList = UnitService.Instance.GetListByType(0).Where(o => o.iParentID == user.iUnitID).ToList();
                List<SelectListItem> unitselect = new List<SelectListItem>();
                unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
                foreach (var unit in unitList)
                {
                    unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString(), Selected = unit.ID == entity.iUseDeptID });
                }
                ViewBag.unitList = unitselect;
            }
            else
            {
                if (entity.iClientID == 0 && entity.iRepairDeptID == 0)
                {
                    if (entity.iUseDeptID == 0)
                    {
                        unitList = UnitService.Instance.GetListByType(0).Where(o => o.iParentID == user.iUnitID).ToList();
                        List<SelectListItem> unitselect = new List<SelectListItem>();
                        unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
                        foreach (var unit in unitList)
                        {
                            unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString(), Selected = unit.ID == entity.iUseDeptID });
                        }
                        ViewBag.unitList = unitselect;
                    }
                    else
                    {
                        EHECD_Unit unit = UnitService.Instance.Get(entity.iUseDeptID);
                        List<SelectListItem> unitselect = new List<SelectListItem>();
                        unitselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
                        unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString(), Selected = unit.ID == entity.iUseDeptID });
                        ViewBag.unitList = unitselect;
                    }
                }
                else
                {
                    unitList = UnitService.Instance.GetClientRelUnitList(entity.iClientID).ToList();
                    List<SelectListItem> unitselect = new List<SelectListItem>();
                    foreach (var unit in unitList)
                    {
                        unitselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString(), Selected = unit.ID == entity.iUseDeptID });
                    }
                    ViewBag.unitList = unitselect;
                }
            }

            #endregion

            #region 绑定设备类型列表

            List<EHECD_DeviceType> typeList = DeviceTypeService.Instance.GetAllList().ToList();
            List<SelectListItem> typeselect = new List<SelectListItem>();
            typeselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });

            var query = from x in typeList
                        where (from y in unitList where x.iUseDeptID == y.ID || x.iUseDeptID == 0 select y).Any()
                        select x;

            foreach (var type in query)
            {
                typeselect.Add(new SelectListItem() { Text = type.sName, Value = type.ID.ToString(), Selected = type.ID == entity.iDeviceTypeID });
            }
            ViewBag.typeList = typeselect;

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
            LoginUser user = AuthHelper.GetLogFireUser();
            param.condition.Add("iUnitID", user.iUnitID);
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
            LoginUser user = AuthHelper.GetLogFireUser();
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
            var iUseDeptID = Request.Form["iUseDeptID"];
            var sClientName = Request.Form["sClientName"];
            var dStartTime = Request.Form["dStartTime"];
            var dEndTime = Request.Form["dEndTime"];
            var dExpStartTime = Request.Form["dExpStartTime"];
            var dExpEndTime = Request.Form["dExpEndTime"];
            var dFocStartTime = Request.Form["dFocStartTime"];
            var dFocEndTime = Request.Form["dFocEndTime"];

            LoginUser user = AuthHelper.GetLogFireUser();
            QueryParams param = new QueryParams();
            param.condition.Add("sKeyword", sKeyword);
            param.condition.Add("iAbnormalStatus", iAbnormalStatus);
            param.condition.Add("iDeviceTypeID", iDeviceTypeID);
            param.condition.Add("iUseDeptID", iUseDeptID);
            param.condition.Add("iUnitID", user.iUnitID);
            param.condition.Add("sClientName", sClientName);
            param.condition.Add("dStartTime", dStartTime);
            param.condition.Add("dEndTime", dEndTime);
            param.condition.Add("dExpStartTime", dExpStartTime);
            param.condition.Add("dExpEndTime", dExpEndTime);
            param.condition.Add("dFocStartTime", dFocStartTime);
            param.condition.Add("dFocEndTime", dFocEndTime);

            return DeviceService.Instance.GetPartDeviceCount(param);
        }

        #endregion

        #region 导出设备数据

        /// <summary>
        /// 导出设备数据
        /// </summary>
        public void GetExportData()
        {
            LoginUser user = AuthHelper.GetLogFireUser();

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
                var iUseDeptID = Request.QueryString["iUseDeptID"];
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
                param.condition.Add("iUseDeptID", iUseDeptID);
                param.condition.Add("iUnitID", user.iUnitID);
                param.condition.Add("sClientName", sClientName);
                param.condition.Add("dStartTime", dStartTime);
                param.condition.Add("dEndTime", dEndTime);
                param.condition.Add("dExpStartTime", dExpStartTime);
                param.condition.Add("dExpEndTime", dExpEndTime);
                param.condition.Add("dFocStartTime", dFocStartTime);
                param.condition.Add("dFocEndTime", dFocEndTime);

                //接收需要导出的数据
                List<EHECD_Device> list = DeviceService.Instance.GetExportPartData(param).ToList();

                //命名导出表格的StringBuilder变量
                StringBuilder sHtml = new StringBuilder(string.Empty);

                //打印表头
                sHtml.Append("<table border=\"1\" width=\"100%\">");
                sHtml.Append("<tr height=\"40\"><td colspan=\"16\" align=\"center\"><b style='font-size: 20px'>设备信息</b></td></tr>");
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
                sHtml.Append("<td style='width: 150px'>所属使用单位</td>");
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
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sUnitName + "</td>");
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

        #region 获取当前使用单位关联的维护公司

        /// <summary>
        /// 获取当前使用单位关联的维护公司
        /// </summary>
        /// <returns></returns>
        public string GetRepairDeptList(int iUseDeptID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(UnitService.Instance.GetRelRepairDeptAllList(iUseDeptID));
        }

        #endregion

        #region 获取当前使用单位关联的点检员

        /// <summary>
        /// 获取当前使用单位关联的维护公司
        /// </summary>
        /// <returns></returns>
        public string GetClientList(int iUseDeptID)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(ClientService.Instance.GetClientByUseDeptID(iUseDeptID));
        }

        #endregion

        #region 复制数据

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <returns></returns>
        public string GetDeviceInfo()
        {
            LoginUser user = AuthHelper.GetLogFireUser();
            return Newtonsoft.Json.JsonConvert.SerializeObject(DeviceService.Instance.GetLatelyDeviceByUnitID(user.iUnitID));
        }

        #endregion
    }
}
