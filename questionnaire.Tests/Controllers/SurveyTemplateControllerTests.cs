using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using questionnaire.Core.Domains.SurveyReport;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Commands.SurveyAnswer;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;
using questionnaire.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace questionnaire.Tests.Controllers
{
    public class SurveyTemplateControllerTests : IClassFixture<TestHostFixture>
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private readonly string _email;

        public SurveyTemplateControllerTests(TestHostFixture fixture)
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

            var survey = JsonConvert.DeserializeObject<SurveyTemplate>(response.Content.ReadAsStringAsync().Result);

            var questionList = survey.QuestionTemplates.ToList().Select(question => new QuestionAnswerToAdd()
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

            var answer = new SurveyAnswerToAdd()
            {
                SurveyTitle = survey.Title,
                SurveyId = survey.Id,
                Questions = questionList
            };

            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<QuestionnaireContext>();

                ISurveyReportRepository _surveyReportRepository = new SurveyReportRepository(context);
                ISurveyUserIdentifierRepository _surveyUserIdentifierRepository = new SurveyUserIdentifierRepository(context);
                ISurveyUserIdentifierService _surveyUserIdentifierService = new SurveyUserIdentifierService(_surveyUserIdentifierRepository);
                ISurveyReportService _surveyReportService = new SurveyReportService(_surveyUserIdentifierRepository);
                await _surveyUserIdentifierService.CreateAsync(_email, survey.Id);
                await _surveyReportService.CreateAsync(survey.Id, survey.Title);
                var jsoncontent = JsonConvert.SerializeObject(answer);
                var httpcontent = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
                var answerRes = await _client.PostAsync("/api/surveyAnswer/" + hash.UserEmailHash, httpcontent);
                answerRes.StatusCode.Should().Be(HttpStatusCode.OK);
            }

        }
    }
}
