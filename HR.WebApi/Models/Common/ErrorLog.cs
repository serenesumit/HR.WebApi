using System;
using System.ComponentModel.DataAnnotations;

namespace HR.WebApi.Models.Common
{
    public class ErrorLog
    {
        [Key]
        public long Id { get; set; }

        public string ControllerName { get; set; }

        public string MethodName { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorType { get; set; }

        public string VerbAttribute { get; set; }

        public Int32 UserId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}