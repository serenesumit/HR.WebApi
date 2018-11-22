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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using AutoMapper;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;
using HR.WebApi.Services;

namespace HR.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/accounts")]
    public class AccountsController : ApiController
    {

      
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountsController(
             IAccountService accountService,
             IUserService userService
          )
        {
            this._accountService = accountService;
            this._userService = userService;
        }

 

        [Route("{userId:int}/{searchTerm}")]
        [HttpGet]
        public async Task<IEnumerable<AccountModel>> GetAccountsByUserId(Int32 UID,string searchTerm)
        {
            if (UID == 0)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
                return null;
            }
            var user = this._userService.Get(UID);
            if (user == null)
            {
                return null;
            }

            var result = user.Accounts.Where(p=>p.AccName !=null && p.AccName.Contains(searchTerm)).ToList();
            List<AccountModel> model = new List<AccountModel>();
            foreach (var account in result)
            {
                var accountModel = Mapper.Map<AccountModel>(account);
                model.Add(accountModel);
            }

            return model;
        }

       
    }
}