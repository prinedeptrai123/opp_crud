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

namespace ConsoleTest
{

    // Main run code
    class Program
    {
        static string connString = "server=sbusel\\sqlexpress; " +
                "database=test; trusted_connection=true;";
        static string con2 = "Data Source=DESKTOP-15SIF8Q\\MISACUKCUKVN; database=System Databases;Integrated Security=True;Connect Timeout=10";
        public static void Main(string[] args)
        {

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

            //TestData qui = MakeData();

            ////them
            ////database.Table(qui.GetType()).Insert(qui);
            ////cap nhat
            //database.Table(qui.GetType()).Update(qui);
            ////xoa
            ////lay danh sach
            //IList list1 = database.Table(qui.GetType()).Query().Select();

            //foreach (TestData td in list1)
            //    Console.WriteLine(td.rowID);
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


            Console.ReadKey();
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
