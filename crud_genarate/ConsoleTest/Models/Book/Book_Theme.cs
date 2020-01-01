using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Book_Theme", "dbo")]
    public class Book_Theme
    {        
        [Field("Theme_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Theme_ID {get;set;}
[Field("Type_IDs", FieldFlags.Key | FieldFlags.ForeignKey, "Book_Type", "Type_IDs")]
public Int32 Type_IDs {get;set;}
[Field("Theme_Name")]
 public string Theme_Name {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

