using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTest.Models;
using framework_crud.ORM;

namespace ConsoleTest.Models
{
    [Table("Course", "dbo")]
    class Course
    {
        [Field("CourseID", FieldFlags.Key|FieldFlags.ForeignKey)]
        public Int32 CourseID { get; set; }
        [Field("Title")]
        public string Title { get; set; }
        [Field("Credits")]
        public Int32 Credits { get; set; }
        [Field("DepartmentID", FieldFlags.ForeignKey, "Department", "DepartmentID")]
        public Int32 DepartmentID { get; set; }
    }
}
