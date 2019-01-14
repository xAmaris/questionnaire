using System;
using System.Threading.Tasks;
using DevBricks.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using questionnaire.Infrastructure.Commands.Events;
using questionnaire.Infrastructure.Services;

namespace questionnaire.Api.Controllers {
    public class EventsController : ApiController {
        private readonly IEventService _eventService;
        public EventsController (IEventService eventService) {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get (string name) {
            var events = await _eventService.BrowseAsync (name);
            return Json (events);
        }

        [HttpGet ("{eventId}")]
        public async Task<IActionResult> Get (Guid eventId) {
            var @event = await _eventService.GetAsync (eventId);
            if(@eventId == null){
                return NotFound();
            }
            return Json (@event);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] CreateEvent command) {
            command.EventId = Guid.NewGuid ();
            await _eventService.CreateAsync (command.EventId, command.Name, command.Description, command.StartDate, command.EndDate);
            await _eventService.AddTicketAsync (command.EventId, command.Tickets, command.Price);
            return Created ($"/events/'{command.EventId}'", null);
        }

        [HttpPut ("{eventId}")]
        public async Task<IActionResult> Put (Guid eventId, [FromBody] UpdateEvent command) {
            command.EventId = Guid.NewGuid ();
            await _eventService.UpdateAsync (command.EventId, command.Name, command.Description);
            return NoContent ();
        }

        [HttpDelete ("{eventId}")]
        public async Task<IActionResult> Delete (Guid eventId) {
            await _eventService.DeleteAsync (eventId);
            return NoContent ();
        }
    }
}