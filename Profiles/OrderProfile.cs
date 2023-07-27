using AutoMapper;
using Practica_.net.Models;
using Practica_.net.Models.DTO;

namespace Practica_.net.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ForMember(destination => destination.TicketCategory, opt => opt.MapFrom(src => src.TicketCategory.Description))
                .ForMember(destination => destination.eventName, opt => opt.MapFrom(src => src.TicketCategory.Event.EventName)).ReverseMap();


        }

    }
}
