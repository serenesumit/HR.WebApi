using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Services
{
    public interface IDepartmentService
    {
        MethodResult<Department> Add(Department model);
        Task<List<Department>> GetAll();
        Department Get(Int32 id);
        Task<Department> DeleteDepartment(Int32 Id);
    }
}
