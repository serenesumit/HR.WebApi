using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HR.WebApi.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;


        public EventService(IEventRepository eventRepository
            )
        {
            this._eventRepository = eventRepository;
        }

        public MethodResult<Event> Add(Event model)
        {
            return this._eventRepository.Add(model);
        }

        public Task<Event> DeleteEvent(int Id)
        {
            return this._eventRepository.DeleteEvent(Id);
        }

        public Event Get(int id)
        {
            return this._eventRepository.Get(id);
        }

        public Task<List<Event>> GetAll()
        {
            return this._eventRepository.GetAll();
        }

        public Task<List<Event>> GetAllByEventTypeId(Int32 eventTypeId)
        {
            return this._eventRepository.GetAllByEventTypeId(eventTypeId);
        }

        public Task<List<Event>> GetAllByMonth(int month, int year)
        {
            return this._eventRepository.GetAllByMonth(month, year);
        }
    }
}