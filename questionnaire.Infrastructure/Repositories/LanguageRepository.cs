using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class LanguageRepository : ILanguageRepository {
        private readonly questionnaireContext _context;

        public LanguageRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (Language language) {
            await _context.Languages.AddAsync (language);
            await _context.SaveChangesAsync ();
        }

        public async Task<Language> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking){
                return await _context.Languages
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Languages
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<Language> GetByNameAsync (string name, bool isTracking = true) {
            if (isTracking){
                return await _context.Languages
                    .AsTracking()
                    .SingleOrDefaultAsync(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
            }
            return await _context.Languages
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        public async Task<IEnumerable<Language>> GetAllAsync (bool isTracking = true) {
            if (isTracking){
                return await Task.FromResult (_context.Languages
                    .AsTracking ()
                    .AsEnumerable ());
            }
            return await Task.FromResult (_context.Languages
                .AsNoTracking ()
                .AsEnumerable ());
        }

        public async Task UpdateAsync (Language language) {
            _context.Languages.Update (language);
            await _context.SaveChangesAsync ();
        }

        public async Task DeleteAsync (Language language) {
            _context.Languages.Remove (language);
            await _context.SaveChangesAsync ();
        }
    }
}