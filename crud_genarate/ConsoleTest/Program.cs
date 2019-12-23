using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using framework_crud.MSSQL;

namespace ConsoleTest
{
    class Program
    {
        static string connString = "server=sbusel\\sqlexpress; " +
                "database=test; trusted_connection=true;";
        static string con2 = "Data Source=DESKTOP-GR8RADT\\SQLEXPRESS; database=test;Integrated Security=True;Connect Timeout=10";
        public static void Main(string[] args)
        {
            Stopwatch timer = Stopwatch.StartNew();

            string SQLServer = "DESKTOP-GR8RADT\\SQLEXPRESS";

            SQLConnector connector = new SQLConnector(SQLServer);

            using (SqlTestSuite testSuite = new SqlTestSuite(con2))
                RunSuite(testSuite);
            timer.Stop();
            Console.WriteLine("\nSqlTestSuite total execution time (ms): " +
                    timer.ElapsedMilliseconds);

            Console.ReadKey();
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
