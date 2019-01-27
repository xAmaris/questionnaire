using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using questionnaire.Infrastructure.Commands.Account;
using questionnaire.Infrastructure.Extensions.ExceptionHandling;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Api.Controllers {
    [Authorize]
    public class AccountUpdateController : ApiUserController {
        private readonly IAccountService _accountService;

        public AccountUpdateController (IAccountService accountService) {
            _accountService = accountService;
        }

        [HttpPut ("accounts")]
        public async Task<IActionResult> AccountUpdate ([FromBody] UpdateAccount command) {
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            try {
                await _accountService.UpdateAsync (UserId, command.Name, command.Surname, command.Email,
                    command.PhoneNumber, command.CompanyName, command.Location, command.CompanyDescription);
                return StatusCode (200);
            } catch (Exception e) {
                return BadRequest (e.Message);
            }
        }

    }
}