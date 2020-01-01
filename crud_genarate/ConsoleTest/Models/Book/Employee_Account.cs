using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Employee_Account", "dbo")]
    public class Employee_Account
    {        
        [Field("Employee_ID", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Employee", "Employee_ID")]
public Int32 Employee_ID {get;set;}
[Field("Employee_ID")]
 public Int32 Employee_ID {get;set;}
[Field("Account_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Account_ID {get;set;}
[Field("Account_User")]
 public string Account_User {get;set;}
[Field("Account_Password")]
 public string Account_Password {get;set;}
[Field("Account_LastLogin")]
 public DateTime Account_LastLogin {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

