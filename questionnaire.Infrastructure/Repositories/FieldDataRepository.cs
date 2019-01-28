using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class FieldDataRepository : IFieldDataRepository {
        private readonly QuestionnaireContext _context;
        public FieldDataRepository (QuestionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (FieldData fieldData) {
            await _context.FieldData.AddAsync (fieldData);
            await _context.SaveChangesAsync ();
        }

        public async Task<FieldData> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.FieldData
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.FieldData
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }
    }
}