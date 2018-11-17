using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class Account
    {
        public Int32 Id { get; set; }

        public string AccName { get; set; }

        public virtual User User { get; set;    }
    }
}