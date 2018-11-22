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
        [Route("{UID:int}")]
        public UserModel GetUserById(Int32 UID)
        {
            var user = this._userService.Get(UID);
            if(user == null)
            {
                return null;
            }

            var userModel = Mapper.Map<UserModel>(user);
            return userModel;
        }

    }
}
