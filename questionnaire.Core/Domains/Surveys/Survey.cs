using System;
using System.Collections.Generic;

namespace questionnaire.Core.Domains.Surveys {
    public class Survey {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<Question> Questions { get; private set; } = new List<Question> ();

        private Survey () { }

        public Survey (string title) {
            SetTitle(title);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetTitle (string title) {
            Title = title;
        }

        public void AddQuestion (Question question) {
            Questions.Add (question);
        }

        public void Update (string title) {
            SetTitle(title);
            CreatedAt = DateTime.UtcNow;
        }
    }
}