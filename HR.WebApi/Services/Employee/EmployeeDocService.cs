
using HR.WebApi.Common;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
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
        private readonly IDbContextRepository _upRepository;
        private readonly IFileRepository _fileRepository;
        string storageAccount = System.Configuration.ConfigurationManager.AppSettings["StorageAccount"];

        public EmployeeDocService(IDbContextRepository upRepository,
             IFileRepository fileRepository
           )
        {
            this._upRepository = upRepository;
            this._fileRepository = fileRepository;
        }


        public virtual MethodResult<EmployeeDoc> Add(EmployeeDoc model)
        {
            var result = new MethodResult<EmployeeDoc>();
            this._upRepository.EmployeeDocs.Add(model);
            this._upRepository.SaveChanges();
            result.Result = model;
            return result;
        }


        public virtual MethodResult<EmployeeDoc> Update(EmployeeDoc model)
        {
            var result = new MethodResult<EmployeeDoc>();
            this._upRepository.EmployeeDocs.Attach(model);
            this._upRepository.SaveChanges();
            result.Result = model;
            return result;
        }

        public bool DeleteEmployeeDocument(Guid Id, Guid resumeId)
        {
            var employeeResume = this._upRepository.EmployeeDocs.Where(p => p.Id == resumeId).FirstOrDefault();
            var isFileDeleted = this.DeleteFileAsync(employeeResume.Name);
            this._upRepository.EmployeeDocs.Remove(employeeResume);
            this._upRepository.SaveChanges();
            return true;
        }


        public virtual async Task<bool> DeleteFileAsync(string path)
        {
            try
            {
                this._fileRepository.Initialize(storageAccount, Constants.Azure.Containers.PageAssets);
                bool isDeleted = await this._fileRepository.DeleteFileAsync(path);
                return isDeleted;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

       


        public virtual async Task<UpFile> AddFileAsync(string containerName, Guid resumeId, string filename, Stream fileStream)
        {
            try
            {
                var path = string.Format(Constants.Azure.BlobPaths.Docs, resumeId.ToString());
                // http://stackoverflow.com/questions/1029740/get-mime-type-from-filename-extension
                var contentType = MimeMapping.GetMimeMapping(filename);
                this._fileRepository.Initialize(storageAccount, containerName);
                var storedFile = await this._fileRepository.StoreFileAsync(path, fileStream, contentType, filename);
                return storedFile;
            }
            catch (Exception ex)
            {
                return new UpFile();
            }

        }
    }
}
