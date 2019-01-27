using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Abstract;
using questionnaire.Infrastructure.Extensions.Email.Interfaces;
using questionnaire.Infrastructure.Extensions.Factories.Interfaces;
using questionnaire.Infrastructure.Repositories.Interfaces;
using MimeKit;

namespace questionnaire.Infrastructure.Extensions.Factories {
    public class AccountEmailFactory : IAccountEmailFactory {
        private readonly IEmailFactory _emailFactory;
        private readonly IEmailConfiguration _emailConfiguration;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnregisteredUserRepository _unregisteredUserRepository;
        private readonly IEmailContent _emailContent;

        public AccountEmailFactory (IEmailFactory emailFactory,
            IEmailConfiguration emailConfiguration,
            IAccountRepository accountRepository,
            IUnregisteredUserRepository unregisteredUserRepository,
            IEmailContent emailContent) {
            _emailFactory = emailFactory;
            _emailConfiguration = emailConfiguration;
            _accountRepository = accountRepository;
            _unregisteredUserRepository = unregisteredUserRepository;
            _emailContent = emailContent;
        }

        public async Task SendActivationEmailAsync (Account account, Guid activationKey) {
            var message = new MimeMessage ();
            message.From.Add (new MailboxAddress (_emailConfiguration.Name, _emailConfiguration.SmtpUsername));
            message.To.Add (new MailboxAddress (account.Name, account.Email));
            message.Subject = "Monitorowanie karier - aktywacja konta.";
            message.Body = new TextPart ("html") {
                Text = _emailContent.ActivationEmail (activationKey)
            };
            await _emailFactory.SendEmailAsync (message);
        }

        public async Task SendRecoveringPasswordEmailAsync (Account account, Guid token) {
            var message = new MimeMessage ();
            message.From.Add (new MailboxAddress (_emailConfiguration.Name, _emailConfiguration.SmtpUsername));
            message.To.Add (new MailboxAddress (account.Name.ToString (), account.Email.ToString ()));
            message.Subject = "Monitorowanie Karier - przywracanie hasla";
            message.Body = new TextPart ("html") {
                Text = _emailContent.RecoveringPasswordEmail (account.Name, token)
            };
            await _emailFactory.SendEmailAsync (message);
        }

        public async Task SendEmailToAllAsync (string subject, string body) {
            var accounts = await _accountRepository.GetAllAsync ();
            foreach (var account in accounts) {
                if (account.Role != "careerOffice") {
                    var message = new MimeMessage ();
                    message.From.Add (new MailboxAddress (_emailConfiguration.Name, _emailConfiguration.SmtpUsername));
                    message.To.Add (new MailboxAddress (account.Name, account.Email));
                    message.Subject = subject;
                    message.Body = new TextPart ("html") {
                        Text = body
                    };
                    await _emailFactory.SendEmailAsync (message);
                }
            }
        }

        public async Task SendEmailToAllUnregisteredAsync (string subject, string body) {
            var unregisteredUsers = await _unregisteredUserRepository.GetAllAsync ();
            foreach (var unregisteredUser in unregisteredUsers) {
                if (unregisteredUser.Role != "careerOffice") {
                    var message = new MimeMessage ();
                    message.From.Add (new MailboxAddress (_emailConfiguration.Name, _emailConfiguration.SmtpUsername));
                    message.To.Add (new MailboxAddress (unregisteredUser.Name, unregisteredUser.Email));
                    message.Subject = subject;
                    message.Body = new TextPart ("html") {
                        Text = body
                    };
                    await _emailFactory.SendEmailAsync (message);
                }
            }
        }
    }
}