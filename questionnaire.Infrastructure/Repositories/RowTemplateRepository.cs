using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class RowTemplateRepository : IRowTemplateRepository {
        private readonly questionnaireContext _context;

        public RowTemplateRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (RowTemplate rowTemplate) {
            await _context.RowTemplates.AddAsync (rowTemplate);
            await _context.SaveChangesAsync ();
        }

        public async Task<RowTemplate> GetByIdAsync (int id) {
            return await _context.RowTemplates.SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<IEnumerable<RowTemplate>> GetAllByFieldDataIdInOrderAsync (int fieldDataTemplateId, bool isTracking = true) {
            if (isTracking) {
                return await Task.FromResult (_context.RowTemplates
                    .AsTracking ()
                    .Where (x => x.FieldDataTemplateId == fieldDataTemplateId)
                    .OrderBy (q => q.RowPosition));
            }
            return await Task.FromResult (_context.RowTemplates
                .AsNoTracking ()
                .Where (x => x.FieldDataTemplateId == fieldDataTemplateId)
                .OrderBy (q => q.RowPosition));
        }

        public async Task<RowTemplate> GetByFieldDataIdAsync (int fieldDataTemplateId, int rowPosition, bool isTracking = true) {
            if (isTracking) {
                return await _context.RowTemplates
                    .AsTracking ()
                    .Where (x => x.FieldDataTemplateId == fieldDataTemplateId && x.RowPosition == rowPosition)
                    .SingleOrDefaultAsync ();
            }
            return await _context.RowTemplates
                .AsNoTracking ()
                .Where (x => x.FieldDataTemplateId == fieldDataTemplateId && x.RowPosition == rowPosition)
                .SingleOrDefaultAsync ();
        }

        public async Task UpdateAsync (RowTemplate rowTemplate) {
            _context.RowTemplates.Update (rowTemplate);
            await _context.SaveChangesAsync ();
        }

        public async Task DeleteAsync (RowTemplate rowTemplate) {
            _context.RowTemplates.Update (rowTemplate);
            await _context.SaveChangesAsync ();
        }
    }
}