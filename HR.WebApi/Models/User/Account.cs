using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Key]
        public Int32 AccountNumber { get; set; }

        public string AccName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}