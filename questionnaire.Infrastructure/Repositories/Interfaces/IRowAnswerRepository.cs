using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IRowAnswerRepository {
        Task AddAsync (RowAnswer rowAnswer);
        Task<RowAnswer> GetByIdAsync (int id, bool isTracking = true);
    }
}