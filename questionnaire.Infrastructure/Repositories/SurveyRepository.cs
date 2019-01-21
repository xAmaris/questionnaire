using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Infrastructure.Repositories.Interfaces;

namespace questionnaire.Infrastructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {

        private static readonly List<Survey> _surveys = new List<Survey>() {
            new Survey ("nazwa")
        };
        public async Task AddSurveyAsync(Survey survey)
        {
            _surveys.Add(survey);
            await Task.CompletedTask;
        }
        public async Task UpdateSurveyAsync(Survey survey)
        {
            await Task.CompletedTask;
        }
        public async Task DeleteSurveyAsync(Survey survey)
        {
            _surveys.Remove(survey);
            await Task.CompletedTask;
        }

        public Task AddAsync(Survey survey)
        {
            throw new System.NotImplementedException();
        }

        public Task<Survey> GetByIdWithQuestionsAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Survey> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Survey> GetByTitleWithQuestionsAsync(string title)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Survey>> GetAllWithQuestionsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Survey survey)
        {
            throw new System.NotImplementedException();
        }
    }
}