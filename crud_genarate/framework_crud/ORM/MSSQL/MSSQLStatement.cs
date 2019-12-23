using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    class MSSQLStatement:IStatement
    {
        public event EventHandler PreExec;
        public event EventHandler PostExec;

        private MSSQLDatabase database;
        private SqlCommand command;
        private Exception lastError;
        private string originalSQL;
        private int expectedParamCount = -1;

        public MSSQLStatement(MSSQLDatabase database, string sql)
        {
            this.database = database;
            originalSQL = sql;
            command = new SqlCommand();
            command.Connection = database.Connection;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            if (command != null)
            {
                command.Dispose();
                command = null;
            }
        }

        public IDatabase Database
        {
            get { return database; }
        }

        public SqlCommand Command
        {
            get { return command; }
        }

        public Exception LastError
        {
            get { return lastError; }
        }

        protected virtual void OnPreExec()
        {
            lastError = null;
            command.Transaction = database.Transaction;
            if (PreExec != null)
                PreExec(this, EventArgs.Empty);
        }

        protected virtual void OnPostExec()
        {
            if (PostExec != null)
                PostExec(this, EventArgs.Empty);
        }

        public int ExecNonQuery(params object[] parameters)
        {
            int result = 0;
            try
            {
                Assign(parameters);
                OnPreExec();
                result = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                lastError = e;
                throw e;
            }
            finally
            {
                OnPostExec();
            }
            return result;
        }

        public object ExecScalar(params object[] parameters)
        {
            object result = null;
            try
            {
                Assign(parameters);
                OnPreExec();
                result = command.ExecuteScalar();
                if (Convert.IsDBNull(result))
                    result = null;
            }
            catch (Exception e)
            {
                lastError = e;
                throw e;
            }
            finally
            {
                OnPostExec();
            }
            return result;
        }

        public IQueryResult ExecQuery(params object[] parameters)
        {
            QueryResult result = null;
            try
            {
                Assign(parameters);
                OnPreExec();
                SqlDataReader reader = command.ExecuteReader();
                result = new QueryResult(reader);
            }
            catch (Exception e)
            {
                lastError = e;
                throw e;
            }
            finally
            {
                OnPostExec();
            }
            return result;
        }

        protected void Assign(object[] parameters)
        {
            int paramCount = (parameters != null) ? parameters.Length : 0;
            if (expectedParamCount < 0)
                Prepare(paramCount);
            if (expectedParamCount != paramCount)
                throw new ArgumentException(string.Format(
                        "Expecting {0} parameters, {1} given.",
                        expectedParamCount, paramCount), "parameters");

            for (int i = 0; i < paramCount; ++i)
                SetParamValue(command.Parameters[i], parameters[i]);
        }

        public void Prepare(int paramCount)
        {
            if (paramCount < 0)
                throw new ArgumentOutOfRangeException(
                        "parameter count must be positive or zero", "paramCount");
            if (expectedParamCount > -1)
                throw new InvalidOperationException("statement already prepared");

            expectedParamCount = paramCount;
            string[] names = new string[paramCount];
            for (int i = 0; i < paramCount; ++i)
            {
                SqlParameter p = new SqlParameter();
                names[i] = p.ParameterName = "@p" + i.ToString();
                command.Parameters.Add(p);
            }
            command.CommandText = string.Format(originalSQL, names);
        }

        protected virtual void SetParamValue(SqlParameter p, object value)
        {
            if (value == null)
            {
                p.Value = DBNull.Value;
                p.SqlDbType = SqlDbType.NVarChar;
                p.Size = 0;
                return;
            }
            // HACK: Support setting binary fields to NULL.
            // Without this, Sql Server cannot implicitly convert nvarchar to varbinary.
            if (value == BinaryNull)
            {
                p.Value = DBNull.Value;
                p.SqlDbType = SqlDbType.VarBinary;
                p.Size = 0;
                return;
            }

            p.Value = value;
            Type type = value.GetType();
            if (type == StringType)
            {
                p.SqlDbType = SqlDbType.NVarChar;
                p.Size = ((string)value).Length;
            }
            else if (type == IntType || type == UIntType)
            {
                p.SqlDbType = SqlDbType.Int;
            }
            else if (type == BoolType)
            {
                p.SqlDbType = SqlDbType.Bit;
            }
            else if (type == DateTimeType)
            {
                p.SqlDbType = SqlDbType.DateTime;
            }
            else if (type == DecimalType)
            {
                p.SqlDbType = SqlDbType.Decimal;
                p.Precision = MSSQLDatabase.DecimalPrecision;
                p.Scale = MSSQLDatabase.DecimalScale;
            }
            else if (type == LongType || type == ULongType)
            {
                p.SqlDbType = SqlDbType.BigInt;
            }
            else if (type == GuidType)
            {
                p.SqlDbType = SqlDbType.UniqueIdentifier;
            }
            else if (type == ByteType || type == SByteType)
            {
                p.SqlDbType = SqlDbType.TinyInt;
            }
            else if (type == CharType)
            {
                p.SqlDbType = SqlDbType.Char;
                p.Size = 1;
            }
            else if (type == ShortType || type == UShortType)
            {
                p.SqlDbType = SqlDbType.SmallInt;
            }
            else if (type == FloatType)
            {
                p.SqlDbType = SqlDbType.Real;
            }
            else if (type == DoubleType)
            {
                p.SqlDbType = SqlDbType.Float;
            }
            else if (type == ByteArrayType)
            {
                p.SqlDbType = SqlDbType.VarBinary;
                p.Size = ((byte[])value).Length;
            }
        }

        //TODO: move to new class

        public static readonly Type IntType = typeof(int);
        public static readonly Type UIntType = typeof(uint);
        public static readonly Type LongType = typeof(long);
        public static readonly Type ULongType = typeof(ulong);
        public static readonly Type ByteType = typeof(byte);
        public static readonly Type SByteType = typeof(sbyte);
        public static readonly Type ShortType = typeof(short);
        public static readonly Type UShortType = typeof(ushort);
        public static readonly Type DecimalType = typeof(decimal);
        public static readonly Type FloatType = typeof(float);
        public static readonly Type DoubleType = typeof(double);
        public static readonly Type BoolType = typeof(bool);
        public static readonly Type StringType = typeof(string);
        public static readonly Type CharType = typeof(char);
        public static readonly Type DateTimeType = typeof(DateTime);
        public static readonly Type GuidType = typeof(Guid);
        public static readonly Type ByteArrayType = typeof(byte[]);

        public static readonly object BinaryNull = new object();
    }
}

