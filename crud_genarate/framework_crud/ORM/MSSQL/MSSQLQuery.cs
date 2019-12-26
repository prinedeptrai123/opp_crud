using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public class MSSQLQuery:IQuery
    {
        private MSSQLDatabase database;
        private MSSQLTable table;

        private StringBuilder where = new StringBuilder();
        private StringBuilder order = new StringBuilder();
        private ArrayList values = new ArrayList();
        private int selectTop = 0;
        private bool distinct = false;

        private int N = 0;

        public MSSQLQuery(MSSQLTable table)
        {
            this.table = table;
            database = table.Database as MSSQLDatabase;
        }

        public ITable Table
        {
            get { return table; }
        }

        public IQuery Eq(string name, object value)
        {
            if (value == null)
                where.Append(database.QuoteName(name)).Append(" IS NULL");
            else
            {
                where.Append(database.QuoteName(name))
                    .Append(@"={").Append(N++).Append("}");
                values.Add(value);
            }
            return this;
        }

        public IQuery Ne(string name, object value)
        {
            if (value == null)
                where.Append(database.QuoteName(name)).Append(" IS NOT NULL");
            else
            {
                where.Append(database.QuoteName(name))
                    .Append(@"<>{").Append(N++).Append("}");
                values.Add(value);
            }
            return this;
        }

        public IQuery Gt(string name, object value)
        {
            where.Append(database.QuoteName(name))
                .Append(@">{").Append(N++).Append("}");
            values.Add(value);
            return this;
        }

        public IQuery Ge(string name, object value)
        {
            where.Append(database.QuoteName(name))
                .Append(@">={").Append(N++).Append("}");
            values.Add(value);
            return this;
        }

        public IQuery Lt(string name, object value)
        {
            where.Append(database.QuoteName(name))
                .Append(@"<{").Append(N++).Append("}");
            values.Add(value);
            return this;
        }

        public IQuery Le(string name, object value)
        {
            where.Append(database.QuoteName(name))
                .Append(@"<={").Append(N++).Append("}");
            values.Add(value);
            return this;
        }

        public IQuery Like(string name, string value)
        {
            where.Append(database.QuoteName(name))
                .Append(@" LIKE {").Append(N++).Append("}");
            values.Add(value);
            return this;
        }

        public IQuery NotLike(string name, string value)
        {
            where.Append(database.QuoteName(name))
                .Append(@" NOT LIKE {").Append(N++).Append("}");
            values.Add(value);
            return this;
        }

        public IQuery In(string name, ICollection list)
        {
            where.Append(database.QuoteName(name)).Append(" IN (");
            string[] places = new string[list.Count];
            for (int i = 0; i < places.Length; ++i)
                places[i] = "{" + (N++) + "}";
            where.Append(string.Join(",", places)).Append(")");
            values.AddRange(list);
            return this;
        }

        public IQuery NotIn(string name, ICollection list)
        {
            where.Append(database.QuoteName(name)).Append(" NOT IN (");
            string[] places = new string[list.Count];
            for (int i = 0; i < places.Length; ++i)
                places[i] = "{" + (N++) + "}";
            where.Append(string.Join(",", places)).Append(")");
            values.AddRange(list);
            return this;
        }

        public IQuery Sub()
        {
            where.Append("(");
            return this;
        }

        public IQuery EndSub()
        {
            where.Append(")");
            return this;
        }

        public IQuery And()
        {
            where.Append(" AND ");
            return this;
        }

        public IQuery Or()
        {
            where.Append(" OR ");
            return this;
        }

        public IQuery Between(string name, object min, object max)
        {
            where.Append(database.QuoteName(name))
                .Append(@" BETWEEN {")
                .Append(N++)
                .Append(@"} AND {")
                .Append(N++)
                .Append("}");
            values.Add(min);
            values.Add(max);
            return this;
        }

        public IQuery OrderBy(string name, bool ascending)
        {
            if (order.Length > 0)
                order.Append(",");
            order.Append(database.QuoteName(name))
                .Append(ascending ? " ASC" : " DESC");
            return this;
        }

        public IQuery Limit(int limit)
        {
            if (limit > 0)
                selectTop = limit;
            return this;
        }

        public IQuery Offset(int offset)
        {
            // not supported
            return this;
        }

        public int Delete()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM ").Append(table.QuotedName);
            if (where.Length > 0)
                sql.Append(" WHERE ").Append(where.ToString());
            int rowcount = 0;
            using (MSSQLStatement stmt = database.Prepare(sql.ToString()) as MSSQLStatement)
                rowcount = stmt.ExecNonQuery(values.ToArray());
            return rowcount;
        }

        protected void Select(IList list)
        {
            MSSQLField[] readables = table.GetFields(FieldFlags.Read);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            if (distinct)
                sql.Append("DISTINCT ");
            if (selectTop > 0)
                sql.Append("TOP ").Append(selectTop).Append(" ");
            bool first = true;
            foreach (MSSQLField f in readables)
            {
                if (!first)
                    sql.Append(",");
                first = false;
                sql.Append(database.QuoteName(f.Name));
            }
            sql.Append(" FROM ").Append(table.QuotedName);
            if (where.Length > 0)
                sql.Append(" WHERE ").Append(where.ToString());

            ForEachCallback callback = new ForEachCallback(delegate (IRow row)
            {
                object obj = table.NewObject();
                object value = null;
                int i = 0;
                foreach (MSSQLField f in readables)
                {
                    value = row[i++];
                    if (Convert.IsDBNull(value))
                        value = null;
                    f.SetValue(obj, value);
                }
                list.Add(obj);
            });

            using (MSSQLStatement stmt = database.Prepare(sql.ToString()) as MSSQLStatement)
            using (IQueryResult result = stmt.ExecQuery(values.ToArray()))
            {
                result.ForEach(callback);
            }

            table.FireTrigger(MSSQLTrigger.AfterSelect, list);
        }

        public IList Select()
        {
            IList list = new ArrayList();
            Select(list);
            return list;
        }

        public IList<T> Select<T>()
        {
            IList<T> list = new List<T>();
            Select(list as IList);
            return list;
        }

        public IList SelectDistinct()
        {
            IList list = null;
            distinct = true;
            try
            {
                list = Select();
            }
            finally
            {
                distinct = false;
            }
            return list;
        }

        public IList<T> SelectDistinct<T>()
        {
            IList<T> list = null;
            distinct = true;
            try
            {
                list = Select<T>();
            }
            finally
            {
                distinct = false;
            }
            return list;
        }

        public object Find()
        {
            IList list = null;
            int oldSelectTop = selectTop;
            selectTop = 1;
            try
            {
                list = Select();
            }
            finally
            {
                selectTop = oldSelectTop;
            }
            if (list.Count > 0)
                return list[0];
            return null;
        }

        public T Find<T>()
        {
            object obj = Find();
            if (obj != null)
                return (T)obj;
            return default(T);
        }

        public int Count()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(*) FROM ").Append(table.QuotedName);
            if (where.Length > 0)
                sql.Append(" WHERE ").Append(where.ToString());

            int count = 0;
            using (MSSQLStatement stmt = database.Prepare(sql.ToString()) as MSSQLStatement)
                count = (int)stmt.ExecScalar();
            return count;
        }
    }
}
