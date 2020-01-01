using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Publishing_Company", "dbo")]
    public class Publishing_Company
    {        
        [Field("Company_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Company_ID {get;set;}
[Field("Company_Name")]
 public string Company_Name {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

