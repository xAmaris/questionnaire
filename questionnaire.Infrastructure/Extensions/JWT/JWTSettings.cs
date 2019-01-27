using questionnaire.Infrastructure.Extension.JWT.Interfaces;

namespace questionnaire.Infrastructure.Extensions.JWT
{
    public class JWTSettings : IJWTSettings {
        public string Key { get; set; }
        public int ExpiryDays { get; set; }
    }
}