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
    public class UserSettingService : IUserSettingService
    {
        private readonly IUserSettingRepository _userSettingRepository;

        public UserSettingService(IUserSettingRepository userSettingRepository
           )
        {
            this._userSettingRepository = userSettingRepository;
        }

        public MethodResult<UserSetting> Add(UserSetting model)
        {
            return this._userSettingRepository.Add(model);
        }

        public Task<UserSetting> DeleteSetting(int Id)
        {
            return this._userSettingRepository.DeleteSetting(Id);
        }

        public UserSetting Get(int Id)
        {
            return this._userSettingRepository.Get(Id);
        }

        public Task<List<UserSetting>> GetAll()
        {
            return this._userSettingRepository.GetAll();
        }

        public Task<List<UserSetting>> GetAllByUserId(int userId)
        {
            return this._userSettingRepository.GetAllByUserId(userId);
        }
    }
}