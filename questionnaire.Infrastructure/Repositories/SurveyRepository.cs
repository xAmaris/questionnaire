using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class SurveyRepository : ISurveyRepository {
        private readonly questionnaireContext _context;

        public SurveyRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (Survey survey) {
            await _context.Surveys.AddAsync (survey);
            await _context.SaveChangesAsync ();
        }

        public async Task<Survey> GetByIdWithQuestionsAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.Surveys
                    .AsTracking ()
                    .Include (x => x.Questions)
                    .ThenInclude (x => x.FieldData)
                    .ThenInclude (x => x.ChoiceOptions)
                    .Include (x => x.Questions)
                    .ThenInclude (x => x.FieldData)
                    .ThenInclude (x => x.Rows)
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Surveys
                .AsNoTracking ()
                .Include (x => x.Questions)
                .ThenInclude (x => x.FieldData)
                .ThenInclude (x => x.ChoiceOptions)
                .Include (x => x.Questions)
                .ThenInclude (x => x.FieldData)
                .ThenInclude (x => x.Rows)
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<Survey> GetByTitleWithQuestionsAsync (string title, bool isTracking = true) {
            if (isTracking) {
                return await _context.Surveys
                    .AsTracking ()
                    .Include (x => x.Questions)
                    .SingleOrDefaultAsync (x => x.Title == title);
            }
            return await _context.Surveys
                .AsNoTracking ()
                .Include (x => x.Questions)
                .SingleOrDefaultAsync (x => x.Title == title);
        }

        public async Task<IEnumerable<Survey>> GetAllWithQuestionsAsync (bool isTracking = true) {
            if (isTracking) {
                return await Task.FromResult (_context.Surveys
                    .AsTracking ()
                    .Include (x => x.Questions)
                    .AsEnumerable ());
            }
            return await Task.FromResult (_context.Surveys
                .AsNoTracking ()
                .Include (x => x.Questions)
                .AsEnumerable ());
        }

        public async Task<Survey> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.Surveys
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Surveys
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task DeleteAsync (Survey survey) {
            _context.Surveys.Remove (survey);
            await _context.SaveChangesAsync ();
        }
    }
}