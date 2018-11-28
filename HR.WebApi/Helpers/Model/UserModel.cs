using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Helpers.Model
{
    public class UserModel
    {
        public Int32 UId { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public UserSettingDTO UserSetting { get; set; }
    }
}