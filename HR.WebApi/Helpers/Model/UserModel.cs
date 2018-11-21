using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Helpers.Model
{
    public class UserModel
    {
        public UserModel()
        {
            this.Accounts = new List<AccountModel>();
        }

        public Int32 Id { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public List<AccountModel> Accounts { get; set; }
    }
}