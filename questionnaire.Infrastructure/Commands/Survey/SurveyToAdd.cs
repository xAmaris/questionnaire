using System.Collections.Generic;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Infrastructure.Commands.Survey
{
    public class SurveyToAdd
    {
        public string Title { get; set; }
        public ICollection<QuestionToAdd> Questions { get; set; }
    }
}