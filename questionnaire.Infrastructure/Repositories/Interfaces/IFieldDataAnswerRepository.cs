using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface IFieldDataAnswerRepository
    {
        Task AddAsync (FieldDataAnswer fieldDataAnswer);
        Task<FieldDataAnswer> GetByIdAsync (int id, bool isTracking = true);
    }
}