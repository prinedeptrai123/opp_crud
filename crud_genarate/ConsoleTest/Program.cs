using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using framework_crud;
using framework_crud.MSSQL;
using framework_crud.ORM;
using System.Threading;
using framework_crud.ProjectGen;
using System.Text.RegularExpressions;

namespace ConsoleTest
{
    class Program
    {
        static string con2 = "Data Source=DESKTOP-GR8RADT\\SQLEXPRESS; database=School;Integrated Security=True;Connect Timeout=10";
        string database = "School, Test, BookStoreData";
        //[STAThread]
        public static void Main(string[] args)
        {
            //TestSchool();
            //TestTest();
            //check2();

            //FieldFlags a = FieldFlags.Key | FieldFlags.ForeignKey;

            //Console.WriteLine(a);
            checkdelete();
            Console.ReadKey();
        }

        static void checkdelete()
        {
            con2 = @"Data Source=DESKTOP-GR8RADT\SQLEXPRESS;Initial Catalog=School;Integrated Security=True;Connect Timeout=10";
            MSSQLDatabase a = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(con2));

            CourseInstructor coure = new CourseInstructor()
            {
                CourseID = 1061,
                PersonID = 31
            };


            a.Table(typeof(CourseInstructor)).Delete(coure);
            a.Table(typeof(CourseInstructor)).Insert(coure);
           // a.Table(typeof(CourseInstructor)).Update(coure);
           // a.Table(typeof(CourseInstructor)).Query().Select();
            //a.Table(typeof(CourseInstructor)).Query().And().Eq("CourseID", 1).EndSub();

        }

        static void check2()
        {

        }

        static void TestSchool()
        {
            con2 = @"Data Source=DESKTOP-GR8RADT\SQLEXPRESS;Initial Catalog=Student;Integrated Security=True;Connect Timeout=10";
            MSSQLDatabase a = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(con2));

            Info test = new Info()
            {
                ID = 1,
                Phone = "222",
                Address = "22",
                Age = 2,
                Class = "sss",
                Name = "quii"
            };

            a.Table(typeof(Info)).Insert(test);
        }

        static void TestTest()
        {
            con2 = @"Data Source=DESKTOP-GR8RADT\SQLEXPRESS;Initial Catalog=test;Integrated Security=True;Connect Timeout=10";
            MSSQLDatabase a = new MSSQLDatabase(new System.Data.SqlClient.SqlConnection(con2));

            Table1 test = new Table1()
            {
                id = "10",
                name = "222"
            };
            a.Table(typeof(Table1)).Insert(test);
        }
    }
}
