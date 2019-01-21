using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface ISurveyTemplateRepository
    {
        Task AddAsync(SurveyTemplate surveyTemplate);
        Task<SurveyTemplate> GetByIdWithQuestionTemplatesAsync(int id);
        Task<SurveyTemplate> GetByIdAsync(int id);
        Task<SurveyTemplate> GetByTitleWithQuestionTemplatesAsync(string title);
        Task<IEnumerable<SurveyTemplate>> GetAllWithQuestionTemplatesAsync();
        Task UpdateAsync(SurveyTemplate surveyTemplate);
        Task DeleteAsync(SurveyTemplate surveyTemplate);
    }
}