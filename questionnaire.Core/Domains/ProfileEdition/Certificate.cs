using System;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class Certificate {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public DateTime DateOfReceived { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        protected Certificate () { }

        public Certificate (int accountId, string title, DateTime dateOfReceived) {
            SetAccountId(accountId);
            SetTitle(title);
            SetDateOfReceived(dateOfReceived);
        }

        private void SetAccountId (int accountId)
        {
            AccountId = accountId;
        }

        private void SetTitle (string title)
        {
            Title = title;
        }

        private void SetDateOfReceived (DateTime dateOfReceived)
        {
            DateOfReceived = dateOfReceived;
        }
    }
}