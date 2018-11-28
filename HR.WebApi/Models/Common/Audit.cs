using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Models.Common
{
    public class Audit
    {
        [Key]
        public long Id { get; set; }

        public string TableName { get; set; }

        public string Action { get; set; }

        public DateTime UtcDatetime { get; set; }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string RecordID { get; set; }
    }
}
