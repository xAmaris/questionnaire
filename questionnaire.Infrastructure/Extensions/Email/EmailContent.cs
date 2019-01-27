using System;
using System.Threading.Tasks;
using questionnaire.Infrastructure.Extensions.Email.Interfaces;

namespace questionnaire.Infrastructure.Extensions.Email {
    public class EmailContent : IEmailContent {
        public EmailContent () {
        }

        public string ActivationEmail (Guid activationKey) {
            return $"Oto mail wygenerowany automatycznie, potwierdzający Twoją rejestrację w aplikacji <b>Monitorowanie karier</b><br/> Kliknij w" +
                $" <a href=\"http://localhost:4200/auth/activation/{activationKey}\">link aktywacyjny</a>, dzięki czemu aktywujesz swoje konto w serwisie.";
        }

        public string RecoveringPasswordEmail (string name, Guid token) {
            return $"Witaj, {name}.Ten mail został wygenerowany automatycznie.</b><br/> Kliknij w" +
                $" <a href=\"http://localhost:4200/auth/recoveringPassword/{token}\">link </a>, aby zmienić swoje hasło.";
        }

        public string SurveyEmail (int surveyId, string email) {
            return $"Witaj! Biuro karier WSEI zaprasza do wypełnienia krótkiej ankiety. Aby przejść do ankiety klinkij w ten" +
                $" <a href=\"http://localhost:4200/survey/viewform/s/{surveyId}/{email}\">link</a> .";
        }

    }
}