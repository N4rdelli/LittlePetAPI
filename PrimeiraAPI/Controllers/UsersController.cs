using LittlePetAPI.Data;
using LittlePetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittlePetAPI.Data;
using LittlePetAPI.Models;

namespace LittlePetAPI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MyContext _context;

        public UsersController(UserManager<IdentityUser> userManager, MyContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        //[Authorize]
        [HttpPost("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        // GET : api/users - Listar todos os usuários
        [HttpGet("api/users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users != null)
            {
                return Ok(users);
            }
            return NoContent();
        }

        // GET : api/users/{id} - Buscar usuário por id
        [HttpGet("api/users/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NoContent();
        }

        // GET : api/users/{name} - Buscar usuário por nome
        [HttpGet("api/users/getName/{name}")]

        public async Task<IActionResult> GetByName(string name)
        {

            var listaUsers = _context.Users.Where(u => u.UserName.Contains(name)).ToList();

            if (listaUsers != null)
            {
                return Ok(listaUsers);
            }
            return NoContent();

        }

        // PUT : api/users/{id} - Atualizar usuário
        [HttpPut("api/users/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RegisterModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE : api/users/{id} - Deletar usuário
        [HttpDelete("api/users/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest();
            }
            return NoContent();
        }
    }
}
