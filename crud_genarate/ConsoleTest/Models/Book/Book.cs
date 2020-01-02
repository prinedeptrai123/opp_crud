using System;
using System.Collections.Generic;
using System.Linq;
using DEMO.Models;
using framework_crud.ORM;

namespace DEMO.Models
{
	[Table("Book", "dbo")]
    public class Book
    {        
        [Field("Book_ID", FieldFlags.Read | FieldFlags.Key)]
public Int32 Book_ID {get;set;}
[Field("Book_Name")]
 public string Book_Name {get;set;}
[Field("Book_Author")]
 public string Book_Author {get;set;}
[Field("Book_Type", FieldFlags.Key | FieldFlags.ForeignKey, "Book_Theme", "Type_IDs")]
public Int32 Book_Type {get;set;}
[Field("Book_Theme", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Book_Theme", "Theme_ID")]
public Int32 Book_Theme {get;set;}
[Field("Book_Company", FieldFlags.ReadWrite | FieldFlags.ForeignKey, "Publishing_Company", "Company_ID")]
public Int32 Book_Company {get;set;}
[Field("Book_Price")]
 public Double Book_Price {get;set;}
[Field("Book_Promotion")]
 public Double Book_Promotion {get;set;}
[Field("Book_Image")]
 public Byte[] Book_Image {get;set;}
[Field("Book_Count")]
 public Int32 Book_Count {get;set;}
[Field("Exist")]
 public Boolean Exist {get;set;}

    }
}

