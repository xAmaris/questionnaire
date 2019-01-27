using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class QuestionRepository : IQuestionRepository {
        private readonly questionnaireContext _context;
        public QuestionRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (Question question) {
            await _context.Questions.AddAsync (question);
            await _context.SaveChangesAsync ();
        }

        public async Task<Question> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.Questions
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Questions
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }
    }
}