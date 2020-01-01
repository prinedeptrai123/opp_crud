using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Discount_Code", "dbo")]
    public class Discount_Code
    {        
        [Field("Code_ID", FieldFlags.Read | FieldFlags.Key)]
public string Code_ID {get;set;}
[Field("Code_Name")]
 public string Code_Name {get;set;}
[Field("Code_Type", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Promotion_Type", "Type_IDs")]
public Int32 Code_Type {get;set;}
[Field("Date_Begin")]
 public DateTime Date_Begin {get;set;}
[Field("Date_End")]
 public DateTime Date_End {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

