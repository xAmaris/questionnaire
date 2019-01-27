namespace questionnaire.Core.Domains {
    public class JobOffer {
        public int Id { get; private set; }
        public string JobType { get; private set; }
        public string Position { get; private set; }
        public string CompanyName { get; private set; }
        public string Location { get; private set; }
        public string ContactPerson { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Description { get; private set; }
        public string Email { get; private set; }
        public string WebsiteAddress { get; private set; }

        public JobOffer () { }

        public JobOffer (string jobType, string position, string companyName, string location, string contactPerson, string phoneNumber, string description, string email, string webSiteAddress) {
            JobType = jobType;
            Position = position;
            CompanyName = companyName;
            Location = location;
            ContactPerson = contactPerson;
            PhoneNumber = phoneNumber;
            Description = description;
            Email = email;
            WebsiteAddress = webSiteAddress;
        }

    }
}