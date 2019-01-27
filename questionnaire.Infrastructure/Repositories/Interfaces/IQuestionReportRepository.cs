using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IQuestionReportRepository {
        Task AddAsync (QuestionReport questionReport);
        Task<QuestionReport> GetBySurveyReportAsync (int surveyReportId, string content, string select,
            bool isTracking = true);
        Task<QuestionReport> GetBySurveyReportContentAndPositionAsync (int surveyReportId, int questionPosition, string select);
        Task UpdateAsync (QuestionReport questionReport);
    }
}