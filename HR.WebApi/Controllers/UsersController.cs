using AutoMapper;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Services;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

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
            if (user == null)
            {
                return null;
            }

           // throw new System.ArgumentException("Parameter cannot be null", "original");

            var userModel = Mapper.Map<UserModel>(user);

            return userModel;
        }

    }
}
