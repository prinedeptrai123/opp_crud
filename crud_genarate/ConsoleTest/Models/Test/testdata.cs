using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace ConsoleTest
{
	[Table("testdata", "dbo")]
    public class testdata
    {        
        [Field("rowID", FieldFlags.Read | FieldFlags.Key)]
public Int32 rowID {get;set;}
[Field("byte1")]
 public Byte byte1 {get;set;}
[Field("short1")]
 public Int16 short1 {get;set;}
[Field("int1")]
 public Int32 int1 {get;set;}
[Field("long1")]
 public Int64 long1 {get;set;}
[Field("float1")]
 public Single float1 {get;set;}
[Field("double1")]
 public Double double1 {get;set;}
[Field("decimal1")]
 public Decimal decimal1 {get;set;}
[Field("money1")]
 public Decimal money1 {get;set;}
[Field("string1")]
 public string string1 {get;set;}
[Field("binary1")]
 public Byte[] binary1 {get;set;}
[Field("image1")]
 public Byte[] image1 {get;set;}
[Field("bool1")]
 public Boolean bool1 {get;set;}
[Field("datetime1")]
 public DateTime datetime1 {get;set;}
[Field("guid1")]
 public Guid guid1 {get;set;}

    }
}

