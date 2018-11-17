using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Services
{
    public interface IUserService
    {
        MethodResult<User> Add(User model);
        Task<List<User>> GetAll();
        User Get(Int32 id);
        Task<User> DeleteUser(Int32 Id);
    }
}
