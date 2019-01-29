using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Commands.Survey;

namespace questionnaire.Infrastructure.Services.Interfaces
{
    public interface ISurveyTemplateService
    {
        Task<int> CreateSurveyTemplateAsync (SurveyToAdd command);
        Task<IEnumerable<SurveyTemplate>> GetAllSurveyTemplatesAsync ();
        Task<SurveyTemplate> GetSurveyTemplateByIdAsync (int surveyTemplateId);
        Task UpdateSurveyTemplateAsync (SurveyToUpdate command);
        Task DeleteSurveyTemplateAsync (int surveyTemplateId);
    }
}