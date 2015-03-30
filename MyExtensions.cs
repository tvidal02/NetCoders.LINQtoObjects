using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoders.LINQ.Aquecimento
{
    public static class MyExtensions
    {
        public static string ToURL(this String str) 
        {
            return str.Insert(0, "http://www.") + ".com";
        }
    }
}
