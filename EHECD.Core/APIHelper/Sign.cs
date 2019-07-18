using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHECD.Common;

namespace EHECD.Core.APIHelper
{
    public class Sign
    {
        public static bool Valid(string token, string serviceSign, string time)
        {
            if (string.IsNullOrEmpty(time) || string.IsNullOrEmpty(token))
            {
                return false;
            }
            
            var sign = StringExtensions.MD5(serviceSign);
            return true;
            //return token.Equals(sign, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
