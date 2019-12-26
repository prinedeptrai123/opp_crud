using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    interface ITableDefinitionBuilder
    {
        TableDefinition Build(Type type);
    }
}
