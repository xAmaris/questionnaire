using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using questionnaire.Infrastructure.Commands.User;
using questionnaire.Infrastructure.Data;

namespace questionnaire.Tests
{
    public class TestHostFixture : IDisposable
    {
        public CustomWebApplicationFactory _factory;
        public QuestionnaireContext _context;
        public HttpClient Client { get; set; }

        public string Email = "user@user.pl";
        public TestHostFixture()
        {
            SetHttpClient();
            Task.Run(SetAuthToken).Wait();
        }
        public void SetHttpClient()
        {
            _factory = new CustomWebApplicationFactory();

            Client = _factory.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5000/");

        }
        public async Task SetAuthToken()
        {

            var user = new SignIn()
            {
                Email = Email,
                Password = "!A123456a"
            };
            var jsoncontent = JsonConvert.SerializeObject(user);
            var httpcontent = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            var result = await Client.PostAsync("api/auth/login", httpcontent);
            var response = result.Content.ReadAsStringAsync().Result;
            var template = new { loginData = new { token = string.Empty } };
            var sth = JsonConvert.DeserializeAnonymousType(response, template);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sth.loginData.token);
        }

        public void Dispose()
        {
            _context?.Dispose();
            Client?.Dispose();
        }
    }
}