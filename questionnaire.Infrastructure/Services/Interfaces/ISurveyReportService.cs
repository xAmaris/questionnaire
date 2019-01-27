using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface ISurveyReportService {
        Task<int> CreateAsync (int surveyId, string surveyTitle);
    }
}