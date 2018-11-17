using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Services
{
    public interface IAccountService
    {
        MethodResult<Account> Add(Account model);
        Task<List<Account>> GetAll();
        Account Get(Int32 id);
        Task<Account> DeleteAccount(Int32 Id);
    }
}
