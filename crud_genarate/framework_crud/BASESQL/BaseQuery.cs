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

        /// <summary>
        /// Return 
        /// </summary>
        public static string GET_TABLE_INFOMATION_2 =
            @"SELECT
	            INFO_ALL.TABLE_NAME,INFO_ALL.COLUMN_NAME,INFO_ALL.DATA_TYPE,INFO_ALL.CONSTRAINT_TYPE,
	            FK_INFO.referenced_table AS RF_TABLE,FK_INFO.referenced_column AS RF_COLUMN
                FROM
	                (SELECT ISC.TABLE_NAME,KCU_TC.CONSTRAINT_NAME,ISC.DATA_TYPE,ISC.COLUMN_NAME,KCU_TC.CONSTRAINT_TYPE from INFORMATION_SCHEMA.COLUMNS AS ISC 
	                LEFT JOIN (SELECT 
					                KCU.TABLE_NAME,
					                KCU.COLUMN_NAME,
					                KCU.CONSTRAINT_NAME,
					                TC.CONSTRAINT_TYPE
					                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
					                LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC ON KCU.CONSTRAINT_NAME = TC.CONSTRAINT_NAME
					                WHERE KCU.TABLE_NAME = '{0}') KCU_TC 
					                ON ISC.TABLE_NAME = KCU_TC.TABLE_NAME AND ISC.COLUMN_NAME = KCU_TC.COLUMN_NAME
					                WHERE ISC.TABLE_NAME = '{0}'
	
	                ) AS INFO_ALL
	                LEFT JOIN (SELECT  obj.name AS FK_NAME,
                    sch.name AS [schema_name],
                    tab1.name AS [table_2],
                    col1.name AS [column_2],
                    tab2.name AS [referenced_table],
                    col2.name AS [referenced_column]
	                FROM sys.foreign_key_columns fkc
	                INNER JOIN sys.objects obj
		                ON obj.object_id = fkc.constraint_object_id
	                INNER JOIN sys.tables tab1
		                ON tab1.object_id = fkc.parent_object_id
	                INNER JOIN sys.schemas sch
		                ON tab1.schema_id = sch.schema_id
	                INNER JOIN sys.columns col1
		                ON col1.column_id = parent_column_id AND col1.object_id = tab1.object_id
	                INNER JOIN sys.tables tab2
		                ON tab2.object_id = fkc.referenced_object_id
	                INNER JOIN sys.columns col2
		                ON col2.column_id = referenced_column_id AND col2.object_id = tab2.object_id) AS FK_INFO
		                ON FK_INFO.table_2 = INFO_ALL.TABLE_NAME AND INFO_ALL.COLUMN_NAME = FK_INFO.column_2
                WHERE INFO_ALL.TABLE_NAME = '{0}'";

        public static string GET_TABLE_IDENTITY = @"SELECT	name, is_identity
                        FROM sys.columns
                        WHERE[object_id] = object_id('{0}') AND is_identity = 1";
    }
}
