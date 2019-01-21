using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IQuestionTemplateRepository {
        Task AddAsync (QuestionTemplate questionTemplate);
        Task<QuestionTemplate> GetByIdAsync (int id);
        Task DeleteAsync (QuestionTemplate questionTemplate);
    }
}