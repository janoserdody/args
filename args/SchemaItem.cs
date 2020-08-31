using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace args
{
    public class SchemaItem
    {
        public string Name
        {
            get; set;
        }

        public Type FlagType
        {
            get; set;
        }

        /// <summary>
        /// 0 = not ordered
        /// 1 = first
        /// 2 = second
        /// </summary>
        public int OrdinalNumber
        {
            get; set;
        }
    }
}
