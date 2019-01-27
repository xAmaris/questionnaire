using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class StudentRepository : IStudentRepository {
        private readonly questionnaireContext _context;

        public StudentRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (Student student) {
            await _context.Students.AddAsync (student);
            await _context.SaveChangesAsync ();
        }

        public async Task<Student> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking){
                return await _context.Students
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Students
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<Student> GetByIndexNumberAsync (string indexNumber, bool isTracking = true) {
            if (isTracking){
                return await _context.Students
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.IndexNumber == indexNumber);
            }
            return await _context.Students
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.IndexNumber == indexNumber);
        }

        public async Task<Student> GetByEmailAsync (string email, bool isTracking = true) {
            if (isTracking){
                return await _context.Students
                    .AsTracking()
                    .SingleOrDefaultAsync(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant());
            }
            return await _context.Students
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant());
        }
    }
}