using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Bill", "dbo")]
    public class Bill
    {        
        [Field("Bill_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Bill_ID {get;set;}
[Field("Bill_Type", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Bill_Type", "Type_IDs")]
public Int32 Bill_Type {get;set;}
[Field("Discount_Code", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Discount_Code", "Code_ID")]
public string Discount_Code {get;set;}
[Field("Sum_Money")]
 public Double Sum_Money {get;set;}
[Field("Total_Money")]
 public Double Total_Money {get;set;}
[Field("Excess_Cash")]
 public Double Excess_Cash {get;set;}
[Field("Customer_Cash")]
 public Double Customer_Cash {get;set;}
[Field("Customer_ID", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Customer", "Customer_ID")]
public Int32 Customer_ID {get;set;}
[Field("Employee_ID", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Employee", "Employee_ID")]
public Int32 Employee_ID {get;set;}
[Field("Bill_Date")]
 public DateTime Bill_Date {get;set;}

    }
}

