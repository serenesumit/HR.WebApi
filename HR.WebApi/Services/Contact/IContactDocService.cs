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
    public interface IContactDocService
    {
        MethodResult<ContactDoc> Add(ContactDoc model);

        MethodResult<ContactDoc> Update(ContactDoc model);

        bool DeleteContactDocument(Int32 Id, Int32 docId);

        Task<UpFile> AddFileAsync(string containerName, Int32 docId, string filename, Stream fileStream);

        Task<bool> DeleteFileAsync(string path);

        Int32 GetMaxId();

        Task<bool> DeleteContactDocumentsByContactId(Int32 contactId);

        ContactDoc Get(int id);
    }
}
