using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Bill_Type", "dbo")]
    public class Bill_Type
    {        
        [Field("Type_IDs", FieldFlags.Read | FieldFlags.Key)]
public Int32 Type_IDs {get;set;}
[Field("Type_Names")]
 public string Type_Names {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

