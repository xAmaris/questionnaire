using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class FieldDataTemplateRepository : IFieldDataTemplateRepository {
        private readonly questionnaireContext _context;
        public FieldDataTemplateRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (FieldDataTemplate fieldDataTemplate) {
            await _context.FieldDataTemplates.AddAsync (fieldDataTemplate);
            await _context.SaveChangesAsync ();
        }

        public async Task<FieldDataTemplate> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.FieldDataTemplates
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.FieldDataTemplates
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }
    }
}