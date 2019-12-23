using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
            Inherited = true, AllowMultiple = false)]
    public class FieldAttribute: Attribute
    {
        public string Name = string.Empty;
        public FieldFlags Flags = FieldFlags.ReadWrite;

        public FieldAttribute() : this(string.Empty)
        { }

        public FieldAttribute(string name) : this(name, FieldFlags.ReadWrite)
        { }

        public FieldAttribute(string name, FieldFlags flags)
        {
            this.Name = name;
            this.Flags = flags;
        }
    }
}
