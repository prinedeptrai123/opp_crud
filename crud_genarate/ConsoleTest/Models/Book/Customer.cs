using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Customer", "dbo")]
    public class Customer
    {        
        [Field("Customer_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Customer_ID {get;set;}
[Field("Customer_Name")]
 public string Customer_Name {get;set;}
[Field("Customer_Gender")]
 public string Customer_Gender {get;set;}
[Field("Customer_Address")]
 public string Customer_Address {get;set;}
[Field("Customer_Email")]
 public string Customer_Email {get;set;}
[Field("Customer_Phone")]
 public string Customer_Phone {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

