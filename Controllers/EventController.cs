using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Practica_.net.Models;
using Practica_.net.Models.DTO;
using Practica_.net.Repositories;


namespace Practica_.net.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        { 
            var events = _eventRepository.GetAll();
            //var dtoEvents = events.Select(e =>// new EventDto()
           // {
                /*EventId = e.EventId,
                EventDescription = e.EventDescription,
                EventName=e.EventName,
                EventType=e.EventType?.EventTypeName ?? string.Empty,
                Venue=e.Venue?.Location ?? string.Empty*/

                  var dtos= _mapper.Map<List<EventDto>>(events);
        //});
            return Ok(dtos);
        }
        [HttpGet]
        public ActionResult<EventDto> GetById(int id)
        {
            var @event = _eventRepository.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }

            /*var dtoEvent = new EventDto()
            {
                EventId = @event.EventId,
                EventDescription = @event.EventDescription,
                EventName = @event.EventName,
                EventType = @event.EventType?.EventTypeName ?? string.Empty,
                Venue = @event.Venue?.Location ?? string.Empty
            };*/
             var dtoEvent = _mapper.Map<EventDto>(@event);

            return Ok(dtoEvent);
        }
        [HttpPatch]
        public ActionResult<EventPatchDto> Patch(EventPatchDto eventPatch)
        {
            var eventEntity =  _eventRepository.GetById(eventPatch.EventId);
            if(eventEntity== null)
            {
                return NotFound();
            }
            eventEntity.EventName=eventPatch.EventName;
            eventEntity.EventDescription=eventPatch.EventDescription; 
            _eventRepository.Update(eventEntity);
            return Ok(eventEntity);
        }
        [HttpDelete]
        public ActionResult<EventPatchDto> Delete(int id)
        {
            var eventEntity = _eventRepository.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
          
            return NoContent();
        }
    }
    }

