using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class RowRepository : IRowRepository {
        private readonly questionnaireContext _context;
        public RowRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (Row row) {
            await _context.Rows.AddAsync (row);
            await _context.SaveChangesAsync ();
        }
    }
}