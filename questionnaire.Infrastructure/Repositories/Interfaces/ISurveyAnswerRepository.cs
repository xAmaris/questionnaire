using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface ISurveyAnswerRepository
    {
        Task AddAsync (SurveyAnswer surveyAnswer);
        Task<SurveyAnswer> GetByIdWithQuestionsAsync (int id, bool isTracking = true);
        Task<SurveyAnswer> GetByIdAsync(int id, bool isTracking = true);
        Task<SurveyAnswer> GetByTitleWithQuestionsAsync (string title, bool isTracking = true);
        Task<IEnumerable<SurveyAnswer>> GetAllWithQuestionsAsync (bool isTracking = true);
        Task DeleteAsync (SurveyAnswer surveyAnswer);
    }
}