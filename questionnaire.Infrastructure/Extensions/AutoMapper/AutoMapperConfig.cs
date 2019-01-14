using System.Linq;
using AutoMapper;
using questionnaire.Core.Domain;
using questionnaire.Infrastructure.DTO.Event;
using questionnaire.Infrastructure.DTO.Ticket;

namespace questionnaire.Infrastructure.Extensions.AutoMapper {
    public static class AutoMapperConfig {
        public static IMapper Initialize () => new MapperConfiguration (cfg => {
            cfg.CreateMap<Event, EventDto> ().ForMember (x => x.TicketsCount, m => m.MapFrom (p => p.Tickets.Count ()));
            cfg.CreateMap<Event, EventDetailsDto> ();
            cfg.CreateMap<Ticket, TicketDto> ();
        }).CreateMapper ();
    }
}