using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using questionnaire.Tests.Fixtures;
using Xunit;

namespace questionnaire.Tests.Repositories
{
    public class CareerOfficeRepositoryTests : IClassFixture<TestHostFixture>
    {
        private readonly QuestionnaireContext _context;
        private readonly ICareerOfficeRepository _careerOfficeRepository;
        public CareerOfficeRepositoryTests(TestHostFixture fixture)
        {
            _context = new TestHostFixture().Context;
            _careerOfficeRepository = new CareerOfficeRepository(_context);
        }

        [Fact]
        public async Task AddAsync_NewCarrerOfficeAddedCorrectly_ReturnTrue()
        {
            //Arrange
            var careerOffice = new CareerOffice("jan", "nowak", "wp@wp.pl", "+48123456789", "!A123456a");

            //Act
            await _careerOfficeRepository.AddAsync(careerOffice);
            //Assert
            var obj = _context.CareerOffices.FirstOrDefault(x => x.Id == careerOffice.Id);
            Assert.Equal(careerOffice.Email, obj.Email);
        }
        [Fact]
        public async Task GetByIdAsync_GetCorrectCareerOffice_ReturnTrue()
        {
            //Arrange
            int id = 1;
            var existingUser = _context.CareerOffices.FirstOrDefault(x => x.Id == id);
            //Act
            var user = await _careerOfficeRepository.GetByIdAsync(id);
            //Assert
            Assert.Equal(existingUser, user);
        }
        [Fact]
        public async Task GetByEmailAsync_GetCorrectCareerOffice_ReturnTrue()
        {
            //Arrange
            string email = "wp@wp.pl";
            var existingUser = _context.CareerOffices.FirstOrDefault(x => x.Email == email);
            //Act
            var user = await _careerOfficeRepository.GetByEmailAsync(email);
            //Assert
            Assert.Equal(existingUser, user);
        }
        [Fact]
        public async Task UpdateAsync_ObjectNameHasBeenCorrectlyChanged_ReturnTrue()
        {
            //Arrange
            int id = 1;
            string name = "Piotr";
            var user = await _careerOfficeRepository.GetByIdAsync(id);
            user.SetName(name);
            //Act
            await _careerOfficeRepository.UpdateAsync(user);
            //Assert
            var obj = await _careerOfficeRepository.GetByIdAsync(id);
            Assert.Equal(user.Name, obj.Name);
        }
    }
}