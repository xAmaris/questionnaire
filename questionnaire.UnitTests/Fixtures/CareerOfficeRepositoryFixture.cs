//using questionnaire.Core.Domains;
//using questionnaire.Infrastructure.Data;
//using questionnaire.Integration.Tests;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xunit;

//namespace questionnaire.UnitTests.Fixtures
//{
//    public class CareerOfficeRepositoryFixture : IClassFixture<TestHostFixture>
//    {
//        public QuestionnaireContext context;
//        public CareerOfficeRepositoryFixture(TestHostFixture fixture)
//        {
//            context = fixture.Context;
//            AddCareerOffices(context);
//        }
//        private static void AddCareerOffices(QuestionnaireContext context)
//        {
//            context.CareerOffices.Add(new CareerOffice("jan", "nowak", "wp@wp.pl", "+48123456789", "!A123456a"));
//            context.CareerOffices.Add(new CareerOffice("karol", "kowalski", "onet@onet.pl", "+123123123", "!B123456b"));
//            context.SaveChanges();
//        }
//    }
//}
