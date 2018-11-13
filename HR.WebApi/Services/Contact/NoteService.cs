using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories;

namespace HR.WebApi.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository
           )
        {
            this._noteRepository = noteRepository;
        }

        public MethodResult<Note> Add(Note model)
        {
            throw new NotImplementedException();
        }

        public Task<Note> DeleteNote(int Id)
        {
            throw new NotImplementedException();
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Note>> GetAll(Int32 contactId)
        {
            return this._noteRepository.GetAll(contactId);
        }
    }
}