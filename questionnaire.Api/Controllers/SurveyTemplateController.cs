using System;
using System.Threading.Tasks;
using questionnaire.Infrastructure.Commands.Survey;
using questionnaire.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace questionnaire.Api.Controllers
{
    [Authorize (Policy = "careerOffice")]
    public class SurveyTemplateController : ApiUserController
    {
        private readonly ISurveyTemplateService _surveyTemplateService;

        public SurveyTemplateController (ISurveyTemplateService surveyTemplateService) {
            _surveyTemplateService = surveyTemplateService;
        }

        [HttpGet ("{surveyId}")]
        public async Task<IActionResult> GetSurvey (int surveyId, string email) {
            try{
                var survey = await _surveyTemplateService.GetSurveyTemplateByIdAsync (surveyId);
                return Json (survey);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpGet ("{surveyId}/{email}/{userId}")]
        public async Task<IActionResult> GetSurveyWithEmail (int surveyId) {
            try{
                var survey = await _surveyTemplateService.GetSurveyTemplateByIdAsync (surveyId);
                return Json (survey);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpGet ("surveys")]
        public async Task<IActionResult> GetAllSurveys () {
            try{
                var surveys = await _surveyTemplateService.GetAllSurveyTemplatesAsync ();
                return Json (surveys);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost ("surveys")]
        public async Task<IActionResult> CreateSurvey ([FromBody] SurveyToAdd command) {
            try{
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);
                var surveyTemplateId = await _surveyTemplateService.CreateSurveyTemplateAsync (command);
                return Json(surveyTemplateId);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPut ("surveys")]
        public async Task<IActionResult> UpdateSurvey ([FromBody] SurveyToUpdate command) {
            try{
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);
                await _surveyTemplateService.UpdateSurveyTemplateAsync (command);
                return StatusCode (200);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpDelete ("{surveyId}")]
        public async Task<IActionResult> DeleteSurvey (int surveyId){
            try{
                await _surveyTemplateService.DeleteSurveyTemplateAsync(surveyId);
                return StatusCode(200);
            }
            catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}