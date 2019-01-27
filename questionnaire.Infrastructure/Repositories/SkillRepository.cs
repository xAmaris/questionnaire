using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class SkillRepository : ISkillRepository {
        private readonly questionnaireContext _context;

        public SkillRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task<Skill> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.Skills
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Skills
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }
    }
}