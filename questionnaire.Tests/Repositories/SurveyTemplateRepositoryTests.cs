using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Tests.Fixtures;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace questionnaire.Tests.Repositories
{
    public class SurveyTemplateRepositoryTests : IClassFixture<SurveyTemplateRepositoryFixture>
    {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;
        QuestionnaireContext _context;
        public SurveyTemplateRepositoryTests(SurveyTemplateRepositoryFixture fixture)
        {
            _context = fixture.context;
            _surveyTemplateRepository = new SurveyTemplateRepository(_context);
        }
        [Fact]
        public async Task AddAsync_NewSurveyTemplateAddedCorrectly_ReturnTrue()
        {
            //Arrange
            var surveyTemplate = new SurveyTemplate("Nowy szablon ankiety");

            //Act
            await _surveyTemplateRepository.AddAsync(surveyTemplate);
            //Assert
            var obj = _context.SurveyTemplates.FirstOrDefault(x => x.Id == surveyTemplate.Id);
            Assert.Equal(surveyTemplate.Title, obj.Title);
        }
        [Fact]
        public async Task GetByIdWithQuestionTemplatesAsync_GetCorrectSurveyTemplateWithQuestionTemplate_ReturnTrue()
        {
            //Arrange
            int id = 1;
            var existingSurveyTemplate = _context.SurveyTemplates.FirstOrDefault(x => x.Id == id);
            //Act
            var surveyTemplate = await _surveyTemplateRepository.GetByIdWithQuestionTemplatesAsync(id);
            //Assert
            Assert.Equal(existingSurveyTemplate, surveyTemplate);
        }
        [Fact]
        public async Task GetByIdAsync_GetCorrectSurveyTemplate_ReturnTrue()
        {
            //Arrange
            int id = 1;
            var existingSurveyTemplate = _context.SurveyTemplates.FirstOrDefault(x => x.Id == id);
            //Act
            var surveyTemplate = await _surveyTemplateRepository.GetByIdAsync(id);
            //Assert
            Assert.Equal(existingSurveyTemplate, surveyTemplate);
        }
    }
}
