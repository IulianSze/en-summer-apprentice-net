using AutoMapper;
using Practica_.net.Models;
using Practica_.net.Models.DTO;
namespace Practica_.net.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>().ForMember(destination => destination.Venue, opt => opt.MapFrom(src => src.Venue.Location))
                .ForMember(destination => destination.EventType, opt => opt.MapFrom(src => src.EventType.EventTypeName)).ReverseMap();
            CreateMap<Event, EventPatchDto>().ReverseMap();
        }
    }
}
