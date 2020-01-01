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
using ConsoleTest.Models;
using System.Text.RegularExpressions;

namespace ConsoleTest
{

    // Main run code
    class Program
    {
        static string connString = "server=sbusel\\sqlexpress; " +
                "database=test; trusted_connection=true;";
        static string con2 = "Data Source=DESKTOP-GR8RADT\\SQLEXPRESS; database=School;Integrated Security=True;Connect Timeout=10";
        //[STAThread]
        public static void Main(string[] args)
        {

            FieldFlags a = FieldFlags.ForeignKey | FieldFlags.Key | FieldFlags.Auto;
            //string a1 = " " + a.ToString();
            //a1 = a1.Replace(" ", "FieldFlags.");
            //a1 = a1.Replace(",", " | ");
            Console.WriteLine(a & FieldFlags.Write);
            if((a & FieldFlags.Key) == FieldFlags.Key){
                Console.WriteLine("hihi");
            }




            //Console.WriteLine(a1);

                //string s = Regex.Replace(a.ToString(), @"\,\b", "something", RegexOptions.IgnoreCase);

                //Console.WriteLine(a);

                //ProjectMaster.Instance.genTable();
                //Stopwatch timer = Stopwatch.StartNew();

                //string SQLServer = "DESKTOP-GR8RADT\\SQLEXPRESS";


                //SQLConnector connector = new SQLConnector(SQLServer);

                ////using (SqlTestSuite testSuite = new SqlTestSuite(con2))
                ////    RunSuite(testSuite);
                ////timer.Stop();
                //Console.WriteLine("Opening database connection: " + connString);


                //MSSQLDatabase database = new MSSQLDatabase(
                //        new SqlConnection(con2));
                //database.listTable();



                //using(var database = new MSSQLDatabase(new SqlConnection(con2)))
                //{
                //    IList list = database.Table(typeof(Course)).Query().Select();

                //    foreach (Course c in list)
                //    {
                //        Console.WriteLine(c.Title);
                //    }
                //}

                //using (var database = new MSSQLDatabase(new SqlConnection(con2)))
                //{
                //    Course newCourse = new Course
                //    {
                //        CourseID = 11,
                //        Credits = 1,
                //        DepartmentID = 7,
                //        Title = "quinew"
                //    };
                //   // database.Table(typeof(Course)).Insert(newCourse);
                //}
                //Console.WriteLine("---------");

                //using (var database = new MSSQLDatabase(new SqlConnection(con2)))
                //{
                //    IList a = database.Table(typeof(Course)).Query().Eq("Title", "54").Select();

                //    ////foreach (Course cour in a)
                //    ////{
                //    ////    Console.WriteLine(cour.CourseID);
                //    ////}
                //    database.Table(typeof(Course)).Delete(a[0]);
                //}
                //using (var database = new MSSQLDatabase(new SqlConnection(con2)))
                //{
                //    IList list = database.Table(typeof(Course)).Query().Select();

                //    foreach (Course c in list)
                //    {
                //        Console.WriteLine(c.Title);
                //    }
                //}


                //Console.WriteLine("---------");

                //string path = @"F:\OPP\demoApp";
                //string nameSpace = "ConsoleTest";
                //ProjectMaster master = new ProjectMaster(con2, nameSpace, path);
                //master.genTable();

                //TestData qui = MakeData();
                //History qui = new History();

                ////them
                ////database.Table(qui.GetType()).Insert(qui);
                ////cap nhat
                //database.Table(qui.GetType()).Update(qui);
                ////xoa
                ///
                ////lay danh sach
                //IList list1 = database.Table(qui.GetType()).Query().Select();

                //foreach (History td in list1)
                //    Console.WriteLine(td.TrangThai);
                //Console.WriteLine("\nSqlTestSuite total execution time (ms): " +
                //        timer.ElapsedMilliseconds);

                //IList list = database.Table(typeof(TestData))
                //        .Query().In("rowID", new int[] { 1, 2, 3 }).Select();
                //foreach (TestData td in list)
                //{
                //    Console.WriteLine(td.rowID);
                //    Console.WriteLine(td.string1);
                //    Console.WriteLine(td.float1);
                //}

                //TableTest qui2 = new TableTest { name = "q", age = 222 };
                ////delete qui2


                //database.Table(qui2.GetType()).Insert(qui2);
                //database.Table(qui2.GetType()).Delete(qui2);
                ////detete
                //database.Table(qui.GetType()).Delete(list);
                ////database.Table(qui.GetType()).

                ////delete theo id
                ////TODO: 

                //generateProject();
                // string path = @"D:\TemplateSolution";
                // string projectName = "TemplateProject";
                //// IncludeHelper.IncludeFile("Models", path, projectName);

                // CommonInclude includeHandle = new IncludeFile();
                // includeHandle.includeProject("", "kkkkk", path, projectName);
                // includeHandle.includeProject("qui", "lllll", path, projectName);

                // includeHandle = new IncludeForm();
                // includeHandle.includeProject("qui", "Form1", path, projectName);

                //generateProject();
                Console.ReadKey();
        }

        static public void generateProject()
        {
            string solutionName = "TemplateSolution";
            string projectName = "TemplateProject";

            string path = @"D:/Template/";
           // ProjectMaster.Instance.generateProject(solutionName, projectName, path);
            ProjectMaster a = new ProjectMaster("Data Source=DESKTOP-GR8RADT\\SQLEXPRESS; database=test;Integrated Security=True;Connect Timeout=10", "qui", "kkk");
            a.generateProject(solutionName, projectName, path);

        }

        static TestData MakeData()
        {
            TestData data = new TestData();
            data.byte1 = byte.MaxValue / 2;
            data.short1 = short.MaxValue / 2;
            data.int1 = int.MaxValue / 2;
            data.long1 = long.MaxValue / 2;
            data.string1 = "update";
            data.bool1 = true;
            data.datetime1 = DateTime.Now;
            data.guid1 = Guid.NewGuid();
            data.float1 = float.MaxValue / 2;
            data.double1 = double.MaxValue / 2;
            data.money1 = 109654.9875m; //allows 4 digits after decimal point
            data.decimal1 = 1234567890.39381234m;
            data.binary1 = ASCIIEncoding.Unicode.GetBytes(data.string1);
            data.image1 = ASCIIEncoding.Unicode.GetBytes(data.string1);
            return data;
        }

        static void RunSuite(object testSuite)
        {
            MethodInfo before = testSuite.GetType().GetMethod("BeforeTest");
            MethodInfo after = testSuite.GetType().GetMethod("AfterTest");
            MethodInfo[] methods = testSuite.GetType().GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.Name.StartsWith("Test"))
                {
                    Console.WriteLine("---" + method.Name + "---");
                    if (before != null)
                        InvokeMethod(before, testSuite);
                    InvokeMethod(method, testSuite);
                    if (after != null)
                        InvokeMethod(after, testSuite);
                    Console.WriteLine();
                }
            }
        }

        static void InvokeMethod(MethodInfo method, object testSuite)
        {
            try
            {
                method.Invoke(testSuite, null);
            }
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null)
                    HandleException(e.InnerException);
                else
                    HandleException(e);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        static void HandleException(Exception e)
        {
            Console.WriteLine("---ERROR");
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
            Console.WriteLine("--------");
        }
    }
}
