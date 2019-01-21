using System;
using System.Collections.Generic;
using questionnaire.Core.Domains.Surveys;

namespace questionnaire.Core.Domains.SurveyTemplates
{
    public class SurveyTemplate
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<QuestionTemplate> QuestionTemplates { get; private set; } = new List<QuestionTemplate> ();

        private SurveyTemplate () { }

        public SurveyTemplate (string title) {
            SetTitle(title);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetTitle (string title) {
            Title = title;
        }

        public void AddQuestionTemplate (QuestionTemplate questionTemplate) {
            QuestionTemplates.Add (questionTemplate);
        }

        public void Update (string title) {
            SetTitle(title);
            CreatedAt = DateTime.UtcNow;
        }
    }
}