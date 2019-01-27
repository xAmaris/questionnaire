using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace questionnaire.Infrastructure.Services.Interfaces
{
    public interface ISurveyUserIdentifierService
    {
        Task CreateAsync(string userEmail, int surveyId);
        Task<string> VerifySurveyUser(string userEmail, int surveyId);
    }
}
