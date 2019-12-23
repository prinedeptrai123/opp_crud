using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    class MSSQLField
    {
        private string name;
        private FieldFlags flags = FieldFlags.None;
        private PropertyInfo property;
        private FieldInfo field;

        public MSSQLField(string name, MemberInfo member, FieldFlags flags)
        {
            this.name = name;
            this.flags = flags;

            if (member.MemberType == MemberTypes.Property)
                property = member as PropertyInfo;
            else
                field = member as FieldInfo;
        }

        public string Name
        {
            get { return name; }
        }

        public MemberInfo Member
        {
            get
            {
                if (property != null)
                    return property;
                return field;
            }
        }

        public FieldFlags Flags
        {
            get { return flags; }
        }

        public Type DataType
        {
            get
            {
                if (property != null)
                    return property.PropertyType;
                return field.FieldType;
            }
        }

        public bool CanRead
        {
            get
            {
                if (property != null)
                    return property.CanRead;
                return true;
            }
        }

        public bool CanWrite
        {
            get
            {
                if (property != null)
                    return property.CanWrite;
                return true;
            }
        }

        public void SetValue(object target, object value)
        {
            Type dataType = DataType;
            // Handle Nullable types.
            // TODO: Maybe better checking to ensure that it's really Nullable.
            if (dataType.IsGenericType)
                dataType = dataType.GetGenericArguments()[0];
            if (value != null && !dataType.IsAssignableFrom(value.GetType()))
                value = Convert.ChangeType(value, dataType);

            try
            {
                if (property != null)
                    property.SetValue(target, value, null);
                else
                    field.SetValue(target, value);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                throw e;
            }
        }

        public object GetValue(object source)
        {
            object value = null;
            try
            {
                if (property != null)
                    value = property.GetValue(source, null);
                else
                    value = field.GetValue(source);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                throw e;
            }
            return value;
        }
    }
}
