using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class UserSetting
    {
        public Int32 Id { get; set; }

        public Int32 UserId { get; set; }

        public string Settings { get; set; }

        public virtual User User { get; set; }
    }
}