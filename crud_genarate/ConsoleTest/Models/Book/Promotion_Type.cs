using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Promotion_Type", "dbo")]
    public class Promotion_Type
    {        
        [Field("Type_IDs", FieldFlags.Read | FieldFlags.Key)]
public Int32 Type_IDs {get;set;}
[Field("Type_Names")]
 public string Type_Names {get;set;}
[Field("Book_Count")]
 public Int32 Book_Count {get;set;}
[Field("Type_Promotion")]
 public Double Type_Promotion {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

