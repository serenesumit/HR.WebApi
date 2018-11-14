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
    public interface IEventDocService
    {
        MethodResult<EventDoc> Add(EventDoc model);

        MethodResult<EventDoc> Update(EventDoc model);

        bool DeleteEventDocument(Int32 Id, Int32 docId);

        Task<UpFile> AddFileAsync(string containerName, Int32 docId, string filename, Stream fileStream);

        Task<bool> DeleteFileAsync(string path);

        Int32 GetMaxId();

        Task<bool> DeleteEventDocumentsByEventId(Int32 eventId);

        EventDoc Get(int id);
    }
}
