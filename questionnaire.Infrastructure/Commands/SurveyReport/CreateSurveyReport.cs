using System;

namespace questionnaire.Infrastructure.Commands.SurveyReport
{
    public class CreateSurveyReport
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
    }
}