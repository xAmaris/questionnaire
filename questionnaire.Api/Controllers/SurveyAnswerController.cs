using System;
using System.Threading.Tasks;
using questionnaire.Infrastructure.Commands.SurveyAnswer;
using questionnaire.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace questionnaire.Api.Controllers {
    public class SurveyAnswerController : ApiUserController {
        private readonly ISurveyAnswerService _surveyAnswerService;
        private readonly ISurveyUserIdentifierService _surveyUserIdentifierService;

        public SurveyAnswerController (ISurveyAnswerService surveyAnswerService,
            ISurveyUserIdentifierService surveyUserIdentifierService) {
            _surveyAnswerService = surveyAnswerService;
            _surveyUserIdentifierService = surveyUserIdentifierService;
        }

        [HttpPost ("{email}")]
        public async Task<IActionResult> CreateSurveyAnswer (string email, [FromBody] SurveyAnswerToAdd command) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            try {
                var verification = await _surveyUserIdentifierService.VerifySurveyUser (email, command.SurveyId);
                if (verification == "answered")
                    return BadRequest ("you already answered to that survey");
                else if (verification == "unauthorized")
                    return Unauthorized ();
                await _surveyAnswerService.CreateSurveyAnswerAsync(command);
                return StatusCode (201);
            } catch (Exception e) {
                return BadRequest (e.Message);
            }
        }
    }
}