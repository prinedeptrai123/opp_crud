using framework_crud.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    [Table("CourseInstructor", "dbo")]
    public class CourseInstructor
    {
        [Field("CourseID", FieldFlags.Key | FieldFlags.ForeignKey, "Course", "CourseID")]
        public Int32 CourseID { get; set; }
        [Field("PersonID", FieldFlags.Key| FieldFlags.ForeignKey, "Person", "PersonID")]
        public Int32 PersonID { get; set; }
    }
}
