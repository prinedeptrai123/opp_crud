using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    class AttributesTableDefinitionBuilder: ITableDefinitionBuilder
    {
        public TableDefinition Build(Type type)
        {
            if (!type.IsDefined(typeof(TableAttribute), false))
                return null;

            TableAttribute ta = (TableAttribute)type.GetCustomAttributes(
                    typeof(TableAttribute), false)[0];

            TableDefinition table = new TableDefinition(ta.Name)
                    .Schema(ta.Schema).Sequence(ta.Sequence);

            BindingFlags flags = BindingFlags.Instance
                    | BindingFlags.Public
                    | BindingFlags.NonPublic;

            List<MemberInfo> members = new List<MemberInfo>();
            members.AddRange(type.GetFields(flags));
            members.AddRange(type.GetProperties(flags));

            foreach (MemberInfo member in members)
            {
                if (!member.IsDefined(typeof(FieldAttribute), false))
                    continue;

                FieldAttribute fa = (FieldAttribute)member.GetCustomAttributes(
                        typeof(FieldAttribute), false)[0];

                table.Field(fa.Name).MapTo(member.Name).Flags(fa.Flags).Add();
            }

            return table;
        }
    }
}
