using EHECD.Common;
using FluentScheduler;
using System;
using System.ServiceProcess;

namespace EHECD.FirePatrolInspection.WindowsService
{
    public partial class FireInsService : ServiceBase
    {
        public FireInsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TTracer.WriteLog("乐消APP监听服务启动 =========> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //巡检提醒服务
            Registry reg = new Registry();
            reg.Schedule(() =>
                InspectionMonitor.SendInsSiteMsg()
                    ).WithName("SendInsSiteMsg").ToRunNow().AndEvery(10).Minutes();
            JobManager.Initialize(reg);

            //设备过期服务
            Registry deviceReg = new Registry();
            deviceReg.Schedule(() =>
            {

            }).WithName("ScheduleDeviceOutOfDate").ToRunNow().AndEvery(1).Hours();
            JobManager.Initialize(deviceReg);
        }

        protected override void OnStop()
        {
            TTracer.WriteLog("乐消APP监听服务关闭 =========> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
