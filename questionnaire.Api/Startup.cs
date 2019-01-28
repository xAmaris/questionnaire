using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using questionnaire.Api.ActionFilters;
using questionnaire.Core.Domains.ImportFile;
using questionnaire.Infrastructure.Commands.Account;
using questionnaire.Infrastructure.Commands.CareerOffice;
using questionnaire.Infrastructure.Commands.Email;
using questionnaire.Infrastructure.Commands.ImportFile;
using questionnaire.Infrastructure.Commands.User;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Extension.JWT;
using questionnaire.Infrastructure.Extension.JWT.Interfaces;
using questionnaire.Infrastructure.Extensions.AutoMapper;
using questionnaire.Infrastructure.Extensions.Email;
using questionnaire.Infrastructure.Extensions.Email.Interfaces;
using questionnaire.Infrastructure.Extensions.Factories;
using questionnaire.Infrastructure.Extensions.Factories.Interfaces;
using questionnaire.Infrastructure.Extensions.URL;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;
using questionnaire.Infrastructure.Services.Interfaces;
using questionnaire.Infrastructure.Validators.Account;
using questionnaire.Infrastructure.Validators.CareerOffice;
using questionnaire.Infrastructure.Validators.Email;
using questionnaire.Infrastructure.Validators.ImportFile;
using questionnaire.Infrastructure.Validators.User;
using static questionnaire.Infrastructure.Extension.Exception.ExceptionsHelper;
using NLog.Extensions.Logging;
using NLog.Web;
using questionnaire.Infrastructure.Extensions.Aggregate;
using questionnaire.Infrastructure.Extensions.Aggregate.Interfaces;
using questionnaire.Infrastructure.Extensions.JWT;

namespace questionnaire.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc (opt => {
                    opt.Filters.Add (typeof (ValidatorActionFilter));
                }).AddFluentValidation ()
                .AddJsonOptions (options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            #region DbContextAndSettings

            services.AddCors ();
            ConfigureDatabase (services);
            var key = Encoding.ASCII.GetBytes (Configuration.GetSection ("JWTSettings:Key").Value);
            services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer (options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (key),
                    ValidateIssuer = false,
                    ValidateAudience = false

                    };
                });
            services.AddSingleton<IJWTSettings> (Configuration.GetSection ("JWTSettings").Get<JWTSettings> ());
            services.AddSingleton<IEmailConfiguration> (Configuration.GetSection ("EmailConfiguration")
                .Get<EmailConfiguration> ());
            services.AddSingleton<IURLSettings> (Configuration.GetSection ("Url").Get<URLSettings> ());
            services.AddSingleton (AutoMapperConfig.Initialize ());
            services.AddAuthorization (options => options.AddPolicy ("student", policy => policy.RequireRole ("student")));
            services.AddAuthorization (options =>
                options.AddPolicy ("careerOffice", policy => policy.RequireRole ("careerOffice")));
            services.AddAuthorization (options =>
                options.AddPolicy ("unregisteredUser", policy => policy.RequireRole ("unregisteredUser")));

            #endregion
            #region Repositories

            services.AddScoped<IAccountRepository, AccountRepository> ();
            services.AddScoped<IStudentRepository, StudentRepository> ();
            services.AddScoped<ICareerOfficeRepository, CareerOfficeRepository> ();
            services.AddScoped<ISurveyRepository, SurveyRepository> ();
            services.AddScoped<IQuestionRepository, QuestionRepository> ();
            services.AddScoped<IFieldDataRepository, FieldDataRepository> ();
            services.AddScoped<IChoiceOptionRepository, ChoiceOptionRepository> ();
            services.AddScoped<IRowRepository, RowRepository> ();
            services.AddScoped<ISurveyTemplateRepository, SurveyTemplateRepository> ();
            services.AddScoped<IQuestionTemplateRepository, QuestionTemplateRepository> ();
            services.AddScoped<IFieldDataTemplateRepository, FieldDataTemplateRepository> ();
            services.AddScoped<IChoiceOptionTemplateRepository, ChoiceOptionTemplateRepository> ();
            services.AddScoped<IRowTemplateRepository, RowTemplateRepository> ();
            services.AddScoped<ISurveyAnswerRepository, SurveyAnswerRepository> ();
            services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository> ();
            services.AddScoped<IFieldDataAnswerRepository, FieldDataAnswerRepository> ();
            services.AddScoped<IChoiceOptionAnswerRepository, ChoiceOptionAnswerRepository> ();
            services.AddScoped<IRowAnswerRepository, RowAnswerRepository> ();
            services.AddScoped<IRowChoiceOptionAnswerRepository, RowChoiceOptionAnswerRepository> ();
            services.AddScoped<ISurveyReportRepository, SurveyReportRepository> ();
            services.AddScoped<IQuestionReportRepository, QuestionReportRepository> ();
            services.AddScoped<IDataSetRepository, DataSetRepository> ();
            services.AddScoped<ISurveyUserIdentifierRepository, SurveyUserIdentifierRepository> ();
            services.AddScoped<IUnregisteredUserRepository, UnregisteredUserRepository> ();

            #endregion
            #region Services

            services.AddScoped<IAccountService, AccountService> ();
            services.AddScoped<IAuthService, AuthService> ();
            services.AddScoped<IStudentService, StudentService> ();
            services.AddScoped<ICareerOfficeService, CareerOfficeService> ();
            services.AddScoped<ISurveyService, SurveyService> ();
            services.AddScoped<ISurveyTemplateService, SurveyTemplateService> ();
            services.AddScoped<ISurveyAnswerService, SurveyAnswerService> ();
            services.AddScoped<ISurveyReportService, SurveyReportService> ();
            services.AddScoped<ISurveyUserIdentifierService, SurveyUserIdentifierService> ();
            services.AddScoped<IUnregisteredUserService, UnregisteredUserService> ();

            #endregion
            #region Validations

            services.AddTransient<IValidator<SignIn>, SignInValidator> ();
            services.AddTransient<IValidator<RegisterStudent>, RegisterStudentValidator> ();
            services.AddTransient<IValidator<RegisterCareerOffice>, RegisterCareerOfficeValidator> ();
            services.AddTransient<IValidator<ChangePassword>, ChangePasswordValidator> ();
            services.AddTransient<IValidator<RestorePassword>, RestorePasswordValidator> ();
            services.AddTransient<IValidator<ChangePasswordByRestoringPassword>, ChangePasswordByRestoringPasswordValidator> ();
            services.AddTransient<IValidator<EmailToSend>, EmailToSendValidator> ();
            services.AddTransient<IValidator<AddUnregisteredUser>, AddUnregisteredUserValidator> ();
            services.AddTransient<IValidator<UpdateUnregisteredUser>, UpdateUnregisteredUserValidator> ();

            #endregion
            #region Factories

            services.AddScoped<IEmailFactory, EmailFactory> ();
            services.AddScoped<IAccountEmailFactory, AccountEmailFactory> ();
            services.AddScoped<ISurveyEmailFactory, SurveyEmailFactory> ();
            services.AddScoped<IImportFileAggregate, ImportFileAggregate> ();
            services.AddScoped<IEmailContent, EmailContent> ();

            #endregion

        }
        public virtual void ConfigureDatabase (IServiceCollection services) {
            services.AddDbContext<QuestionnaireContext> (options =>
                options.UseSqlServer (Configuration.GetConnectionString ("questionnaireDatabase"),
                    b => b.MigrationsAssembly ("questionnaire.Api")));

        }
        public virtual void Migrate (IServiceScope serviceScope) {
            serviceScope.ServiceProvider.GetService<QuestionnaireContext> ().Database.Migrate ();
        }

        public virtual void SeedData (IServiceScope serviceScope) { }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler (builder => {
                    builder.Run (async context => {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature> ();
                        if (error != null) {
                            context.Response.AddApplicationError (error.Error.Message);
                            await context.Response.WriteAsync (error.Error.Message);
                        }
                    });
                });
            }

            using (var serviceScope = app.ApplicationServices.CreateScope ()) {
                Migrate (serviceScope);
                SeedData (serviceScope);
            }

            app.UseCors (x => x.AllowAnyHeader ().AllowAnyMethod ().AllowAnyOrigin ().AllowCredentials ());
            app.UseAuthentication ();
            app.UseMvc ();
        }
    }
}