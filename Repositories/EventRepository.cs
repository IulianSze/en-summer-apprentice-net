using Microsoft.EntityFrameworkCore;
using Practica_.net.Models;
using Practica_.net.Repositories;
using System.Runtime.CompilerServices;

namespace Practica_.net.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public EventRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }
        public async Task<IEnumerable<Event>> GetAll()
        {
            var events = _dbContext.Events.Include(e=>e.EventType).Include(e=>e.Venue);
            return events;
        }
        public async Task<Event> GetById(int id)
        {
                var @event = _dbContext.Events.Where(e => e.EventId == id).Include(e => e.EventType).Include(e => e.Venue).FirstOrDefault();

                return @event;
        }
        public async Task Update(Event @event)
        {
            /*var eventEntity = GetById(@event.EventId);
            eventEntity = @event;*/
            _dbContext.Entry(@event).State = EntityState.Modified; //il considera modificat si face automat framework-urile update-urile
            _dbContext.SaveChanges();
        }

        public async Task Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }
    }
}
