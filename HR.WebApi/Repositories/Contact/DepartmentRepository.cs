using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HR.WebApi.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDbContextRepository _upRepository;

        public DepartmentRepository(IDbContextRepository upRepository
            )
        {
            this._upRepository = upRepository;
        }


        public MethodResult<Department> Add(Department model)
        {
            var result = new MethodResult<Department>();
            try
            {
                if (model.Id == 0)
                {
                    this._upRepository.Departments.Add(model);
                }
                else
                {

                    var dbDepartment = this._upRepository.Departments.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (dbDepartment != null)
                    {
                        dbDepartment.Name = model.Name;
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

        public async Task<List<Department>> GetAll()
        {
            var data = _upRepository.Departments.ToList();
            return data;
        }

    

        public Department Get(Int32 id)
        {
            return this._upRepository.Departments.Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<Department> DeleteDepartment(Int32 Id)
        {
            var dbDepartment = this._upRepository.Departments.Where(p => p.Id == Id).FirstOrDefault();

            this._upRepository.Departments.Remove(dbDepartment);
            this._upRepository.SaveChanges();
            return dbDepartment;
        }

    }
}