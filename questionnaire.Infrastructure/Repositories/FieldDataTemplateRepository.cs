using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;

namespace questionnaire.Infrastructure.Repositories {
    public class FieldDataTemplateRepository : IFieldDataTemplateRepository {
        private readonly QuestionnaireContext _context;
        public FieldDataTemplateRepository (QuestionnaireContext context) {
            _context = context;
        }
        public async Task AddAsync (FieldDataTemplate fieldDataTemplate) {
            await _context.FieldDataTemplates.AddAsync (fieldDataTemplate);
            await _context.SaveChangesAsync ();
        }

        public async Task<FieldDataTemplate> GetByIdAsync (int id) {
            return await _context.FieldDataTemplates
                .AsTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }
    }
}