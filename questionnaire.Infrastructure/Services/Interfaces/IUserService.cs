using System;
using System.Threading.Tasks;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface IUserService {
        Task RegisterAsync (Guid userId, string email, string name, string password, string role = "user");
    }
}