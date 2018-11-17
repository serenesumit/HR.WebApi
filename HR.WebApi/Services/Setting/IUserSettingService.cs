using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Services
{
    public interface IUserSettingService
    {
        MethodResult<UserSetting> Add(UserSetting model);
        Task<List<UserSetting>> GetAll();
        UserSetting Get(Int32 id);
        Task<UserSetting> DeleteSetting(Int32 Id);
        Task<List<UserSetting>> GetAllByUserId(Int32 userId);
    }
}
