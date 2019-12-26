using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    class StaticMethodTableDefinitionBuilder: ITableDefinitionBuilder
    {
        public static readonly string MethodName = "DefineTable";

        public TableDefinition Build(Type type)
        {
            //TODO:review this
            BindingFlags flags = BindingFlags.Public | BindingFlags.Static;
            MethodInfo method = type.GetMethod(MethodName, flags);
            if (method == null)
                return null;
            if (method.ReturnType != typeof(TableDefinition))
                return null;
            TableDefinition table = (TableDefinition)method.Invoke(null, null);
            return table;
        }
    }
}
