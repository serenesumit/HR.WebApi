using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;
using HR.WebApi.Services;

namespace HR.WebApi.Controllers
{
    public class UserSettingsController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IUserSettingService _userSettingService;
        public UserSettingsController(
           IUserService userService,
            IAccountService accountService,
            IUserSettingService userSettingService
         )
        {
            this._userService = userService;
            this._accountService = accountService;
            this._userSettingService = userSettingService;
        }


        // PUT: api/UserSettings/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("{userId:int}")]
        public async Task<IHttpActionResult> PutUserSetting(int userId, UserSettingDTO userSetting)
        {
            var user = this._userService.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (user.UserSetting == null)
            {
                return NotFound();
            }

            user.UserSetting.Settings = userSetting.Settings;
            this._userSettingService.Add(user.UserSetting);
            this._userService.Add(user);
            var userSettingModel = Mapper.Map<UserSettingDTO>(user.UserSetting);
            return Ok(userSettingModel);
        }

        // POST: api/UserSettings
        [ResponseType(typeof(UserSetting))]
        [HttpPost]
        [Route("{userId:int}")]
        public async Task<IHttpActionResult> PostUserSetting(int userId, UserSettingDTO userSetting)
        {
            var user = this._userService.Get(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (user.UserSetting != null)
            {
                return BadRequest();
            }
            user.UserSetting = new UserSetting();
            user.UserSetting.Settings = userSetting.Settings;
            user.UserSetting.User = user;
            this._userService.Add(user);
            var userSettingModel = Mapper.Map<UserSettingDTO>(user.UserSetting);
            return Ok(userSettingModel);

        }


    }
}