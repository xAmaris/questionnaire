using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;

namespace questionnaire.Infrastructure.Repositories {
    public class SurveyTemplateRepository : ISurveyTemplateRepository {
        private readonly QuestionnaireContext _context;

        public SurveyTemplateRepository (QuestionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (SurveyTemplate surveyTemplate) {
            await _context.SurveyTemplates.AddAsync (surveyTemplate);
            await _context.SaveChangesAsync ();
        }

        public async Task<SurveyTemplate> GetByIdWithQuestionTemplatesAsync (int id) {
            return await _context.SurveyTemplates
                .AsNoTracking ()
                .Include (x => x.QuestionTemplates)
                .ThenInclude (x => x.FieldDataTemplates)
                .ThenInclude (x => x.ChoiceOptionTemplates)
                .Include (x => x.QuestionTemplates)
                .ThenInclude (x => x.FieldDataTemplates)
                .ThenInclude (x => x.RowTemplates)
                .SingleOrDefaultAsync (x => x.Id == id);

        }

        public async Task<SurveyTemplate> GetByTitleWithQuestionTemplatesAsync (string title) {
            return await _context.SurveyTemplates
                .AsTracking ()
                .Include (x => x.QuestionTemplates)
                .SingleOrDefaultAsync (x => x.Title == title);
        }

        public async Task<IEnumerable<SurveyTemplate>> GetAllWithQuestionTemplatesAsync () {
            return await Task.FromResult (_context.SurveyTemplates
                .AsNoTracking ()
                .Include (x => x.QuestionTemplates)
                .AsEnumerable ());
        }

        public async Task<SurveyTemplate> GetByIdAsync (int id) {
            return await _context.SurveyTemplates
                .AsTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);

        }

        public async Task UpdateAsync (SurveyTemplate surveyTemplate) {
            _context.SurveyTemplates.Update (surveyTemplate);
            await _context.SaveChangesAsync ();
        }

        public async Task DeleteAsync (SurveyTemplate surveyTemplate) {
            _context.SurveyTemplates.Remove (surveyTemplate);
            await _context.SaveChangesAsync ();
        }
    }
}