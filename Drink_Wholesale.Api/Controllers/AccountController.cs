
using System;
using System.Threading.Tasks;
using Drink_Wholesale.DTO;
using Drink_Wholesale.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ELTE.TravelAgency.Service.Controllers
{
	/// <summary>
	/// Felhasználókezelést biztosító vezérlő.
	/// </summary>
	[Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AccountController : Controller
    {
        /// <summary>
        /// Authentikációs szolgáltatás.
        /// </summary>
        private readonly SignInManager<ApplicationUser> _signInManager;


        /// <summary>
        /// Vezérlő példányosítása.
        /// </summary>
        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
	        _signInManager = signInManager;
        }

        /// <summary>
        /// Bejelentkezés.
        /// </summary>
        /// <param name="loginDTO">Bejelentkezési adatok.</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDTO)
        {
            try
            {
                // bejelentkeztetjük a felhasználót
                var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false, false);
                if (!result.Succeeded) // ha nem sikerült, akkor nincs bejelentkeztetés
                {
                    return Forbid();
                }

                // ha sikeres volt az ellenőrzés
                return Ok();
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }

        /// <summary>
        /// Kijelentkezés.
        /// </summary>
        [HttpGet("logout")]
        [Authorize] // csak bejelentkezett felhasználóknak
        public async Task<IActionResult> Logout()
        {
            try
            {
				// kijelentkeztetjük az aktuális felhasználót
				await _signInManager.SignOutAsync();
				return Ok();
            }
            catch
            {
				// Internal Server Error
	            return StatusCode(StatusCodes.Status500InternalServerError);
			}
        }
    }
}
