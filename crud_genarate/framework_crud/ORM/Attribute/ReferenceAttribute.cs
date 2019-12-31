using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
           Inherited = true, AllowMultiple = false)]
    public class ReferenceAttribute:Attribute
    {
        public string table { get; set; }
        public string column { get; set; }

        public ReferenceAttribute() : this(string.Empty)
        { }

        public ReferenceAttribute(string table) : this(table,string.Empty)
        { }

        public ReferenceAttribute(string table,string column)
        {
            this.table = table;
            this.column = column;
        }
    }
}
