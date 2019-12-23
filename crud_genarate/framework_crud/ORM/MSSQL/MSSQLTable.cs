using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    //TODO:review
    class MSSQLTable:ITable
    {
        private MSSQLDatabase database;
        private Type classType;

        private string name;
        private string schema;
        private MSSQLField[] fields;
        private MSSQLTrigger[] triggers;
        private object[] triggerParameters; //don't need new object[] for every trigger call

        public MSSQLTable(MSSQLDatabase database, Type type)
        {
            this.database = database;
            classType = type;
            triggerParameters = new object[] { database, EventArgs.Empty };

            BuildTable();
        }

        public IDatabase Database
        {
            get { return database; }
        }

        public Type ClassType
        {
            get { return classType; }
        }

        public MSSQLField GetField(string name)
        {
            foreach (MSSQLField field in fields)
                if (field.Name.Equals(name))
                    return field;
            return null;
        }

        public MSSQLField[] GetFields(FieldFlags flags)
        {
            return GetFields(delegate (MSSQLField f) {
                return ((f.Flags & flags) == flags);
            });
        }

        public MSSQLField[] GetFields(Predicate<MSSQLField> match)
        {
            return Array.FindAll(fields, match);
        }

        public int Insert(object obj)
        {
            return Insert(new object[] { obj });
        }

        public int Insert(ICollection list)
        {
            MSSQLField[] writeable = GetFields(FieldFlags.Write);
            MSSQLField[] identity = GetFields(FieldFlags.Auto);
            string[] names = new string[writeable.Length];
            string[] places = new string[writeable.Length];
            for (int i = 0; i < writeable.Length; ++i)
            {
                names[i] = database.QuoteName(writeable[i].Name);
                places[i] = "{" + i + "}";
            }
            string fieldstr = string.Join(",", names);
            string valuestr = string.Join(",", places);
            string sql = "INSERT INTO " + QuotedName +
                    "(" + fieldstr + ") VALUES(" + valuestr + ");";

            FireTrigger(MSSQLTrigger.BeforeInsert, list);

            SqlParameter pID = null;
            if (identity.Length > 0)
            {
                sql += " SET @ID=SCOPE_IDENTITY();";
                pID = new SqlParameter();
                pID.ParameterName = "@ID";
                pID.SqlDbType = SqlDbType.BigInt;
                pID.Direction = ParameterDirection.Output;
            }

            int rowcount = 0;
            object[] parameters = new object[writeable.Length];
            using (MSSQLStatement stmt = database.Prepare(sql) as MSSQLStatement)
            {
                stmt.Prepare(writeable.Length);
                if (pID != null)
                    stmt.Command.Parameters.Add(pID);
                foreach (object obj in list)
                {
                    for (int i = 0; i < writeable.Length; ++i)
                    {
                        parameters[i] = writeable[i].GetValue(obj);
                        // HACK: Support setting binary fields to NULL.
                        //       See SqlStatement.SetParamValue().
                        if (parameters[i] == null && writeable[i].DataType == MSSQLStatement.ByteArrayType)
                            parameters[i] = MSSQLStatement.BinaryNull;
                    }
                    rowcount += stmt.ExecNonQuery(parameters);
                    if (pID != null)
                    {
                        object idValue = Convert.IsDBNull(pID.Value) ? null : pID.Value;
                        identity[0].SetValue(obj, idValue);
                    }
                }
            }
            FireTrigger(MSSQLTrigger.AfterInsert, list);
            return rowcount;
        }

        public int Update(object obj)
        {
            return Update(new object[] { obj });
        }

        public int Update(ICollection list)
        {
            MSSQLField[] nonKeys = GetFields(delegate (MSSQLField f) {
                return ((f.Flags & FieldFlags.Write) == FieldFlags.Write &&
                        (f.Flags & FieldFlags.Key) != FieldFlags.Key);
            });
            MSSQLField[] keys = GetFields(FieldFlags.Key);
            int i = 0;
            bool first = true;
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ")
                    .Append(QuotedName)
                    .Append(" SET ");
            foreach (MSSQLField f in nonKeys)
            {
                if (!first)
                    sql.Append(",");
                sql.Append(database.QuoteName(f.Name))
                        .Append("=")
                        .Append("{").Append(i).Append("}");
                first = false;
                i++;
            }
            if (keys.Length > 0)
            {
                first = true;
                sql.Append(" WHERE ");
                foreach (MSSQLField f in keys)
                {
                    if (!first)
                        sql.Append(" AND ");
                    sql.Append(database.QuoteName(f.Name))
                            .Append("=")
                            .Append("{").Append(i).Append("}");
                    first = false;
                    i++;
                }
            }

            FireTrigger(MSSQLTrigger.BeforeUpdate, list);
            int rowcount = 0;
            object[] parameters = new object[nonKeys.Length + keys.Length];
            using (MSSQLStatement stmt = database.Prepare(sql.ToString()) as MSSQLStatement)
            {
                foreach (object obj in list)
                {
                    i = 0;
                    foreach (MSSQLField f in nonKeys)
                        parameters[i++] = f.GetValue(obj);
                    foreach (MSSQLField f in keys)
                        parameters[i++] = f.GetValue(obj);
                    rowcount += stmt.ExecNonQuery(parameters);
                }
            }
            FireTrigger(MSSQLTrigger.AfterUpdate, list);
            return rowcount;
        }

        public int Delete(object obj)
        {
            return Delete(new object[] { obj });
        }

        public int Delete(ICollection list)
        {
            MSSQLField[] keys = GetFields(FieldFlags.Key);
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM ")
                    .Append(QuotedName)
                    .Append(" WHERE ");
            bool first = true;
            for (int i = 0; i < keys.Length; ++i)
            {
                if (!first)
                    sql.Append(" AND ");
                sql.Append(database.QuoteName(keys[i].Name))
                        .Append("=")
                        .Append("{").Append(i).Append("}");
            }

            FireTrigger(MSSQLTrigger.BeforeDelete, list);
            int rowcount = 0;
            object[] parameters = new object[keys.Length];
            using (MSSQLStatement stmt = database.Prepare(sql.ToString()) as MSSQLStatement)
            {
                foreach (object obj in list)
                {
                    for (int i = 0; i < keys.Length; ++i)
                        parameters[i] = keys[i].GetValue(obj);
                    rowcount += stmt.ExecNonQuery(parameters);
                }
            }
            FireTrigger(MSSQLTrigger.AfterDelete, list);
            return rowcount;
        }

        internal void FireTrigger(int which, ICollection list)
        {
            MSSQLTrigger trigger = triggers[which];
            if (trigger != null)
                foreach (object obj in list)
                    trigger.Fire(obj, triggerParameters);
        }

        public string QuotedName
        {
            get
            {
                if (!string.IsNullOrEmpty(schema))
                    return database.QuoteName(schema) + "." + database.QuoteName(name);
                return database.QuoteName(name);
            }
        }

        public IQuery Query()
        {
            return Query(null);
        }

        public IQuery Query(object template)
        {
            MSSQLQuery query = new MSSQLQuery(this);
            if (template != null)
            {
                object value = null;
                bool first = true;
                foreach (MSSQLField f in fields)
                {
                    value = f.GetValue(template);
                    if (value == null)
                        continue;
                    if (!first)
                        query.And();
                    first = false;
                    query.Eq(f.Name, value);
                }
            }
            return query;
        }

        public virtual object NewObject()
        {
            return Activator.CreateInstance(classType);
        }

        protected void BuildTable()
        {
            TableDefinition tableDef = TableDefinitionFactory.Build(classType);
            if (tableDef == null)
                throw new Exception("Could not get table definition for " +
                        classType.FullName);
            ProcessTableDefinition(tableDef);
        }

        protected void ProcessTableDefinition(TableDefinition tableDef)
        {
            this.name = tableDef.name;
            this.schema = tableDef.schema;
            this.fields = new MSSQLField[tableDef.fields.Count];
            this.triggers = new MSSQLTrigger[MSSQLTrigger.Names.Length];

            int i = 0;
            BindingFlags flags = BindingFlags.Instance
                    | BindingFlags.Public
                    | BindingFlags.NonPublic;

            foreach (FieldDefinition fieldDef in tableDef.fields)
            {
                MemberInfo member = classType.GetProperty(fieldDef.memberName, flags);
                if (member == null)
                    member = classType.GetField(fieldDef.memberName, flags);
                if (member == null)
                    throw new Exception(string.Format(
                            "Cannot find {0} in {1} for column name {2}",
                            fieldDef.memberName, classType.FullName, fieldDef.columnName));
                this.fields[i] = new MSSQLField(fieldDef.columnName, member, fieldDef.flags);
                i++;
            }

            for (i = 0; i < MSSQLTrigger.Names.Length; ++i)
            {
                MethodInfo method = classType.GetMethod(MSSQLTrigger.Names[i], flags);
                if (method != null)
                    this.triggers[i] = new MSSQLTrigger(method);
            }
        }
    }
}
