
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
    /// 值班记录控制器
    /// </summary>
    public class DutyRecordController : Controller
    {
		#region 后台值班记录列表视图
	
        /// <summary>
        /// 后台值班记录列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            #region 绑定值班室列表

            LoginUser user = AuthHelper.GetLogUseUser();
            List<EHECD_DutyRoom> dutyroomList = DutyRoomService.Instance.GetListByUnitID(user.iUnitID).ToList();
            List<SelectListItem> dutyroomselect = new List<SelectListItem>();
            dutyroomselect.Add(new SelectListItem() { Text = "全部", Value = string.Empty });
            foreach (var unit in dutyroomList)
            {
                dutyroomselect.Add(new SelectListItem() { Text = unit.sName, Value = unit.ID.ToString() });
            }
            ViewBag.dutyroomList = dutyroomselect;

            #endregion

            return View();
        }
		
		#endregion
		
		#region 后台值班记录编辑视图

        /// <summary>
        /// 后台值班记录编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Set(long id)
		        
		{
            EHECD_DutyRecord entity = new  EHECD_DutyRecord();
					if (id != 0)
			
						
				entity = DutyRecordService.Instance.Get(id);
					
            return View(entity);
        }
		
		#endregion
		
		#region 后台值班记录详情视图

        /// <summary>
        /// 后台值班记录详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
				
		public ActionResult Detail(long id)
		{
            return View(DutyRecordService.Instance.Get(id));
        }
		       
        #endregion

		#region 获取值班记录数据

        /// <summary>
        /// 获取值班记录数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult GetGridData([ModelBinder(typeof(QueryParamBinder))]QueryParams param)
        {
            LoginUser user = AuthHelper.GetLogUseUser();
            param.condition.Add("iUseDeptID", user.iUnitID);
            return Content(DutyRecordService.Instance.GetGridData(param));
        }
		
		#endregion
		
		#region 添加编辑值班记录

        /// <summary>
        /// 添加编辑值班记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Set(EHECD_DutyRecord entity)
        {
            return Json(DutyRecordService.Instance.Set(entity));
        }

		#endregion
		
		#region 删除值班记录

        /// <summary>
        /// 删除值班记录
        /// </summary>
        /// <param name="sIds"></param>
        /// <returns></returns>
        public JsonResult Delete(string sIds)
        {
            return Json(DutyRecordService.Instance.Delete(sIds));
        }

        #endregion

        #region 导出值班记录

        /// <summary>
        /// 导出值班记录
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
                var dSStartTime = Request.QueryString["dSStartTime"];
                var dSEndTime = Request.QueryString["dSEndTime"];
                var dEStartTime = Request.QueryString["dEStartTime"];
                var dEEndTime = Request.QueryString["dEEndTime"];
                var iDutyRoomID = Request.QueryString["iDutyRoomID"];

                QueryParams param = new QueryParams();
                param.condition.Add("sKeyword", sKeyword);
                param.condition.Add("iUseDeptID", user.iUnitID);
                param.condition.Add("dSStartTime", dSStartTime);
                param.condition.Add("dSEndTime", dSEndTime);
                param.condition.Add("dEStartTime", dEStartTime);
                param.condition.Add("dEEndTime", dEEndTime);
                param.condition.Add("iDutyRoomID", iDutyRoomID);

                //接收需要导出的数据
                List <EHECD_DutyRecord> list = DutyRecordService.Instance.GetExportData(param).ToList();

                //命名导出表格的StringBuilder变量
                StringBuilder sHtml = new StringBuilder(string.Empty);

                //打印表头
                sHtml.Append("<table border=\"1\" width=\"100%\">");
                sHtml.Append("<tr height=\"40\"><td colspan=\"7\" align=\"center\"><b style='font-size: 20px'>值班记录信息</b></td></tr>");
                sHtml.Append("<tr align=\"center\">");
                sHtml.Append("<td style='width: 200px'>账号/电话</td>");
                sHtml.Append("<td style='width: 150px'>姓名</td>");
                sHtml.Append("<td style='width: 150px'>值班室</td>");
                sHtml.Append("<td style='width: 150px'>值班开始时间</td>");
                sHtml.Append("<td style='width: 150px'>值班结束时间</td>");
                sHtml.Append("<td style='width: 150px'>值班时长</td>");
                sHtml.Append("<td style='width: 150px'>值班情况</td>");
                sHtml.Append("</tr>");

                //循环读取List集合 
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    sHtml.Append("<tr height=\"40\" align=\"center\">");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sPhone + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sClientName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sDutyRoomName + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sStartTime + "</td>");
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sEndTime + "</td>");
                    if (!string.IsNullOrWhiteSpace(item.sStartTime) && !string.IsNullOrWhiteSpace(item.sEndTime))
                    {
                        var dtStart = DateTime.Parse(item.sStartTime);
                        var dtEnd = DateTime.Parse(item.sEndTime);
                        TimeSpan tsStart = new TimeSpan(dtStart.Ticks);
                        TimeSpan tsEnd = new TimeSpan(dtEnd.Ticks);
                        var dtSub = tsEnd.Subtract(tsStart);
                        sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + ((dtSub.Days > 0 ? dtSub.Days + "天 " : "") + (dtSub.Hours > 0 ? dtSub.Hours + "小时 " : "") + dtSub.Minutes + "分钟") + "</td>");
                    }
                    else
                    {
                        sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>-</td>");
                    }
                    sHtml.Append("<td style='vnd.ms-excel.numberformat:@'>" + item.sDesc + "</td>");
                    sHtml.Append("</tr>");
                }
                sHtml.Append("</table>");

                //调用输出Excel表的方法
                ExportToExcel("application/ms-excel", "值班记录列表" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", sHtml.ToString());
                exportSuc = true;
            }
            catch
            {
                exportSuc = false;
            }
            finally
            {
                ActionLogService.Instance.Add(null, user.ID, "导出值班记录", exportSuc ? "导出值班记录成功" : "导出值班记录失败");
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
