using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public class FieldDefinition
    {
        private TableDefinition table;

        public string memberName;
        public string columnName;
        public FieldFlags flags = FieldFlags.ReadWrite;

        public FieldReference fieldReference { get; set; }

        public FieldDefinition(TableDefinition table, string name)
        {
            this.table = table;
            columnName = name;
        }

        //TODO: reference to class
        public FieldDefinition ReferenceTo(string tableName,string columnName)
        {
            fieldReference = new FieldReference
            {
                table = tableName,
                column = columnName
            };
            return this;
        }

        public FieldDefinition MapTo(string name)
        {
            memberName = name;
            return this;
        }

        public FieldDefinition Flags(FieldFlags flags)
        {
            this.flags = flags;
            return this;
        }

        public FieldDefinition Key()
        {
            this.flags |= FieldFlags.Key;
            return this;
        }

        public FieldDefinition ForeignKey()
        {
            this.flags |= FieldFlags.ForeignKey;
            return this;
        }

        public FieldDefinition Auto()
        {
            this.flags |= FieldFlags.Auto;
            return this;
        }

        public FieldDefinition ReadOnly()
        {
            this.flags &= ~(FieldFlags.Write);
            this.flags |= FieldFlags.Read;
            return this;
        }

        public FieldDefinition WriteOnly()
        {
            this.flags &= ~(FieldFlags.Read);
            this.flags |= FieldFlags.Write;
            return this;
        }

        public FieldDefinition ReadWrite()
        {
            this.flags |= FieldFlags.ReadWrite;
            return this;
        }

        public TableDefinition Add()
        {
            table.fields.Add(this);
            return table;
        }
    }
}
