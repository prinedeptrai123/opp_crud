using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Warehouse", "dbo")]
    public class Warehouse
    {        
        [Field("Warehouse_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Warehouse_ID {get;set;}
[Field("Warehouse_Type")]
 public Boolean Warehouse_Type {get;set;}
[Field("Warehouse_Toltal_Money")]
 public Double Warehouse_Toltal_Money {get;set;}
[Field("Warehouse_Date")]
 public DateTime Warehouse_Date {get;set;}
[Field("Employee_ID", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Employee", "Employee_ID")]
public Int32 Employee_ID {get;set;}

    }
}

