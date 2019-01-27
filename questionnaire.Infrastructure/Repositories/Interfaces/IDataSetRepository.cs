using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;

namespace questionnaire.Infrastructure.Repositories.Interfaces
{
    public interface IDataSetRepository
    {
        Task AddAsync (DataSet dataSet);
        Task UpdateAsync (DataSet dataSet);
    }
}