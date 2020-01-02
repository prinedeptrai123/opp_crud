using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Pay_Wage", "dbo")]
    public class Pay_Wage
    {        
        [Field("ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 ID {get;set;}
[Field("Employee_ID", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Employee", "Employee_ID")]
public Int32 Employee_ID {get;set;}
[Field("Salary")]
 public Double Salary {get;set;}
[Field("PayWage_Date")]
 public DateTime PayWage_Date {get;set;}

    }
}

