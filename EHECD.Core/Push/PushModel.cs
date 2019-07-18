using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHECD.Core.Push
{
    /// <summary>
    /// 消息内容格式
    /// </summary>
    public  class PushModel
    {
        /// <summary>
        /// 消息类型(1站内信,2巡检通知,3异常通知)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// 用户ID,群发时不需要
        /// </summary>
        public string CID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string iClientID { get; set; }
    }
}
