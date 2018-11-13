using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HR.WebApi.Common;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;

namespace HR.WebApi.Repositories
{
    public class EmployeeDocRepository : IEmployeeDocRepository
    {
        private readonly IDbContextRepository _upRepository;
        private readonly IFileRepository _fileRepository;
        string storageAccount = System.Configuration.ConfigurationManager.AppSettings["StorageAccount"];

        public EmployeeDocRepository(IDbContextRepository upRepository,
            IFileRepository fileRepository
          )
        {
            this._upRepository = upRepository;
            this._fileRepository = fileRepository;
        }

        public MethodResult<EmployeeDoc> Add(EmployeeDoc model)
        {
            var result = new MethodResult<EmployeeDoc>();

            this._upRepository.EmployeeDocs.Add(model);
            this._upRepository.SaveChanges();

            result.Result = model;
            return result;
        }

        public async Task<UpFile> AddFileAsync(string containerName, int resumeId, string filename, Stream fileStream)
        {
            var path = string.Format(Constants.Azure.BlobPaths.EmployeeResumes, resumeId.ToString());
            // http://stackoverflow.com/questions/1029740/get-mime-type-from-filename-extension
            var contentType = MimeMapping.GetMimeMapping(filename);
            this._fileRepository.Initialize(storageAccount, containerName);
            var storedFile = await this._fileRepository.StoreFileAsync(path, fileStream, contentType, filename);
            return storedFile;
        }

        public async Task<bool> DeleteDocumentsByEmployeeId(int Id)
        {
            var employeeResumes = this._upRepository.EmployeeDocs.Where(p => p.EmployeeId == Id).ToList();
            foreach (var resume in employeeResumes)
            {
                try
                {
                    var isFileDeleted = await this.DeleteFileAsync(resume.Name);
                    if (isFileDeleted)
                    {
                        this._upRepository.EmployeeDocs.Remove(resume);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return true;
        }

        public bool DeleteEmployeeDocument(int Id, int resumeId)
        {
            var employeeResume = this._upRepository.EmployeeDocs.Where(p => p.Id == resumeId).FirstOrDefault();
            var isFileDeleted = this.DeleteFileAsync(employeeResume.Name);
            this._upRepository.EmployeeDocs.Remove(employeeResume);
            this._upRepository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteFileAsync(string path)
        {
            try
            {
                this._fileRepository.Initialize(storageAccount, Constants.Azure.Containers.PageEmployeeAssets);
                bool isDeleted = await this._fileRepository.DeleteFileAsync(path);
                return isDeleted;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int GetMaxId()
        {
            var data = _upRepository.EmployeeDocs.OrderByDescending(u => u.Id).FirstOrDefault();
            if (data != null) return data.Id + 1;
            return 1;
        }

        public MethodResult<EmployeeDoc> Update(EmployeeDoc model)
        {
            var result = new MethodResult<EmployeeDoc>();
            this._upRepository.EmployeeDocs.Attach(model);
            this._upRepository.SaveChanges();
            result.Result = model;
            return result;
        }
    }
}