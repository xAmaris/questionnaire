using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using questionnaire.Infrastructure.Commands.Survey;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;
using questionnaire.Infrastructure.Services.Interfaces;
using questionnaire.Tests.Mocks;
using Xunit;

namespace questionnaire.Tests.Services
{
    public class SurveyServiceTemplateTests : IClassFixture<TestHostFixture>
    {
        private readonly QuestionnaireContext _context;
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;
        private readonly IQuestionTemplateRepository _questionTemplateRepository;
        private readonly IFieldDataTemplateRepository _fieldDataTemplateRepository;
        private readonly IChoiceOptionTemplateRepository _choiceOptionTemplateRepository;
        private readonly IRowTemplateRepository _rowTemplateRepository;

        private ISurveyTemplateService _surveyTemplateService;
        public SurveyServiceTemplateTests(TestHostFixture fixture)
        {
            _context = fixture.Context;
            _surveyTemplateRepository = new SurveyTemplateRepository(_context);
            _questionTemplateRepository = new QuestionTemplateRepository(_context);
            _fieldDataTemplateRepository = new FieldDataTemplateRepository(_context);
            _choiceOptionTemplateRepository = new ChoiceOptionTemplateRepository(_context);
            _rowTemplateRepository = new RowTemplateRepository(_context);

            _surveyTemplateService = new SurveyTemplateService(_surveyTemplateRepository, _questionTemplateRepository, _fieldDataTemplateRepository, _choiceOptionTemplateRepository, _rowTemplateRepository);

        }

        [Fact]
        public async Task CreateSurveyTemplateAsync_SurveyTemplateAddedCorrectly_NotNull()
        {
            //Arrange
            SurveyToAdd surveyToAdd = SurveyToAddMock.GetSurveyToAdd();
            //Act
            var surveyTemplateId = await _surveyTemplateService.CreateSurveyTemplateAsync(surveyToAdd);
            //Assert
            var createdSurvey = _context.SurveyTemplates.FirstOrDefault(x => x.Id == surveyTemplateId);
            Assert.NotNull(createdSurvey);
        }

        [Fact]
        public async Task UpdateSurveyTemplateAsync_UpdateSurveyTemplateCorrectly()
        {
            //Arrange
            string title = "super secret updated title";
            SurveyToAdd surveyToAdd = SurveyToAddMock.GetSurveyToAdd();
            var surveyTemplateId = await _surveyTemplateService.CreateSurveyTemplateAsync(surveyToAdd);
            SurveyToUpdate surveyToUpdate = new SurveyToUpdate
            {
                SurveyId = surveyTemplateId,
                Title = title,
                Questions = surveyToAdd.Questions
            };
            //Act
            await _surveyTemplateService.UpdateSurveyTemplateAsync(surveyToUpdate);
            //Assert
            var surveyTemplate = _context.SurveyTemplates.FirstOrDefault(x => x.Id == surveyTemplateId);
            Assert.Equal(surveyTemplate.Id, surveyTemplateId);
            Assert.Equal(surveyTemplate.Title, title);
        }
    }
}