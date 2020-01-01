using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("tabletest", "dbo")]
    public class tabletest
    {        
        [Field("name")]
 public string name {get;set;}
[Field("age")]
 public Double age {get;set;}

    }
}

