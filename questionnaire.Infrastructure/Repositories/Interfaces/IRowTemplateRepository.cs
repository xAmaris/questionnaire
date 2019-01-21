using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface IRowTemplateRepository
    {
        Task AddAsync(RowTemplate rowTemplate);
        Task UpdateAsync(RowTemplate rowTemplate);
        Task DeleteAsync(RowTemplate rowTemplate);
        Task<RowTemplate> GetByFieldDataIdAsync(int fieldDataTemplateId, int rowPosition);
        Task<IEnumerable<RowTemplate>> GetAllByFieldDataIdInOrderAsync(int fieldDataTemplateId);
        Task<RowTemplate> GetByIdAsync(int id);
    }
}