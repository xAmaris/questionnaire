using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace questionnaire.Core.Domains.SurveyTemplates
{
    public class SurveyTemplate
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; private set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; private set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; private set; }
        [JsonProperty(PropertyName = "questionTemplates")]
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