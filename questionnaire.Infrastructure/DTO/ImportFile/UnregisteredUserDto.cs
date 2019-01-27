using System;

namespace questionnaire.Infrastructure.DTO.ImportFile {
    public class UnregisteredUserDto {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Course { get; private set; }
        public DateTime DateOfCompletion { get; private set; }
        public string TypeOfStudy { get; private set; }
        public string Email { get; private set; }
    }
}