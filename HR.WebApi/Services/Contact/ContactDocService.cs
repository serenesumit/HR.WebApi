using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories;

namespace HR.WebApi.Services
{
    public class ContactDocService : IContactDocService
    {
        private readonly IContactDocRepository _contactDocRepository;


        public ContactDocService(IContactDocRepository contactDocRepository
            )
        {
            this._contactDocRepository = contactDocRepository;
        }

        public MethodResult<ContactDoc> Add(ContactDoc model)
        {
            return this._contactDocRepository.Add(model);
        }

        public Task<UpFile> AddFileAsync(string containerName, int docId, string filename, Stream fileStream)
        {
            return this._contactDocRepository.AddFileAsync(containerName, docId, filename, fileStream);
        }

        public bool DeleteContactDocument(int Id, int docId)
        {
            return this._contactDocRepository.DeleteContactDocument(Id, docId);
        }

        public Task<bool> DeleteContactDocumentsByContactId(int contactId)
        {
            return this._contactDocRepository.DeleteContactDocumentsByContactId(contactId);
        }

        public Task<bool> DeleteFileAsync(string path)
        {
            return this._contactDocRepository.DeleteFileAsync(path);
        }

        public ContactDoc Get(int id)
        {
            return this._contactDocRepository.Get(id);
        }

        public int GetMaxId()
        {
            return this._contactDocRepository.GetMaxId();
        }

        public MethodResult<ContactDoc> Update(ContactDoc model)
        {
            return this._contactDocRepository.Update(model);
        }
    }
}