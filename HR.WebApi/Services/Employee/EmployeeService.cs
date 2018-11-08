
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

namespace HR.WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbContextRepository _upRepository;
        private readonly IEmployeeDocService _employeeDocService;


        public EmployeeService(IDbContextRepository upRepository,
            IEmployeeDocService employeeDocService
            )
        {
            this._upRepository = upRepository;
            this._employeeDocService = employeeDocService;
           
        }

        public virtual MethodResult<Employee> Add(Employee model)
        {
            var result = new MethodResult<Employee>();
            try
            {
                if (!model.Id.HasValue)
                {
                    model.Id = Guid.NewGuid();
                    this._upRepository.Employees.Add(model);
                }
                else
                {
                    Employee employee = this._upRepository.Employees.Where(x => x.Id == model.Id.Value).FirstOrDefault();
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

        public async Task<List<Employee>> GetAll()
        {
            var data = this._upRepository.Employees.Include(p => p.EmployeeResumes).ToList();
            return data;
        }

        public Employee Get(Guid id)
        {
            return this._upRepository.Employees.Include(p => p.EmployeeResumes).Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<Employee> DeleteEmployee(Guid Id)
        {
            var employeeResumes = this._upRepository.EmployeeDocs.Where(p => p.EmployeeId == Id).ToList();
            foreach (var resume in employeeResumes)
            {
                try
                {
                    var isFileDeleted = await this._employeeDocService.DeleteFileAsync(resume.Name);
                    if (isFileDeleted)
                    {
                        this._upRepository.EmployeeDocs.Remove(resume);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            var employee = this._upRepository.Employees.Where(p => p.Id == Id).FirstOrDefault();
            this._upRepository.Employees.Remove(employee);
            this._upRepository.SaveChanges();
            return employee;
        }

      

    }
}




