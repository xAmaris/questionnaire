using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface ISurveyRepository {
        Task AddAsync (Survey survey);
        Task<Survey> GetByIdWithQuestionsAsync (int id, bool isTracking = true);
        Task<Survey> GetByIdAsync(int id, bool isTracking = true);
        Task<Survey> GetByTitleWithQuestionsAsync (string title, bool isTracking = true);
        Task<IEnumerable<Survey>> GetAllWithQuestionsAsync (bool isTracking = true);
        Task DeleteAsync (Survey survey);
    }
}