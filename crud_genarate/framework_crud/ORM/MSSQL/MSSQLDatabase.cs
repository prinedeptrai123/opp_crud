using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public class MSSQLDatabase: IDatabase
    {
        // Defaults based on MSDN (?)
        public static byte DecimalPrecision = 18;
        public static byte DecimalScale = 0;

        public event EventHandler<TraceEventArgs> Trace;

        private SqlConnection connection;
        private SqlTransaction transaction;
        private bool closeConnection = false;
        private LRUCache tableCache = new LRUCache(10); /*TODO: hardcoded size*/

        public MSSQLDatabase(SqlConnection connection)
        {
            this.connection = connection;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                closeConnection = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
                Close();
        }

        public void Close()
        {
            if (connection == null)
                return;
            Rollback();
            if (closeConnection && connection.State != ConnectionState.Closed)
                connection.Close();
            connection = null;
        }

        public bool IsClosed
        {
            get { return (connection == null); }
        }

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public SqlTransaction Transaction
        {
            get { return transaction; }
        }

        public bool InTransaction
        {
            get { return (transaction != null); }
        }

        public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public void BeginTransaction(IsolationLevel level)
        {
            transaction = connection.BeginTransaction(level);
        }

        public void Commit()
        {
            if (transaction != null)
            {
                transaction.Commit();
                transaction = null;
            }
        }

        public void Rollback()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
        }

        public IStatement Prepare(string sql)
        {
            MSSQLStatement stmt = new MSSQLStatement(this, sql);
            stmt.PreExec += OnStmtPreExec;
            return stmt;
        }

        private void OnStmtPreExec(object sender, EventArgs args)
        {
            if (Trace != null)
                Trace(this, new TraceEventArgs((sender as MSSQLStatement).Command));
        }

        public ITable Table(Type type)
        {
            MSSQLTable table = tableCache.Get(type) as MSSQLTable;
            if (table == null)
            {
                table = new MSSQLTable(this, type);
                tableCache.Put(type, table);
            }
            return table;
        }

        public ITable Table<T>()
        {
            return Table(typeof(T));
        }

        public string QuoteName(string name)
        {
            return "[" + name + "]";
        }
    }
}
