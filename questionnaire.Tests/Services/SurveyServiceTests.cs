using Moq;
using questionnaire.Core.Domains.SurveyReport;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace questionnaire.Tests.Services
{
    class SurveyServiceTests
    {
        private SurveyReport _surveyReport;
        private QuestionReport _questionReport;
        private SurveyReportService _surveyReportService;
        private Mock<ISurveyReportRepository> _surveyReportRepository;
        private Mock<ISurveyRepository> _surveyRepository;
        private Mock<IDataSetRepository> _dataSetRepository;
        private Mock<IQuestionReportRepository> _questionReportRepository;
        public SurveyServiceTests()
        {
            _surveyReportRepository = new Mock<ISurveyReportRepository>();
            _surveyRepository = new Mock<ISurveyRepository>();
            _dataSetRepository = new Mock<IDataSetRepository>();
            _questionReportRepository = new Mock<IQuestionReportRepository>();
            _surveyReportService = new SurveyReportService(_surveyReportRepository.Object, _surveyRepository.Object, _dataSetRepository.Object, _questionReportRepository.Object);
        }
    }
}
