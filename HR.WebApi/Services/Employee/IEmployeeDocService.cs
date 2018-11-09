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

        bool DeleteEmployeeDocument(Guid Id, Guid resumeId);

        Task<UpFile> AddFileAsync(string containerName, Guid resumeId, string filename, Stream fileStream);

        Task<bool> DeleteFileAsync(string path);
        
    }
}
