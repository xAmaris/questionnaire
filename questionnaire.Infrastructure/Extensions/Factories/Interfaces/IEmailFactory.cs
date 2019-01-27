using System.Threading.Tasks;
using MimeKit;

namespace questionnaire.Infrastructure.Extensions.Factories.Interfaces {
    public interface IEmailFactory {
        Task SendEmailAsync (MimeMessage mimeMessage);
    }
}