using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using framework_crud.ORM;

namespace ConsoleTest
{
    class SqlTestSuite : IDisposable
    {
        private IDatabase database;
        private bool trace = true;

        public SqlTestSuite(string connString)
        {
            Console.WriteLine("Opening database connection: " + connString);
            database = new MSSQLDatabase(
                    new SqlConnection(connString));

            database.Trace += TraceCommand;
        }

        public void Dispose()
        {
            Console.WriteLine("Closing database connection.");
            database.Dispose();
        }

        void TraceCommand(object sender, TraceEventArgs args)
        {
            if (!trace)
                return;
            Console.WriteLine("/****************************");
            Console.WriteLine(args.Command.CommandText);
            /*foreach (DbParameter p in args.Command.Parameters)
				Console.WriteLine("{0} {1} {2}",
						p.ParameterName,
						p.DbType,
						Convert.IsDBNull(p.Value) ? "DBNULL" : (p.Value ?? "NULL"));*/
            Console.WriteLine("****************************/");
        }

        void Truncate()
        {
            string sql = "truncate table testdata";
            database.Prepare(sql).ExecNonQuery();
        }

        TestData MakeData()
        {
            TestData data = new TestData();
            data.byte1 = byte.MaxValue / 2;
            data.short1 = short.MaxValue / 2;
            data.int1 = int.MaxValue / 2;
            data.long1 = long.MaxValue / 2;
            data.string1 = "Sqless.Test suite";
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

        public void BeforeTest()
        {
            Truncate();
        }

        public void Test1()
        {
            // Insert all NULL values.
            TestData data = new TestData();
            database.Table(data.GetType()).Insert(data);
            Console.WriteLine("Inserted ID " + data.rowID);
        }

        public void Test2()
        {
            // Insert real values.
            TestData data = MakeData();
            database.Table(typeof(TestData)).Insert(data);
            Console.WriteLine("Inserted ID " + data.rowID);
        }

        public void Test3()
        {
            TestData data = MakeData();
            database.Table<TestData>().Insert(data);
            Console.WriteLine("Inserted ID " + data.rowID);

            TestData copy = database.Table(typeof(TestData))
                    .Query().Eq("rowID", data.rowID).Find() as TestData;

            Console.WriteLine(copy.rowID == data.rowID);
            Console.WriteLine(copy.string1 == data.string1);
            Console.WriteLine(copy.int1 == data.int1);
            Console.WriteLine(copy.float1 == data.float1);
            Console.WriteLine(copy.double1 == data.double1);
            Console.WriteLine(copy.guid1 == data.guid1);
            Console.WriteLine(copy.decimal1 == data.decimal1);
            Console.WriteLine(copy.money1 == data.money1);
        }

        public void Test4()
        {
            ITable table = database.Table(typeof(TestData));
            for (int i = 0; i < 3; ++i)
                table.Insert(MakeData());
            IList selected = table.Query().Select();
            Console.WriteLine("Selected: " + selected.Count);
        }

        public void Test5()
        {
            for (int i = 0; i < 6; ++i)
            {
                TestData d = MakeData();
                d.bool1 = (i % 2) == 0;
                database.Table(typeof(TestData)).Insert(d);
            }
            IList selected = database.Table(typeof(TestData))
                    .Query().Eq("bool1", false).Select();
            Console.WriteLine("Selected: " + selected.Count);
        }

        public void Test6()
        {
            TestData data = MakeData();
            database.Table(typeof(TestData)).Insert(data);

            TestData template = new TestData();
            template.int1 = data.int1;
            template.float1 = data.float1;
            template.string1 = data.string1;
            TestData copy = database.Table(typeof(TestData))
                    .Query(template).Find() as TestData;

            Console.WriteLine(copy.rowID == data.rowID);
            Console.WriteLine(copy.string1 == data.string1);
            Console.WriteLine(copy.int1 == data.int1);
            Console.WriteLine(copy.float1 == data.float1);
            Console.WriteLine(copy.double1 == data.double1);
            Console.WriteLine(copy.guid1 == data.guid1);
            Console.WriteLine(copy.decimal1 == data.decimal1);
            Console.WriteLine(copy.money1 == data.money1);
        }

        public void Test7()
        {
            TestData data = MakeData();
            data.float1 = null;
            database.Table(typeof(TestData)).Insert(data);

            TestData copy = database.Table(typeof(TestData))
                    .Query().Eq("float1", null).Find() as TestData;

            Console.WriteLine(copy.rowID == data.rowID);
            Console.WriteLine(copy.string1 == data.string1);
            Console.WriteLine(copy.int1 == data.int1);
            Console.WriteLine(copy.float1 == data.float1);
            Console.WriteLine(copy.double1 == data.double1);
            Console.WriteLine(copy.guid1 == data.guid1);
            Console.WriteLine(copy.decimal1 == data.decimal1);
            Console.WriteLine(copy.money1 == data.money1);
        }

        public void Test8()
        {
            for (int i = 0; i < 4; ++i)
                database.Table(typeof(TestData)).Insert(MakeData());

            IList list = database.Table(typeof(TestData))
                    .Query().In("rowID", new int[] { 1, 2, 3 }).Select();
            foreach (TestData td in list)
                Console.WriteLine(td.rowID);
        }

        public void Test9()
        {
            for (int i = 0; i < 3; ++i)
                database.Table(typeof(TestData)).Insert(MakeData());

            string sql = "select rowID,int1,string1 from testdata";
            database.Prepare(sql).ExecQuery()
                    .ForEach(delegate (IRow row) {
                        Console.WriteLine(row["rowID"] + " " +
                            row["int1"] + " " + row["string1"]);
                    });
        }

        public void Test10()
        {
            trace = false;

            Stopwatch timer = new Stopwatch();
            TestData[] data = new TestData[1000];
            for (int i = 0; i < data.Length; ++i)
                data[i] = MakeData();

            ITable table = database.Table(typeof(TestData));
            timer.Start();
            foreach (TestData d in data)
                table.Insert(d);
            timer.Stop();
            Console.WriteLine(data.Length + " individual inserts took (ms): " +
                    timer.ElapsedMilliseconds);

            timer.Reset();
            timer.Start();
            table.Insert(data);
            timer.Stop();
            Console.WriteLine("Batch insert of " + data.Length +
                    " rows took (ms): " + timer.ElapsedMilliseconds);

            trace = true;
        }

        public void Test11()
        {
            TestDefine test = new TestDefine();
            database.Table(test.GetType()).Insert(test);
            Console.WriteLine("Insert id " + test.id);
        }

        public void Test12()
        {
            TestData[] data = new TestData[5];
            for (int i = 0; i < data.Length; ++i)
                data[i] = MakeData();
            database.Table(typeof(TestData)).Insert(data);

            string sql = "select rowID,int1,string1 from testdata";
            DataTable dt = database.Prepare(sql).ExecQuery().ToDataTable();
            foreach (DataRow r in dt.Rows)
                Console.WriteLine("{0} {1} {2}",
                        r["rowID"], r["int1"], r["string1"]);
        }
    }
}
