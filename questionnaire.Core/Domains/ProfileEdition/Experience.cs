using System;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class Experience {
        public int Id { get; private set; }
        public string Position { get; private set; }
        public string CompanyName { get; private set; }
        public string Location { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public bool IsCurrentJob { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        protected Experience () { }

        public Experience (string position, string companyName, string location,
            DateTime from, DateTime to, bool isCurrentJob) {
            SetPosition(position);
            SetCompanyName(companyName);
            SetLocation(location);
            SetFrom(from);
            SetTo(to);
            SetIsCurrentJob(isCurrentJob);
        }

        public void SetPosition (string position) {
            Position = position;
        }

        public void SetCompanyName (string companyName) {
            CompanyName = companyName;
        }

        public void SetLocation (string location) {
            Location = location;
        }

        public void SetFrom (DateTime from) {
            From = from;
        }

        public void SetTo (DateTime to) {
            To = to;
        }

        public void SetIsCurrentJob (bool isCurrentJob) {
            IsCurrentJob = isCurrentJob;
        }
    }
}