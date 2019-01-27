using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface IStudentRepository {
        Task AddAsync (Student student);
        Task<Student> GetByIdAsync (int id, bool isTracking = true);
        Task<Student> GetByIndexNumberAsync (string indexNumber, bool isTracking = true);
        Task<Student> GetByEmailAsync (string email, bool isTracking = true);
    }
}