using MultipartDataMediaFormatter.Infrastructure;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Helpers.Model
{
    public class ContactModel
    {
        public Int32 Id { get; set; }

        public Int32 DepartmentId { get; set; }

        public string Title { get; set; }

        public string Bio { get; set; }

        public string Name { get; set; }

        public Int32 PhoneNumber { get; set; }

        public Int32 MobileNumber { get; set; }

        public string EmailAddress { get; set; }

        public string QuickFacts { get; set; }

        public string Website { get; set; }

        public Int32 NoteId { get; set; }

        public List<HttpFile> Files { get; set; }

        public List<ContactDoc> ContactDocs { get; set; }
    }
}