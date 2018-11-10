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
    public class EventDocRepository : IEventDocRepository
    {
        private readonly IDbContextRepository _upRepository;
        private readonly IFileRepository _fileRepository;
        string storageAccount = System.Configuration.ConfigurationManager.AppSettings["StorageAccount"];

        public EventDocRepository(IDbContextRepository upRepository,
             IFileRepository fileRepository
           )
        {
            this._upRepository = upRepository;
            this._fileRepository = fileRepository;
        }

        public Int32 GetMaxId()
        {
            var data = _upRepository.Events.OrderByDescending(u => u.Id).FirstOrDefault();
            if (data != null) return data.Id;
            return 0;
        }

        public MethodResult<EventDoc> Add(EventDoc model)
        {
            var result = new MethodResult<EventDoc>();
            this._upRepository.EventDocs.Add(model);
            this._upRepository.SaveChanges();
            result.Result = model;
            return result;
        }


        public MethodResult<EventDoc> Update(EventDoc model)
        {
            var result = new MethodResult<EventDoc>();
            this._upRepository.EventDocs.Attach(model);
            this._upRepository.SaveChanges();
            result.Result = model;
            return result;
        }

        public bool DeleteEventDocument(Int32 Id, Int32 docId)
        {
            var eventDoc = this._upRepository.EventDocs.Where(p => p.Id == docId).FirstOrDefault();
            var isFileDeleted = this.DeleteFileAsync(eventDoc.Name);
            this._upRepository.EventDocs.Remove(eventDoc);
            this._upRepository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteEventDocumentsByEventId(Int32 eventId)
        {
            var eventDocs = this._upRepository.EventDocs.Where(p => p.EventId == eventId).ToList();
            foreach (var doc in eventDocs)
            {
                try
                {
                    var isFileDeleted = await this.DeleteFileAsync(doc.Name);
                    if (isFileDeleted)
                    {
                        this._upRepository.EventDocs.Remove(doc);
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
                this._fileRepository.Initialize(storageAccount, Constants.Azure.Containers.PageEventAssets);
                bool isDeleted = await this._fileRepository.DeleteFileAsync(path);
                return isDeleted;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        public virtual async Task<UpFile> AddFileAsync(string containerName, Int32 docId, string filename, Stream fileStream)
        {
            try
            {
                var path = string.Format(Constants.Azure.BlobPaths.EventDocs, docId.ToString());
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