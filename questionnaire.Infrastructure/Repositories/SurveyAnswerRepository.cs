using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class SurveyAnswerRepository : ISurveyAnswerRepository {
        private readonly questionnaireContext _context;

        public SurveyAnswerRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (SurveyAnswer surveyAnswer) {
            await _context.SurveyAnswers.AddAsync (surveyAnswer);
            await _context.SaveChangesAsync ();
        }

        public async Task<SurveyAnswer> GetByIdWithQuestionsAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.SurveyAnswers
                    .AsTracking ()
                    .Include (x => x.QuestionsAnswers)
                    .ThenInclude (x => x.FieldDataAnswers)
                    .ThenInclude (x => x.ChoiceOptionAnswers)
                    .Include (x => x.QuestionsAnswers)
                    .ThenInclude (x => x.FieldDataAnswers)
                    .ThenInclude (x => x.RowsAnswers)
                    .ThenInclude (x => x.RowChoiceOptionAnswers)
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.SurveyAnswers
                .AsNoTracking ()
                .Include (x => x.QuestionsAnswers)
                .ThenInclude (x => x.FieldDataAnswers)
                .ThenInclude (x => x.ChoiceOptionAnswers)
                .Include (x => x.QuestionsAnswers)
                .ThenInclude (x => x.FieldDataAnswers)
                .ThenInclude (x => x.RowsAnswers)
                .ThenInclude (x => x.RowChoiceOptionAnswers)
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<SurveyAnswer> GetByIdAsync(int id, bool isTracking = true)
        {
            if (isTracking){
                return await _context.SurveyAnswers
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.SurveyAnswers
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }
        public async Task<SurveyAnswer> GetByTitleWithQuestionsAsync (string surveyTitle, bool isTracking = true) {
            if (isTracking) {
                return await _context.SurveyAnswers
                    .AsTracking ()
                    .Include (x => x.QuestionsAnswers)
                    .ThenInclude (x => x.FieldDataAnswers)
                    .ThenInclude (x => x.ChoiceOptionAnswers)
                    .Include (x => x.QuestionsAnswers)
                    .ThenInclude (x => x.FieldDataAnswers)
                    .ThenInclude (x => x.RowsAnswers)
                    .ThenInclude (x => x.RowChoiceOptionAnswers)
                    .SingleOrDefaultAsync (x => x.SurveyTitle == surveyTitle);
            }
            return await _context.SurveyAnswers
                .AsNoTracking ()
                .Include (x => x.QuestionsAnswers)
                .ThenInclude (x => x.FieldDataAnswers)
                .ThenInclude (x => x.ChoiceOptionAnswers)
                .Include (x => x.QuestionsAnswers)
                .ThenInclude (x => x.FieldDataAnswers)
                .ThenInclude (x => x.RowsAnswers)
                .ThenInclude (x => x.RowChoiceOptionAnswers)
                .SingleOrDefaultAsync (x => x.SurveyTitle == surveyTitle);
        }

        public async Task<IEnumerable<SurveyAnswer>> GetAllWithQuestionsAsync(bool isTracking = true)
        {
            if (isTracking){
                return await Task.FromResult (_context.SurveyAnswers
                    .AsTracking ()
                    .Include (x => x.QuestionsAnswers)
                    .ThenInclude (x => x.FieldDataAnswers)
                    .ThenInclude (x => x.ChoiceOptionAnswers)
                    .Include (x => x.QuestionsAnswers)
                    .ThenInclude (x => x.FieldDataAnswers)
                    .ThenInclude (x => x.RowsAnswers)
                    .ThenInclude (x => x.RowChoiceOptionAnswers)
                    .AsEnumerable ());
            }
            return await Task.FromResult (_context.SurveyAnswers
                .AsNoTracking ()
                .Include (x => x.QuestionsAnswers)
                .ThenInclude (x => x.FieldDataAnswers)
                .ThenInclude (x => x.ChoiceOptionAnswers)
                .Include (x => x.QuestionsAnswers)
                .ThenInclude (x => x.FieldDataAnswers)
                .ThenInclude (x => x.RowsAnswers)
                .ThenInclude (x => x.RowChoiceOptionAnswers)
                .AsEnumerable ());
        }

        public async Task DeleteAsync(SurveyAnswer surveyAnswer)
        {
            _context.SurveyAnswers.Remove (surveyAnswer);
            await _context.SaveChangesAsync ();
        }
    }
}