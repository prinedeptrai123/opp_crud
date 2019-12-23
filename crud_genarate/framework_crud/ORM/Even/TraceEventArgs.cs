using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    class TraceEventArgs: EventArgs
    {
        private IDbCommand command;

        public TraceEventArgs(IDbCommand command) : base()
        {
            this.command = command;
        }

        public IDbCommand Command
        {
            get { return command; }
        }
    }
}
