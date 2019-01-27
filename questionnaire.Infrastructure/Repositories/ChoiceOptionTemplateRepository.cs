using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class ChoiceOptionTemplateRepository : IChoiceOptionTemplateRepository {
        private readonly questionnaireContext _context;

        public ChoiceOptionTemplateRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (ChoiceOptionTemplate choiceOptionTemplate) {
            await _context.ChoiceOptionTemplates.AddAsync (choiceOptionTemplate);
            await _context.SaveChangesAsync ();
        }
    }
}