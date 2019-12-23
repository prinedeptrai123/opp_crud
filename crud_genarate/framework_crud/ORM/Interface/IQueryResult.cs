using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public delegate void ForEachCallback(IRow row);
    public delegate object ToListCallback(IRow row);
    public delegate T ToListCallback<T>(IRow row);

    interface IQueryResult:IDisposable
    {
        RowSet ToRowSet();
        DataTable ToDataTable();

        void ForEach(ForEachCallback callback);
        IList ToList(ToListCallback callback);
        IList<T> ToList<T>(ToListCallback<T> callback);
    }
}
