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

        private DbContextRepository db = new DbContextRepository();

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

        // GET: api/Accounts
        public IEnumerable<AccountModel> GetAccounts()
        {
            var result = db.Accounts;
            List<AccountModel> model = new List<AccountModel>();
            foreach (var account in result)
            {
                var accountModel = Mapper.Map<AccountModel>(account);
                model.Add(accountModel);
            }

            return model;
        }

        [Route("{userId:int}/accounts")]
        [HttpGet]
        public async Task<IEnumerable<AccountModel>> GetAccountsByUserId(Int32 userId)
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

            var result = user.Accounts.ToList();
            List<AccountModel> model = new List<AccountModel>();
            foreach (var account in result)
            {
                var accountModel = Mapper.Map<AccountModel>(account);
                model.Add(accountModel);
            }

            return model;
        }

       

        // GET: api/Accounts/5
        [ResponseType(typeof(Account))]
        public async Task<IHttpActionResult> GetAccount(int id)
        {
            Account account = this._accountService.Get(id);
            if (account == null)
            {
                return NotFound();
            }
        
            return Ok(Mapper.Map<AccountModel>(account));
        }

      

        // DELETE: api/Accounts/5
        [ResponseType(typeof(Account))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAccount(int id)
        {
            Account account = this._accountService.Get(id);
            if (account == null)
            {
                return NotFound();
            }

            await this._accountService.DeleteAccount(id);


            return Ok(Mapper.Map<AccountModel>(account));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

      
    }
}