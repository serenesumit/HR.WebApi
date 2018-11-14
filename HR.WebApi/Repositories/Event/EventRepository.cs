using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using HR.WebApi.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HR.WebApi.Repositories
{

    public class EventRepository : IEventRepository
    {
        private readonly IDbContextRepository _upRepository;

        public EventRepository(IDbContextRepository upRepository
            )
        {
            this._upRepository = upRepository;
        }




        public MethodResult<Event> Add(Event model)
        {
            var result = new MethodResult<Event>();

            if (model.Id == 0)
            {
                this._upRepository.Events.Add(model);
            }
            else
            {

                var dbevent = this._upRepository.Events.Where(x => x.Id == model.Id).FirstOrDefault();
                if (dbevent != null)
                {
                    dbevent.Title = model.Title;
                }
            }

            this._upRepository.SaveChanges();

            result.Result = model;
            return result;
        }

        public async Task<List<Event>> GetAll()
        {
            var data = _upRepository.Events.Include(p => p.EventDocs).ToList();
            return data;
        }

        public async Task<List<Event>> GetAllByEventTypeId(Int32 eventTypeId)
        {
            var data = _upRepository.Events.Include(p => p.EventDocs).Where(p => p.EventTypeId != null && p.EventTypeId == eventTypeId).ToList();
            return data;
        }

        public Event Get(Int32 id)
        {
            return this._upRepository.Events.Include(p => p.EventDocs).Where(p => p.Id == id).FirstOrDefault();
        }

        public async Task<List<Event>> GetAllByMonth(Int32 month, Int32 year)
        {
            return this._upRepository.Events.Include(p => p.EventDocs).Where(p => p.StartDate != null && p.StartDate.Month == month && p.StartDate.Year == year).ToList();
        }

        public async Task<Event> DeleteEvent(Int32 Id)
        {
            var dbEvent = this._upRepository.Events.Where(p => p.Id == Id).FirstOrDefault();

            this._upRepository.Events.Remove(dbEvent);
            this._upRepository.SaveChanges();
            return dbEvent;
        }

    }

}