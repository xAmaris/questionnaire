using System;
using System.Collections.Generic;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Core.Domains {
    public class Student : Account {
        public string IndexNumber { get; private set; }

        protected Student () { }

        public Student (string name, string surname, string email, string indexNumber, string phoneNumber, string password) : base (name, surname, email, phoneNumber, password) {
            Role = "student";
            IndexNumber = indexNumber;
        }

        public Student(string email, string indexNumber) : base(email)
        {
            IndexNumber = indexNumber;
        }

        public void Update (string name, string surname, string email, string phoneNumber) {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}