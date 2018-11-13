using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public interface IEmployeeRepository
    {
        MethodResult<Employee> Add(Employee model);
        Task<List<Employee>> GetAll();
        Employee Get(Int32 id);
        Task<Employee> DeleteEmployee(Int32 Id);
    }
}
