using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using framework_crud.ORM;

namespace ConsoleTest
{

    // test table to call query

    [Table("testdata", "dbo")]
    public class TestData
    {
        [Field("rowID", FieldFlags.Read | FieldFlags.Key | FieldFlags.Auto)]
        public int? rowID;

        [Field("byte1")] public byte? byte1;
        [Field("short1")] public short? short1;
        [Field("int1")] public int? int1;
        [Field("long1")] public long? long1;
        [Field("string1")] public string string1;
        [Field("bool1")] public bool? bool1;
        [Field("datetime1")] public DateTime? datetime1;
        [Field("guid1")] public Guid? guid1;
        [Field("float1")] public float? float1;
        [Field("double1")] public double? double1;
        [Field("decimal1")] public decimal? decimal1;
        [Field("money1")] public decimal? money1;
        [Field("binary1")] public byte[] binary1;
        [Field("image1")] public byte[] image1;
    }


    public class TestDefine
    {
        public int id;
        public int number;
        public string name;

        public static TableDefinition DefineTable()
        {
            return new TableDefinition("testdata").Schema("dbo")
                    .Field("rowID").MapTo("id").Key().Auto().ReadOnly().Add()
                    .Field("int1").MapTo("number").Add()
                    .Field("string1").MapTo("name").Add();
        }
    }
}
