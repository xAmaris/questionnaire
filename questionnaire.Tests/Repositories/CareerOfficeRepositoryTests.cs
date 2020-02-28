using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Xunit;

namespace questionnaire.Tests.Repositories
{
    public class CareerOfficeRepositoryTests : IClassFixture<TestHostFixture>
    {
        private readonly QuestionnaireContext _context;
        private readonly ICareerOfficeRepository _careerOfficeRepository;
        public CareerOfficeRepositoryTests(TestHostFixture fixture)
        {
            _context = fixture._context;
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
            var careerOffice = _context.CareerOffices.FirstOrDefault();
            //Act
            var user = await _careerOfficeRepository.GetByIdAsync(careerOffice.Id);
            //Assert
            Assert.NotNull(user);
            Assert.Equal(careerOffice, user);
        }
        [Fact]
        public async Task GetByEmailAsync_GetCorrectCareerOffice_ReturnTrue()
        {
            //Arrange
            var careerOffice = _context.CareerOffices.FirstOrDefault();
            //Act
            var user = await _careerOfficeRepository.GetByEmailAsync(careerOffice.Email);
            //Assert
            Assert.NotNull(user);
            Assert.Equal(careerOffice, user);
        }
        [Fact]
        public async Task UpdateAsync_ObjectNameHasBeenCorrectlyChanged_ReturnTrue()
        {
            //Arrange
            string name = "Piotr";
            var careerOffice = _context.CareerOffices.FirstOrDefault();
            careerOffice.SetName(name);
            //Act
            await _careerOfficeRepository.UpdateAsync(careerOffice);
            //Assert
            var obj = await _careerOfficeRepository.GetByIdAsync(careerOffice.Id);
            Assert.Equal(careerOffice.Name, obj.Name);
        }
    }
}