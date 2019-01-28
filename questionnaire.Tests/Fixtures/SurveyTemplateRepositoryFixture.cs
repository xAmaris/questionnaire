using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using Xunit;

namespace questionnaire.Tests.Fixtures
{
    public class SurveyTemplateRepositoryFixture : IClassFixture<TestHostFixture>
    {
        TestHostFixture _fixtureWithSurveyTemplates;
        public QuestionnaireContext context;
        public SurveyTemplateRepositoryFixture(TestHostFixture fixture)
        {
            _fixtureWithSurveyTemplates = fixture;
            context = fixture.Context;
            AddSurveyTemplates(_fixtureWithSurveyTemplates);
        }
        private static void AddSurveyTemplates(TestHostFixture fixture)
        {
            var context = fixture.Context;
            context.SurveyTemplates.Add(new SurveyTemplate("Brak nazwy"));
            context.SaveChanges();
        }
    }
}
