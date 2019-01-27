using System.Collections.Generic;

namespace questionnaire.Core.Domains.SurveysAnswers {
    public class FieldDataAnswer {
        public int Id { get; private set; }
        public string MinLabel { get; private set; }
        public string MaxLabel { get; private set; }
        public string Input { get; private set; }
        public int QuestionAnswerId { get; private set; }
        public QuestionAnswer QuestionAnswer { get; private set; }
        public ICollection<ChoiceOptionAnswer> ChoiceOptionAnswers { get; private set; } = new List<ChoiceOptionAnswer>();
        public ICollection<RowAnswer> RowsAnswers { get; private set; } = new List<RowAnswer>();

        public FieldDataAnswer () { }

        public void AddChoiceOptionAnswer (ChoiceOptionAnswer choiceOptionAnswer) {
            ChoiceOptionAnswers.Add(choiceOptionAnswer);
        }

        public FieldDataAnswer (string input) {
            SetInput(input);
        }

        public FieldDataAnswer (string minLabel, string maxLabel) {
            SetMinLabel(minLabel);
            SetMaxLabel(maxLabel);
        }

        public void SetInput (string input) {
            Input = input;
        }

        public void SetMinLabel (string minLabel) {
            MinLabel = minLabel;
        }

        public void SetMaxLabel (string maxLabel) {
            MaxLabel = maxLabel;
        }

        public void AddRow (RowAnswer rowAnswer) {
            RowsAnswers.Add(rowAnswer);
        }
    }
}