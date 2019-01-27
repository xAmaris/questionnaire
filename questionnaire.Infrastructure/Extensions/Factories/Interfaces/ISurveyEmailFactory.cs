using System.Threading.Tasks;

namespace questionnaire.Infrastructure.Extensions.Factories.Interfaces {
    public interface ISurveyEmailFactory {
        Task SendSurveyEmailAsync (int surveyId);
        Task SendSurveyEmailToUnregisteredUsersAsync (int surveyId);
        string CalculateEmailHash(string input);
    }
}