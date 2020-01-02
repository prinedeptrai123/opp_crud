using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace ConsoleTest
{
	[Table("Book_Type", "dbo")]
    public class Book_Type
    {        
        [Field("Type_IDs", FieldFlags.Read | FieldFlags.Key)]
public Int32 Type_IDs {get;set;}
[Field("Type_Names")]
 public string Type_Names {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

