using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface ISurveyReportRepository {
        Task AddAsync (SurveyReport surveyReport);
        Task<SurveyReport> GetBySurveyIdAsync (int surveyId, bool isTracking = true);
        Task DeleteAsync (SurveyReport surveyReport);
    }
}