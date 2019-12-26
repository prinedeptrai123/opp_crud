using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public interface ITable
    {
        IDatabase Database { get; }
        Type ClassType { get; }

        int Insert(object obj);
        int Insert(ICollection list);
        int Update(object obj);
        int Update(ICollection list);
        int Delete(object obj);
        int Delete(ICollection list);

        IQuery Query();
        IQuery Query(object template);

        object NewObject();
    }
}
