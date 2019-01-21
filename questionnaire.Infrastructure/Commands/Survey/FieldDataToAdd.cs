using System.Collections.Generic;

namespace questionnaire.Infrastructure.Commands.Survey
{
    public class FieldDataToAdd
    {
        public string Input { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public string MinLabel { get; set; }
        public string MaxLabel { get; set; }
        public ICollection<ChoiceOptionToAdd> ChoiceOptions { get; set; }
        public ICollection<RowToAdd> Rows { get; set; }
    }
}