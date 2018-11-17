using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class User
    {
        public Int32 Id { get; set; }

      //  public Int32 UID { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<UserSetting> UserSettings { get; set; } 
    }
}