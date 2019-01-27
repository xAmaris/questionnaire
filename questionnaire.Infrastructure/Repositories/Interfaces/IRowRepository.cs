using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IRowRepository {
        Task AddAsync (Row row);
    }
}