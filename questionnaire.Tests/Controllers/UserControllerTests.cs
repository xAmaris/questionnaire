using FluentAssertions;
using Newtonsoft.Json;
using questionnaire.Infrastructure.Commands.Account;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace questionnaire.Tests.Controllers
{
    public class UserControllerTests : IClassFixture<TestHostFixture>
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;

        public UserControllerTests(TestHostFixture fixture)
        {
            _client = fixture.Client;
            _factory = fixture._factory;
        }

        [Fact]
        public async Task UpdateUserData()
        {
            var updateAccount = new UpdateAccount()
            {
                Name = "newName",
                Surname = "newSurname",
                Email = "new@new.pl",
                PhoneNumber = "+48123123123",
            };
            var jsoncontent = JsonConvert.SerializeObject(updateAccount);
            var httpcontent = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/accountupdate/accounts", httpcontent);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
