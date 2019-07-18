using System.ServiceProcess;

namespace EHECD.FirePatrolInspection.WindowsService
{
    static class Program
    {
        #region 应用程序的主入口点

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FireInsService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        #endregion
    }
}