using ApiMultiPartFormData.Models;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.WebApi.Helpers.Model
{
    public class EventModel
    {
        public EventModel()
        {
            this.EventDocs = new List<EventDoc>();
        }

        public Int32 Id { get; set; }

        public Int32? EventTypeId { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Website { get; set; }

        public string Schedule { get; set; }

        public string Agenda { get; set; }

        public List<HttpFile> Files { get; set; }

        public List<EventDoc> EventDocs { get; set; }
    }
}