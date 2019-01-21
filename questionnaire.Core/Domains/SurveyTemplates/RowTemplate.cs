namespace questionnaire.Core.Domains.SurveyTemplates
{
    public class RowTemplate
    {
        public int Id { get; private set; }
        public int RowPosition { get; private set; }
        public string Input { get; private set; }
        public int FieldDataTemplateId { get; private set; }
        public FieldDataTemplate FieldDataTemplate { get; private set; }

        private RowTemplate () { }

        public RowTemplate (int rowPosition, string input) {
            SetRowPosition(rowPosition);
            SetInput(input);
        }

        public void SetRowPosition (int rowPosition) {
            RowPosition = rowPosition;
        }

        public void SetInput (string input) {
            Input = input;
        }
    }
}