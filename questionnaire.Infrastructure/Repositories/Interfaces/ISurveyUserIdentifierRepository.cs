using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface ISurveyUserIdentifierRepository {
        Task AddAsync (SurveyUserIdentifier surveyUserIdentifier);
        Task<IEnumerable<SurveyUserIdentifier>> GetAllBySurveyIdAsync (int surveyId);
        Task UpdateAsync (SurveyUserIdentifier surveyUserIdentifier);
    }
}