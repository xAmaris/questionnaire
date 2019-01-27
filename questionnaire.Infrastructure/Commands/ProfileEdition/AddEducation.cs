namespace questionnaire.Infrastructure.Commands.ProfileEdition {
    public class AddEducation {
        public string Course { get; set; }
        public int Year { get; set; }
        public string Specialization { get; set; }
        public string NameOfUniversity { get; set; }
        public bool Graduated { get; set; }
    }
}