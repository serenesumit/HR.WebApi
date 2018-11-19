using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class Account
    {
        public Account()
        {
            this.Users = new HashSet<User>();
        }

        public Int32 Id { get; set; }

        public string AccName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}