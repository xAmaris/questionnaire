using System.Collections.Generic;

namespace questionnaire.Infrastructure.Commands.Survey
{
    public class QuestionToAdd
    {
        public int QuestionPosition { get; set; }
        public string Content { get; set; }
        public string Select { get; set; }
        public bool IsRequired { get; set; }
        public ICollection<FieldDataToAdd> FieldData { get; set; }
    }
}