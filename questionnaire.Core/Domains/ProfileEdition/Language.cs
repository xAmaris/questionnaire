using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class Language {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Proficiency { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        protected Language () { }

        public Language (string name, string proficiency) {
            SetName(name);
            SetProficiency(proficiency);
        }

        public void SetName (string name) {
            Name = name;
        }

        public void SetProficiency (string proficiency) {
            Proficiency = proficiency;
        }
    }
}