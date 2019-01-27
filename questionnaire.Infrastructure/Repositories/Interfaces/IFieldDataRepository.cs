using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface IFieldDataRepository
    {
        Task AddAsync (FieldData fieldData);
        Task<FieldData> GetByIdAsync (int id, bool isTracking = true);
    }
}