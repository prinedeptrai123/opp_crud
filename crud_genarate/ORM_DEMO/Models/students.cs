using System;
using System.Collections.Generic;
using System.Linq;
using ORM_DEMO.Models;
using framework_crud.ORM;

namespace ORM_DEMO.Models
{
	[Table("students", "dbo")]
    public class students
    {        
        [Field("id")]
 public string id;
[Field("name")]
 public string name;
[Field("school")]
 public string school;

    }
}

