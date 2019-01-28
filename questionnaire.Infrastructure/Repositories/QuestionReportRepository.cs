using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X9;

namespace questionnaire.Infrastructure.Repositories {
    public class QuestionReportRepository : IQuestionReportRepository {
        private readonly QuestionnaireContext _context;

        public QuestionReportRepository (QuestionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (QuestionReport questionReport) {
            await _context.QuestionReports.AddAsync (questionReport);
            await _context.SaveChangesAsync ();
        }

        public async Task<QuestionReport> GetBySurveyReportAsync (int surveyReportId, string content, string select,
            bool isTracking = true) {
            if (isTracking) {
                return await _context.QuestionReports
                    .AsTracking ()
                    .Include (x => x.DataSets)
                    .Where (x => x.SurveyReportId == surveyReportId && x.Content == content && x.Select == select)
                    .SingleOrDefaultAsync ();
            }
            return await _context.QuestionReports
                .AsNoTracking ()
                .Include (x => x.DataSets)
                .Where (x => x.SurveyReportId == surveyReportId && x.Content == content && x.Select == select)
                .SingleOrDefaultAsync ();
        }

        public async Task<QuestionReport> GetBySurveyReportContentAndPositionAsync (int surveyReportId,
            int questionPosition, string select) {
            return await _context.QuestionReports
                .Include (x => x.DataSets)
                .SingleOrDefaultAsync (x => x.SurveyReportId == surveyReportId && x.QuestionPosition == questionPosition && x.Select == select);
        }

        public async Task UpdateAsync (QuestionReport questionReport) {
            _context.QuestionReports.Update (questionReport);
            await _context.SaveChangesAsync ();
        }
    }
}