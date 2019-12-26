using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public interface IQuery
    {
        IQuery Eq(string name, object value);
        IQuery Ne(string name, object value);
        IQuery Gt(string name, object value);
        IQuery Ge(string name, object value);
        IQuery Lt(string name, object value);
        IQuery Le(string name, object value);
        IQuery Like(string name, string value);
        IQuery NotLike(string name, string value);
        IQuery In(string name, ICollection list);
        IQuery NotIn(string name, ICollection list);
        IQuery Between(string name, object min, object max);
        IQuery Sub();
        IQuery EndSub();
        IQuery And();
        IQuery Or();

        IQuery OrderBy(string name, bool ascending);

        IQuery Limit(int limit);
        IQuery Offset(int offset);

        //implement here
        int Count();
        int Delete();
        IList Select();
        IList SelectDistinct();
        object Find();
        //implement here
        IList<T> Select<T>();
        IList<T> SelectDistinct<T>();
        T Find<T>();
    }
}
