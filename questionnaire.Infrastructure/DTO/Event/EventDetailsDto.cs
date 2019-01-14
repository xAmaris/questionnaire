using System.Collections.Generic;
using questionnaire.Infrastructure.DTO.Ticket;

namespace questionnaire.Infrastructure.DTO.Event {
    public class EventDetailsDto : EventDto {
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}