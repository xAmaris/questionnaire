using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using questionnaire.Infrastructure.Commands.Survey;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Api.Controllers
{
    public class SurveyTemplateController : ApiController
    {
        private readonly ISurveyTemplateService _surveyTemplateService;
        public SurveyTemplateController(ISurveyTemplateService surveyTemplateService)
        {
            _surveyTemplateService = surveyTemplateService;
        }

        [HttpGet("{surveyId}")]
        public async Task<IActionResult> GetSurvey(int surveyId, string email)
        {
            try
            {
                var survey = await _surveyTemplateService.GetByIdAsync(surveyId);
                return Json(survey);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("surveys")]
        public async Task<IActionResult> GetAllSurveys()
        {
            try
            {
                var surveys = await _surveyTemplateService.GetAllAsync();
                return Json(surveys);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("surveys")]
        public async Task<IActionResult> CreateSurvey([FromBody] SurveyToAdd command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var surveyTemplateId = await _surveyTemplateService.CreateSurveyAsync(command);
                return Json(surveyTemplateId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("surveys")]
        public async Task<IActionResult> UpdateSurvey([FromBody] SurveyToUpdate command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                await _surveyTemplateService.UpdateSurveyAsync(command);
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}