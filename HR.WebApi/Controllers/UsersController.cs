using AutoMapper;
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
using System.Web.Http.Description;

namespace HR.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public UsersController(
            IUserService userService,
             IAccountService accountService
          )
        {
            this._userService = userService;
            this._accountService = accountService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var result = await this._userService.GetAll();
            List<UserModel> model = new List<UserModel>();
            foreach (var user in result)
            {
                var userModel = Mapper.Map<UserModel>(user);
                model.Add(userModel);
            }

            return model;
        }


        [Route("{accountId:int}/users")]
        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsersByAccountId(Int32 accountId)
        {
            if (accountId == 0)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
                return null;
            }
            var account = this._accountService.Get(accountId);
            if (account == null)
            {
                return null;
            }

            var result = account.Users.ToList();
            List<UserModel> model = new List<UserModel>();
            foreach(var user in result)
            {
                var userModel = Mapper.Map<UserModel>(user);
                model.Add(userModel);
            }
           
            return model;
        }


        [Route("{userId:int}/setting")]
        [HttpGet]
        public UserSetting GetSettingByUserId(Int32 userId)
        {
            if (userId == 0)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
                return null;
            }
            var user = this._userService.Get(userId);
            if (user == null)
            {
                return null;
            }

            var result = user.UserSetting;
            return result;
        }


        [HttpGet]
        [Route("{id:int}")]
        public UserModel GetUserById(Int32 id)
        {
            var user = this._userService.Get(id);
            if(user == null)
            {
                return null;
            }

            var userModel = Mapper.Map<UserModel>(user);
            return userModel;
        }


       

    
        [HttpDelete]
        [Route("{userid:int}")]
        public async Task<HttpResponseMessage> DeleteUser(Int32 userid)
        {

            HttpResponseMessage result = null;
            if (userid == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            User userModel = this._userService.Get(userid);
            if (userModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            //await this._Acc.DeleteEventDocumentsByEventId(eventid.Value);
            await this._userService.DeleteUser(userid);
            return Request.CreateResponse(HttpStatusCode.OK,  Mapper.Map<UserModel>(userModel));
        }

    }
}
