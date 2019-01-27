using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class Education {
        public int Id { get; private set; }
        public string Course { get; private set; }
        public int Year { get; private set; }
        public string Specialization { get; private set; }
        public string NameOfUniversity { get; private set; }
        public bool Graduated { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        protected Education () { }

        public Education (string course, int year, string specialization, string nameOfUniveristy, bool graduated) {
            SetCourse(course);
            SetYear(year);
            SetSpecialization(specialization);
            SetNameOfUniversity(nameOfUniveristy);
            SetGraduated(graduated);
        }

        public void SetCourse(string course)
        {
            Course = course;
        }

        public void SetYear(int year)
        {
            Year = year;
        }

        public void SetSpecialization (string specialization)
        {
            Specialization = specialization;
        }

        public void SetNameOfUniversity (string nameOfUniveristy)
        {
            NameOfUniversity = nameOfUniveristy;
        }

        public void SetGraduated (bool graduated)
        {
            Graduated = graduated;
        }
    }
}