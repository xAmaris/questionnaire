using System;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Abstract;

namespace questionnaire.Infrastructure.Extensions.Factories.Interfaces {
    public interface IAccountEmailFactory {
        Task SendActivationEmailAsync (Account account, Guid activationKey);
        Task SendRecoveringPasswordEmailAsync (Account account, Guid token);
        Task SendEmailToAllAsync (string subejct, string body);
        Task SendEmailToAllUnregisteredAsync (string subejct, string body);
    }
}