//using System;
//using Microsoft.EntityFrameworkCore;
//using questionnaire.Infrastructure.Data;

//namespace questionnaire.Integration.Tests
//{
//    public class TestHostFixture : IDisposable
//    {

//        public QuestionnaireContext Context => InMemoryContext();
//        private static QuestionnaireContext InMemoryContext()
//        {
//            var options = new DbContextOptionsBuilder<QuestionnaireContext>()
//                .UseInMemoryDatabase("TestsDB")
//                .Options;
//            var context = new QuestionnaireContext(options);

//            return context;
//        }
//        public void Dispose()
//        {
//            Context?.Dispose();
//        }
//    }
//}