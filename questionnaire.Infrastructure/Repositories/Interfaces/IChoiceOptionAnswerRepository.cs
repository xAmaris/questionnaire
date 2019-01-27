using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IChoiceOptionAnswerRepository {
        Task AddAsync (ChoiceOptionAnswer choiceOptionAnswer);
    }
}