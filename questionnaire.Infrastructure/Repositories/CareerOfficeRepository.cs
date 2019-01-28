using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class CareerOfficeRepository : ICareerOfficeRepository {
        private readonly QuestionnaireContext _context;

        public CareerOfficeRepository (QuestionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (CareerOffice careerOffice) {
            await _context.CareerOffices.AddAsync (careerOffice);
            await _context.SaveChangesAsync ();
        }

        public async Task<CareerOffice> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.CareerOffices
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.CareerOffices
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<CareerOffice> GetByEmailAsync (string email, bool isTracking = true) {
            if (isTracking) {
                return await _context.CareerOffices
                    .AsTracking()
                    .SingleOrDefaultAsync(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant());
            }
            return await _context.CareerOffices.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant());
        }

        public async Task UpdateAsync (CareerOffice careerOffice) {
            _context.CareerOffices.Update(careerOffice);
            await _context.SaveChangesAsync();
        }

    }
}