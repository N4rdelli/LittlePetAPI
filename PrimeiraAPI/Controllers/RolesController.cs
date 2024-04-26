using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LittlePetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<IdentityRole>> PostRole(string newRole)
        {
            var role = new IdentityRole(newRole);
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return Ok(role);
            }
            return BadRequest();
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(string id, string newRole)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return BadRequest("Role Não Cadastrada!");
            }

            role.Name = newRole;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return BadRequest("Role Não Cadastrada!");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }


        // Vincular Role ao Usuário
        [HttpPost("AddRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Usuario Não Cadastrado!");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return BadRequest("Role Não Cadastrada!");
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        //Buscar Roles do Usuário
        [HttpGet("GetRolesByUser")]
        public async Task<ActionResult<IEnumerable<string>>> GetRolesByUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Usuario Não Cadastrado!");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        // Desvincular Role do Usuário
        [HttpDelete("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Usuario Não Cadastrado!");
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return BadRequest("Role Não Cadastrada!");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
