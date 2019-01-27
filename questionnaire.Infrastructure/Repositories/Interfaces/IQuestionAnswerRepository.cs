using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IQuestionAnswerRepository {
        Task AddAsync (QuestionAnswer questionAnswer);
        Task<QuestionAnswer> GetByIdAsync(int id, bool isTracking = true);
    }
}