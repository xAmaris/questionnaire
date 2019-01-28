using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using questionnaire.UnitTests.Startup;
using Xunit;

namespace questionnaire.Integration.Tests {
    public class TestHostFixture : ICollectionFixture<WebApplicationFactory<TestsStartup>> {

        public TestHostFixture () {

        }
    }
}