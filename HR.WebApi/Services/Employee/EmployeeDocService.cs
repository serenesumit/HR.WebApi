
using HR.WebApi.Common;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories;
using HR.WebApi.Repositories.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HR.WebApi.Services
{
    public class EmployeeDocService : IEmployeeDocService
    {
        private readonly IEmployeeDocRepository _employeeDocRepository;

        public EmployeeDocService(
           IEmployeeDocRepository employeeDocRepository
           )
        {
            this._employeeDocRepository = employeeDocRepository;

        }


        public virtual MethodResult<EmployeeDoc> Add(EmployeeDoc model)
        {
            return this._employeeDocRepository.Add(model);
        }


        public virtual MethodResult<EmployeeDoc> Update(EmployeeDoc model)
        {
            return this._employeeDocRepository.Update(model);
        }

        public bool DeleteEmployeeDocument(Int32 Id, Int32 resumeId)
        {
            return this._employeeDocRepository.DeleteEmployeeDocument(Id, resumeId);
        }


        public async Task<bool> DeleteFileAsync(string path)
        {
            return await this._employeeDocRepository.DeleteFileAsync(path);
        }




        public async Task<UpFile> AddFileAsync(string containerName, Int32 resumeId, string filename, Stream fileStream)
        {
            return await this._employeeDocRepository.AddFileAsync(containerName, resumeId, filename, fileStream);

        }

        public async Task<bool> DeleteDocumentsByEmployeeId(Int32 Id)
        {
            return await this._employeeDocRepository.DeleteDocumentsByEmployeeId(Id);
        }

        public int GetMaxId()
        {
            return this._employeeDocRepository.GetMaxId();
        }

        public EmployeeDoc Get(int id)
        {
            return this._employeeDocRepository.Get(id);
        }
    }
}
