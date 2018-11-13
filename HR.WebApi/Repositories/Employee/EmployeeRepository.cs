using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;

namespace HR.WebApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly IDbContextRepository _upRepository;

        public EmployeeRepository(IDbContextRepository upRepository
            )
        {
            this._upRepository = upRepository;
        }



        public MethodResult<Employee> Add(Employee model)
        {
            var result = new MethodResult<Employee>();
            try
            {
                if (model.Id == 0)
                {
                    this._upRepository.Employees.Add(model);
                }
                else
                {
                    Employee employee = this._upRepository.Employees.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (employee != null)
                    {
                        employee.FirstName = model.FirstName;
                        employee.LastName = model.LastName;
                    }
                }

                this._upRepository.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            result.Result = model;
            return result;
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var employee = this._upRepository.Employees.Where(p => p.Id == Id).FirstOrDefault();
            this._upRepository.Employees.Remove(employee);
            this._upRepository.SaveChanges();
            return employee;
        }

        public Employee Get(int id)
        {
            return this._upRepository.Employees.Include(p => p.EmployeeResumes).Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<List<Employee>> GetAll()
        {
            var data = this._upRepository.Employees.Include(p => p.EmployeeResumes).ToList();
            return data;
        }
    }
}