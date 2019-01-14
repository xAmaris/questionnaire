using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Infrastructure.DTO.Event;

namespace questionnaire.Infrastructure.Services {
    public interface IEventService {
        Task<EventDetailsDto> GetAsync (Guid id);
        Task<EventDetailsDto> GetAsync (string name);
        Task<IEnumerable<EventDto>> BrowseAsync (string name = null);
        Task CreateAsync (Guid id, string name, string description, DateTime startDate, DateTime endDate);
        Task AddTicketAsync (Guid eventId, int amount, decimal price);
        Task UpdateAsync (Guid id, string name, string description);
        Task DeleteAsync (Guid id);
    }
}