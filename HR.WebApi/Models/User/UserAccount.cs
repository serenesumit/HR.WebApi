using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class UserAccount
    {
        [Key]
        [Column(Order = 1)]
        public Int32 UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Int32 AccountId { get; set; }
    }
}