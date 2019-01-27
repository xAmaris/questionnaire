using System;

namespace questionnaire.Infrastructure.Commands.ProfileEdition
{
    public class AddExperience
    {
         public string Position { get;  set; }
        public string CompanyName { get;  set; }
        public string Location { get;  set; }
        public DateTime From { get;  set; }
        public DateTime To { get;  set; }
        public bool IsCurrentJob { get;  set; }
    }
}