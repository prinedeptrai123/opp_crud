using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    public class TableDefinition
    {
        public string name { get; set; }
        public string schema { get; set; }
        public string sequence { get; set; }

        public List<FieldDefinition> fields = new List<FieldDefinition>();

        public TableDefinition(string name)
        {
            this.name = name;
        }

        public TableDefinition Schema(string schema)
        {
            this.schema = schema;
            return this;
        }

        public TableDefinition Sequence(string sequence)
        {
            this.sequence = sequence;
            return this;
        }

        public FieldDefinition Field(string columnName)
        {
            return new FieldDefinition(this, columnName);
        }
    }
}
