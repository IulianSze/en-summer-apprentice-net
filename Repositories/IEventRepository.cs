using Practica_.net.Models;
using System.Xml.Serialization;

namespace Practica_.net.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

       Event GetById(int id);

       // int Add(Event @event);

        void Update(Event @event);

        void Delete(Event @event);
    }
}
