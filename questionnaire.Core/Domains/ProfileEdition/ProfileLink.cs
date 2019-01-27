using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class ProfileLink {
        public int Id { get; private set; }
        public string Content { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        protected ProfileLink () { }

        public ProfileLink (string content) {
            SetContent(content);
        }

        public void SetContent (string content) {
            Content = content;
        }
    }
}