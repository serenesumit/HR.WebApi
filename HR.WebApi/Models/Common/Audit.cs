using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Models.Common
{
    public class Audit
    {
        public long Id { get; set; }

        public string TableName { get; set; }

        public string Action { get; set; }

        public DateTime UtcDatetime { get; set; }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }
    }
}
