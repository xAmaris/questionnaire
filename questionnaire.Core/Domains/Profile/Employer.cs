using System.Collections.Generic;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class Employer : Account {
        public string CompanyName { get; private set; }
        public string Location { get; private set; }
        public string CompanyDescription { get; private set; }
        public ICollection<JobOffer> JobOffers { get; private set; }

        protected Employer () { }

        public Employer (string name, string surname, string email, string phoneNumber, string password, string companyName,
            string location, string companyDescription) : base (name, surname, email, phoneNumber, password) {
            Role = "employer";
            CompanyName = companyName;
            Location = location;
            CompanyDescription = companyDescription;
        }

        public void Update (string name, string surname, string phoneNumber, string companyName, string location, string companyDescription) {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            CompanyName = companyName;
            Location = location;
            CompanyDescription = companyDescription;
        }

        public void AddJobOffer (JobOffer jobOffer) {
            JobOffers.Add (jobOffer);
        }
    }
}