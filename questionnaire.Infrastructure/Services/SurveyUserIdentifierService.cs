using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class SurveyUserIdentifierService : ISurveyUserIdentifierService {
        private readonly ISurveyUserIdentifierRepository _surveyUserIdentifierRepository;

        public SurveyUserIdentifierService (ISurveyUserIdentifierRepository surveyUserIdentifierRepository) {
            _surveyUserIdentifierRepository = surveyUserIdentifierRepository;
        }

        public async Task CreateAsync (string userEmail, int surveyId) {
            var identifier = new SurveyUserIdentifier (userEmail, surveyId);
            await _surveyUserIdentifierRepository.AddAsync (identifier);
        }

        public async Task<string> VerifySurveyUser(string userEmail, int surveyId)
        {
            var identifiers = await _surveyUserIdentifierRepository.GetAllBySurveyIdAsync(surveyId);
            List<SurveyUserIdentifier> Identifiers = new List<SurveyUserIdentifier>();
            foreach (var identifier in identifiers)
            {

                if (VerifyEmailHash(userEmail, identifier.UserEmailHash))
                {
                    if (!identifier.Answered)
                    {
                        Identifiers.Add(identifier);
                    }
                    else
                    {
                        return await Task.FromResult("answered");
                    }
                }
            }
            foreach (var identifier in Identifiers)
            {
                identifier.MarkAsAnswered();
                await _surveyUserIdentifierRepository.UpdateAsync(identifier);
                return await Task.FromResult("authorized");
            }
            return "unauthorized";
        }

        private bool VerifyEmailHash (string input, string hash) {
            if (input == null || hash == null) {
                return false;
            }
            return input.Equals (hash);
        }
    }
}