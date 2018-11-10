using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models
{
    public class EventDoc
    {
        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public virtual Event Event { get; set; }

        public Int32? EventId { get; set; }
    }
}