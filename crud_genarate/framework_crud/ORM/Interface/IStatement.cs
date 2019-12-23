using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    interface IStatement: IDisposable
    {
        IDatabase Database { get; }

        int ExecNonQuery(params object[] parameters);
        object ExecScalar(params object[] parameters);
        IQueryResult ExecQuery(params object[] parameters);
    }
}
