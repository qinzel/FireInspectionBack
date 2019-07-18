using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EHECD.Core.Push
{
    public class PushFactory
    {
        private PushFactory()
        {

        }

        public static IPushManager GetPushManager(string type)
        {
            switch (type)
            {
                case "getui":
                    {
                        string host = ConfigurationManager.AppSettings["getui.host"];
                        string appid = ConfigurationManager.AppSettings["getui.appid"];
                        string appkey = ConfigurationManager.AppSettings["getui.appkey"];
                        string masterSecret = ConfigurationManager.AppSettings["getui.masterSecret"];
                        GeTuiConfig config = new GeTuiConfig
                        {
                            APPId = appid,
                            APPKey = appkey,
                            MasterSecret = masterSecret,
                            Host = host
                        };
                        return new GeTuiPushManager(config);
                    }
                default:
                    return null;
            }
        }
    }
}
