using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Decentralization", "dbo")]
    public class Decentralization
    {        
        [Field("Decentralization_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Decentralization_ID {get;set;}
[Field("Decentralization_Name")]
 public string Decentralization_Name {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

