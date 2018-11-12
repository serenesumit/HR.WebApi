
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
    [RoutePrefix("api/departments")]
    public class DepartmentsController : ApiController
    {

        private readonly IContactService _contactService;
        private readonly IContactDocService _contactDocService;

        public DepartmentsController(
            IContactService contactService,
             IContactDocService contactDocService
          )
        {
            this._contactService = contactService;
            this._contactDocService = contactDocService;

        }


        [Route("{id:int}/contacts")]
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetContactsByDepartmentId(Int32 id)
        {
            if (id == 0)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
                return null;
            }

            var result = await this._contactService.GetAllByDepartmentId(id);
            List<Contact> contactmodelList = new List<Contact>();
            foreach (var model in result)
            {
                Contact contactModel = new Contact();
                foreach (var dbDoc in model.ContactDocs)
                {
                    ContactDoc contactDoc = new ContactDoc();
                    contactDoc.ContactId = dbDoc.ContactId;
                    contactDoc.Name = dbDoc.Name;
                    contactDoc.Link = dbDoc.Link;
                    contactDoc.Id = dbDoc.Id;
                    contactModel.ContactDocs.Add(contactDoc);
                }

                contactModel.Title = model.Title;
                contactModel.DepartmentId = model.DepartmentId;
                contactModel.Bio = model.Bio;
                contactModel.Name = model.Name;
                contactModel.PhoneNumber = model.PhoneNumber;
                contactModel.MobileNumber = model.MobileNumber;
                contactModel.EmailAddress = model.EmailAddress;
                contactModel.QuickFacts = model.QuickFacts;
                contactModel.Website = model.Website;

                contactmodelList.Add(contactModel);
            }

            return contactmodelList;
        }
    }
}
