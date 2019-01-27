namespace questionnaire.Core.Domains.SurveysAnswers {
    public class ChoiceOptionAnswer {
        public int Id { get; private set; }
        public int OptionPosition { get; private set; }
        public bool Value { get; private set; }
        public string ViewValue { get; private set; }
        public int FieldDataAnswerId { get; private set; }
        public FieldDataAnswer FieldDataAnswer { get; private set; }

        private ChoiceOptionAnswer () { }

        public ChoiceOptionAnswer (int optionPosition, bool value, string viewValue) {
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