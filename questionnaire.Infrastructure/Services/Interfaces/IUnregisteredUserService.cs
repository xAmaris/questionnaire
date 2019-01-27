using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains.ImportFile;

namespace questionnaire.Infrastructure.Services.Interfaces {
    public interface IUnregisteredUserService {
        Task<bool> ExistByEmailAsync (string email);
        Task CreateAsync (string name, string surname, string course,
            string dateOfCompletion, string typeOfStudy, string email);
        Task<IEnumerable<UnregisteredUser>> GetAllAsync ();
        Task<UnregisteredUser> GetByIdAsync (int id);
        Task UpdateAsync (int id, string name, string surname, string course,
            string dateOfCompletion, string typeOfStudy, string email);
        Task DeleteAsync (int id);
    }
}