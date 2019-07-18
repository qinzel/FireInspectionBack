using EHECD.Common;
using EHECD.FirePatrolInspection.Service;
using System;
using System.Linq;

namespace EHECD.FirePatrolInspection.WindowsService
{
    public class InspectionMonitor
    {
        /// <summary>
        /// 发送巡检通知
        /// </summary>
        public static void SendInsSiteMsg()
        {
            // 获取运行日期
            var iRunDay = ReadConfig.ReadAppSetting("RUNDAY");

            //TTracer.WriteLog("开始执行任务，当前运行时间为每月第" + iRunDay + "日");

            if (DateTime.Now.Day == (string.IsNullOrWhiteSpace(iRunDay) ? 25 : Convert.ToInt32(iRunDay)) && DateTime.Now.Hour > 8)
            {
                //TTracer.WriteLog("进入程序内部，开始执行未巡检点检员查询");
                // 获取当月未巡检的点检员信息
                var list = ClientService.Instance.GetListWithoutIns().ToList();

                //TTracer.WriteLog("未巡检点检员数量为：" + (list == null ? 0 : list.Count));

                if (list != null && list.Count > 0)
                {
                    // 获取当月未巡检但已经发送了通知的点检员
                    var _cList = SiteMsgService.Instance.GetCurrentMonthNotices();

                    //TTracer.WriteLog("未巡检已发送通知的点检员数量为：" + (_cList == null ? 0 : _cList.Count()));

                    foreach (var client in list.ToArray())
                    {
                        if (_cList.Where(o => o.sReceiveClient == client.sPhone).FirstOrDefault() != null)
                        {
                            // 排除已经发送过通知的点检员
                            list.Remove(client);
                        }
                    }

                    //TTracer.WriteLog("准备开始发送通知，点检员数量为：" + list.Count);

                    SiteMsgService.Instance.SendNotice(list);
                }
            }
        }
    }
}