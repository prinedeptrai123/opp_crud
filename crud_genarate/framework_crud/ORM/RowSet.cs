using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    class RowSet: IEnumerable<object[]>
    {
        private List<object[]> data;
        private string[] fields;
        private RowSet next;
        private int current = -1;

        public RowSet(int capacity)
        {
            if (capacity < 0)
                capacity = 0;
            data = new List<object[]>(capacity);
        }

        public RowSet() : this(4)
        { }

        public string[] Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public int Count
        {
            get { return data.Count; }
        }

        public RowSet NextSet
        {
            get { return next; }
            set { next = value; }
        }

        public int IndexOf(string name)
        {
            if (string.IsNullOrEmpty(name) || fields == null)
                return -1;
            return Array.IndexOf(fields, name);
        }

        public void Add(params object[] row)
        {
            data.Add(row);
        }

        public object[] RowAt(int index)
        {
            return data[index];
        }

        public void Clear()
        {
            data.Clear();
            Reset();
        }

        public void Reset()
        {
            current = -1;
        }

        public bool Next()
        {
            current++;
            return (current < data.Count);
        }

        public object Get(int row, int field)
        {
            object[] r = RowAt(row);
            return r[field];
        }

        public object Get(int field)
        {
            return Get(current, field);
        }

        public object Get(int row, string name)
        {
            int field = IndexOf(name);
            if (field > -1)
                return Get(row, field);
            return null;
        }

        public object Get(string name)
        {
            return Get(current, name);
        }

        public object this[int field]
        {
            get { return Get(field); }
        }

        public object this[string name]
        {
            get { return Get(name); }
        }

        IEnumerator<object[]> IEnumerable<object[]>.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }


        public static string[] GetFieldNames(IDataRecord row)
        {
            int count = row.FieldCount;
            string[] names = new string[count];
            for (int i = 0; i < count; ++i)
                names[i] = row.GetName(i);
            return names;
        }

        public static RowSet From(IDataReader reader)
        {
            RowSet root = new RowSet();
            RowSet rs = root;

            while (true)
            {
                rs.Fields = GetFieldNames(reader);
                int count = reader.FieldCount;
                object[] values = null;
                while (reader.Read())
                {
                    values = new object[count];
                    reader.GetValues(values);
                    for (int i = 0; i < count; ++i)
                    {
                        if (Convert.IsDBNull(values[i]))
                            values[i] = null;
                    }
                    rs.Add(values);
                }
                if (reader.NextResult())
                {
                    rs.NextSet = new RowSet();
                    rs = rs.NextSet;
                }
                else
                    break;
            }
            return root;
        }

        
    }
}
