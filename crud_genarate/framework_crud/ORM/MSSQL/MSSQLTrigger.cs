using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public class MSSQLTrigger
    {
        internal const int BeforeInsert = 0;
        internal const int AfterInsert = 1;
        internal const int BeforeUpdate = 2;
        internal const int AfterUpdate = 3;
        internal const int BeforeDelete = 4;
        internal const int AfterDelete = 5;
        internal const int AfterSelect = 6;

        internal static readonly string[] Names = new string[] {
            "BeforeInsert", "AfterInsert",
            "BeforeUpdate", "AfterUpdate",
            "BeforeDelete", "AfterDelete",
            "AfterSelect"
        };

        //TODO: research this
        private MethodInfo method;

        public MSSQLTrigger(MethodInfo method)
        {
            this.method = method;
        }

        public MethodInfo Method
        {
            get { return method; }
        }

        public void Fire(object target, object[] parameters)
        {
            try
            {
                method.Invoke(target, parameters);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                throw e;
            }
        }
    }
}
