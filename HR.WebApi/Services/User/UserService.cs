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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository
           )
        {
            this._userRepository = userRepository;
        }

        public MethodResult<User> Add(User model)
        {
            return this._userRepository.Add(model);
        }

        public Task<User> DeleteUser(int Id)
        {
            return this._userRepository.DeleteUser(Id);
        }

        public User Get(int id)
        {
            return this._userRepository.Get(id);
        }

        public Task<List<User>> GetAll()
        {
            return this._userRepository.GetAll();
        }
    }
}