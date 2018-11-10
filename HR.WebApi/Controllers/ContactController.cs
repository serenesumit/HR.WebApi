
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
    [RoutePrefix("api/events")]
    public class ContactController : ApiController
    {

        private readonly IContactService _contactService;
        private readonly IContactDocService _contactDocService;

        public ContactController(
            IContactService contactService,
             IContactDocService contactDocService
          )
        {
            this._contactService = contactService;
            this._contactDocService = contactDocService;

        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostContact(ContactModel model)
        {

            HttpResponseMessage result = null;

            Contact contactModel = new Contact();
            contactModel.Title = model.Title;
            contactModel.DepartmentId = model.DepartmentId;
            contactModel.Bio = model.Bio;
            contactModel.Name = model.Name;
            contactModel.PhoneNumber = model.PhoneNumber;
            contactModel.MobileNumber = model.MobileNumber;
            contactModel.EmailAddress = model.EmailAddress;
            contactModel.QuickFacts = model.QuickFacts;
            contactModel.Website = model.Website;

            this._contactService.Add(contactModel);

            if (model.Files != null && model.Files.Count > 0)
            {

                foreach (var file in model.Files)
                {
                    MemoryStream stream = new MemoryStream();

                    stream.Write(file.Buffer, 0, file.Buffer.Length);
                    ContactDoc contactDoc = new ContactDoc();
                    var maxid = this._contactDocService.GetMaxId();
                    var fileResult = await this._contactDocService.AddFileAsync(Constants.Azure.Containers.PageAssets, maxid, file.Name, stream);

                    contactDoc.Link = fileResult.FullPath;
                    contactDoc.Name = fileResult.Name;
                    contactDoc.ContactId = contactModel.Id;
                    this._contactDocService.Add(contactDoc);

                }

                result = Request.CreateResponse(HttpStatusCode.Created, contactModel);
            }

            return result;

        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var result = await this._contactService.GetAll();
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
                contactModel.Website = model.Website;

                contactmodelList.Add(contactModel);
            }

            return contactmodelList;
        }

        [HttpGet]
        [Route("{contactId:int}")]
        public Contact GetContact(Int32 contactId)
        {
            return this._contactService.Get(contactId);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutContact(ContactModel model)
        {
            HttpResponseMessage result = null;

            if (model.Id == 0)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Contact contactModel = this._contactService.Get(model.Id);
            if (contactModel == null)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
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
            contactModel.Website = model.Website;
            this._contactService.Add(contactModel);

            if (model.Files != null && model.Files.Count > 0)
            {

                foreach (var file in model.Files)
                {
                    MemoryStream stream = new MemoryStream();
                    stream.Write(file.Buffer, 0, file.Buffer.Length);
                    ContactDoc contactDoc = new ContactDoc();
                    var maxid = this._contactDocService.GetMaxId();
                    var fileResult = await this._contactDocService.AddFileAsync(Constants.Azure.Containers.PageAssets, maxid, file.Name, stream);

                    contactDoc.Link = fileResult.FullPath;
                    contactDoc.Name = fileResult.Name;
                    contactDoc.ContactId = contactModel.Id;
                    this._contactDocService.Add(contactDoc);
                }

                Contact returnModel = new Contact();
                foreach (var dbDoc in model.ContactDocs)
                {
                    ContactDoc eventDoc = new ContactDoc();
                    eventDoc.ContactId = dbDoc.ContactId;
                    eventDoc.Name = dbDoc.Name;
                    eventDoc.Link = dbDoc.Link;
                    eventDoc.Id = dbDoc.Id;
                    returnModel.ContactDocs.Add(eventDoc);
                }

                returnModel.Title = model.Title;


                result = Request.CreateResponse(HttpStatusCode.Created, returnModel);
            }

            return result;
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteContact(Int32? eventid = 0)
        {
            HttpResponseMessage result = null;
            if (eventid == 0)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            await this._contactDocService.DeleteContactDocumentsByContactId(eventid.Value);
            await this._contactService.DeleteContact(eventid.Value);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{contactId:int}/contactDocs/{contactdocId:int}")]
        public async Task<HttpResponseMessage> DeleteContactDocument(Int32 contactId, Int32? contactdocId)
        {
            HttpResponseMessage result = null;
            if (contactId == 0 || contactdocId == 0)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this._contactDocService.DeleteContactDocument(contactId, contactdocId.Value);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
