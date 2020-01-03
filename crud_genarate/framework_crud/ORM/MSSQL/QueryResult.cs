using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public class QueryResult:IQueryResult, IRow
    {
        private IDataReader reader;
        private string[] fieldNames;

        public QueryResult(IDataReader reader)
        {
            this.reader = reader;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if (reader != null)
            {
                if (!reader.IsClosed)
                    reader.Close();
                reader.Dispose();
                reader = null;
            }
        }

        public DataTable ToDataTable()
        {
            DataTable table = new DataTable();
            using (this)
                table.Load(reader);
            return table;
        }

        public RowSet ToRowSet()
        {
            using (this)
                return RowSet.From(reader);
        }

        public void ForEach(ForEachCallback callback)
        {
            using (this)
                while (reader.Read())
                    callback(this);
        }

        public IList ToList(ToListCallback callback)
        {
            ArrayList list = new ArrayList();
            using (this)
                while (reader.Read())
                    list.Add(callback(this));
            return list;
        }

        public IList<T> ToList<T>(ToListCallback<T> callback)
        {
            List<T> list = new List<T>();
            using (this)
                while (reader.Read())
                    list.Add(callback(this));
            return list;
        }

        public object this[string name]
        {
            get
            {
                object value = reader[name];
                if (Convert.IsDBNull(value))
                    value = null;
                return value;
            }
        }

        public object this[int index]
        {
            get
            {
                object value = reader[index];
                if (Convert.IsDBNull(value))
                    value = null;
                return value;
            }
        }

        public int FieldCount
        {
            get { return reader.FieldCount; }
        }

        public string[] FieldNames
        {
            get
            {
                if (fieldNames == null)
                    fieldNames = RowSet.GetFieldNames(reader);
                return fieldNames;
            }
        }

        public int GetValues(object[] values)
        {
            int ret = reader.GetValues(values);
            for (int i = 0; i < values.Length; ++i)
                if (Convert.IsDBNull(values[i]))
                    values[i] = null;
            return ret;
        }
    }
}
