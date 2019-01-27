using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class FieldDataAnswerRepository : IFieldDataAnswerRepository {
        private readonly questionnaireContext _context;
        public FieldDataAnswerRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (FieldDataAnswer fieldDataAnswer) {
            await _context.FieldDataAnswers.AddAsync (fieldDataAnswer);
            await _context.SaveChangesAsync ();
        }

        public async Task<FieldDataAnswer> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.FieldDataAnswers
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.FieldDataAnswers
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }
    }
}