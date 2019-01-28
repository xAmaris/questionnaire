using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using questionnaire.UnitTests.Startup;

namespace questionnaire.Integration.Tests.Startup {
    public class CustomWebApplicationFactory : WebApplicationFactory<TestsStartup> {
        protected override IWebHostBuilder CreateWebHostBuilder () {
            return WebHost.CreateDefaultBuilder (new string[0])
                .UseStartup<TestsStartup> ();
        }
    }
}