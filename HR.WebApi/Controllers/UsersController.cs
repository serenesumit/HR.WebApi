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
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this._userService.GetAll();
        }


        [Route("{accountId:int}/users")]
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsersByAccountId(Int32 accountId)
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
            return result;
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
        public User GetUserById(Int32 id)
        {
            return this._userService.Get(id);
        }


        // POST: api/Accounts
        [ResponseType(typeof(User))]
        [HttpPost]
        public async Task<IHttpActionResult> PostUser(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = new User();
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            this._userService.Add(user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }


        [HttpPut]
        [Route("{userId:int}")]
        public async Task<HttpResponseMessage> PutUser(Int32 userId, UserDTO model)
        {
            HttpResponseMessage result = null;

            if (userId == 0 || model == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            User userModel = this._userService.Get(userId);
            if (userModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            userModel.Email = model.Email;
            userModel.FirstName = model.FirstName;
            this._userService.Add(userModel);

            result = Request.CreateResponse(HttpStatusCode.OK, userModel);
            return result;
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
            return Request.CreateResponse(HttpStatusCode.OK, userModel);
        }

    }
}
