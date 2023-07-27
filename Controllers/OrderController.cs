using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Practica_.net.Models;
using Practica_.net.Models.DTO;
using Practica_.net.Repositories;

namespace Practica_.net.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository, IMapper mapper) {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAllOrders()
        {
            var orders = _orderRepository.GetAll();

             /*var dtoOrders = orders.Select(e => new OrderDto()
             {
                 OrderId = e.OrderId,
                 eventName = e.TicketCategory?.Event?.EventName ?? string.Empty,
                 OrderedAt = e.OrderedAt,
                 NumberOfTickets = e.NumberOfTickets,
                 TicketCategory = e.TicketCategory?.Description ?? string.Empty,
                 TotalPrice = e.TotalPrice,
             });*/
            var dtoOrders = _mapper.Map<List<OrderDto>>(orders);
            return Ok(dtoOrders);
        }

            [HttpGet]
            public ActionResult<OrderDto> GetOrderById(int id)
            {
                Order order = _orderRepository.GetById(id);
                if (order == null)
                {
                    return NotFound();
                }
                /*OrderDto dtoOrder = new OrderDto()
                {
                    OrderId = order.OrderId,
                    eventName = order.TicketCategory?.Event?.EventName ?? string.Empty,
                    OrderedAt = order.OrderedAt,
                    NumberOfTickets = order.NumberOfTickets,
                    TicketCategory = order.TicketCategory?.Description ?? string.Empty,
                    TotalPrice = order.TotalPrice
                };*/
            var dtoOrder = _mapper.Map<OrderDto>(order);
                return Ok(dtoOrder);
            }
        [HttpPatch]
        public ActionResult<OrderPatchDto> Patch(OrderPatchDto orderPatch)
        {
            var orderEntity = _orderRepository.GetById(orderPatch.OrderId);
            if (orderEntity == null)
            {
                return NotFound();
            }
            //_mapper.Map(orderPatch, orderEntity);
            orderEntity.TicketCategoryId = orderPatch.TicketCategoryId;
            orderEntity.NumberOfTickets=orderPatch.NumberOfTickets;
            orderEntity.TotalPrice = orderEntity.NumberOfTickets * orderEntity.TicketCategory.Price;
            _orderRepository.Update(orderEntity);
            return Ok(orderEntity);
        }

    }
}
