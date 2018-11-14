using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HR.WebApi.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly IDbContextRepository _upRepository;

        public NoteRepository(IDbContextRepository upRepository
            )
        {
            this._upRepository = upRepository;
        }

        public async Task<List<Note>> GetAll(Int32 contactId)
        {
            var data = _upRepository.Notes.Where(p => p.ContactId == contactId).ToList();
            return data;
        }


        public MethodResult<Note> Add(Note model)
        {
            var result = new MethodResult<Note>();
            try
            {
                if (model.Id == 0)
                {
                    this._upRepository.Notes.Add(model);
                }
                else
                {

                    var dbnote = this._upRepository.Notes.Where(x => x.Id == model.Id).FirstOrDefault();
                  
                }

                this._upRepository.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            result.Result = model;
            return result;
        }



        public Note Get(Int32 id)
        {
            return this._upRepository.Notes.Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<Note> DeleteNote(Int32 Id)
        {
            var dbnote = this._upRepository.Notes.Where(p => p.Id == Id).FirstOrDefault();

            this._upRepository.Notes.Remove(dbnote);
            this._upRepository.SaveChanges();
            return dbnote;
        }
    }
}