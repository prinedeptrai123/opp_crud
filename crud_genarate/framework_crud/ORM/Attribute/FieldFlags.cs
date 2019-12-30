using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    [FlagsAttribute]
    public enum FieldFlags
    {
        // Field is not used
        None = 0,
		// doc
		Read = 1,
		// ghi
		Write = 2,
		// doc va ghi
		ReadWrite = 3,
		//khóa chính
		Key = 4,
		// trường tự tăng trong db
		Auto = 5,
        //là khóa ngoại
        ForeignKey = 6
    }
}
