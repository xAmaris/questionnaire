using System.Security.Cryptography;
using System.Text;

namespace questionnaire.Core.Domains.Surveys
{
    public class SurveyUserIdentifier
    {
        public int Id { get; private set; }
        public string UserEmailHash { get; private set; }
        public int SurveyId { get; private set; }
        public bool Answered { get; private set; }

        private SurveyUserIdentifier () { }

        public SurveyUserIdentifier(string userEmail, int surveyId)
        {
            UserEmailHash = CalculateEmailHash(userEmail);
            SurveyId = surveyId;
        }

        public void MarkAsAnswered()
        {
            Answered = true;
        }

        public string CalculateEmailHash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}