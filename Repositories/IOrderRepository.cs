using Practica_.net.Models;
using System.Xml.Serialization;

namespace Practica_.net.Repositories
{
    public interface IOrderRepository
    {
       Task< IEnumerable<Order>> GetAll();

        Task<Order> GetById(int id);

        Task Update(Order order);
        Task Delete(Order order);
    }
}
