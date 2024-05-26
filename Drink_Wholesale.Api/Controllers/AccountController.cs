using Drink_Wholesale.DTO;
using Drink_Wholesale.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Drink_Wholesale.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // api/Account/Login
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            if (_signInManager.IsSignedIn(User))
                await _signInManager.SignOutAsync();

            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized("A bejelentkezés sikertelen!");
        }

        // api/Account/Logout
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto userReg)
        {
            ApplicationUser user = new()
            {
                UserName = userReg.UserName,
                Email = userReg.Email
            };
            var result =await  _userManager.CreateAsync(user, userReg.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Hiba");
            }
            await _signInManager.SignInAsync(user, false);
            return Created();
        }
    }
}
