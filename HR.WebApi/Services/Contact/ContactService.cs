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
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository
           )
        {
            this._contactRepository = contactRepository;
        }

        public MethodResult<Contact> Add(Contact model)
        {
            return this._contactRepository.Add(model);
        }

        public Task<Contact> DeleteContact(int Id)
        {
            return this._contactRepository.DeleteContact(Id);
        }

        public Contact Get(int id)
        {
            return this._contactRepository.Get(id);
        }

        public Task<List<Contact>> GetAll()
        {
            return this._contactRepository.GetAll();
        }

        public Task<List<Contact>> GetAllByDepartmentId(int departmentId)
        {
            return this._contactRepository.GetAllByDepartmentId(departmentId);
        }
    }
}