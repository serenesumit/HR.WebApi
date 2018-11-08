using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Models
{
    public class EmployeeDoc 
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public virtual Employee Employee { get; set; }

        public Guid EmployeeId { get; set; }
      
    }
}
