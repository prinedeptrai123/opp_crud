using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct,
            Inherited = false, AllowMultiple = false)]
    public class TableAttribute: Attribute
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public string Sequence { get; set; }

        public TableAttribute() : this(string.Empty)
        { }

        public TableAttribute(string name) :
            this(name, string.Empty)
        { }

        public TableAttribute(string name, string schema) :
            this(name, schema, string.Empty)
        { }

        public TableAttribute(string name, string schema, string sequence) : base()
        {
            this.Name = name;
            this.Schema = schema;
            this.Sequence = sequence;
        }
    }
}
