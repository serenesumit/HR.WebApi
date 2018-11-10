
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
    public class EventController : ApiController
    {

        private readonly IEventService _eventService;
        private readonly IEventDocService _eventDocService;

        public EventController(
            IEventService eventService,
             IEventDocService eventDocService
          )
        {
            this._eventService = eventService;
            this._eventDocService = eventDocService;

        }

        [HttpPost]
        public async Task<HttpResponseMessage> PostEvent(EventModel model)
        {

            HttpResponseMessage result = null;

            Event eventModel = new Event();
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
            this._eventService.Add(eventModel);

            if (model.Files != null && model.Files.Count > 0)
            {

                foreach (var file in model.Files)
                {
                    MemoryStream stream = new MemoryStream();

                    stream.Write(file.Buffer, 0, file.Buffer.Length);
                    EventDoc eventDoc = new EventDoc();
                    var maxid = this._eventDocService.GetMaxId();
                    var fileResult = await this._eventDocService.AddFileAsync(Constants.Azure.Containers.PageEventAssets, maxid, file.Name, stream);

                    eventDoc.Link = fileResult.FullPath;
                    eventDoc.Name = fileResult.Name;
                    eventDoc.EventId = eventModel.Id;
                    this._eventDocService.Add(eventDoc);

                }

                result = Request.CreateResponse(HttpStatusCode.Created, eventModel);
            }

            return result;

        }

        [HttpGet]
        public async Task<IEnumerable<Event>> GetEvents()
        {
            var result = await this._eventService.GetAll();
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

        [HttpGet]
        [Route("{eventId:int}")]
        public Event GetEvent(Int32 eventId)
        {
            return this._eventService.Get(eventId);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> PutEvent(EventModel model)
        {
            HttpResponseMessage result = null;

            if (model.Id == 0)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Event eventModel = this._eventService.Get(model.Id);
            if (eventModel == null)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
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
            this._eventService.Add(eventModel);

            if (model.Files != null && model.Files.Count > 0)
            {

                foreach (var file in model.Files)
                {
                    MemoryStream stream = new MemoryStream();
                    stream.Write(file.Buffer, 0, file.Buffer.Length);
                    EventDoc eventDoc = new EventDoc();
                    var maxid = this._eventDocService.GetMaxId();
                    var fileResult = await this._eventDocService.AddFileAsync(Constants.Azure.Containers.PageEventAssets, maxid, file.Name, stream);

                    eventDoc.Link = fileResult.FullPath;
                    eventDoc.Name = fileResult.Name;
                    eventDoc.EventId = eventModel.Id;
                    this._eventDocService.Add(eventDoc);
                }

                Event returnModel = new Event();
                foreach (var dbDoc in model.EventDocs)
                {
                    EventDoc eventDoc = new EventDoc();
                    eventDoc.EventId = dbDoc.EventId;
                    eventDoc.Name = dbDoc.Name;
                    eventDoc.Link = dbDoc.Link;
                    eventDoc.Id = dbDoc.Id;
                    returnModel.EventDocs.Add(eventDoc);
                }

                returnModel.Title = model.Title;
                returnModel.EventTypeId = model.EventTypeId;
                returnModel.Location = model.Location;
                returnModel.City = model.City;
                returnModel.State = model.State;
                returnModel.Zip = model.Zip;
                returnModel.StartDate = model.StartDate;
                returnModel.EndDate = model.EndDate;
                returnModel.Website = model.Website;
                returnModel.Schedule = model.Schedule;
                returnModel.Agenda = model.Agenda;

                result = Request.CreateResponse(HttpStatusCode.Created, returnModel);
            }

            return result;
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteEvent(Int32? eventid = 0)
        {
            HttpResponseMessage result = null;
            if (eventid == 0)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            await this._eventDocService.DeleteEventDocumentsByEventId(eventid.Value);
            await this._eventService.DeleteEvent(eventid.Value);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{eventId:int}/eventsDocs/{eventdocId:int}")]
        public async Task<HttpResponseMessage> DeleteEvent(Int32 eventId, Int32? eventdocId)
        {
            HttpResponseMessage result = null;
            if (eventId == 0 || eventdocId == 0)
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            this._eventDocService.DeleteEventDocument(eventId, eventdocId.Value);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
