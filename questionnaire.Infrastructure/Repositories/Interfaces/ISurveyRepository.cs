using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface ISurveyRepository
    {
        Task AddAsync(Survey survey);
        Task<Survey> GetByIdWithQuestionsAsync(int id);
        Task<Survey> GetByIdAsync(int id);
        Task<Survey> GetByTitleWithQuestionsAsync(string title);
        Task<IEnumerable<Survey>> GetAllWithQuestionsAsync();
        Task DeleteAsync(Survey survey);
    }
}