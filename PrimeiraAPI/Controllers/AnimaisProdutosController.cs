using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittlePetAPI.Data;
using LittlePetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittlePetAPI.Data;
using LittlePetAPI.Models;

namespace LittlePet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimaisProdutosController : ControllerBase
    {
        private readonly MyContext _context;

        public AnimaisProdutosController(MyContext context)
        {
            _context = context;
        }

        // GET: api/AnimaisProdutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalProduto>>> GetAnimalProdutos()
        {
            if (_context.AnimalProdutos == null)
            {
                return NotFound();
            }
            return await _context.AnimalProdutos.ToListAsync();
        }

        // GET: api/AnimaisProdutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalProduto>> GetAnimalProduto(int id)
        {
            if (_context.AnimalProdutos == null)
            {
                return NotFound();
            }
            var animalProduto = await _context.AnimalProdutos.FindAsync(id);

            if (animalProduto == null)
            {
                return NotFound();
            }

            return animalProduto;
        }

        [HttpGet("api/animaisprod/getName/{name}")]
        public async Task<ActionResult<IEnumerable<AnimalProduto>>> GetAnimalProdutoByName(string name)

        {

            var listaAnimalProduto = _context.AnimalProdutos.Where(u => u.AnimalProdutoNome.Contains(name)).ToList();

            if (listaAnimalProduto != null)
            {
                return Ok(listaAnimalProduto);
            }
            return NoContent();

        }

        // PUT: api/AnimaisProdutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimalProduto(int id, AnimalProduto animalProduto)
        {
            if (id != animalProduto.AnimalProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(animalProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AnimaisProdutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnimalProduto>> PostAnimalProduto(AnimalProduto animalProduto)
        {
            if (_context.AnimalProdutos == null)
            {
                return Problem("Entity set 'MyContext.AnimalProdutos'  is null.");
            }
            _context.AnimalProdutos.Add(animalProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimalProduto", new { id = animalProduto.AnimalProdutoId }, animalProduto);
        }

        // DELETE: api/AnimaisProdutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimalProduto(int id)
        {
            if (_context.AnimalProdutos == null)
            {
                return NotFound();
            }
            var animalProduto = await _context.AnimalProdutos.FindAsync(id);
            if (animalProduto == null)
            {
                return NotFound();
            }

            _context.AnimalProdutos.Remove(animalProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnimalProdutoExists(int id)
        {
            return (_context.AnimalProdutos?.Any(e => e.AnimalProdutoId == id)).GetValueOrDefault();
        }
    }
}
