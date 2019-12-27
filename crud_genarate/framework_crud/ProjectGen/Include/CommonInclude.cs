using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace framework_crud.ProjectGen
{
    public interface CommonInclude
    {
        /// <summary>
        /// Hàm include file vào project
        /// </summary>
        /// <param name="folder">Folder lưu file, không có để ""</param>
        /// <param name="fileName">file name cần thêm</param>
        /// <param name="solutionPath">Vị trí solution</param>
        /// <param name="projectName">Tên project</param>
        void includeProject(string folder, string fileName, string solutionPath, string projectName);
    }
}
