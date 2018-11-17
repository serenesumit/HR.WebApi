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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository
           )
        {
            this._accountRepository = accountRepository;
        }

        public MethodResult<Account> Add(Account model)
        {
            return this._accountRepository.Add(model);
        }

        public Task<Account> DeleteAccount(int Id)
        {
            return this._accountRepository.DeleteAccount(Id);
        }

        public Account Get(int id)
        {
            return this._accountRepository.Get(id);
        }

        public  Task<List<Account>> GetAll()
        {
            return this._accountRepository.GetAll();
        }
    }
}