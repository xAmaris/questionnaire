using System;
using System.Threading.Tasks;
using questionnaire.Infrastructure.Commands.Survey;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace questionnaire.Api.Controllers {
    public class SurveyController : ApiUserController {
        private readonly ISurveyService _surveyService;
        private readonly ISurveyReportService _surveyReportService;

        public SurveyController (ISurveyService surveyService,
            ISurveyReportService surveyReportService) {
            _surveyService = surveyService;
            _surveyReportService = surveyReportService;
        }

        [Authorize (Policy = "careerOffice")]
        [HttpGet ("{surveyId}")]
        public async Task<IActionResult> GetSurvey (int surveyId, string email) {
            try {
                var survey = await _surveyService.GetByIdAsync (surveyId);
                return Json (survey);
            } catch (Exception e) {
                return BadRequest (e.Message);
            }
        }

        [HttpGet ("{surveyId}/{email}")]
        public async Task<IActionResult> GetSurveyWithEmail (int surveyId) {
            try {
                var survey = await _surveyService.GetByIdAsync (surveyId);
                return Json (survey);
            } catch (Exception e) {
                return BadRequest (e.Message);
            }
        }

        [Authorize (Policy = "careerOffice")]
        [HttpGet ("surveys")]
        public async Task<IActionResult> GetAllSurveys () {
            try {
                var surveys = await _surveyService.GetAllAsync ();
                return Json (surveys);
            } catch (Exception e) {
                return BadRequest (e.Message);
            }
        }
    }
}