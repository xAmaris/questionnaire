using System.Collections.Generic;

namespace questionnaire.Infrastructure.Commands.SurveyAnswer
{
    public class RowAnswerToAdd
    {
        public int RowPosition { get; set; }
        public string Input { get; set; }
        public ICollection<RowChoiceOptionAnswerToAdd> ChoiceOptions { get; set; }
    }
}