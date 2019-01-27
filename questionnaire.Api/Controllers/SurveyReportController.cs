using System;
using System.Threading.Tasks;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace questionnaire.Api.Controllers {
    [Authorize(Policy = "careerOffice")]
    public class SurveyReportController : ApiUserController {
        private readonly ISurveyReportRepository _surveyReportRepository;

        public SurveyReportController (ISurveyReportRepository surveyReportRepository) {
            _surveyReportRepository = surveyReportRepository;
        }

        [HttpGet ("surveyReports/{surveyId}")]
        public async Task<IActionResult> GetSurveyReport (int surveyId) {
            try{
                var surveyReport = await _surveyReportRepository.GetBySurveyIdAsync (surveyId);
                return Json (surveyReport);
            }
            catch(Exception e){
                return BadRequest (e.Message);
            }
        }
    }
}