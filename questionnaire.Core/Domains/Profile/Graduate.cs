using System.Collections.Generic;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class Graduate : Account {

        protected Graduate () { }

        public Graduate (string name, string surname, string email, string phoneNumber, string password) : base (name, surname, email, phoneNumber, password) {
            Role = "graduate";
        }

        public void Update (string name, string surname, string email, string phoneNumber) {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}