using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface IFieldDataTemplateRepository
    {
        Task AddAsync (FieldDataTemplate fieldDataTemplate);
        Task<FieldDataTemplate> GetByIdAsync (int id, bool isTracking = true);
    }
}