
using AutoMapper;
using Moq;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;
using questionnaire.Infrastructure.Services.Interfaces;
using Xunit;

namespace questionnaire.Tests.Services
{
    class SurveyServiceTemplateTests : IClassFixture<TestHostFixture>
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
    }
}
