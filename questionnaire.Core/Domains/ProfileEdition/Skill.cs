namespace questionnaire.Core.Domains {
    public class Skill {
        public int Id { get; private set; }
        public string Name { get; private set; }

        protected Skill () { }

        public Skill (string name) {
            SetName(name);
        }

        public void SetName (string name) {
            Name = name;
        }
    }
}