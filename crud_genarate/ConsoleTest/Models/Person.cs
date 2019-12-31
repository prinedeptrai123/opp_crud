using framework_crud.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Models
{
    [Table("Person", "dbo")]
    class Person
    {
        [Field("PersonID", FieldFlags.Read | FieldFlags.Key | FieldFlags.Auto)]
        public Int32 PersonID { get; set; }
        [Field("LastName")]
        public string LastName { get; set; }
        [Field("FirstName")]
        public string FirstName { get; set; }
        [Field("HireDate")]
        public DateTime HireDate { get; set; }
        [Field("EnrollmentDate")]
        public DateTime EnrollmentDate { get; set; }
    }
}
