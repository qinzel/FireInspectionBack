using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHECD.FirePatrolInspection.Entity
{
    public class ClientMessage
    {
        /// <summary>
        /// 数据是否发生修改
        /// </summary>
        public bool IsModified { get; set; }

        /// <summary>
        /// 是否返回全部数据
        /// </summary>
        public bool IsAll { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 同步数据时间
        /// </summary>
        public string LastModifyTime { get; set; }
    }
}
