using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class User
    {
        public User()
        {
            this.Accounts = new HashSet<Account>();
        }

        public Int32 Id { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual UserSetting UserSetting { get; set; } 
    }
}