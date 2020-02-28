using Microsoft.Extensions.DependencyInjection;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace questionnaire.Tests.Repositories
{
    public class SurveyTemplateRepositoryTests : IClassFixture<TestHostFixture>
    {
        private readonly QuestionnaireContext _context;
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;
        public SurveyTemplateRepositoryTests(TestHostFixture fixture)
        {
            _context = fixture._factory.Server.Host.Services.CreateScope().ServiceProvider.GetRequiredService<QuestionnaireContext>();
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
            var existingSurveyTemplate = _context.SurveyTemplates.FirstOrDefault();
            //Act
            var surveyTemplate = await _surveyTemplateRepository.GetByIdWithQuestionTemplatesAsync(existingSurveyTemplate.Id);
            //Assert
            Assert.NotNull(surveyTemplate);
            Assert.Equal(existingSurveyTemplate, surveyTemplate);
        }
        [Fact]
        public async Task GetByIdAsync_GetCorrectSurveyTemplate_ReturnTrue()
        {
            //Arrange
            var existingSurveyTemplate = _context.SurveyTemplates.FirstOrDefault();
            //Act
            var surveyTemplate = await _surveyTemplateRepository.GetByIdAsync(existingSurveyTemplate.Id);
            //Assert
            Assert.NotNull(surveyTemplate);
            Assert.Equal(existingSurveyTemplate, surveyTemplate);
        }
        [Fact]
        public async Task GetAllWithQuestionTemplatesAsync_GetAllCorrectly()
        {
            //Arrange
            var currentSurveyTemplates = _context.SurveyTemplates.AsEnumerable();
            //Act
            var surveyTemplates = await _surveyTemplateRepository.GetAllWithQuestionTemplatesAsync();
            //Assert
            Assert.NotNull(surveyTemplates);
            Assert.Equal(currentSurveyTemplates, surveyTemplates);
        }
        [Fact]
        public async Task UpdateAsync_UpdatedCorrectly()
        {
            //Assert
            string title = "updated title";
            var surveyTemplate = _context.SurveyTemplates.FirstOrDefault();
            surveyTemplate.SetTitle(title);
            //Act
            await _surveyTemplateRepository.UpdateAsync(surveyTemplate);
            var updatedSurveyTemplate = await _surveyTemplateRepository.GetByIdAsync(surveyTemplate.Id);
            //Assert
            Assert.NotNull(updatedSurveyTemplate);
            Assert.Equal(title, updatedSurveyTemplate.Title);
        }
        [Fact]
        public async Task DeleteAsync_DeletedCorrectly_ReturnNull()
        {
            //Arrange
            var surveyTemplate = new SurveyTemplate("name");
            await _surveyTemplateRepository.AddAsync(surveyTemplate);
            //Act
            await _surveyTemplateRepository.DeleteAsync(surveyTemplate);
            //Assert
            var obj = await _surveyTemplateRepository.GetByIdAsync(surveyTemplate.Id);
            Assert.Null(obj);
        }
    }
}
