using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveyReport;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class SurveyReportService : ISurveyReportService {
        private readonly ISurveyReportRepository _surveyReportRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IQuestionReportRepository _questionReportRepository;
        private readonly IDataSetRepository _dataSetRepository;

        public SurveyReportService (ISurveyReportRepository surveyReportRepository,
            ISurveyRepository surveyRepository,
            IDataSetRepository dataSetRepository,
            IQuestionReportRepository questionReportRepository) {
            _surveyReportRepository = surveyReportRepository;
            _surveyRepository = surveyRepository;
            _dataSetRepository = dataSetRepository;
            _questionReportRepository = questionReportRepository;
        }

        public async Task<int> CreateAsync (int surveyId, string surveyTitle) {
            var surveyReport = new SurveyReport (surveyId, surveyTitle);
            await _surveyReportRepository.AddAsync (surveyReport);
            var survey = await _surveyRepository.GetByIdWithQuestionsAsync (surveyId);

            foreach (var question in survey.Questions) {
                var questionReport =
                    new QuestionReport (question.Content, question.Select, 0, question.QuestionPosition);
                surveyReport.AddQuestionReport (questionReport);
                await _questionReportRepository.AddAsync (questionReport);
                foreach (var fieldData in question.FieldData) {
                    switch (questionReport.Select) {
                        case "short-answer":
                        case "long-answer":
                            {
                                var dataSet = new DataSet ();
                                questionReport.AddDataSet (dataSet);
                                await _dataSetRepository.AddAsync (dataSet);
                                await _questionReportRepository.UpdateAsync (questionReport);
                                foreach (var label in questionReport.Labels) {
                                    dataSet.AddData ("0");
                                    await _dataSetRepository.UpdateAsync (dataSet);
                                }
                            }
                            break;
                        case "dropdown-menu":
                            {
                                foreach (var choiceOption in fieldData.ChoiceOptions) {
                                    questionReport.AddLabel (choiceOption.ViewValue);
                                }
                                var dataSet = new DataSet ();
                                questionReport.AddDataSet (dataSet);
                                await _dataSetRepository.AddAsync (dataSet);
                                await _questionReportRepository.UpdateAsync (questionReport);
                                foreach (var label in questionReport.Labels) {
                                    dataSet.AddData ("0");
                                    await _dataSetRepository.UpdateAsync (dataSet);
                                }
                            }
                            break;
                        case "linear-scale":
                            {
                                for (var i = 1; i <= fieldData.MaxValue; i++) {
                                    questionReport.AddLabel (i.ToString ());
                                }
                                var dataSet = new DataSet (question.Content);
                                questionReport.AddDataSet (dataSet);
                                await _dataSetRepository.AddAsync (dataSet);
                                await _questionReportRepository.UpdateAsync (questionReport);
                                foreach (var label in questionReport.Labels) {
                                    dataSet.AddData ("0");
                                    await _dataSetRepository.UpdateAsync (dataSet);
                                }
                            }
                            break;
                        case "multiple-grid":
                        case "single-grid":
                            {
                                foreach (var row in fieldData.Rows) {
                                    questionReport.AddLabel (row.Input);
                                }
                                foreach (var choiceOption in fieldData.ChoiceOptions) {
                                    var dataSet = new DataSet (choiceOption.ViewValue);
                                    questionReport.AddDataSet (dataSet);
                                    await _dataSetRepository.AddAsync (dataSet);
                                    await _questionReportRepository.UpdateAsync (questionReport);
                                    foreach (var row in fieldData.Rows) {
                                        dataSet.AddData ("0");
                                        await _dataSetRepository.UpdateAsync (dataSet);
                                    }
                                }
                            }
                            break;
                        case "single-choice":
                        case "multiple-choice":
                            {
                                foreach (var choiceOption in fieldData.ChoiceOptions) {
                                    questionReport.AddLabel (choiceOption.ViewValue);
                                }
                                var dataSet = new DataSet (question.Content);
                                questionReport.AddDataSet (dataSet);
                                await _dataSetRepository.AddAsync (dataSet);
                                await _questionReportRepository.UpdateAsync (questionReport);
                                foreach (var label in questionReport.Labels) {
                                    dataSet.AddData ("0");
                                    await _dataSetRepository.UpdateAsync (dataSet);
                                }
                            }
                            break;
                    }
                }
            }
            return surveyReport.Id;
        }
    }
}