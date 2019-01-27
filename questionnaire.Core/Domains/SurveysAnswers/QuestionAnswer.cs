using System.Collections.Generic;

namespace questionnaire.Core.Domains.SurveysAnswers {
    public class QuestionAnswer {
        public int Id { get; private set; }
        public int QuestionPosition { get; private set; }
        public string Content { get; private set; }
        public string Select { get; private set; }
        public bool IsRequired { get; private set; }
        public int SurveyAnswerId { get; private set; }
        public SurveyAnswer SurveyAnswer { get; private set; }
        public ICollection<FieldDataAnswer> FieldDataAnswers { get; private set; } = new List<FieldDataAnswer>();

        private QuestionAnswer () { }

        public QuestionAnswer (int questionPosition, string content, string select, bool isRequired) {
            SetQuestionPosition(questionPosition);
            SetContent(content);
            SetSelect(select);
            SetIsRequired(isRequired);
        }

        public void SetQuestionPosition (int questionPosition) {
            QuestionPosition = questionPosition;
        }

        public void SetContent (string content) {
            Content = content;
        }

        public void SetSelect (string select) {
            Select = select;
        }

        public void SetIsRequired (bool isRequired) {
            IsRequired = isRequired;
        }

        public void AddFieldDataAnswer (FieldDataAnswer fieldDataAnswer)
        {
            FieldDataAnswers.Add(fieldDataAnswer);
        }
    }
}