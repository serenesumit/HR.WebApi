using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public interface IContactRepository
    {
        MethodResult<Contact> Add(Contact model);
        Task<List<Contact>> GetAll();
        Contact Get(Int32 id);
        Task<Contact> DeleteContact(Int32 Id);

        Task<List<Contact>> GetAllByDepartmentId(Int32 departmentId);
    }
}
