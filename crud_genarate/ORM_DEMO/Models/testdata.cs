using System;
using System.Collections.Generic;
using System.Linq;
using ORM_DEMO.Models;
using framework_crud.ORM;

namespace ORM_DEMO.Models
{
	[Table("testdata", "dbo")]
    public class testdata
    {        
        [Field("rowID")]
 public int rowID;
[Field("byte1")]
 public Byte? byte1;
[Field("short1")]
 public Int16? short1;
[Field("int1")]
 public Int32? int1;
[Field("long1")]
 public Int64? long1;
[Field("float1")]
 public Single? float1;
[Field("double1")]
 public Double? double1;
[Field("decimal1")]
 public Decimal? decimal1;
[Field("money1")]
 public Decimal? money1;
[Field("string1")]
 public string string1;
[Field("binary1")]
 public Byte[] binary1;
[Field("image1")]
 public Byte[] image1;
[Field("bool1")]
 public Boolean bool1;
[Field("datetime1")]
 public DateTime? datetime1;
[Field("guid1")]
 public Guid? guid1;

    }
}

