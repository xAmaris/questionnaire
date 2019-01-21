using System.Collections.Generic;

namespace questionnaire.Core.Domains.Surveys {
    public class FieldData {
        public int Id { get; private set; }
        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }
        public string MinLabel { get; private set; }
        public string MaxLabel { get; private set; }
        public string Input { get; private set; }
        public int QuestionId { get; private set; }
        public Question Question { get; private set; }
        public ICollection<ChoiceOption> ChoiceOptions { get; private set; } = new List<ChoiceOption>();
        public ICollection<Row> Rows { get; private set; } = new List<Row>();

        public FieldData () { }

        public void AddChoiceOption (ChoiceOption choiceOption) {
            ChoiceOptions.Add(choiceOption);
        }

        public void AddRow (Row row) {
            Rows.Add(row);
        }

        public FieldData (string input) {
            SetInput(input);
        }

        public FieldData (int minValue, int maxValue, string minLabel, string maxLabel) {
            SetMinValue(minValue);
            SetMaxValue(maxValue);
            SetMinLabel(minLabel);
            SetMaxLabel(maxLabel);
        }

        public void SetInput (string input) {
            Input = input;
        }

        public void SetMinValue(int minValue) {
            MinValue = minValue;
        }

        public void SetMaxValue (int maxValue) {
            MaxValue = maxValue;
        }

        public void SetMinLabel (string minLabel) {
            MinLabel = minLabel;
        }

        public void SetMaxLabel (string maxLabel) {
            MaxLabel = maxLabel;
        }
    }
}