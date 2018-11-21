using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HR.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/usersettings")]
    public class UserSettingsController : ApiController
    {

        private readonly IUserSettingService _userSettingService;

        public UserSettingsController(
            IUserSettingService userSettingService
          )
        {
            this._userSettingService = userSettingService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserSetting>> GetUsers()
        {
            return await this._userSettingService.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public UserSetting GetUserSettingById(Int32 id)
        {
            return this._userSettingService.Get(id);
        }






        [HttpDelete]
        [Route("{userSettingId:int}")]
        public async Task<HttpResponseMessage> DeleteUserSetting(Int32 userSettingId)
        {

            HttpResponseMessage result = null;
            if (userSettingId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            UserSetting userSettingModel = this._userSettingService.Get(userSettingId);
            if (userSettingModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            
            await this._userSettingService.DeleteSetting(userSettingId);
            return Request.CreateResponse(HttpStatusCode.OK, userSettingModel);
        }

    }
}
