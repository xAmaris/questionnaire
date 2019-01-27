using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class Course {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        protected Course () { }

        public Course (string name) {
            SetName(name);
        }

        public void SetName (string name)
        {
            Name = name;
        }
    }
}