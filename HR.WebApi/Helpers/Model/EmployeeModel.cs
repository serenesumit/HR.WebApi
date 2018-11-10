using HR.WebApi.Models;
using MultipartDataMediaFormatter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Helpers.Model
{
    public class EmployeeModel
    {
        public EmployeeModel()
        {
            this.EmployeeResumes = new List<EmployeeDoc>();
        }

        public Int32? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<HttpFile> Files { get; set; }

        public List<EmployeeDoc> EmployeeResumes { get; set; }
    }
}