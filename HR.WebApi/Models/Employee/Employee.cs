using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Models
{
    public class Employee
    {
        public Employee()
        {
            this.EmployeeResumes = new List<EmployeeDoc>();
        }

        public Int32 Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Int32 EmployeeTypeId { get; set; }

        public virtual EmployeeType EmployeeType { get; set; }

        public virtual ICollection<EmployeeDoc> EmployeeResumes { get; set; }
    }
}
