using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.BASESQL
{
    class BaseQuery
    {
        public static string GET_TABLE_INFOMATION = "SELECT C.Column_name, data_type, is_Nullable, U.CONSTRAINT_NAME"
                + " FROM information_Schema.Columns C FULL OUTER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE U ON C.COLUMN_NAME = U.COLUMN_NAME"
                + " WHERE C.TABLE_NAME= '{0}'";

        public static string GET_ALL_TALBE = "SELECT TABLE_NAME FROM information_schema.tables WHERE TABLE_TYPE ='BASE TABLE' order by TABLE_NAME";
    }
}
