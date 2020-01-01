using framework_crud.BASESQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
            //MSSQLTable table = tableCache.Get(type) as MSSQLTable;
            //if (table == null)
            //{
            //    table = new MSSQLTable(this, type);
            //    tableCache.Put(type, table);
            //}
            MSSQLTable table = new MSSQLTable(this, type);
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


        //TODO: move to new class
        public List<TableDefinition> listTable()
        {
            List<TableDefinition> result = new List<TableDefinition>();
            //TODO: get all table infomation
            string query = "SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_TYPE ='BASE TABLE' order by TABLE_NAME";

            DataTable db = GetDataTalbe(query);
            
            for (int i = 0; i < db.Rows.Count; i++)
            {
                string tableName = db.Rows[i][0].ToString();
                Console.WriteLine(tableName+"---------------");
                if (tableName == "sysdiagrams")
                {
                    continue;
                }
                else
                {
                    TableDefinition table = new TableDefinition(tableName).Schema("dbo");

                    DataTable column = getTableColumn(tableName);
                    for (int j = 0; j < column.Rows.Count; j++)
                    {
                        
                        string fieldName = column.Rows[j][1].ToString();
                        string fieldData = column.Rows[j][2].ToString();
                        string CONTRAINT_TYPE = column.Rows[j][3].ToString();
                        string RF_TABLE = column.Rows[j][4].ToString();
                        string RF_COLUMN = column.Rows[j][5].ToString();

                        if (CONTRAINT_TYPE.Contains("PRIMARY KEY"))
                        {
                            int lastIndex = table.fields.FindIndex(x => x.columnName == fieldName);
                            if (lastIndex < 0)
                            {
                                table.Field(fieldName).MapTo(MSSQLDataType.MsqlToCSharp(fieldData)).Key().ReadOnly().Add();
                            }
                            else
                            {
                                table.fields[lastIndex].flags = FieldFlags.Key | FieldFlags.ForeignKey;
                                //table.fields[lastIndex].flags |= FieldFlags.Key;
                            }
                        }
                        else if (CONTRAINT_TYPE.Contains("FOREIGN KEY"))
                        {
                            //check if this is primary key
                            //check if contains key and foreikey
                            //update field
                            int lastIndex = table.fields.FindIndex(x => x.columnName == fieldName);
                            if (lastIndex < 0)
                            {
                                table.Field(fieldName).MapTo(MSSQLDataType.MsqlToCSharp(fieldData))
                                .ForeignKey()
                                .ReferenceTo(RF_TABLE, RF_COLUMN).Add();
                            }
                            else
                            {
                                table.fields[lastIndex].flags = FieldFlags.Key | FieldFlags.ForeignKey;
                                table.fields[lastIndex].fieldReference = new FieldReference
                                {
                                    table = RF_TABLE,
                                    column = RF_COLUMN
                                };
                            }
                        }
                        else
                        {
                            table.Field(fieldName).MapTo(MSSQLDataType.MsqlToCSharp(fieldData)).Add();
                        }
                    }
                    result.Add(table);
                }
            }
            return result;
        }

        //TODO: change this code
        private DataTable getTableColumn(string tableName)
        {
            //string query = $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
            //string query = "SELECT C.Column_name, data_type, is_Nullable, U.CONSTRAINT_NAME" +
            //                "FROM information_Schema.Columns C FULL OUTER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE U ON C.COLUMN_NAME = U.COLUMN_NAME" +
            //                $"WHERE C.TABLE_NAME = '{tableName}'";
            //Console.WriteLine(string.Format(BaseQuery.GET_TABLE_INFOMATION, tableName));
            string query = string.Format(BaseQuery.GET_TABLE_INFOMATION_2, tableName);
            return GetDataTalbe(query);
        }

        //TODO: change this type
        private DataTable GetDataTalbe(string query)
        {
            DataTable db = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataSet ds = new DataSet("Data");
                da.Fill(ds);
                if (ds.Tables.Count == 1)
                {
                    return ds.Tables[0];
                }
                else
                {
                    Debug.WriteLine("Erro here");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return null;
        }


    }
}
