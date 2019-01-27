using System.Collections.Generic;

namespace questionnaire.Infrastructure.Commands.SurveyAnswer
{
    public class SurveyAnswerToAdd
    {
        public string SurveyTitle { get; set; }
        public int SurveyId { get; set; }
        public ICollection<QuestionAnswerToAdd> Questions { get; set; }
    }
}