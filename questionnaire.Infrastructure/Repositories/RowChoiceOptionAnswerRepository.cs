using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class RowChoiceOptionAnswerRepository : IRowChoiceOptionAnswerRepository {
        private readonly questionnaireContext _context;

        public RowChoiceOptionAnswerRepository (questionnaireContext context) {
            _context = context;
        }
        public async Task AddAsync (RowChoiceOptionAnswer rowChoiceOptionAnswer) {
            await _context.RowChoiceOptionsAnswers.AddAsync (rowChoiceOptionAnswer);
            await _context.SaveChangesAsync ();
        }
    }
}