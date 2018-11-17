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
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextRepository _upRepository;

        public UserRepository(IDbContextRepository upRepository
            )
        {
            this._upRepository = upRepository;
        }

        public MethodResult<User> Add(User model)
        {
            var result = new MethodResult<User>();

            if (model.Id == 0)
            {
                this._upRepository.Users.Add(model);
            }
            else
            {

                var dbUser = this._upRepository.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            }

            this._upRepository.SaveChanges();

            result.Result = model;
            return result;
        }

        public async Task<User> DeleteUser(int Id)
        {
            var dbUser = this._upRepository.Users.Where(p => p.Id == Id).FirstOrDefault();

            this._upRepository.Users.Remove(dbUser);
            this._upRepository.SaveChanges();
            return dbUser;
        }

        public User Get(int id)
        {
            return this._upRepository.Users.Include(p => p.Accounts).Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<List<User>> GetAll()
        {
            return this._upRepository.Users.Include(p => p.Accounts).ToList();
        }
    }
}