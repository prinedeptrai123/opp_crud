using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Book_Inventory", "dbo")]
    public class Book_Inventory
    {        
        [Field("Book_ID", FieldFlags.Key | FieldFlags.ForeignKey, "Book", "Book_ID")]
public Int32 Book_ID {get;set;}
[Field("Warehouse_ID", FieldFlags.Key | FieldFlags.ForeignKey, "Warehouse", "Warehouse_ID")]
public Int32 Warehouse_ID {get;set;}
[Field("Book_Count")]
 public Int32 Book_Count {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

