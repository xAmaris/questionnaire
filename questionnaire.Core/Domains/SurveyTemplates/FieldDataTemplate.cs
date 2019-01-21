using System.Collections.Generic;

namespace questionnaire.Core.Domains.SurveyTemplates
{
    public class FieldDataTemplate
    {
        public int Id { get; private set; }
        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }
        public string MinLabel { get; private set; }
        public string MaxLabel { get; private set; }
        public string Input { get; private set; }
        public int QuestionTemplateId { get; private set; }
        public QuestionTemplate QuestionTemplate { get; private set; }
        public ICollection<ChoiceOptionTemplate> ChoiceOptionTemplates { get; private set; } = new List<ChoiceOptionTemplate>();
        public ICollection<RowTemplate> RowTemplates { get; private set; } = new List<RowTemplate>();

        public FieldDataTemplate () { }

        public void AddChoiceOptionTemplate (ChoiceOptionTemplate choiceOptionTemplate) {
            ChoiceOptionTemplates.Add(choiceOptionTemplate);
        }

        public void AddRowTemplate (RowTemplate rowTemplate) {
            RowTemplates.Add(rowTemplate);
        }

        public FieldDataTemplate (string input) {
            SetInput(input);
        }

        public FieldDataTemplate (int minValue, int maxValue, string minLabel, string maxLabel) {
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