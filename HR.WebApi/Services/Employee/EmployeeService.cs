
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using HR.WebApi.Repositories.Common;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Common;
using HR.WebApi.Repositories;

namespace HR.WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
       

        public EmployeeService(IEmployeeRepository employeeRepository
            )
        {
            this._employeeRepository = employeeRepository;
        
        }

        public virtual MethodResult<Employee> Add(Employee model)
        {
            return this._employeeRepository.Add(model);
        }

        public async Task<List<Employee>> GetAll()
        {
            return await this._employeeRepository.GetAll();
        }

        public Employee Get(Int32 id)
        {
            return this._employeeRepository.Get(id);
        }

        public async Task<Employee> DeleteEmployee(Int32 Id)
        {
            return await this._employeeRepository.DeleteEmployee(Id);
        }
    }
}




