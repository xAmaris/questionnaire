using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface ILanguageRepository {
        Task AddAsync (Language language);
        Task<Language> GetByIdAsync (int id, bool isTracking = true);
        Task<Language> GetByNameAsync (string name, bool isTracking = true);
        Task<IEnumerable<Language>> GetAllAsync (bool isTracking = true);
        Task UpdateAsync (Language language);
        Task DeleteAsync (Language language);
    }
}