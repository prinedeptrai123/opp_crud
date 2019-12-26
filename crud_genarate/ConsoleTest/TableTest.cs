using framework_crud.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{

    [Table("TableTest", "dbo")]
    class TableTest
    {
        [Field("name")] public string name;
        [Field("age")] public double? age;
    }
}
