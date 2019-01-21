namespace questionnaire.Core.Domains.SurveyTemplates
{
    public class ChoiceOptionTemplate
    {
        public int Id { get; private set; }
        public int OptionPosition { get; private set; }
        public bool Value { get; private set; }
        public string ViewValue { get; private set; }
        public int FieldDataTemplateId { get; private set; }
        public FieldDataTemplate FieldDataTemplate { get; private set; }

        private ChoiceOptionTemplate () { }

        public ChoiceOptionTemplate (int optionPosition, bool value, string viewValue) {
            SetOptionPosition(optionPosition);
            SetValue(value);
            SetViewValue(viewValue);
        }

        public void SetOptionPosition (int optionPosition) {
            OptionPosition = optionPosition;
        }

        public void SetValue (bool value) {
            Value = value;
        }

        public void SetViewValue (string viewValue) {
            ViewValue = viewValue;
        }
    }
}