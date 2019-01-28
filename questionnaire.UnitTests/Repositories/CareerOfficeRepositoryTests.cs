using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Extensions.Factories.Interfaces;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Infrastructure.Services;
using questionnaire.Infrastructure.Services.Interfaces;
using Xunit;

namespace questionnaire.UnitTests.Repositories {
    public class CareerOfficeRepositoryTests {

        [Fact]
        public async Task AddAsync_NewUserAddedCorrectlyToTheList_ReturnTrue () {
            //Arrange

            // var studentRepositoryMock = new Mock<IStudentRepository> ();
            // var accountRepositoryMock = new Mock<IAccountRepository> ();
            // var careerOfficeRepositoryMock = new Mock<ICareerOfficeRepository> ();
            // var studentServiceMock = new Mock<IStudentService> ();
            // var careerOfficeServiceMock = new Mock<ICareerOfficeService> ();
            // var accountEmailFactoryMock = new Mock<IAccountEmailFactory> ();
            // var careerOffice = new CareerOffice ("jan", "nowak", "wp@wp.pl", "+48123456789", "!A123456a");
            // var authService = new AuthService (accountRepositoryMock.Object, studentRepositoryMock.Object, studentServiceMock.Object, careerOfficeRepositoryMock.Object, careerOfficeServiceMock.Object, accountEmailFactoryMock.Object);
            var careerOffice = new CareerOffice ("jan", "nowak", "wp@wp.pl", "+48123456789", "!A123456a");
            IList<CareerOffice> careerOfficeArr = new List<CareerOffice> { };
            var careerOfficesMock = DbSetMock.CreateDbSetMock (careerOfficeArr);
            var questionnaireContextMock = new Mock<QuestionnaireContext> ();
            questionnaireContextMock.Setup (x => x.CareerOffices).Returns (careerOfficesMock.Object);
            questionnaireContextMock.Setup (x => x.CareerOffices.AddAsync (It.IsAny<CareerOffice> ()))
            //Act

            //Assert
        }
    }
}