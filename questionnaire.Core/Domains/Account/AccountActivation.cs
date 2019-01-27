using System;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class AccountActivation {
        public int Id { get; private set; }
        public Guid ActivationKey { get; private set; }
        public bool Active { get; private set; }
        public DateTime ActivatedAt { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }

        protected AccountActivation () {
            ActivationKey = Guid.NewGuid ();
            Active = false;
        }
        public AccountActivation (Guid activationKey) {
            ActivationKey = activationKey;
            Active = false;
        }
        public void Activate () {
            ActivatedAt = DateTime.UtcNow;
            Active = true;
        }
    }
}