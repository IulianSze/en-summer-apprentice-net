using Practica_.net.Models;
using System.Xml.Serialization;

namespace Practica_.net.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Order GetById(int id);

        void Update(Order order);
    }
}
