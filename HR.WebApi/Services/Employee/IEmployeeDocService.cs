using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Services
{
    public interface IEmployeeDocService
    {
        MethodResult<EmployeeDoc> Add(EmployeeDoc model);

        MethodResult<EmployeeDoc> Update(EmployeeDoc model);

        bool DeleteEmployeeDocument(Int32 Id, Int32 resumeId);

        Task<UpFile> AddFileAsync(string containerName, Int32 resumeId, string filename, Stream fileStream);

        Task<bool> DeleteFileAsync(string path);

        int GetMaxId();
        Task<bool> DeleteDocumentsByEmployeeId(Int32 Id);

        EmployeeDoc Get(int id);
    }
}
