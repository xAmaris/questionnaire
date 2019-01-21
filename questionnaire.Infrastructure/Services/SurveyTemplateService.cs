using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Commands.Survey;
using questionnaire.Infrastructure.Repositories.Interfaces;

namespace questionnaire.Infrastructure.Services.Interfaces
{
    public class SurveyTemplateService : ISurveyTemplateService
    {

        private readonly ISurveyTemplateRepository _surveyTemplateRepository;
        private readonly IFieldDataTemplateRepository _fieldDataTemplateRepository;
        private readonly IQuestionTemplateRepository _questionTemplateRepository;
        private readonly IRowTemplateRepository _rowTemplateRepository;
        private readonly IChoiceOptionTemplateRepository _choiceOptionTemplateRepository;

        public SurveyTemplateService(ISurveyTemplateRepository surveyTemplateRepository, IFieldDataTemplateRepository fieldDataTemplateRepository, IQuestionTemplateRepository questionTemplateRepository, IRowTemplateRepository rowTemplateRepository, IChoiceOptionTemplateRepository choiceOptionTemplateRepository)
        {
            _surveyTemplateRepository = surveyTemplateRepository;
            _fieldDataTemplateRepository = fieldDataTemplateRepository;
            _questionTemplateRepository = questionTemplateRepository;
            _rowTemplateRepository = rowTemplateRepository;
            _choiceOptionTemplateRepository = choiceOptionTemplateRepository;
        }

        public async Task<int> AddFieldDataToQuestionAsync(int questionTemplateId, string input, int minValue, int maxValue, string minLabel, string maxLabel)
        {
            var questionTemplate = await _questionTemplateRepository.GetByIdAsync(questionTemplateId);
            switch (questionTemplate.Select)
            {
                case "short-answer":
                    {
                        var fieldDataTemplate = new FieldDataTemplate(input);
                        questionTemplate.AddFieldDataTemplate(fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync(fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "long-answer":
                    {
                        var fieldDataTemplate = new FieldDataTemplate(input);
                        questionTemplate.AddFieldDataTemplate(fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync(fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "single-choice":
                    {
                        var fieldDataTemplate = new FieldDataTemplate();
                        questionTemplate.AddFieldDataTemplate(fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync(fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "multiple-choice":
                    {
                        var fieldDataTemplate = new FieldDataTemplate();
                        questionTemplate.AddFieldDataTemplate(fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync(fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "dropdown-menu":
                    {
                        var fieldDataTemplate = new FieldDataTemplate();
                        questionTemplate.AddFieldDataTemplate(fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync(fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "linear-scale":
                    {
                        var fieldDataTemplate = new FieldDataTemplate(minValue, maxValue, minLabel, maxLabel);
                        questionTemplate.AddFieldDataTemplate(fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync(fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "single-grid":
                    {
                        var fieldDataTemplate = new FieldDataTemplate();
                        questionTemplate.AddFieldDataTemplate(fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync(fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                case "multiple-grid":
                    {
                        var fieldDataTemplate = new FieldDataTemplate();
                        questionTemplate.AddFieldDataTemplate(fieldDataTemplate);
                        await _fieldDataTemplateRepository.AddAsync(fieldDataTemplate);
                        return fieldDataTemplate.Id;
                    }
                default:
                    throw new Exception("Invalid select value");
            }
        }

        public async Task<int> AddQuestionToSurveyAsync(int surveyTemplateId, int questionPosition, string content, string select, bool isRequired)
        {
            var surveyTemplate = await _surveyTemplateRepository.GetByIdAsync(surveyTemplateId);
            if (content == "")
                content = "Brak pytania";
            var questionTemplate = new QuestionTemplate(questionPosition, content, select, isRequired);
            surveyTemplate.AddQuestionTemplate(questionTemplate);
            await _questionTemplateRepository.AddAsync(questionTemplate);
            return questionTemplate.Id;
        }

        public Task AddRowAsync(int fieldDataTemplateId, int rowPosition, string input)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync(string title)
        {
            var surveyTemplate = new SurveyTemplate(title);
            await _surveyTemplateRepository.AddAsync(surveyTemplate);
            return surveyTemplate.Id;
        }

        public async Task<int> CreateSurveyAsync(SurveyToAdd command)
        {
            var surveyTemplateId = await CreateAsync(command.Title);
            if (command.Questions == null)
                throw new NullReferenceException("Cannot create empty survey");
            foreach (var question in command.Questions)
            {
                var questionTemplateId = await AddQuestionToSurveyAsync(surveyTemplateId, question.QuestionPosition,
                    question.Content, question.Select, question.IsRequired);
                if (question.FieldData == null)
                    throw new NullReferenceException("Question must contain FieldData");
                foreach (var fieldData in question.FieldData)
                {
                    await AddChoiceOptionsAndRowsAsync(questionTemplateId, question.Select, fieldData);
                }
            }
            return surveyTemplateId;
        }
        private async Task AddChoiceOptionsAndRowsAsync(int questionTemplateId, string select, FieldDataToAdd fieldDataToAdd)
        {
            var fieldDataId = await AddFieldDataToQuestionAsync(questionTemplateId,
                fieldDataToAdd.Input,
                fieldDataToAdd.MinValue,
                fieldDataToAdd.MaxValue,
                fieldDataToAdd.MinLabel,
                fieldDataToAdd.MaxLabel);
            if (select == "single-grid" || select == "multiple-grid")
            {
                await AddRowsAsync(fieldDataToAdd, select, fieldDataId);
                await AddChoiceOptionsAsync(fieldDataToAdd, select, fieldDataId);
            }
            else if (select == "single-choice" || select == "multiple-choice" || select == "dropdown-menu" ||
              select == "single-grid" || select == "multiple-grid")
            {
                await AddChoiceOptionsAsync(fieldDataToAdd, select, fieldDataId);
            }
            else
            {
                await Task.CompletedTask;
            }
        }
        private async Task AddRowsAsync(FieldDataToAdd fieldDataToAdd, string select, int fieldDataId)
        {
            if (fieldDataToAdd.Rows == null)
                await Task.CompletedTask;
            foreach (var row in fieldDataToAdd.Rows)
            {
                await AddRowAsync(fieldDataId, row.RowPosition, row.Input);
            }
        }
        private async Task AddChoiceOptionsAsync(FieldDataToAdd fieldDataToAdd, string select, int fieldDataId)
        {
            if (fieldDataToAdd.ChoiceOptions != null)
            {
                var counter = 0;
                foreach (var choiceOption in fieldDataToAdd.ChoiceOptions)
                {
                    await AddChoiceOptionAsync(fieldDataId, counter,
                        choiceOption.Value, choiceOption.ViewValue);
                    counter++;
                }
            }
        }
        public async Task AddChoiceOptionAsync(int fieldDataTemplateId, int optionPosition, bool value, string viewValue)
        {
            var fieldDataTemplate = await _fieldDataTemplateRepository.GetByIdAsync(fieldDataTemplateId);
            var choiceOptionTemplate = new ChoiceOptionTemplate(optionPosition, value, viewValue);
            fieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplate);
            await _choiceOptionTemplateRepository.AddAsync(choiceOptionTemplate);
        }
        public Task DeleteAsync(int surveyTemplateId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SurveyTemplate>> GetAllAsync()
        {
            IEnumerable<SurveyTemplate> surveyTemplates = await _surveyTemplateRepository.GetAllWithQuestionTemplatesAsync();
            return surveyTemplates;
        }

        public async Task<SurveyTemplate> GetByIdAsync(int surveyTemplateId)
        {
            var surveyTemplate = await _surveyTemplateRepository.GetByIdWithQuestionTemplatesAsync(surveyTemplateId);
            return surveyTemplate;
        }

        public Task<int> UpdateAsync(int surveyTemplateId, string title)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSurveyAsync(SurveyToUpdate command)
        {
            var surveyTemplateId = await UpdateAsync(command.SurveyId, command.Title);
            if (command.Questions == null)
                throw new NullReferenceException("Cannot create empty survey");
            foreach (var question in command.Questions)
            {
                var questionId = await AddQuestionToSurveyAsync(surveyTemplateId, question.QuestionPosition,
                    question.Content, question.Select, question.IsRequired);
                if (question.FieldData == null)
                    throw new NullReferenceException("Question must contain FieldData");
                foreach (var fieldData in question.FieldData)
                {
                    await AddChoiceOptionsAndRowsAsync(questionId, question.Select, fieldData);
                }
            }
        }

    }
}