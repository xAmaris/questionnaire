using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;

namespace questionnaire.Infrastructure.Repositories {
    public class ChoiceOptionTemplateRepository : IChoiceOptionTemplateRepository {
        private QuestionnaireContext _context;
        public ChoiceOptionTemplateRepository (QuestionnaireContext context) {
            _context = context;
        }
        public async Task AddAsync (ChoiceOptionTemplate choiceOptionTemplate) {
            await _context.ChoiceOptionTemplates.AddAsync (choiceOptionTemplate);
            await _context.SaveChangesAsync ();
        }
    }
}