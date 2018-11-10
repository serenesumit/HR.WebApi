using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Models
{
    public class EmployeeDoc
    {
        public Int32 Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public virtual Employee Employee { get; set; }
        
        public Int32 EmployeeId { get; set; }

    }
}
