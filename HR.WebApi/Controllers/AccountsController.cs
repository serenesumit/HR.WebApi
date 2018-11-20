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
        public IQueryable<Account> GetAccounts()
        {
            return db.Accounts;
        }

        [Route("{userId:int}/accounts")]
        [HttpGet]
        public async Task<IEnumerable<Account>> GetAccountsByUserId(Int32 userId)
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
            return result;
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

            return Ok(account);
        }

        // PUT: api/Accounts/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public async Task<IHttpActionResult> PutAccount(int id, AccountDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            Account account = this._accountService.Get(id);
            if (account == null)
            {
                return NotFound();
            }
            account.AccName = model.AccName;
            this._accountService.Add(account);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Accounts
        [ResponseType(typeof(Account))]
        [HttpPost]
        public async Task<IHttpActionResult> PostAccount(AccountDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Account account = new Account();
            account.AccName = model.AccName;
            this._accountService.Add(account);

            return CreatedAtRoute("DefaultApi", new { id = account.Id }, account);
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


            return Ok(account);
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