using questionnaire.Infrastructure.Extensions.Factories.Interfaces;

namespace questionnaire.Infrastructure.Extensions.Factories {
    public class EmailConfiguration : IEmailConfiguration {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string Name { get; set; }
    }
}