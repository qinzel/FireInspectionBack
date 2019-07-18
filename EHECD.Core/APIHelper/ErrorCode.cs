using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHECD.Core.APIHelper
{
    public class ErrorCode
    {
        /// <summary>
        /// 缺少方法名
        /// </summary>
        public static int MethodIsNull = 10001;

        /// <summary>
        /// 找不到方法
        /// </summary>
        public static int MethodCannotFound = 10002;

        /// <summary>
        /// 找不到Token
        /// </summary>
        public static int TokenIsNull = 10003;

        /// <summary>
        /// Token错误
        /// </summary>
        public static int TokenError = 10004;
    }
}
