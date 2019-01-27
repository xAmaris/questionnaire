using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface IChoiceOptionRepository
    {
        Task AddAsync (ChoiceOption choiceOption);
    }
}