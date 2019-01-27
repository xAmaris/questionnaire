using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class ChoiceOptionRepository : IChoiceOptionRepository {
        private readonly questionnaireContext _context;

        public ChoiceOptionRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (ChoiceOption choiceOption) {
            await _context.ChoiceOptions.AddAsync (choiceOption);
            await _context.SaveChangesAsync ();
        }
    }
}