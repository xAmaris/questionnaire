using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using questionnaire.Infrastructure.Services.Interfaces;

namespace questionnaire.Api.Controllers {
    public class AccountController : Controller {
        private readonly IUserService _userService;
        public AccountController (IUserService userService) {
            _userService = userService;
        }

        // [HttpGet]
        // public async Task<IActionResult> GetTask () {
        //     throw new NotImplementedException ();
        // }

        // [HttpGet ("tickets")]
        // public async Task<IActionResult> GetTickets () {
        //     throw new NotImplementedException ();
        // }

        // [HttpPost ("register")]
        // public async Task<IActionResult> Post () {
        //     throw new NotImplementedException ();
        // }

        // [HttpPost ("login")]
        // public async Task<IActionResult> Login () {
        //     throw new NotImplementedException ();
        // }
        // public async Task<IActionResult> Register () {
        //     throw new NotImplementedException ();
        // }
    }
}