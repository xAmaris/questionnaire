using System;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class AccountRestoringPassword {
        public int Id { get; set; }
        public DateTime RestoredAt { get; private set; }
        public Guid Token { get; private set; }
        public bool Restored { get; private set; }
        public int AccountId { get; private set; }
        public Account Account { get; private set; }
        protected AccountRestoringPassword () { }
        public AccountRestoringPassword (Guid token) {
            RestoredAt = default (DateTime);
            Token = token;
            Restored = false;
        }
        public void PasswordRestoring () {
            Restored = true;
            RestoredAt = DateTime.UtcNow;
            Token = Guid.NewGuid ();
        }
        public void ResetState (Guid token) {
            RestoredAt = default (DateTime);
            Token = token;
            Restored = false;
        }
    }
}