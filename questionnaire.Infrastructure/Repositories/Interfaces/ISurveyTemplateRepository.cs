using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface ISurveyTemplateRepository
    {
        Task AddAsync (SurveyTemplate surveyTemplate);
        Task<SurveyTemplate> GetByIdWithQuestionTemplatesAsync (int id, bool isTracking = true);
        Task<SurveyTemplate> GetByIdAsync(int id, bool isTracking = true);
        Task<SurveyTemplate> GetByTitleWithQuestionTemplatesAsync (string title, bool isTracking = true);
        Task<IEnumerable<SurveyTemplate>> GetAllWithQuestionTemplatesAsync (bool isTracking = true);
        Task UpdateAsync (SurveyTemplate surveyTemplate);
        Task DeleteAsync (SurveyTemplate surveyTemplate);
    }
}