using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Commands.Survey;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Extensions.ExceptionHandling;
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
            _context = fixture._context;
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
        public async Task CreateSurveyTemplateAsync_SurveyTemplateNotAdded_ThrowsNullReferenceException()
        {
            //Arrange
            SurveyToAdd surveyToAdd = SurveyToAddMock.GetSurveyToAdd();
            surveyToAdd.Questions = null;
            //Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _surveyTemplateService.CreateSurveyTemplateAsync(surveyToAdd));
        }
        [Fact]
        public async Task CreateSurveyTemplateAsync_SurveyTemplateNotAddedBecauseFieldDatasAreNull_ThrowsNullReferenceException()
        {
            //Arrange
            SurveyToAdd surveyToAdd = SurveyToAddMock.GetSurveyToAdd();
            surveyToAdd.Questions.ToList().First().FieldData = null;
            //Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _surveyTemplateService.CreateSurveyTemplateAsync(surveyToAdd));
        }
        [Fact]
        public async Task CreateSurveyTemplateAsync_SurveyTemplateNotAddedBecauseQuestionSelectIsInvalid_ThrowsInvalidValueException()
        {
            //Arrange
            SurveyToAdd surveyToAdd = SurveyToAddMock.GetSurveyToAdd();
            surveyToAdd.Questions.ToList().First().Select = "invalid select value";
            //Act & Assert
            await Assert.ThrowsAsync<InvalidValueException>(() => _surveyTemplateService.CreateSurveyTemplateAsync(surveyToAdd));
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

        [Fact]
        public async Task UpdateSurveyTemplateAsync_SurveyTemplateNotUpdatedBecauseQuestionSelectIsInvalid_ThrowsInvalidValueException()
        {
            //Arrange
            SurveyToAdd surveyToAdd = SurveyToAddMock.GetSurveyToAdd();
            var surveyTemplateId = await _surveyTemplateService.CreateSurveyTemplateAsync(surveyToAdd);
            SurveyToUpdate surveyToUpdate = new SurveyToUpdate
            {
                SurveyId = surveyTemplateId,
                Title = surveyToAdd.Title,
                Questions = surveyToAdd.Questions
            };
            surveyToUpdate.Questions = null;
            //Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _surveyTemplateService.UpdateSurveyTemplateAsync(surveyToUpdate));
        }

        [Fact]
        public async Task UpdateSurveyTemplateAsync_SurveyTemplateNotUpdatedBecauseFieldDatasAreNull_ThrowsNullReferenceException()
        {
            //Arrange
            SurveyToAdd surveyToAdd = SurveyToAddMock.GetSurveyToAdd();
            var surveyTemplateId = await _surveyTemplateService.CreateSurveyTemplateAsync(surveyToAdd);
            SurveyToUpdate surveyToUpdate = new SurveyToUpdate
            {
                SurveyId = surveyTemplateId,
                Title = surveyToAdd.Title,
                Questions = surveyToAdd.Questions
            };
            surveyToUpdate.Questions.ToList().First().FieldData = null;
            //Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _surveyTemplateService.UpdateSurveyTemplateAsync(surveyToUpdate));
        }

        [Fact]
        public async Task DeleteSurveyTemplateAsync_DeleteCorrectSurveyTemplate()
        {
            //Arrange
            var surveyToAdd = SurveyToAddMock.GetSurveyToAdd();
            var surveyTemplateId = await _surveyTemplateService.CreateSurveyTemplateAsync(surveyToAdd);
            //Act
            await _surveyTemplateService.DeleteSurveyTemplateAsync(surveyTemplateId);
            //Assert
            var deletedSurvey = _context.SurveyTemplates.FirstOrDefault(x => x.Id == surveyTemplateId);
            Assert.Null(deletedSurvey);
        }
        [Fact]
        public async Task DeleteSurveyTemplateAsync_CannotDeleteSurveyTemplateBecauseSurveyTemplateDoesNotExist_ThrowsObjectDoesNotExistException()
        {
            //Arrange
            int id = -1;
            //Act & Assert
            await Assert.ThrowsAsync<ObjectDoesNotExistException>(() => _surveyTemplateService.DeleteSurveyTemplateAsync(id));
        }
        [Fact]
        public async Task GetAllSurveyTemplatesAsync_GetAllSurveysCorrectly()
        {
            //Arrange
            var surveyTemplatesFromContext = _context.SurveyTemplates.AsEnumerable();
            //Act
            var surveyTemplates = await _surveyTemplateService.GetAllSurveyTemplatesAsync();
            //Assert
            Assert.Equal(surveyTemplatesFromContext, surveyTemplates);
        }
        [Fact]
        public async Task GetSurveyTemplateByIdAsync_CorrectlyFetchSurveyTemplate()
        {
            //Arrange
            var surveyTemplateFromContext = _context.SurveyTemplates.First();
            //Act
            var surveyTemplate = await _surveyTemplateService.GetSurveyTemplateByIdAsync(surveyTemplateFromContext.Id);
            //Assert
            Assert.Equal(surveyTemplateFromContext, surveyTemplate);
        }
    }
}