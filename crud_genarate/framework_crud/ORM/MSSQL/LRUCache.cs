using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    //TODO:Remove this
    public class LRUCache
    {
        private int capacity;
        private OrderedDictionary data;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            data = new OrderedDictionary(capacity);
        }

        public int Capacity
        {
            get { return capacity; }
        }

        public int Count
        {
            get { return data.Count; }
        }

        public object Get(object key)
        {
            object item = null;
            if (data.Contains(key))
            {
                Touch(key);
                item = data[key];
            }
            return item;
        }

        public object Put(object key, object item)
        {
            object removed = null;
            if (data.Count >= capacity)
            {
                removed = data[data.Count - 1];
                data.RemoveAt(data.Count - 1);
            }
            data.Insert(0, key, item);
            return removed;
        }

        public void Clear()
        {
            // Iterate in reverse order to prevent useless copying.
            for (int i = (data.Count - 1); i > -1; --i)
                data.RemoveAt(i);
        }

        private void Touch(object key)
        {
            object item = data[key];
            data.Remove(key);
            data.Insert(0, key, item);
        }
    }
}
