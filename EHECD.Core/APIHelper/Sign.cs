using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EHECD.Common;

namespace EHECD.Core.APIHelper
{
    public class Sign
    {
        public static string CreateToken()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
