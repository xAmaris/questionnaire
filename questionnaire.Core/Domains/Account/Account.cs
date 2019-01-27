using System;
using System.Collections.Generic;
using questionnaire.Core.Domains;

namespace questionnaire.Core.Domains.Abstract {
    public abstract class Account {
        public int Id { get; private set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Email { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }
        public string Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public bool Deleted { get; protected set; }
        public bool Activated { get; protected set; }
        public AccountActivation AccountActivation { get; protected set; }
        public AccountRestoringPassword AccountRestoringPassword { get; protected set; }

        protected Account () { }

        public Account (string name, string surname, string email, string phoneNumber, string password) {
            SetName (name);
            SetSurname (surname);
            SetEmail (email);
            SetPhoneNumber (phoneNumber);
            CreatePasswordHash (password);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Deleted = false;
            Activated = false;
        }

        public Account (string email) {
            Email = email;
        }

        public void SetName (string name) {
            Name = name;
        }
        public void SetSurname (string surname) {
            Surname = surname;
        }
        public void SetEmail (string email) {
            Email = email;
        }
        public void SetPhoneNumber (string phoneNumber) {
            PhoneNumber = phoneNumber;
        }

        public void Activate (AccountActivation accountActivation) {
            if (!accountActivation.Active) {
                Activated = true;
                UpdatedAt = DateTime.UtcNow;
                accountActivation.Activate ();
            }
        }

        public void Delete () {
            if (Deleted == false) {
                Deleted = true;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void UpdatePassword (string newPassword) {
            CreatePasswordHash (newPassword);
            UpdatedAt = DateTime.UtcNow;
        }

        private void CreatePasswordHash (string password) {
            using (var hmac = new System.Security.Cryptography.HMACSHA512 ()) {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            }
        }

        public void AddAccountActivation (AccountActivation accountActivation) {
            AccountActivation = accountActivation;
        }

        public void ChangeAccountRestoringPassword (Guid token) {
            if (AccountRestoringPassword != null)
                AccountRestoringPassword.ResetState (token);
        }

        public void AddAccountRestoringPassword (AccountRestoringPassword accountRestoringPassword) {
            if (accountRestoringPassword != null)
                AccountRestoringPassword = accountRestoringPassword;
        }
    }
}