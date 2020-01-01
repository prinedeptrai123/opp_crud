using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Employee", "dbo")]
    public class Employee
    {        
        [Field("Employee_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Employee_ID {get;set;}
[Field("Employee_Name")]
 public string Employee_Name {get;set;}
[Field("Employee_Address")]
 public string Employee_Address {get;set;}
[Field("Employee_Email")]
 public string Employee_Email {get;set;}
[Field("Employee_DOB")]
 public DateTime Employee_DOB {get;set;}
[Field("Employee_Gender")]
 public string Employee_Gender {get;set;}
[Field("Employee_Role", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Employee_Role", "Role_ID")]
public Int32 Employee_Role {get;set;}
[Field("Employee_FirstDate")]
 public DateTime Employee_FirstDate {get;set;}
[Field("Employee_Sum_Date")]
 public Int32 Employee_Sum_Date {get;set;}
[Field("Employee_Date_Work")]
 public Int32 Employee_Date_Work {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}
[Field("Employee_Phone")]
 public string Employee_Phone {get;set;}
[Field("Employee_Identity")]
 public string Employee_Identity {get;set;}
[Field("Employee_Image")]
 public Byte[] Employee_Image {get;set;}

    }
}

