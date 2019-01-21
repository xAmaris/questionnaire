namespace questionnaire.Core.Domains.Surveys {
    public class Row {
        public int Id { get; private set; }
        public int RowPosition { get; private set; }
        public string Input { get; private set; }
        public int FieldDataId { get; private set; }
        public FieldData FieldData { get; private set; }

        private Row () { }

        public Row (int rowPosition, string input) {
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