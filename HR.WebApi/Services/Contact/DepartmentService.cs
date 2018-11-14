using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories;

namespace HR.WebApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentService(IDepartmentRepository departmentRepository
            )
        {
            this._departmentRepository = departmentRepository;

        }

        public MethodResult<Department> Add(Department model)
        {
           return this._departmentRepository.Add(model);
        }

        public Task<Department> DeleteDepartment(int Id)
        {
            return this._departmentRepository.DeleteDepartment(Id);
        }

        public Department Get(int id)
        {
            return this._departmentRepository.Get(id);
        }

        public Task<List<Department>> GetAll()
        {
            return this._departmentRepository.GetAll();
        }
    }
}