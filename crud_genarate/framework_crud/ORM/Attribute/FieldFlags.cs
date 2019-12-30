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
		// Field can be read from the database.
		Read = 1,
		// Field can be written to the database.
		Write = 2,
		// Field can be read and written. Same as Read | Write.
		ReadWrite = 3,
		//khóa chính
		Key = 4,
		// trường tự tăng trong db
		Auto = 5,
        //là khóa ngoại
        ForeignKey = 6
    }
}
