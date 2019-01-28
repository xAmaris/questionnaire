using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class QuestionTemplateRepository : IQuestionTemplateRepository {
        private readonly QuestionnaireContext _context;
        public QuestionTemplateRepository (QuestionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (QuestionTemplate questionTemplate) {
            await _context.QuestionTemplates.AddAsync (questionTemplate);
            await _context.SaveChangesAsync ();
        }

        public async Task<QuestionTemplate> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.QuestionTemplates
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.QuestionTemplates
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task DeleteAsync (QuestionTemplate questionTemplate) {
            _context.QuestionTemplates.Remove (questionTemplate);
            await _context.SaveChangesAsync ();
        }
    }
}