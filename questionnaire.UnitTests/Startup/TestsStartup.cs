using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using questionnaire.Infrastructure.Data;

namespace questionnaire.UnitTests.Startup {
    public class TestsStartup : Api.Startup {
        public TestsStartup (IConfiguration configuration) : base (configuration) { }
        public override void ConfigureDatabase (IServiceCollection services) {
            services.AddDbContext<QuestionnaireContext> (options =>
                options.UseInMemoryDatabase ("TestsDB"));
        }
        public override void SeedData (IServiceScope serviceScope) {
            var context = serviceScope.ServiceProvider.GetService<QuestionnaireContext> ();
            var data = new TestData ();
            data.SeedData (context);
        }

    }
}