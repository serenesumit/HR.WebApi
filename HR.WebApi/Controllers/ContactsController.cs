
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
    [RoutePrefix("api/contacts")]
    public class ContactsController : ApiController
    {

        private readonly IContactService _contactService;
        private readonly IContactDocService _contactDocService;
        private readonly INoteService _noteService;
        private readonly IDepartmentService _departmentService;

        public ContactsController(
            IContactService contactService,
            IContactDocService contactDocService,
             INoteService noteService,
             IDepartmentService departmentService
          )
        {
            this._contactService = contactService;
            this._contactDocService = contactDocService;
            this._noteService = noteService;
            this._departmentService = departmentService;

        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostContact(ContactDTO model)
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
            contactModel.Department = this._departmentService.Get(model.DepartmentId);

            this._contactService.Add(contactModel);


            if (model.Files != null && model.Files.Count > 0)
            {

                foreach (var file in model.Files)
                {
                    MemoryStream stream = new MemoryStream();

                    stream.Write(file.Buffer, 0, file.Buffer.Length);
                    ContactDoc contactDoc = new ContactDoc();
                    var maxid = this._contactDocService.GetMaxId();
                    var fileResult = await this._contactDocService.AddFileAsync(Constants.Azure.Containers.PageContactAssets, maxid, file.FileName, stream);

                    contactDoc.Link = fileResult.FullPath;
                    contactDoc.Name = fileResult.Name;
                    contactDoc.ContactId = contactModel.Id;
                    this._contactDocService.Add(contactDoc);
                    contactModel.ContactDocs.Add(contactDoc);

                }
            }

            result = Request.CreateResponse(HttpStatusCode.Created, GetContactModel(contactModel));

            return result;

        }

        [Route("{contactId:int}/notes")]
        [HttpGet]
        public async Task<IEnumerable<Note>> GetNotesByContactId(Int32 contactId)
        {
            if (contactId == 0)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
                return null;
            }

            var result = await this._noteService.GetAll(contactId);
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

                foreach (var dbNote in model.Notes)
                {
                    Note note = new Note();
                    note.ContactId = dbNote.ContactId;
                    note.Title = dbNote.Title;
                    note.Desc = dbNote.Desc;
                    note.Id = dbNote.Id;
                    contactModel.Notes.Add(note);
                }

                contactModel.Id = model.Id;
                contactModel.Title = model.Title;
                contactModel.DepartmentId = model.DepartmentId;
                contactModel.Bio = model.Bio;
                contactModel.Name = model.Name;
                contactModel.PhoneNumber = model.PhoneNumber;
                contactModel.MobileNumber = model.MobileNumber;
                contactModel.EmailAddress = model.EmailAddress;
                contactModel.QuickFacts = model.QuickFacts;
                contactModel.Website = model.Website;
                if (model.DepartmentId.HasValue)
                {
                    contactModel.Department = this._departmentService.Get(model.DepartmentId.Value);
                }

                contactmodelList.Add(contactModel);
            }

            return contactmodelList;
        }

        [HttpPost]
        [Route("{contactId:int}/notes")]
        public async Task<HttpResponseMessage> PostNote(Int32 contactId, NoteDTO model)
        {
            HttpResponseMessage result = null;
            Note noteModel = new Note();
            noteModel.Title = model.Title;
            noteModel.Desc = model.Desc;
            noteModel.ContactId = contactId;
            this._noteService.Add(noteModel);
            noteModel.Id = noteModel.Id;
            result = Request.CreateResponse(HttpStatusCode.Created, noteModel);

            return result;
        }

        [HttpGet]
        [Route("{contactId:int}")]
        public Contact GetContact(Int32 contactId)
        {
            var model = this._contactService.Get(contactId);
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
            contactModel.Website = model.Website;
            contactModel.Id = model.Id;
            if (model.DepartmentId.HasValue)
            {
                contactModel.Department = this._departmentService.Get(model.DepartmentId.Value);
            }

            foreach (var dbDoc in model.ContactDocs)
            {
                ContactDoc eventDoc = new ContactDoc();
                eventDoc.ContactId = dbDoc.ContactId;
                eventDoc.Name = dbDoc.Name;
                eventDoc.Link = dbDoc.Link;
                eventDoc.Id = dbDoc.Id;
                contactModel.ContactDocs.Add(eventDoc);
            }

            foreach (var dbNote in model.Notes)
            {
                Note note = new Note();
                note.ContactId = dbNote.ContactId;
                note.Title = dbNote.Title;
                note.Desc = dbNote.Desc;
                note.Id = dbNote.Id;
                contactModel.Notes.Add(note);
            }


            return contactModel;
        }

        [HttpPut]
        [Route("{contactId:int}")]
        public async Task<HttpResponseMessage> PutContact(Int32 contactId, ContactDTO model)
        {
            HttpResponseMessage result = null;

            if (contactId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            Contact returnModel = new Contact();
            Contact contactModel = this._contactService.Get(contactId);
            if (contactModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
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
                    var fileResult = await this._contactDocService.AddFileAsync(Constants.Azure.Containers.PageContactAssets, maxid, file.FileName, stream);

                    contactDoc.Link = fileResult.FullPath;
                    contactDoc.Name = fileResult.Name;
                    contactDoc.ContactId = contactModel.Id;
                    this._contactDocService.Add(contactDoc);
                    returnModel.ContactDocs.Add(contactDoc);
                }


            }

            returnModel.Title = model.Title;
            returnModel.DepartmentId = model.DepartmentId;
            returnModel.Bio = model.Bio;
            returnModel.Name = model.Name;
            returnModel.PhoneNumber = model.PhoneNumber;
            returnModel.MobileNumber = model.MobileNumber;
            returnModel.EmailAddress = model.EmailAddress;
            returnModel.QuickFacts = model.QuickFacts;
            returnModel.Website = model.Website;
            returnModel.Website = model.Website;
            returnModel.Id = model.Id;
            result = Request.CreateResponse(HttpStatusCode.OK, returnModel);

            return result;
        }

        [HttpPut]
        [Route("{contactId:int}/notes/{noteId}")]
        public async Task<HttpResponseMessage> PutNote(Int32 contactId, Int32 noteId, NoteDTO model)
        {
            HttpResponseMessage result = null;

            if (noteId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Note dbNote = this._noteService.Get(noteId);
            if (dbNote == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            dbNote.Title = model.Title;
            dbNote.Desc = model.Desc;

            NoteDTO noteModel = new NoteDTO();
            noteModel.Title = model.Title;
            noteModel.Desc = model.Desc;
            noteModel.ContactId = model.ContactId;
            this._noteService.Add(dbNote);
            noteModel.Id = dbNote.Id;
            
            result = Request.CreateResponse(HttpStatusCode.OK, noteModel);

            return result;
        }

        [HttpDelete]
        [Route("{contactId:int}")]
        public async Task<HttpResponseMessage> DeleteContact(Int32? contactId = 0)
        {
            HttpResponseMessage result = null;
            if (contactId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Contact contactModel = this._contactService.Get(contactId.Value);
            if (contactModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            await this._contactDocService.DeleteContactDocumentsByContactId(contactId.Value);
            await this._contactService.DeleteContact(contactId.Value);
            return Request.CreateResponse(HttpStatusCode.OK, contactModel);
        }

        [HttpDelete]
        [Route("{contactId:int}/notes/{noteId:int}")]
        public async Task<HttpResponseMessage> DeleteNoteById(Int32 contactId, Int32 noteId = 0)
        {
            HttpResponseMessage result = null;
            if (noteId == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Note note = this._noteService.Get(noteId);
            if (note == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            await this._noteService.DeleteNote(noteId);
            return Request.CreateResponse(HttpStatusCode.OK, note);
        }

        [HttpDelete]
        [Route("{contactId:int}/contactDocs/{contactdocId:int}")]
        public async Task<HttpResponseMessage> DeleteContactDocument(Int32 contactId, Int32? contactdocId)
        {
            HttpResponseMessage result = null;
            if (contactId == 0 || contactdocId == 0 || contactdocId == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            ContactDoc contactDoc = this._contactDocService.Get(contactdocId.Value);
            if (contactDoc == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this._contactDocService.DeleteContactDocument(contactId, contactdocId.Value);
            return Request.CreateResponse(HttpStatusCode.OK, contactDoc);
        }

        private Contact GetContactModel(Contact model)
        {
            Contact returnModel = new Contact();
            foreach (var dbNote in model.Notes)
            {
                Note note = new Note();
                note.ContactId = dbNote.ContactId;
                note.Title = dbNote.Title;
                note.Desc = dbNote.Desc;
                note.Id = dbNote.Id;
                returnModel.Notes.Add(note);
            }

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
            returnModel.DepartmentId = model.DepartmentId;
            returnModel.Bio = model.Bio;
            returnModel.Name = model.Name;
            returnModel.PhoneNumber = model.PhoneNumber;
            returnModel.MobileNumber = model.MobileNumber;
            returnModel.EmailAddress = model.EmailAddress;
            returnModel.QuickFacts = model.QuickFacts;
            returnModel.Website = model.Website;
            returnModel.Website = model.Website;
            returnModel.Id = model.Id;

            return returnModel;
        }

    }
}
