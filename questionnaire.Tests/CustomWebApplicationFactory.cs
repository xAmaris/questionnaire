using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using questionnaire.Api;
using questionnaire.Core.Domains;
using questionnaire.Core.Domains.Surveys;
using questionnaire.Core.Domains.SurveyTemplates;
using questionnaire.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace questionnaire.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private const string Password = "!A123456a";
        private const string Email = "user@user.pl";

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                services.AddDbContext<QuestionnaireContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(serviceProvider);
            });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<QuestionnaireContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        SeedData(appDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
            base.ConfigureWebHost(builder);
        }

        public static void SeedData(QuestionnaireContext context)
        {
            //SurveyTemplates
            SeedSurveyTemplate(context);

            //CareerOffices
            var contextCareerOffice = context.CareerOffices.FirstOrDefault(x => x.Email == Email);
            if (contextCareerOffice is null)
            {
                var careerOffice = new CareerOffice("user", "user", Email, "+48123456789", Password);
                var activation = new AccountActivation(Guid.NewGuid());
                careerOffice.AddAccountActivation(activation);
                careerOffice.Activate(activation);
                context.CareerOffices.Add(careerOffice);
            }


            //Surveys
            var survey = new Survey("survey title");
            context.Surveys.Add(survey);

            context.SaveChanges();
        }

        public static void SeedSurveyTemplate(QuestionnaireContext context)
        {
            var surveyTemplate = new SurveyTemplate("surveyTemplate title");

            //QuestionTemplates
            var shortAnswerQuestion = new QuestionTemplate(0, "1", "short-answer", false);
            var longAnswerQuestion = new QuestionTemplate(1, "2", "long-answer", false);
            var singleChoiceQuestion = new QuestionTemplate(2, "3", "single-choice", false);
            var multipleChoiceQuestion = new QuestionTemplate(3, "4", "multiple-choice", false);
            var dropdownMenuQuestion = new QuestionTemplate(4, "5", "dropdown-menu", false);
            var linearScaleQuestion = new QuestionTemplate(5, "6", "linear-scale", false);
            var singleGridQuestion = new QuestionTemplate(6, "7", "single-grid", false);
            var multipleGridQuestion = new QuestionTemplate(7, "8", "multiple-grid", false);


            //FieldDataTemplates
            var shortAnswerFieldDataTemplate = new FieldDataTemplate("");
            var longAnswerFieldDataTemplate = new FieldDataTemplate("");
            var singleChoiceFieldDataTemplate = new FieldDataTemplate();
            var multipleChoiceFieldDataTemplate = new FieldDataTemplate();
            var dropdownMenuFieldDataTemplate = new FieldDataTemplate();
            var linearScaleFieldDataTemplate = new FieldDataTemplate(1, 5, "minLabel", "maxLabel");
            var singleGridFieldDataTemplate = new FieldDataTemplate();
            var multipleGridFieldDataTemplate = new FieldDataTemplate();


            //ChoiceOptionTemplates
            var choiceOptionTemplateForSingleChoiceFieldDataTemplate1 = new ChoiceOptionTemplate(0, false, "opcja 1");
            var choiceOptionTemplateForSingleChoiceFieldDataTemplate2 = new ChoiceOptionTemplate(1, false, "opcja 2");
            var choiceOptionTemplateForSingleChoiceFieldDataTemplate3 = new ChoiceOptionTemplate(2, false, "opcja 3");

            var choiceOptionTemplateForMultipleChoiceFieldDataTemplate1 = new ChoiceOptionTemplate(0, false, "opcja 1");
            var choiceOptionTemplateForMultipleChoiceFieldDataTemplate2 = new ChoiceOptionTemplate(1, false, "opcja 2");
            var choiceOptionTemplateForMultipleChoiceFieldDataTemplate3 = new ChoiceOptionTemplate(2, false, "opcja 3");

            var choiceOptionTemplateForDropdownMenuFieldDataTemplate1 = new ChoiceOptionTemplate(0, false, "opcja 1");
            var choiceOptionTemplateForDropdownMenuFieldDataTemplate2 = new ChoiceOptionTemplate(1, false, "opcja 2");
            var choiceOptionTemplateForDropdownMenuFieldDataTemplate3 = new ChoiceOptionTemplate(2, false, "opcja 3");

            var choiceOptionTemplateForSingleGridFieldDataTemplate1 = new ChoiceOptionTemplate(0, false, "opcja 1");
            var choiceOptionTemplateForSingleGridFieldDataTemplate2 = new ChoiceOptionTemplate(1, false, "opcja 2");
            var choiceOptionTemplateForSingleGridFieldDataTemplate3 = new ChoiceOptionTemplate(2, false, "opcja 3");

            var choiceOptionTemplateForMultipleGridFieldDataTemplate1 = new ChoiceOptionTemplate(0, false, "opcja 1");
            var choiceOptionTemplateForMultipleGridFieldDataTemplate2 = new ChoiceOptionTemplate(1, false, "opcja 2");
            var choiceOptionTemplateForMultipleGridFieldDataTemplate3 = new ChoiceOptionTemplate(2, false, "opcja 3");


            //RowTemplates
            var rowTemplateForSingleGridFieldDataTemplate1 = new RowTemplate(0, "wiersz 1");
            var rowTemplateForSingleGridFieldDataTemplate2 = new RowTemplate(1, "wiersz 2");
            var rowTemplateForSingleGridFieldDataTemplate3 = new RowTemplate(2, "wiersz 3");

            var rowTemplateForMultipleGridFieldDataTemplate1 = new RowTemplate(0, "wiersz 1");
            var rowTemplateForMultipleGridFieldDataTemplate2 = new RowTemplate(1, "wiersz 2");
            var rowTemplateForMultipleGridFieldDataTemplate3 = new RowTemplate(2, "wiersz 3");


            //AddChoiceOptionTemplatesToFieldDataTemplate
            singleChoiceFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForSingleChoiceFieldDataTemplate1);
            singleChoiceFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForSingleChoiceFieldDataTemplate2);
            singleChoiceFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForSingleChoiceFieldDataTemplate3);

            multipleChoiceFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForMultipleChoiceFieldDataTemplate1);
            multipleChoiceFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForMultipleChoiceFieldDataTemplate2);
            multipleChoiceFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForMultipleChoiceFieldDataTemplate3);

            dropdownMenuFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForDropdownMenuFieldDataTemplate1);
            dropdownMenuFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForDropdownMenuFieldDataTemplate2); dropdownMenuFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForDropdownMenuFieldDataTemplate3);

            singleGridFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForSingleGridFieldDataTemplate1);
            singleGridFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForSingleGridFieldDataTemplate2); singleGridFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForSingleGridFieldDataTemplate3);

            multipleGridFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForMultipleGridFieldDataTemplate1);
            multipleGridFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForMultipleGridFieldDataTemplate2); multipleGridFieldDataTemplate.AddChoiceOptionTemplate(choiceOptionTemplateForMultipleGridFieldDataTemplate3);


            //AddRowTemplatesToFieldDataTemplate
            singleGridFieldDataTemplate.AddRowTemplate(rowTemplateForSingleGridFieldDataTemplate1);
            singleGridFieldDataTemplate.AddRowTemplate(rowTemplateForSingleGridFieldDataTemplate2);
            singleGridFieldDataTemplate.AddRowTemplate(rowTemplateForSingleGridFieldDataTemplate3);

            multipleGridFieldDataTemplate.AddRowTemplate(rowTemplateForMultipleGridFieldDataTemplate1);
            multipleGridFieldDataTemplate.AddRowTemplate(rowTemplateForMultipleGridFieldDataTemplate2);
            multipleGridFieldDataTemplate.AddRowTemplate(rowTemplateForMultipleGridFieldDataTemplate3);


            //AddFieldDataTemplatesToQuestionTemplate
            shortAnswerQuestion.AddFieldDataTemplate(shortAnswerFieldDataTemplate);
            longAnswerQuestion.AddFieldDataTemplate(longAnswerFieldDataTemplate);
            singleChoiceQuestion.AddFieldDataTemplate(singleChoiceFieldDataTemplate);
            multipleChoiceQuestion.AddFieldDataTemplate(multipleChoiceFieldDataTemplate);
            dropdownMenuQuestion.AddFieldDataTemplate(dropdownMenuFieldDataTemplate);
            linearScaleQuestion.AddFieldDataTemplate(linearScaleFieldDataTemplate);
            singleGridQuestion.AddFieldDataTemplate(singleGridFieldDataTemplate);
            multipleGridQuestion.AddFieldDataTemplate(multipleGridFieldDataTemplate);


            //AddQuestionTemplatesToSurveyTemplate
            surveyTemplate.AddQuestionTemplate(shortAnswerQuestion);
            surveyTemplate.AddQuestionTemplate(longAnswerQuestion);
            surveyTemplate.AddQuestionTemplate(singleChoiceQuestion);
            surveyTemplate.AddQuestionTemplate(multipleChoiceQuestion);
            surveyTemplate.AddQuestionTemplate(dropdownMenuQuestion);
            surveyTemplate.AddQuestionTemplate(linearScaleQuestion);
            surveyTemplate.AddQuestionTemplate(singleGridQuestion);
            surveyTemplate.AddQuestionTemplate(multipleGridQuestion);


            //AddChoiceOptionTemplatesToContext
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForSingleChoiceFieldDataTemplate1);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForSingleChoiceFieldDataTemplate2);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForSingleChoiceFieldDataTemplate3);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForMultipleChoiceFieldDataTemplate1);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForMultipleChoiceFieldDataTemplate2);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForMultipleChoiceFieldDataTemplate3);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForDropdownMenuFieldDataTemplate1);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForDropdownMenuFieldDataTemplate2);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForDropdownMenuFieldDataTemplate3);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForSingleGridFieldDataTemplate1);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForSingleGridFieldDataTemplate2);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForSingleGridFieldDataTemplate3);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForMultipleGridFieldDataTemplate1);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForMultipleGridFieldDataTemplate2);
            context.ChoiceOptionTemplates.Add(choiceOptionTemplateForMultipleGridFieldDataTemplate3);


            //AddRowTemplatesToContext
            context.RowTemplates.Add(rowTemplateForSingleGridFieldDataTemplate1);
            context.RowTemplates.Add(rowTemplateForSingleGridFieldDataTemplate2);
            context.RowTemplates.Add(rowTemplateForSingleGridFieldDataTemplate3);
            context.RowTemplates.Add(rowTemplateForMultipleGridFieldDataTemplate1);
            context.RowTemplates.Add(rowTemplateForMultipleGridFieldDataTemplate2);
            context.RowTemplates.Add(rowTemplateForMultipleGridFieldDataTemplate3);


            //AddFieldDataTemplatesToContext
            context.FieldDataTemplates.Add(shortAnswerFieldDataTemplate);
            context.FieldDataTemplates.Add(longAnswerFieldDataTemplate);
            context.FieldDataTemplates.Add(singleChoiceFieldDataTemplate);
            context.FieldDataTemplates.Add(multipleChoiceFieldDataTemplate);
            context.FieldDataTemplates.Add(dropdownMenuFieldDataTemplate);
            context.FieldDataTemplates.Add(linearScaleFieldDataTemplate);
            context.FieldDataTemplates.Add(singleGridFieldDataTemplate);
            context.FieldDataTemplates.Add(multipleGridFieldDataTemplate);


            //AddQuestionTemplatesToContext
            context.QuestionTemplates.Add(shortAnswerQuestion);
            context.QuestionTemplates.Add(longAnswerQuestion);
            context.QuestionTemplates.Add(singleChoiceQuestion);
            context.QuestionTemplates.Add(multipleChoiceQuestion);
            context.QuestionTemplates.Add(dropdownMenuQuestion);
            context.QuestionTemplates.Add(linearScaleQuestion);
            context.QuestionTemplates.Add(singleGridQuestion);
            context.QuestionTemplates.Add(multipleGridQuestion);

            context.SurveyTemplates.Add(surveyTemplate);
        }

    }
}
