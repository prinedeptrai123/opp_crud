using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Employee_Role", "dbo")]
    public class Employee_Role
    {        
        [Field("Role_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Role_ID {get;set;}
[Field("Role_Name")]
 public string Role_Name {get;set;}
[Field("Role_Decentralization", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Decentralization", "Decentralization_ID")]
public Int32 Role_Decentralization {get;set;}
[Field("Role_Salary")]
 public Double Role_Salary {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

