using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace ConsoleTest
{
	[Table("Table1", "dbo")]
    public class Table1
    {        
        [Field("id", FieldFlags.Read | FieldFlags.Key)]
public string id {get;set;}
[Field("name")]
 public string name {get;set;}

    }
}

