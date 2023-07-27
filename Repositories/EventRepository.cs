using Microsoft.EntityFrameworkCore;
using Practica_.net.Models;
using Practica_.net.Repositories;

namespace Practica_.net.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public EventRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }
        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events.Include(e=>e.EventType).Include(e=>e.Venue);
            return events;
        }
        public  Event GetById(int id)
        {
            var @event = _dbContext.Events.Where(e=>e.EventId == id).Include(e => e.EventType).Include(e => e.Venue).FirstOrDefault();
            /*if (@event != null)
            {
                throw new Exception("The object doesn't exist");
            }*/
            return @event;
        }
        public void Update(Event @event)
        {
            /*var eventEntity = GetById(@event.EventId);
            eventEntity = @event;*/
            _dbContext.Entry(@event).State = EntityState.Modified; //il considera modificat si face automat framework-urile update-urile
            _dbContext.SaveChanges();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }
    }
}
