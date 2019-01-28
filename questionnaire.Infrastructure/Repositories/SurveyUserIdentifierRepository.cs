using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class SurveyUserIdentifierRepository : ISurveyUserIdentifierRepository {
        private readonly QuestionnaireContext _context;

        public SurveyUserIdentifierRepository (QuestionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (SurveyUserIdentifier surveyUserIdentifier) {
            await _context.AddAsync (surveyUserIdentifier);
            await _context.SaveChangesAsync ();
        }

        public async Task<IEnumerable<SurveyUserIdentifier>> GetAllBySurveyIdAsync (int surveyId) {
            return await Task.FromResult (_context.SurveyUserIdentifiers.Where (x => x.SurveyId == surveyId));
        }

        public async Task UpdateAsync (SurveyUserIdentifier surveyUserIdentifier) {
            _context.Update (surveyUserIdentifier);
            await _context.SaveChangesAsync ();
        }
    }
}