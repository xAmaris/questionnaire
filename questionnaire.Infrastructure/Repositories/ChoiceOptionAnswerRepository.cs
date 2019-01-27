using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class ChoiceOptionAnswerRepository : IChoiceOptionAnswerRepository {
        private readonly questionnaireContext _context;

        public ChoiceOptionAnswerRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task AddAsync (ChoiceOptionAnswer choiceOptionAnswer) {
            await _context.ChoiceOptionsAnswers.AddAsync (choiceOptionAnswer);
            await _context.SaveChangesAsync ();
        }
    }
}