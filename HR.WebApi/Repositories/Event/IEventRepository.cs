using HR.WebApi.Helpers.Model;
using HR.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi.Repositories
{
    public interface IEventRepository
    {
        MethodResult<Event> Add(Event model);
        Task<List<Event>> GetAll();
        Event Get(Int32 id);
        Task<Event> DeleteEvent(Int32 Id);
        Task<List<Event>> GetAllByMonth(Int32 month, Int32 year);
        Task<List<Event>> GetAllByEventTypeId(Int32 eventTypeId);
    }
}
