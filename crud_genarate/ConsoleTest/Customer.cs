using framework_crud.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    [Table("Customer", "dbo")]
    public class Customer
    {
        [Field("ID", FieldFlags.Read | FieldFlags.Key | FieldFlags.Auto)]
        public int ID;
        [Field("Name")]
        public string Name;

        //List<History> histories;
    }
}
