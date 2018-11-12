using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Services
{
    public interface INoteService
    {
        MethodResult<Note> Add(Note model);
        Note Get(Int32 id);
        Task<Note> DeleteNote(Int32 Id);
    }
}
