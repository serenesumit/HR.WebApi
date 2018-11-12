
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
    [RoutePrefix("api/eventtype")]
    public class EventTypeController : ApiController
    {

        private readonly IEventService _eventService;
        private readonly IEventDocService _eventDocService;

        public EventTypeController(
            IEventService eventService,
             IEventDocService eventDocService
          )
        {
            this._eventService = eventService;
            this._eventDocService = eventDocService;

        }


        [Route("{eventTypeId:int}/events")]
        [HttpGet]
        public async Task<IEnumerable<Event>> GetEventsByEventTypeId(Int32 eventTypeId)
        {
            if (eventTypeId == 0)
            {
                Request.CreateResponse(HttpStatusCode.BadRequest);
                return null;
            }

            var result = await this._eventService.GetAllByEventTypeId(eventTypeId);
            List<Event> eventmodelList = new List<Event>();
            foreach (var model in result)
            {
                Event eventModel = new Event();
                foreach (var dbDoc in model.EventDocs)
                {
                    EventDoc eventDoc = new EventDoc();
                    eventDoc.EventId = dbDoc.EventId;
                    eventDoc.Name = dbDoc.Name;
                    eventDoc.Link = dbDoc.Link;
                    eventDoc.Id = dbDoc.Id;
                    eventModel.EventDocs.Add(eventDoc);
                }

                eventModel.Title = model.Title;
                eventModel.EventTypeId = model.EventTypeId;
                eventModel.Location = model.Location;
                eventModel.City = model.City;
                eventModel.State = model.State;
                eventModel.Zip = model.Zip;
                eventModel.StartDate = model.StartDate;
                eventModel.EndDate = model.EndDate;
                eventModel.Website = model.Website;
                eventModel.Schedule = model.Schedule;
                eventModel.Agenda = model.Agenda;

                eventmodelList.Add(eventModel);
            }

            return eventmodelList;
        }
    }
}
