using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HR.WebApi.Services
{
    public class EventDocService : IEventDocService
    {
        private readonly IEventDocRepository _eventDocRepository;


        public EventDocService(IEventDocRepository eventDocRepository
            )
        {
            this._eventDocRepository = eventDocRepository;
        }

        public MethodResult<EventDoc> Add(EventDoc model)
        {
            return this._eventDocRepository.Add(model);
        }

        public Int32 GetMaxId()
        {
            return this._eventDocRepository.GetMaxId();
        }

        public MethodResult<EventDoc> Update(EventDoc model)
        {
            return this._eventDocRepository.Update(model);
        }

        public bool DeleteEventDocument(Int32 Id, Int32 docId)
        {
            return this._eventDocRepository.DeleteEventDocument(Id, docId);
        }

        public Task<UpFile> AddFileAsync(string containerName, Int32 docId, string filename, Stream fileStream)
        {
            return this._eventDocRepository.AddFileAsync(containerName, docId, filename, fileStream);
        }

        public Task<bool> DeleteFileAsync(string path)
        {
            return this._eventDocRepository.DeleteFileAsync(path);
        }
        
         public Task<bool> DeleteEventDocumentsByEventId(Int32 eventId)
        {
            return this._eventDocRepository.DeleteEventDocumentsByEventId(eventId);
        }

        public EventDoc Get(int id)
        {
            return this._eventDocRepository.Get(id);
        }
    }


}