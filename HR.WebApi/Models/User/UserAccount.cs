using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class UserAccount
    {
        public Int32 Id { get; set; }

        public Int32 UserId { get; set; }

        public Int32 AccountId { get; set; }
    }
}