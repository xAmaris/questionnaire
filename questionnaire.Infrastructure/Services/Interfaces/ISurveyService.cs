using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Commands.Survey;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface ISurveyService {
        Task<int> CreateSurveyAsync (int surveyTemplateId);
        Task<int> CreateAsync (string title);
        Task<int> AddQuestionToSurveyAsync (int surveyId, int questionPosition, string content, string select, bool isRequired);
        Task<int> AddFieldDataToQuestionAsync (int questionId, string input, int minValue, int maxValue, string minLabel,
            string maxLabel);
        Task AddChoiceOptionsAsync (int fieldDataId, int optionPosition, bool value, string viewValue);
        Task AddRowAsync (int fieldDataId, int rowPosition, string input);
        Task<IEnumerable<Survey>> GetAllAsync ();
        Task<Survey> GetByIdAsync (int surveyId);
    }
}