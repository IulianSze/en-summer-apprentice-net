using Practica_.net.Models;
using System.Xml.Serialization;

namespace Practica_.net.Repositories
{
    public interface IEventRepository
    {
       Task<IEnumerable<Event>> GetAll();

      Task<Event> GetById(int id);

       // int Add(Event @event);

        Task Update(Event @event);

        Task Delete(Event @event);
    }
}
