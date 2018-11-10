using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class ContactDoc
    {
        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public virtual Contact Contact { get; set; }

        public Int32 ContactId { get; set; }
    }
}