using framework_crud.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    [Table("History", "dbo")]
    class History
    {
        [Field("ID", FieldFlags.Read | FieldFlags.Key | FieldFlags.Auto)]
        public int ID;
        [Field("Price")]
        public float Price;
        [Field("Product")]
        public string Product;
        [Field("Customer_ID", FieldFlags.ForeignKey, "Customer", "ID")]
        public int Customer_ID;
        [Field("Count")]
        public int Count;
        [Field("Time")]
        public DateTime Time;
        [Field("PhiGiaoDich")]
        public float PhiGiaoDich;
        [Field("Type")]
        public bool Type;
        [Field("TrangThai")]
        public bool TrangThai;

        //reference
        //public Customer customer;
    }
}
