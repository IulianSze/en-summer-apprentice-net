using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Practica_.net.Exceptions;
using Practica_.net.Models;
using Practica_.net.Models.DTO;
using Practica_.net.Repositories;

namespace Practica_.net.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketManagementSystemContext _dbContext;
        public OrderRepository() 
        {
            _dbContext = new TicketManagementSystemContext();
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            var orders = _dbContext.Orders.Include(e => e.TicketCategory).Include(e => e.TicketCategory.Event);
            return orders;
        }

        public async Task<Order> GetById(int id) {
            Order order = await _dbContext.Orders.Where(o => o.OrderId == id).Include(e=>e.TicketCategory).ThenInclude(e=>e.Event).FirstOrDefaultAsync();
            if (order == null)
                throw new EntityNotFoundException(id, nameof(Order));
            return order;
        }
        public async Task Update(Order order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChangesAsync();
        }
        public async Task Delete(Order order)
        {
            _dbContext.Remove(@order);
            _dbContext.SaveChangesAsync();
        }
    }
}
