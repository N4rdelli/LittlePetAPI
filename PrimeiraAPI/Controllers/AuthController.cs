using LittlePetAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LittlePetAPI.Services;

namespace LittlePetAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("api/login")]
        public IActionResult Login(string username, string password)
        {

            var user = _userManager.FindByNameAsync(username).Result;
            if (user == null)
            {
                return BadRequest("Usuario Não Cadastrado!");
            }

            var result = _signInManager.CheckPasswordSignInAsync(user, password, false).Result;
            if (result.Succeeded)
            {
                // Adiconar esse linha para adicionar Roles ao Token
                var roles = _userManager.GetRolesAsync(user).Result.ToList();

                // Adicionar o parametro roles
                var token = TokenService.GenerateToken(user, roles);
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost("api/logout")]
        public IActionResult Logout()
        {
            var result = _signInManager.SignOutAsync();
            if (result.IsCompletedSuccessfully)
            {
                return Ok();
            }
            return BadRequest(StatusCodes.Status503ServiceUnavailable);
        }


        //POST: Atualizar senha
        [HttpPost("api/updatepassword")]
        public async Task<IActionResult> UpdatePassword(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Usuario Não Cadastrado!");
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
