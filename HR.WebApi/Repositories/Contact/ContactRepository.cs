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
    public class ContactRepository : IContactRepository
    {
        private readonly IDbContextRepository _upRepository;

        public ContactRepository(IDbContextRepository upRepository
            )
        {
            this._upRepository = upRepository;
        }
        

        public MethodResult<Contact> Add(Contact model)
        {
            var result = new MethodResult<Contact>();
            try
            {
                if (model.Id == 0)
                {
                    this._upRepository.Contacts.Add(model);
                }
                else
                {

                    var dbcontact = this._upRepository.Contacts.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (dbcontact != null)
                    {
                        dbcontact.Title = model.Title;
                    }
                }

                this._upRepository.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            result.Result = model;
            return result;
        }

        public async Task<List<Contact>> GetAll()
        {
            var data = _upRepository.Contacts.Include(p => p.ContactDocs).Include(p => p.Notes).ToList();
            return data;
        }

        public async Task<List<Contact>> GetAllByDepartmentId(Int32 eventTypeId)
        {
            var data = _upRepository.Contacts.Include(p => p.ContactDocs).Include(p => p.Notes).Where(p => p.DepartmentId != null && p.DepartmentId == eventTypeId).ToList();
            return data;
        }

        public Contact Get(Int32 id)
        {
            return this._upRepository.Contacts.Include(p => p.ContactDocs).Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<Contact> DeleteContact(Int32 Id)
        {
            var dbcontact = this._upRepository.Contacts.Where(p => p.Id == Id).FirstOrDefault();

            this._upRepository.Contacts.Remove(dbcontact);
            this._upRepository.SaveChanges();
            return dbcontact;
        }

    }
}