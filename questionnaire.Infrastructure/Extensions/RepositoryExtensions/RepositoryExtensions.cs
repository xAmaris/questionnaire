using System;
using System.Threading.Tasks;
using questionnaire.Core.Domain;
using questionnaire.Core.Repositories;

namespace questionnaire.Infrastructure.Extensions.RepositoryExtensions {
    public static class RepositoryExtensions {
        public static async Task<Event> GetOrFailAsync (this IEventRepository repository, Guid id) {
            var @event = await repository.GetAsync (id);
            if (@event == null) {
                throw new Exception ($"Event with id: '{id}' does not exist.");
            }
            return @event;
        }
    }
}