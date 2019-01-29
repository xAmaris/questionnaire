using System;
using Microsoft.EntityFrameworkCore;
using questionnaire.Core.Domains;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;

namespace questionnaire.Tests
{
    public class TestHostFixture : IDisposable
    {

        public QuestionnaireContext Context;
        public TestHostFixture()
        {
            var options = new DbContextOptionsBuilder<QuestionnaireContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new QuestionnaireContext(options);
            SeedData(context);
            Context = context;
        }
        private static void SeedData(QuestionnaireContext context)
        {
            var careerOffice = new CareerOffice("user", "user", "user@user.pl", "+48123456789", "!A123456a");
            context.CareerOffices.AddAsync(careerOffice);
            var surveyTemplate = new SurveyTemplate("new title");
            context.SurveyTemplates.AddAsync(surveyTemplate);
            context.SaveChanges();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}