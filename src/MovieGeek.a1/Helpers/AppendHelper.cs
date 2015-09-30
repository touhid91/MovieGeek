using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGeek.a1.Helpers
{
    internal class AppendHelper
    {
        internal String Append(IEnumerable<String> values)
        {
            return values.Aggregate((i, j) => i + "," + j);
        }
    }
}
