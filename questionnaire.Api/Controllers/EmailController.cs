using System;
using System.Threading.Tasks;
using questionnaire.Infrastructure.Commands.Email;
using questionnaire.Infrastructure.Extensions.Factories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace questionnaire.Api.Controllers
{
    [Authorize(Policy = "careerOffice")]
    public class EmailController : ApiUserController
    {
        private readonly IAccountEmailFactory _accountEmailFactory;
        private readonly ISurveyEmailFactory _surveyEmailFactory;
        private readonly ISurveyService _surveyService;
        private readonly ISurveyReportService _surveyReportService;

        public EmailController(IAccountEmailFactory accountEmailFactory,
        ISurveyEmailFactory surveyEmailFactory,
        ISurveyService surveyService,
        ISurveyReportService surveyReportService)
        {
            _accountEmailFactory = accountEmailFactory;
            _surveyEmailFactory = surveyEmailFactory;
            _surveyService = surveyService;
            _surveyReportService = surveyReportService;
        }

        [HttpPost("emails")]
        public async Task<IActionResult> SendEmailToAll([FromBody] EmailToSend command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _accountEmailFactory.SendEmailToAllAsync(command.Subject, command.Body);
                await _accountEmailFactory.SendEmailToAllUnregisteredAsync(command.Subject, command.Body);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("survey-emails/{surveyTemplateId}")]
        public async Task<IActionResult> SendSurveyEmail(int surveyTemplateId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var surveyId = _surveyService.CreateSurveyAsync(surveyTemplateId).Result;
                var survey = await _surveyService.GetByIdAsync(surveyId);
                await _surveyReportService.CreateAsync(surveyId, survey.Title);
                await _surveyEmailFactory.SendSurveyEmailAsync(surveyId);
                await _surveyEmailFactory.SendSurveyEmailToUnregisteredUsersAsync(surveyId);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}