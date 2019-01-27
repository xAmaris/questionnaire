using System.Collections.Generic;
using questionnaire.Infrastructure.Validators.Survey;

namespace questionnaire.Infrastructure.Commands.SurveyAnswer
{
    public class QuestionAnswerToAdd
    {
        public int QuestionPosition { get; set; }
        public string Content { get; set; }
        public string Select { get; set; }
        public bool IsRequired { get; set; }
        [FieldDataValidator]
        public ICollection<FieldDataAnswerToAdd> FieldData { get; set; }
    }
}