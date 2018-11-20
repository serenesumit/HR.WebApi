using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Helpers.Model
{
    public class UserDTO
    {
        public Int32 Id { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }
    }
}