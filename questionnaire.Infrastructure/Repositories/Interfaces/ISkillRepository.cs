using System.Collections.Generic;
using System.Threading.Tasks;
using questionnaire.Core.Domains;

namespace questionnaire.Infrastructure.Repositories.Interfaces {
    public interface ISkillRepository {
        Task<Skill> GetByIdAsync (int id, bool isTracking = true);
    }
}