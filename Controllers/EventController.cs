using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Practica_.net.Middleware;
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
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public EventController(IEventRepository eventRepository, IMapper mapper, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDto>>> GetAll()
        { 
            var events = await _eventRepository.GetAll();
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
        public async Task<ActionResult<EventDto>> GetById(int id)
        {
            var @event =await _eventRepository.GetById(id);


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
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);
           
            if (!eventPatch.EventName.IsNullOrEmpty()) eventEntity.EventName = eventPatch.EventName;
            if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.EventDescription = eventPatch.EventDescription;
            //var eventDto = _mapper.Map<EventDto>(eventEntity)
           await _eventRepository.Update(eventEntity);
            var dtoEvent = _mapper.Map<EventDto>(eventEntity);
            return Ok(dtoEvent);
        }
        [HttpDelete]
        public async Task<ActionResult<EventPatchDto>> Delete(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
          await _eventRepository.Delete(eventEntity);
          
            return NoContent();
        }
    }
    }

