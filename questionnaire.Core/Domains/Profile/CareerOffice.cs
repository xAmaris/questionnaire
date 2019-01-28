using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class CareerOffice : Account {
        protected CareerOffice () { }

        public CareerOffice (string name, string surname, string email, string phoneNumber, string password) : base (name, surname, email, phoneNumber, password) {
            Role = "careerOffice";
        }

        public void Update (string name, string surname, string email, string phoneNumber) {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}