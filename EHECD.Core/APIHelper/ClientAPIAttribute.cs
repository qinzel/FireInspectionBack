using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHECD.Core.APIHelper
{
    public class ClientAPIAttribute: Attribute
    {
        /// <summary>
        /// 是否需要登录验证
        /// </summary>
        public bool LoginCheck = false;
    }
}
