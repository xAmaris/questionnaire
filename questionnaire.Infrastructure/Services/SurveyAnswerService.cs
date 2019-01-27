using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.SurveysAnswers;
using questionnaire.Infrastructure.Commands.SurveyAnswer;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class SurveyAnswerService : ISurveyAnswerService {
        private readonly ISurveyAnswerRepository _surveyAnswerRepository;
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly IFieldDataAnswerRepository _fieldDataAnswerRepository;
        private readonly IChoiceOptionAnswerRepository _choiceOptionAnswerRepository;
        private readonly IRowAnswerRepository _rowAnswerRepository;
        private readonly IRowChoiceOptionAnswerRepository _rowChoiceOptionAnswerRepository;
        private readonly IQuestionReportRepository _questionReportRepository;
        private readonly ISurveyReportRepository _surveyReportRepository;
        private readonly IDataSetRepository _dataSetRepository;

        public SurveyAnswerService (ISurveyAnswerRepository surveyAnswerRepository,
            IQuestionAnswerRepository questionAnswerRepository,
            IFieldDataAnswerRepository fieldDataAnswerRepository,
            IChoiceOptionAnswerRepository choiceOptionAnswerRepository,
            IRowAnswerRepository rowAnswerRepository,
            IRowChoiceOptionAnswerRepository rowChoiceOptionAnswerRepository,
            IQuestionReportRepository questionReportRepository,
            ISurveyReportRepository surveyReportRepository,
            IDataSetRepository dataSetRepository) {
            _surveyAnswerRepository = surveyAnswerRepository;
            _questionAnswerRepository = questionAnswerRepository;
            _fieldDataAnswerRepository = fieldDataAnswerRepository;
            _choiceOptionAnswerRepository = choiceOptionAnswerRepository;
            _rowAnswerRepository = rowAnswerRepository;
            _rowChoiceOptionAnswerRepository = rowChoiceOptionAnswerRepository;
            _questionReportRepository = questionReportRepository;
            _surveyReportRepository = surveyReportRepository;
            _dataSetRepository = dataSetRepository;
        }

        public async Task<int> CreateSurveyAnswerAsync (SurveyAnswerToAdd command) {
            var surveyAnswerId = await CreateAsync (command.SurveyTitle, command.SurveyId);
            if (command.Questions == null)
                throw new NullReferenceException ("Cannot create empty survey");
            foreach (var questionAnswer in command.Questions) {
                await AddChoiceOptionsAnswerAndRowAnswerAsync (command.SurveyId, surveyAnswerId, questionAnswer.Select,
                    questionAnswer);
            }
            return surveyAnswerId;
        }

        private async Task AddChoiceOptionsAnswerAndRowAnswerAsync (int surveyId, int surveyAnswerId,
            string select, QuestionAnswerToAdd questionAnswer) {
            var questionAnswerId = await AddQuestionAnswerToSurveyAnswerAsync (surveyId,
                surveyAnswerId,
                questionAnswer.QuestionPosition, questionAnswer.Content, questionAnswer.Select, questionAnswer.IsRequired);
            if (questionAnswer.FieldData == null)
                throw new NullReferenceException ("Question must contain FieldData");
            foreach (var fieldDataAnswer in questionAnswer.FieldData) {
                var fieldDataAnswerId = await AddFieldDataAnswerToQuestionAnswerAsync (surveyId,
                    questionAnswerId,
                    fieldDataAnswer.Input,
                    fieldDataAnswer.MinLabel,
                    fieldDataAnswer.MaxLabel);
                if (fieldDataAnswer.ChoiceOptions != null)
                    await AddChoiceOptionsAnswerAsync (surveyId, fieldDataAnswer, questionAnswer.Select,
                        fieldDataAnswerId,
                        questionAnswer);
                if (fieldDataAnswer.Rows != null)
                    await AddRowsAnswerAsync (surveyId, fieldDataAnswer, questionAnswer.Select, questionAnswer,
                        fieldDataAnswerId);
            }
        }

        private async Task AddChoiceOptionsAnswerAsync (int surveyId, FieldDataAnswerToAdd fieldDataAnswer,
            string select, int fieldDataAnswerId, QuestionAnswerToAdd questionAnswer) {
            if (questionAnswer.Select == "single-choice" || questionAnswer.Select == "multiple-choice" ||
                questionAnswer.Select == "dropdown-menu" || questionAnswer.Select == "linear-scale") {
                var counter = 0;
                foreach (var choiceOption in fieldDataAnswer.ChoiceOptions) {
                    await AddChoiceOptionsAnswerToFieldDataAnswerAsync (surveyId, fieldDataAnswerId,
                        counter,
                        choiceOption.Value, choiceOption.ViewValue);
                    counter++;
                }
            }
        }

        private async Task AddRowsAnswerAsync (int surveyId, FieldDataAnswerToAdd fieldDataAnswer,
            string select, QuestionAnswerToAdd questionAnswer, int fieldDataAnswerId) {
            if (fieldDataAnswer.Rows != null) {
                foreach (var rowAnswer in fieldDataAnswer.Rows) {
                    var rowAnswerId = await AddRowAnswerAsync (fieldDataAnswerId,
                        rowAnswer.RowPosition, rowAnswer.Input);
                    if (rowAnswer.ChoiceOptions != null) {
                        await AddChoiceOptionAnswerToRow (surveyId, rowAnswer, rowAnswerId);
                    }
                }
            }
        }

        private async Task AddChoiceOptionAnswerToRow (int surveyId, RowAnswerToAdd rowAnswer, int rowAnswerId) {
            var counter = 0;
            foreach (var choiceOption in rowAnswer.ChoiceOptions) {
                await AddChoiceOptionAnswerToRowAnswerAsync (surveyId, rowAnswerId,
                    choiceOption.OptionPosition,
                    choiceOption.Value, choiceOption.ViewValue);
                counter++;
            }
            await Task.CompletedTask;
        }

        public async Task<int> CreateAsync (string surveyTitle, int surveyId) {
            var surveyAnswer = new SurveyAnswer (surveyTitle, surveyId);
            await _surveyAnswerRepository.AddAsync (surveyAnswer);
            var surveyReport = await _surveyReportRepository.GetBySurveyIdAsync (surveyId);
            surveyReport.AddAnswer ();
            return surveyAnswer.Id;
        }

        public async Task<int> AddQuestionAnswerToSurveyAnswerAsync (int surveyId, int surveyAnswerId,
            int questionPosition,
            string content, string select, bool isRequired) {
            var surveyAnswer = await _surveyAnswerRepository.GetByIdAsync (surveyAnswerId);
            var questionAnswer = new QuestionAnswer (questionPosition, content, select, isRequired);
            surveyAnswer.AddQuestionAnswer (questionAnswer);
            await _questionAnswerRepository.AddAsync (questionAnswer);
            var surveyReport = await _surveyReportRepository.GetBySurveyIdAsync (surveyId);
            var questionReport =
                await _questionReportRepository.GetBySurveyReportContentAndPositionAsync (surveyReport.Id,
                    questionAnswer.QuestionPosition, questionAnswer.Select);
            questionReport.AddAnswer ();

            return questionAnswer.Id;
        }

        public async Task<int> AddFieldDataAnswerToQuestionAnswerAsync (int surveyId, int questionAnswerId, string input,
            string minLabel, string maxLabel) {
            var surveyReport = await _surveyReportRepository.GetBySurveyIdAsync (surveyId);
            var questionAnswer = await _questionAnswerRepository.GetByIdAsync (questionAnswerId);
            switch (questionAnswer.Select) {
                case "short-answer":
                    {
                        var fieldDataAnswer = new FieldDataAnswer (input);
                        questionAnswer.AddFieldDataAnswer (fieldDataAnswer);
                        await _fieldDataAnswerRepository.AddAsync (fieldDataAnswer);
                        var questionReport = await _questionReportRepository.GetBySurveyReportAsync (surveyReport.Id,
                            questionAnswer.Content, questionAnswer.Select);
                        foreach (var dataSet in questionReport.DataSets) {
                            if (!questionAnswer.IsRequired) {
                                if (input == "")
                                    questionReport.DeleteAnswer ();
                            }
                            dataSet.AddData (fieldDataAnswer.Input);
                            await _dataSetRepository.UpdateAsync (dataSet);
                        }
                        return fieldDataAnswer.Id;
                    }
                case "long-answer":
                    {
                        var fieldDataAnswer = new FieldDataAnswer (input);
                        questionAnswer.AddFieldDataAnswer (fieldDataAnswer);
                        await _fieldDataAnswerRepository.AddAsync (fieldDataAnswer);
                        var questionReport =
                            await _questionReportRepository.GetBySurveyReportAsync (surveyReport.Id, questionAnswer.Content,
                                questionAnswer.Select);
                        foreach (var dataSet in questionReport.DataSets) {
                            if (!questionAnswer.IsRequired) {
                                if (input == "")
                                    questionReport.DeleteAnswer ();
                            }
                            dataSet.AddData (fieldDataAnswer.Input);
                            await _dataSetRepository.UpdateAsync (dataSet);
                        }
                        return fieldDataAnswer.Id;
                    }
                case "single-choice":
                    {
                        var fieldDataAnswer = new FieldDataAnswer ();
                        questionAnswer.AddFieldDataAnswer (fieldDataAnswer);
                        await _fieldDataAnswerRepository.AddAsync (fieldDataAnswer);
                        return fieldDataAnswer.Id;
                    }
                case "multiple-choice":
                    {
                        var fieldDataAnswer = new FieldDataAnswer ();
                        questionAnswer.AddFieldDataAnswer (fieldDataAnswer);
                        await _fieldDataAnswerRepository.AddAsync (fieldDataAnswer);
                        return fieldDataAnswer.Id;
                    }
                case "dropdown-menu":
                    {
                        var fieldDataAnswer = new FieldDataAnswer ();
                        questionAnswer.AddFieldDataAnswer (fieldDataAnswer);
                        await _fieldDataAnswerRepository.AddAsync (fieldDataAnswer);
                        return fieldDataAnswer.Id;
                    }
                case "linear-scale":
                    {
                        var fieldDataAnswer = new FieldDataAnswer (minLabel, maxLabel);
                        questionAnswer.AddFieldDataAnswer (fieldDataAnswer);
                        await _fieldDataAnswerRepository.AddAsync (fieldDataAnswer);
                        return fieldDataAnswer.Id;
                    }
                case "single-grid":
                    {
                        var fieldDataAnswer = new FieldDataAnswer ();
                        questionAnswer.AddFieldDataAnswer (fieldDataAnswer);
                        await _fieldDataAnswerRepository.AddAsync (fieldDataAnswer);
                        return fieldDataAnswer.Id;
                    }
                case "multiple-grid":
                    {
                        var fieldDataAnswer = new FieldDataAnswer ();
                        questionAnswer.AddFieldDataAnswer (fieldDataAnswer);
                        await _fieldDataAnswerRepository.AddAsync (fieldDataAnswer);
                        return fieldDataAnswer.Id;
                    }
                default:
                    throw new Exception ("invalid select value");
            }
        }

        public async Task AddChoiceOptionsAnswerToFieldDataAnswerAsync (int surveyId, int fieldDataAnswerId,
            int optionPosition,
            bool value, string viewValue) {
            var fieldDataAnswer = await _fieldDataAnswerRepository.GetByIdAsync (fieldDataAnswerId);
            var choiceOptionAnswer = new ChoiceOptionAnswer (optionPosition, value, viewValue);
            fieldDataAnswer.AddChoiceOptionAnswer (choiceOptionAnswer);
            await _choiceOptionAnswerRepository.AddAsync (choiceOptionAnswer);
            var questionAnswer = await _questionAnswerRepository.GetByIdAsync (fieldDataAnswer.QuestionAnswerId);
            var surveyReport = await _surveyReportRepository.GetBySurveyIdAsync (surveyId);
            var questionReport =
                await _questionReportRepository.GetBySurveyReportContentAndPositionAsync (surveyReport.Id,
                    questionAnswer.QuestionPosition,
                    questionAnswer.Select);
            if (!questionAnswer.IsRequired) {
                var counter = 0;
                foreach (var ChoiceOptionAnswer in fieldDataAnswer.ChoiceOptionAnswers) {
                    if (ChoiceOptionAnswer.Value) {
                        counter++;
                    }
                }
                if (counter == 0) {
                    questionReport.DeleteAnswer ();
                    await Task.CompletedTask;
                }
            }
            if (choiceOptionAnswer.Value) {
                if (questionAnswer.Content != "") {
                    foreach (var dataSet in questionReport.DataSets) {
                        var index = dataSet._data[choiceOptionAnswer.OptionPosition];
                        var labelCounter = int.Parse (index);
                        labelCounter++;
                        dataSet._data[choiceOptionAnswer.OptionPosition] = labelCounter.ToString ();
                        await _dataSetRepository.UpdateAsync (dataSet);
                    }
                }
            }
        }

        public async Task AddChoiceOptionAnswerToRowAnswerAsync (int surveyId, int rowAnswerId, int optionPosition,
            bool value,
            string viewValue) {
            var rowAnswer = await _rowAnswerRepository.GetByIdAsync (rowAnswerId);
            var fieldDataAnswer = await _fieldDataAnswerRepository.GetByIdAsync (rowAnswer.FieldDataAnswerId);
            var rowChoiceOptionAnswer = new RowChoiceOptionAnswer (optionPosition, value, viewValue);
            rowAnswer.AddChoiceOptionAnswer (rowChoiceOptionAnswer);
            await _rowChoiceOptionAnswerRepository.AddAsync (rowChoiceOptionAnswer);
            var questionAnswer = await _questionAnswerRepository.GetByIdAsync (fieldDataAnswer.QuestionAnswerId);
            var surveyReport = await _surveyReportRepository.GetBySurveyIdAsync (surveyId);
            var questionReport =
                await _questionReportRepository.GetBySurveyReportContentAndPositionAsync (surveyReport.Id,
                    questionAnswer.QuestionPosition,
                    questionAnswer.Select);
            if (!questionAnswer.IsRequired) {
                var counter = 0;
                foreach (var RowChoiceOptionAnswer in rowAnswer.RowChoiceOptionAnswers) {
                    if (RowChoiceOptionAnswer.Value) {
                        counter++;
                    }
                }
                if (counter == 0) {
                    questionReport.DeleteAnswer ();
                    await Task.CompletedTask;
                }
            }
            if (rowChoiceOptionAnswer.Value == true) {
                foreach (var dataSet in questionReport.DataSets) {
                    if (dataSet.Label != rowChoiceOptionAnswer.ViewValue) continue;
                    var index = dataSet._data[rowChoiceOptionAnswer.RowAnswer.RowPosition];
                    var labelCounter = int.Parse (index);
                    labelCounter++;
                    dataSet._data[rowAnswer.RowPosition] = labelCounter.ToString ();
                    await _dataSetRepository.UpdateAsync (dataSet);
                }
            }
        }

        public async Task<int> AddRowAnswerAsync (int fieldDataId, int rowPosition, string input) {
            var fieldDataAnswer = await _fieldDataAnswerRepository.GetByIdAsync (fieldDataId);
            var rowAnswer = new RowAnswer (rowPosition, input);
            fieldDataAnswer.AddRow (rowAnswer);
            await _rowAnswerRepository.AddAsync (rowAnswer);
            return rowAnswer.Id;
        }

        public async Task DeleteAsync (int surveyAnswerId) {
            var surveyAnswer = await _surveyAnswerRepository.GetByIdAsync (surveyAnswerId);
            await _surveyAnswerRepository.DeleteAsync (surveyAnswer);
        }
    }
}