using System;
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
    public class UserSettingRepository : IUserSettingRepository
    {
        private readonly IDbContextRepository _upRepository;

        public UserSettingRepository(IDbContextRepository upRepository
            )
        {
            this._upRepository = upRepository;
        }

        public MethodResult<UserSetting> Add(UserSetting model)
        {
            var result = new MethodResult<UserSetting>();

            if (model.Id == 0)
            {
                this._upRepository.UserSettings.Add(model);
            }
            else
            {

                var dbuserSetting = this._upRepository.Events.Where(x => x.Id == model.Id).FirstOrDefault();
            }

            this._upRepository.SaveChanges();

            result.Result = model;
            return result;
        }

        public async Task<UserSetting> DeleteSetting(int Id)
        {
            var dbSetting = this._upRepository.UserSettings.Where(p => p.Id == Id).FirstOrDefault();

            this._upRepository.UserSettings.Remove(dbSetting);
            this._upRepository.SaveChanges();
            return dbSetting;
        }

        public UserSetting Get(int id)
        {
            return this._upRepository.UserSettings.Include(p => p.User).Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<List<UserSetting>> GetAll()
        {
            var data = _upRepository.UserSettings.Include(p => p.User).ToList();
            return data;
        }

        public async Task<List<UserSetting>> GetAllByUserId(int userId)
        {
            var data = _upRepository.UserSettings.Include(p => p.User).Where(p => p.UserId != null && p.UserId == userId).ToList();
            return data;
        }
    }
}