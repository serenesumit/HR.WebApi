﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;

namespace HR.WebApi.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly IDbContextRepository _upRepository;

        public AccountRepository(IDbContextRepository upRepository
            )
        {
            this._upRepository = upRepository;
        }


        public MethodResult<Account> Add(Account model)
        {
            var result = new MethodResult<Account>();

            if (model.Id == 0)
            {
                this._upRepository.Accounts.Add(model);
            }
            else
            {

                var dbuserSetting = this._upRepository.Accounts.Where(x => x.Id == model.Id).FirstOrDefault();
            }

            this._upRepository.SaveChanges();

            result.Result = model;
            return result;
        }

        public async Task<Account> DeleteAccount(int Id)
        {
            var dbAccount = this._upRepository.Accounts.Where(p => p.Id == Id).FirstOrDefault();

            this._upRepository.Accounts.Remove(dbAccount);
            this._upRepository.SaveChanges();
            return dbAccount;
        }

        public Account Get(int id)
        {
            return this._upRepository.Accounts.Include(p => p.User).Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<List<Account>> GetAll()
        {
            var data = _upRepository.Accounts.Include(p => p.User).ToList();
            return data;
        }

      
    }
}