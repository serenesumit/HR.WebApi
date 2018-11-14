using HR.WebApi.Common;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HR.WebApi.Repositories
{
    public class ContactDocRepository : IContactDocRepository
    {
        private readonly IDbContextRepository _upRepository;
        private readonly IFileRepository _fileRepository;
        string storageAccount = System.Configuration.ConfigurationManager.AppSettings["StorageAccount"];

        public ContactDocRepository(IDbContextRepository upRepository,
             IFileRepository fileRepository
           )
        {
            this._upRepository = upRepository;
            this._fileRepository = fileRepository;
        }

        public MethodResult<ContactDoc> Add(ContactDoc model)
        {
            var result = new MethodResult<ContactDoc>();
            try
            {
                this._upRepository.ContactDocs.Add(model);
                this._upRepository.SaveChanges();
            }
            catch(Exception ex)
            {

            }
            
            result.Result = model;
            return result;
        }

        public async Task<UpFile> AddFileAsync(string containerName, int docId, string filename, Stream fileStream)
        {
            try
            {
                var path = string.Format(Constants.Azure.BlobPaths.ContactDocs, docId.ToString());
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

        public bool DeleteContactDocument(int Id, int docId)
        {
            var contactDoc = this._upRepository.ContactDocs.Where(p => p.Id == docId).FirstOrDefault();
            var isFileDeleted = this.DeleteFileAsync(contactDoc.Name);
            this._upRepository.ContactDocs.Remove(contactDoc);
            this._upRepository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteContactDocumentsByContactId(int contactId)
        {
            var contactDocs = this._upRepository.ContactDocs.Where(p => p.ContactId == contactId).ToList();
            foreach (var doc in contactDocs)
            {
                try
                {
                    var isFileDeleted = await this.DeleteFileAsync(doc.Name);
                    if (isFileDeleted)
                    {
                        this._upRepository.ContactDocs.Remove(doc);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<bool> DeleteFileAsync(string path)
        {
            try
            {
                this._fileRepository.Initialize(storageAccount, Constants.Azure.Containers.PageContactAssets);
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
            var data = _upRepository.ContactDocs.OrderByDescending(u => u.Id).FirstOrDefault();
            if (data != null) return data.Id +1;
            return 1;
        }

        public MethodResult<ContactDoc> Update(ContactDoc model)
        {
            var result = new MethodResult<ContactDoc>();
            this._upRepository.ContactDocs.Attach(model);
            this._upRepository.SaveChanges();
            result.Result = model;
            return result;
        }

        public ContactDoc Get(int id)
        {
            return this._upRepository.ContactDocs.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}