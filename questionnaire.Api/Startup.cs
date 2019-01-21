using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
// using questionnaire.Core.Repositories;
using questionnaire.Infrastructure.Data;
// using questionnaire.Infrastructure.Extensions.AutoMapper;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
             options.SerializerSettings.Formatting = Formatting.Indented);

            services.AddCors();
            services.AddDbContext<QuestionnaireContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("QuestionnaireDatabase"),
                   b => b.MigrationsAssembly("questionnaire.Api")));
            // services.AddSingleton (AutoMapperConfig.Initialize ());

            //repositories
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISurveyTemplateRepository, SurveyTemplateRepository>();
            services.AddScoped<IFieldDataTemplateRepository, FieldDataTemplateRepository>();
            services.AddScoped<IQuestionTemplateRepository, QuestionTemplateRepository>();
            services.AddScoped<IRowTemplateRepository, RowTemplateRepository>();
            services.AddScoped<IChoiceOptionTemplateRepository, ChoiceOptionTemplateRepository>();
            // services
            services.AddScoped<ISurveyTemplateService, SurveyTemplateService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}