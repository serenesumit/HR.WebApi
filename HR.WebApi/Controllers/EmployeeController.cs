
using HR.WebApi.Common;
using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HR.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {

        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeDocService _employeeResumeService;

        public EmployeeController(
            IEmployeeService employeeService,
             IEmployeeDocService employeeResumeService
          )
        {
            this._employeeService = employeeService;
            this._employeeResumeService = employeeResumeService;

        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostEmployee(EmployeeModel model)
        {

            HttpResponseMessage result = null;

            if (string.IsNullOrEmpty(model.FirstName))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Employee employee = new Employee();
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.EmployeeTypeId = model.EmployeeTypeId;
            this._employeeService.Add(employee);

            if (model.Files != null && model.Files.Count > 0)
            {

                foreach (var file in model.Files)
                {
                    //Stream stream = new MemoryStream(file.Buffer);
                    MemoryStream stream = new MemoryStream();

                    stream.Write(file.Buffer, 0, file.Buffer.Length);
                    EmployeeDoc employeeResume = new EmployeeDoc();
                    var maxId = this._employeeResumeService.GetMaxId();
                    var fileResult = await this._employeeResumeService.AddFileAsync(Constants.Azure.Containers.PageEmployeeAssets, maxId, file.FileName, stream);


                    employeeResume.Link = fileResult.FullPath;
                    employeeResume.Name = fileResult.Name;
                    employeeResume.EmployeeId = employee.Id;
                    this._employeeResumeService.Add(employeeResume);
                    employee.EmployeeResumes.Add(employeeResume);

                }

                result = Request.CreateResponse(HttpStatusCode.Created, employee);
            }

            return result;

        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var result = await this._employeeService.GetAll();
            List<Employee> model = new List<Employee>();
            foreach (var employee in result)
            {
                Employee employeeModel = new Employee();
                foreach (var resume in employee.EmployeeResumes)
                {
                    EmployeeDoc employeeResume = new EmployeeDoc();
                    employeeResume.EmployeeId = resume.EmployeeId;
                    employeeResume.Name = resume.Name;
                    employeeResume.Link = resume.Link;
                    employeeResume.Id = resume.Id;
                    employeeModel.EmployeeResumes.Add(employeeResume);
                }

                employeeModel.FirstName = employee.FirstName;
                employeeModel.LastName = employee.LastName;
                employeeModel.Id = employee.Id;
                model.Add(employeeModel);
            }

            return model;
        }

        [HttpGet]
        public Employee GetEmployee(Int32 id)
        {
            var employee = this._employeeService.Get(id);

            if (employee == null)
            {
                return null;
            }

            Employee employeeModel = new Employee();
            foreach (var resume in employee.EmployeeResumes)
            {
                EmployeeDoc employeeResume = new EmployeeDoc();
                employeeResume.EmployeeId = resume.EmployeeId;
                employeeResume.Name = resume.Name;
                employeeResume.Link = resume.Link;
                employeeResume.Id = resume.Id;
                employeeModel.EmployeeResumes.Add(employeeResume);
            }

            employeeModel.FirstName = employee.FirstName;
            employeeModel.LastName = employee.LastName;
            employeeModel.Id = employee.Id;

            return employeeModel;


        }

        [HttpPut]
        [Route("{employeeId:int}")]
        public async Task<HttpResponseMessage> PutEmployee(Int32 employeeId, EmployeeModel model)
        {
            HttpResponseMessage result = null;

            if (employeeId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Employee employee = this._employeeService.Get(employeeId);
            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            this._employeeService.Add(employee);
            Employee employeeModel = new Employee();
            if (model.Files != null && model.Files.Count > 0)
            {

                foreach (var file in model.Files)
                {
                    MemoryStream stream = new MemoryStream();
                    stream.Write(file.Buffer, 0, file.Buffer.Length);
                    EmployeeDoc employeeResume = new EmployeeDoc();
                    var maxId = this._employeeResumeService.GetMaxId();
                    var fileResult = await this._employeeResumeService.AddFileAsync(Constants.Azure.Containers.PageEmployeeAssets, maxId, file.FileName, stream);

                    employeeResume.Link = fileResult.FullPath;
                    employeeResume.Name = fileResult.Name;
                    employeeResume.EmployeeId = employee.Id;
                    this._employeeResumeService.Add(employeeResume);
                    employeeModel.EmployeeResumes.Add(employeeResume);
                }
            }

            employeeModel.FirstName = employee.FirstName;
            employeeModel.LastName = employee.LastName;
            employeeModel.Id = employee.Id;
            result = Request.CreateResponse(HttpStatusCode.OK, employeeModel);
            return result;
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteEmployee(Int32? id)
        {
            HttpResponseMessage result = null;
            if (!id.HasValue)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var employee = this._employeeService.Get(id.Value);
            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await this._employeeResumeService.DeleteDocumentsByEmployeeId(id.Value);
            await this._employeeService.DeleteEmployee(id.Value);
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        [HttpDelete]
        [Route("{employeeId:int}/employee-resume/{resumeId:int}")]
        public async Task<HttpResponseMessage> DeleteEmployeeDocument(Int32? employeeId, Int32? resumeId)
        {
            HttpResponseMessage result = null;
            if (!employeeId.HasValue || !resumeId.HasValue)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            EmployeeDoc empDoc = this._employeeResumeService.Get(resumeId.Value);
            if (empDoc == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this._employeeResumeService.DeleteEmployeeDocument(employeeId.Value, resumeId.Value);
            return Request.CreateResponse(HttpStatusCode.OK, empDoc);
        }

    }
}
