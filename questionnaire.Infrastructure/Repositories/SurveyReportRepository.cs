using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class SurveyReportRepository : ISurveyReportRepository {
        private readonly QuestionnaireContext _context;

        public SurveyReportRepository (QuestionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (SurveyReport surveyReport) {
            await _context.AddAsync (surveyReport);
            await _context.SaveChangesAsync ();
        }

        public async Task<SurveyReport> GetBySurveyIdAsync (int surveyId, bool isTracking = true) {
            if (isTracking) {
                return await _context.SurveyReports
                    .AsTracking ()
                    .Where (x => x.SurveyId == surveyId)
                    .Include (x => x.QuestionsReports)
                    .ThenInclude (x => x.DataSets)
                    .SingleOrDefaultAsync ();
            }
            return await _context.SurveyReports
                .AsNoTracking ()
                .Where (x => x.SurveyId == surveyId)
                .Include (x => x.QuestionsReports)
                .ThenInclude (x => x.DataSets)
                .SingleOrDefaultAsync ();
        }

        public async Task DeleteAsync (SurveyReport surveyReport) {
            _context.Remove (surveyReport);
            await _context.SaveChangesAsync ();
        }
    }
}