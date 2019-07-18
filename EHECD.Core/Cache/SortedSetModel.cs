using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHECD.Core.Cache
{
    public class SortedSetModel<T>
    {
        public int Score { get; set; }

        public T Entity { get; set; }
    }
}
