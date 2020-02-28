using Moq;
using questionnaire.Core.Domains.SurveyReport;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;

namespace questionnaire.Tests.Services
{
    class SurveyServiceReportTests
    {
        private SurveyReportService _surveyReportService;
        private Mock<ISurveyReportRepository> _surveyReportRepository;
        private Mock<ISurveyRepository> _surveyRepository;
        private Mock<IDataSetRepository> _dataSetRepository;
        private Mock<IQuestionReportRepository> _questionReportRepository;
        public SurveyServiceReportTests()
        {
            _surveyReportRepository = new Mock<ISurveyReportRepository>();
            _surveyRepository = new Mock<ISurveyRepository>();
            _dataSetRepository = new Mock<IDataSetRepository>();
            _questionReportRepository = new Mock<IQuestionReportRepository>();
            _surveyReportService = new SurveyReportService(_surveyReportRepository.Object, _surveyRepository.Object, _dataSetRepository.Object, _questionReportRepository.Object);
        }

    }
}
