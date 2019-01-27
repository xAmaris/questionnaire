using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using questionnaire.Core.Domains.Abstract;
using questionnaire.Infrastructure.Data;
using questionnaire.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace questionnaire.Infrastructure.Repositories {
    public class AccountRepository : IAccountRepository {
        private readonly questionnaireContext _context;

        public AccountRepository (questionnaireContext context) {
            _context = context;
        }

        public async Task<Account> GetByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.Accounts
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Accounts
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<Account> GetByEmailAsync (string email, bool isTracking = true) {
            if (isTracking) {
                return await _context.Accounts
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.Email.ToLowerInvariant () == email.ToLowerInvariant ());
            }
            return await _context.Accounts
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.Email.ToLowerInvariant () == email.ToLowerInvariant ());
        }

        public async Task<Account> GetByActivationKeyAsync (Guid activationKey, bool isTracking = true) {
            if (isTracking) {
                return await _context.Accounts
                    .AsTracking ()
                    .Include (x => x.AccountActivation)
                    .SingleOrDefaultAsync (x =>
                        x.AccountActivation.ActivationKey == activationKey && x.AccountActivation.Active == false);
            }
            return await _context.Accounts
                .AsNoTracking ()
                .Include (x => x.AccountActivation)
                .SingleOrDefaultAsync (x =>
                    x.AccountActivation.ActivationKey == activationKey && x.AccountActivation.Active == false);
        }

        public async Task<Account> GetWithAccountRestoringPasswordAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.Accounts
                    .AsTracking ()
                    .Include (x => x.AccountRestoringPassword)
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Accounts
                .AsNoTracking ()
                .Include (x => x.AccountRestoringPassword)
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<Account> GetWithAccountRestoringPasswordByTokenAsync (Guid token, bool isTracking = true) {
            if (isTracking) {
                return await _context.Accounts
                    .Include (x => x.AccountRestoringPassword)
                    .AsTracking ()
                    .SingleOrDefaultAsync (x => x.AccountRestoringPassword.Token == token);
            }
            return await _context.Accounts
                .Include (x => x.AccountRestoringPassword)
                .AsNoTracking ()
                .SingleOrDefaultAsync (x => x.AccountRestoringPassword.Token == token);
        }

        public async Task<Account> GetWithProfileEditionByIdAsync (int id, bool isTracking = true) {
            if (isTracking) {
                return await _context.Accounts
                    .AsTracking ()
                    .Include (x => x.Certificates)
                    .Include (x => x.Courses)
                    .Include (x => x.Educations)
                    .Include (x => x.Experiences)
                    .Include (x => x.Languages)
                    .Include (x => x.ProfileLink)
                    .Include (x => x.Skills)
                    .SingleOrDefaultAsync (x => x.Id == id);
            }
            return await _context.Accounts.AsNoTracking ()
                .Include (x => x.Certificates)
                .Include (x => x.Courses)
                .Include (x => x.Educations)
                .Include (x => x.Experiences)
                .Include (x => x.Languages)
                .Include (x => x.ProfileLink)
                .Include (x => x.Skills)
                .SingleOrDefaultAsync (x => x.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAllAsync (bool isTracking = true) {
            if (isTracking) {
                return await Task.FromResult (_context.Accounts
                    .AsTracking ()
                    .AsEnumerable ());
            }
            return await Task.FromResult (_context.Accounts.AsNoTracking ().AsEnumerable ());
        }

        public async Task UpdateAsync (Account account) {
            _context.Accounts.Update (account);
            await _context.SaveChangesAsync ();
        }

        public async Task DeleteAsync (Account account) {
            _context.Accounts.Remove (account);
            await _context.SaveChangesAsync ();
        }
    }
}