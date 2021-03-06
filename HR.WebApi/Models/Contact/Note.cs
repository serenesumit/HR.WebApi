﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class Note
    {
        [Key]
        public Int32 Id { get; set; }

        public string Title { get; set; }

        public string Desc { get; set; }

        public Int32 ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}