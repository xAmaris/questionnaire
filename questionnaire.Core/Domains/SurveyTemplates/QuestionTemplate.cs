using System.Collections.Generic;

namespace questionnaire.Core.Domains.SurveyTemplates
{
    public class QuestionTemplate
    {
        public int Id { get; private set; }
        public int QuestionPosition { get; private set; }
        public string Content { get; private set; }
        public string Select { get; private set; }
        public bool IsRequired { get; private set; }
        public int SurveyTemplateId { get; private set; }
        public SurveyTemplate SurveyTemplate { get; private set; }
        public ICollection<FieldDataTemplate> FieldDataTemplates { get; private set; } = new List<FieldDataTemplate>();

        private QuestionTemplate () { }

        public QuestionTemplate (int questionPosition, string content, string select, bool isRequired) {
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

        public void AddFieldDataTemplate (FieldDataTemplate fieldDataTemplate)
        {
            FieldDataTemplates.Add(fieldDataTemplate);
        }
    }
}