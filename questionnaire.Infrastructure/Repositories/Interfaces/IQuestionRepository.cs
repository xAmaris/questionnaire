using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IQuestionRepository {
        Task AddAsync (Question question);
        Task<Question> GetByIdAsync (int id, bool isTracking = true);
    }
}