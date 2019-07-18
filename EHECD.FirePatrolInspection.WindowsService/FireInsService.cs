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

            Registry reg = new Registry();
            reg.Schedule(() =>
                InspectionMonitor.SendInsSiteMsg()
                    ).WithName("SendInsSiteMsg").ToRunNow().AndEvery(10).Minutes();
            JobManager.Initialize(reg);
        }

        protected override void OnStop()
        {
            TTracer.WriteLog("乐消APP监听服务关闭 =========> " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
