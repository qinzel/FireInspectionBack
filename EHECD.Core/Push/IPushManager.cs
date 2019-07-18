using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHECD.Core.Push
{
    public interface IPushManager
    {
        /// <summary>
        /// 对单个用户推送(透传)
        /// </summary>
        void PushMessageToSingle(PushModel content);

        /// <summary>
        /// 群推送
        /// </summary>
        void PushMessageToApp(PushModel content);

        /// <summary>
        /// 批量单推
        /// </summary>
        void BatchPushMessageToSingle(List<PushModel> list);
    }
}
