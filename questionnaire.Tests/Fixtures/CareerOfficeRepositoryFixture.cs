using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Data;
using Xunit;

namespace questionnaire.Tests.Fixtures
{
    public class CareerOfficeRepositoryFixture : IClassFixture<TestHostFixture>
    {
        public TestHostFixture _fixture;
        public CareerOfficeRepositoryFixture(TestHostFixture fixture)
        {
            _fixture = fixture;
            AddCareerOffices(_fixture);
        }
        private static void AddCareerOffices(TestHostFixture fixture)
        {
            var context = fixture.Context;
            context.CareerOffices.Add(new CareerOffice("jan", "nowak", "wp@wp.pl", "+48123456789", "!A123456a"));
            context.CareerOffices.Add(new CareerOffice("karol", "kowalski", "onet@onet.pl", "+123123123", "!B123456b"));
            context.SaveChanges();
        }
    }
}
