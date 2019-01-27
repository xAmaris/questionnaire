namespace questionnaire.Infrastructure.Extensions.Factories.Interfaces {
    public interface IEmailConfiguration {
        string SmtpServer { get; }
        int SmtpPort { get; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }
        string Name { get; set; }
    }
}