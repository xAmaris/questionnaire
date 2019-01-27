using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface IRowTemplateRepository
    {
        Task AddAsync (RowTemplate rowTemplate);
    }
}