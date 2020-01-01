using System;
using System.Collections.Generic;
using System.Linq;
using framework_crud.ORM;

namespace ConsoleTest
{
    [Table("Info", "dbo")]
    public class Info
    {
        [Field("ID", FieldFlags.Read | FieldFlags.Key)]
        public Int32 ID { get; set; }
        [Field("Name")]
        public string Name { get; set; }
        [Field("Address")]
        public string Address { get; set; }
        [Field("Age")]
        public Int32 Age { get; set; }
        [Field("Phone")]
        public string Phone { get; set; }
        [Field("Class")]
        public string Class { get; set; }
    }
}

