using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Warehouse_Detail", "dbo")]
    public class Warehouse_Detail
    {        
        [Field("Detail_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Detail_ID {get;set;}
[Field("Warehouse_ID", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Warehouse", "Warehouse_ID")]
public Int32 Warehouse_ID {get;set;}
[Field("Book_ID", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Book", "Book_ID")]
public Int32 Book_ID {get;set;}
[Field("Book_Count")]
 public Int32 Book_Count {get;set;}
[Field("Book_Price")]
 public Double Book_Price {get;set;}

    }
}

