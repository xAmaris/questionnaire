using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Commands.Survey;
using questionnaire.Infrastructure.Extensions.ExceptionHandling;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Infrastructure.Services {
    public class SurveyTemplateService : ISurveyTemplateService {
        private readonly ISurveyTemplateRepository _surveyTemplateRepository;
        private readonly IQuestionTemplateRepository _questionTemplateRepository;
        private readonly IFieldDataTemplateRepository _fieldDataTemplateRepository;
        private readonly IChoiceOptionTemplateRepository _choiceOptionTemplateRepository;
        private readonly IRowTemplateRepository _rowTemplateRepository;

        public SurveyTemplateService (
            ISurveyTemplateRepository surveyTemplateRepository,
            IQuestionTemplateRepository questionTemplateRepository,
            IFieldDataTemplateRepository fieldDataTemplateRepository,
            IChoiceOptionTemplateRepository choiceOptionTemplateRepository,
            IRowTemplateRepository rowTemplateRepository) {
            _surveyTemplateRepository = surveyTemplateRepository;
            _questionTemplateRepository = questionTemplateRepository;
            _fieldDataTemplateRepository = fieldDataTemplateRepository;
            _choiceOptionTemplateRepository = choiceOptionTemplateRepository;
            _rowTemplateRepository = rowTemplateRepository;
        }

        public async Task<int> CreateSurveyTemplateAsync (SurveyToAdd command) {
            var surveyTemplateId = await CreateAsync (command.Title);
            if (command.Questions == null)
                throw new NullReferenceException ("Cannot create empty survey");
            foreach (var question in command.Questions) {
                var questionTemplateId = await AddQuestionToSurveyAsync (surveyTemplateId, question.QuestionPosition,
                    question.Content, question.Select, question.IsRequired);
                if (question.FieldData == null)
                    throw new NullReferenceException ("Question must contain FieldData");
                foreach (var fieldData in question.FieldData) {
                    await AddChoiceOptionsAndRowsAsync (questionTemplateId, question.Select, fieldData);
                }
            }
            return surveyTemplateId;
        }

        public async Task UpdateSurveyTemplateAsync (SurveyToUpdate command) {
            var surveyTemplateId = await UpdateAsync (command.SurveyId, command.Title);
            if (command.Questions == null)
                throw new NullReferenceException ("Cannot create empty survey");
            foreach (var question in command.Questions) {
                var questionId = await AddQuestionToSurveyAsync (surveyTemplateId, question.QuestionPosition,
                    question.Content, question.Select, question.IsRequired);
                if (question.FieldData == null)
                    throw new NullReferenceException ("Question must contain FieldData");
                foreach (var fieldData in question.FieldData) {
                    await AddChoiceOptionsAndRowsAsync (questionId, question.Select, fieldData);
                }
            }
        }

        private async Task AddChoiceOptionsAndRowsAsync (int questionTemplateId, string select, FieldDataToAdd fieldDataToAdd) {
            var fieldDataId = await AddFieldDataTemplateToQuestionTemplateAsync (questionTemplateId,
                fieldDataToAdd.Input,
                fieldDataToAdd.MinValue,
                fieldDataToAdd.MaxValue,
                fieldDataToAdd.MinLabel,
                fieldDataToAdd.MaxLabel);
            if (select == "single-grid" || select == "multiple-grid") {
                await AddRowsAsync (fieldDataToAdd, select, fieldDataId);
                await AddChoiceOptionsAsync (fieldDataToAdd, select, fieldDataId);
            } else if (select == "single-choice" || select == "multiple-choice" || select == "dropdown-menu" ||
                select == "single-grid" || select == "multiple-grid")
                await AddChoiceOptionsAsync (fieldDataToAdd, select, fieldDataId);
            else
                await Task.CompletedTask;
        }
        private async Task AddChoiceOptionsAsync (FieldDataToAdd fieldDataToAdd, string select, int fieldDataId) {
            if (fieldDataToAdd.ChoiceOptions != null) {
                var counter = 0;
                foreach (var choiceOption in fieldDataToAdd.ChoiceOptions) {
                    await AddChoiceOptionsAsync (fieldDataId, counter,
                        choiceOption.Value, choiceOption.ViewValue);
                    counter++;
                }
            }
        }

        private async Task AddRowsAsync (FieldDataToAdd fieldDataToAdd, string select, int fieldDataId) {
            foreach (var row in fieldDataToAdd.Rows) {
                await AddRowAsync (fieldDataId, row.RowPosition, row.Input);
            }
        }

        async Task<int> CreateAsync (string title) {
            var surveyTemplate = new SurveyTemplate (title);
            await _surveyTemplateRepository.AddAsync (surveyTemplate);
            return surveyTemplate.Id;
        }

        async Task<int> AddQuestionToSurveyAsync (int surveyTemplateId, int questionPosition, string content,
            string select, bool isRequired) {
            var surveyTemplate = await _surveyTemplateRepository.GetByIdAsync (surveyTemplateId);
            if (content == "")
                content = "Brak pytania";
            var questionTemplate = new QuestionTemplate (questionPosition, content, select, isRequired);
            surveyTemplate.AddQuestionTemplate (questionTemplate);
            await _questionTemplateRepository.AddAsync (questionTemplate);
            return questionTemplate.Id;
        }

        async Task<int> AddFieldDataTemplateToQuestionTemplateAsync (int questionTemplateId, string input, int minValue, int maxValue,
            string minLabel, string maxLabel) {
            var questionTemplate = await _questionTemplateRepository.GetByIdAsync (questionTemplateId);
            switch (questionTemplate.Select) {
                case "short-answer":
                    {
                        var fieldDataTemplate = new FieldDataTemplate (input);
                        questionTemplate.AddFieldDataTemplate (fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync (fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "long-answer":
                    {
                        var fieldDataTemplate = new FieldDataTemplate (input);
                        questionTemplate.AddFieldDataTemplate (fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync (fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "single-choice":
                    {
                        var fieldDataTemplate = new FieldDataTemplate ();
                        questionTemplate.AddFieldDataTemplate (fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync (fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "multiple-choice":
                    {
                        var fieldDataTemplate = new FieldDataTemplate ();
                        questionTemplate.AddFieldDataTemplate (fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync (fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "dropdown-menu":
                    {
                        var fieldDataTemplate = new FieldDataTemplate ();
                        questionTemplate.AddFieldDataTemplate (fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync (fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "linear-scale":
                    {
                        var fieldDataTemplate = new FieldDataTemplate (minValue, maxValue, minLabel, maxLabel);
                        questionTemplate.AddFieldDataTemplate (fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync (fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "single-grid":
                    {
                        var fieldDataTemplate = new FieldDataTemplate ();
                        questionTemplate.AddFieldDataTemplate (fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync (fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "multiple-grid":
                    {
                        var fieldDataTemplate = new FieldDataTemplate ();
                        questionTemplate.AddFieldDataTemplate (fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync (fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                default:
                    throw new InvalidValueException ("Invalid select value");
            }
        }

        async Task AddChoiceOptionsAsync (int fieldDataTemplateId, int optionPosition, bool value, string viewValue) {
            var fieldDataTemplate = await _fieldDataTemplateRepository.GetByIdAsync (fieldDataTemplateId);
            var choiceOptionTemplate = new ChoiceOptionTemplate (optionPosition, value, viewValue);
            fieldDataTemplate.AddChoiceOptionTemplate (choiceOptionTemplate);
            await _choiceOptionTemplateRepository.AddAsync (choiceOptionTemplate);
        }

        async Task AddRowAsync (int fieldDataTemplateId, int rowPosition, string input) {
            var fieldDataTemplate = await _fieldDataTemplateRepository.GetByIdAsync (fieldDataTemplateId);
            var rowTemplate = new RowTemplate (rowPosition, input);
            fieldDataTemplate.AddRowTemplate (rowTemplate);
            await _rowTemplateRepository.AddAsync (rowTemplate);
        }

        public async Task<IEnumerable<SurveyTemplate>> GetAllSurveyTemplatesAsync () {
            IEnumerable<SurveyTemplate> surveyTemplates = await _surveyTemplateRepository.GetAllWithQuestionTemplatesAsync ();
            return surveyTemplates;
        }

        public async Task<SurveyTemplate> GetSurveyTemplateByIdAsync (int surveyTemplateId) {
            var surveyTemplate = await _surveyTemplateRepository.GetByIdWithQuestionTemplatesAsync (surveyTemplateId);
            return surveyTemplate;
        }

        async Task<int> UpdateAsync (int surveyTemplateId, string title) {
            var surveyTemplate = await _surveyTemplateRepository.GetByIdWithQuestionTemplatesAsync (surveyTemplateId);
            foreach (var questionTemplate in surveyTemplate.QuestionTemplates.ToList ()) {
                await _questionTemplateRepository.DeleteAsync (questionTemplate);
            }
            surveyTemplate.Update (title);
            await _surveyTemplateRepository.UpdateAsync (surveyTemplate);
            return surveyTemplate.Id;
        }

        public async Task DeleteSurveyTemplateAsync (int surveyTemplateId) {
            try {
                var surveyTemplate = await _surveyTemplateRepository.GetByIdAsync (surveyTemplateId);
                await _surveyTemplateRepository.DeleteAsync (surveyTemplate);
            } catch (ArgumentNullException) {
                throw new ObjectDoesNotExistException ($"Survey with given Id: {surveyTemplateId} does not exist.");
            }
        }
    }
}