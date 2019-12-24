using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ORM
{
    //TODO:move to factory
    class Format
    {
        /// <summary>
        /// %s1,s3: name,s2: datatype
        /// </summary>
        public static string FORMAT_FIELD = "[Field(\"{0}\")]\n public {1}? {0};\n";

        /// <summary>
        /// %s1,s3: name,2: datatype
        /// </summary>
        public static string FORMAT_KEY = "[Field(\"{0}\", FieldFlags.Read | FieldFlags.Key | FieldFlags.Auto)]\n" +
        "public {1} {0};\n";

        public static string FORMAT_MODEL = ""
            + "using System;\n"
            + "using System.Collections.Generic;\n"
            + "using System.Linq;\n"
            + "using %NAMESPACE%.Models;\n"
            + "namespace %NAMESPACE%.Model\n"
            + "{\n"
            + "  [Table(\"%CLASS%\", \"%DB%\")]\n"
            + "  class %CLASS%\n"
            + "  {\n"
            + "      %FIELDS%"
            + "  }\n"
            + "}\n";
    }
}
