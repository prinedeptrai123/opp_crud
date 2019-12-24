using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public interface IGenerate
    {
        void Generate(TableDefinition table);
    }
}
