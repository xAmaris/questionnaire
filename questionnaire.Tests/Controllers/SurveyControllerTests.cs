using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Commands.SurveyAnswer;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;
using questionnaire.Infrastructure.Services.Interfaces;
using Xunit;

namespace questionnaire.Tests.Controllers
{
    public class SurveyControllerTests : IClassFixture<TestHostFixture>
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private readonly string _email;

        public SurveyControllerTests(TestHostFixture fixture)
        {
            _client = fixture.Client;
            _factory = fixture._factory;
            _email = fixture.Email;
        }

        [Fact]
        public async Task GetSurveyAndCreateSurveyAnswer()
        {

            var response = await _client.GetAsync("/api/surveyTemplate/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var templateSurvey = JsonConvert.DeserializeObject<SurveyTemplate>(response.Content.ReadAsStringAsync().Result);

            var questionList = templateSurvey.QuestionTemplates.ToList().Select(question => new QuestionAnswerToAdd()
            {
                QuestionPosition = question.QuestionPosition,
                Content = question.Content,
                Select = question.Select,
                IsRequired = question.IsRequired,
                FieldData = question.FieldDataTemplates.ToList().Select(fieldData => new FieldDataAnswerToAdd()
                {
                    Input = fieldData.Input,
                    MinLabel = fieldData.MinLabel,
                    MaxLabel = fieldData.MaxLabel,
                    ChoiceOptions = fieldData.ChoiceOptionTemplates.ToList().Select(choiceOption => new ChoiceOptionAnswerToAdd()
                    {
                        OptionPosition = choiceOption.OptionPosition,
                        Value = choiceOption.Value,
                        ViewValue = choiceOption.ViewValue
                    }).ToList() as ICollection<ChoiceOptionAnswerToAdd>,
                    Rows = fieldData.RowTemplates.ToList().Select(row => new RowAnswerToAdd()
                    {
                        RowPosition = row.RowPosition,
                        Input = row.Input,
                        ChoiceOptions = fieldData.ChoiceOptionTemplates.ToList().Select(rowChoiceOption => new RowChoiceOptionAnswerToAdd()
                        {
                            OptionPosition = rowChoiceOption.OptionPosition,
                            Value = rowChoiceOption.Value,
                            ViewValue = rowChoiceOption.ViewValue
                        }).ToList() as ICollection<RowChoiceOptionAnswerToAdd>
                    }).ToList() as ICollection<RowAnswerToAdd>
                }).ToList() as ICollection<FieldDataAnswerToAdd>
            }).ToList() as ICollection<QuestionAnswerToAdd>;



            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<QuestionnaireContext>();

                ISurveyUserIdentifierRepository _surveyUserIdentifierRepository = new SurveyUserIdentifierRepository(context);
                ISurveyReportRepository _surveyReportRepository = new SurveyReportRepository(context);
                ISurveyTemplateRepository _surveyTemplateRepository = new SurveyTemplateRepository(context);
                ISurveyRepository _surveyRepository = new SurveyRepository(context);
                IDataSetRepository _dataSetRepository = new DataSetRepository(context);
                IQuestionReportRepository _questionReportRepository = new QuestionReportRepository(context);
                IQuestionRepository _questionRepository = new QuestionRepository(context);
                IFieldDataRepository _fieldDataRepository = new FieldDataRepository(context);
                IChoiceOptionRepository _choiceOptionRepository = new ChoiceOptionRepository(context);
                IRowRepository _rowRepository = new RowRepository(context);

                ISurveyUserIdentifierService _surveyUserIdentifierService = new SurveyUserIdentifierService(_surveyUserIdentifierRepository);
                ISurveyReportService _surveyReportService = new SurveyReportService(_surveyReportRepository, _surveyRepository, _dataSetRepository, _questionReportRepository);
                ISurveyService _surveyService = new SurveyService(_surveyRepository, _questionRepository, _fieldDataRepository, _choiceOptionRepository, _rowRepository, _surveyTemplateRepository, _surveyReportService);

                var surveyId = _surveyService.CreateSurveyAsync(templateSurvey.Id).Result;
                var survey = await _surveyService.GetByIdAsync(surveyId);
                await _surveyReportService.CreateAsync(survey.Id, survey.Title);
                await _surveyUserIdentifierService.CreateAsync(_email, survey.Id);

                var answer = new SurveyAnswerToAdd()
                {
                    SurveyTitle = survey.Title,
                    SurveyId = survey.Id,
                    Questions = questionList
                };

                var identifier = context.SurveyUserIdentifiers.FirstOrDefault(x => x.SurveyId == survey.Id);
                var hash = identifier.UserEmailHash;
                var jsoncontent = JsonConvert.SerializeObject(answer);
                var httpcontent = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
                var answerRes = await _client.PostAsync("/api/surveyAnswer/" + hash, httpcontent);
                answerRes.StatusCode.Should().Be(HttpStatusCode.Created);
            }

        }
    }
}