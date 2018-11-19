using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class UserSetting
    {
        [ForeignKey("User")]
        public Int32 Id { get; set; }

        public string Settings { get; set; }

        public virtual User User { get; set; }
    }
}