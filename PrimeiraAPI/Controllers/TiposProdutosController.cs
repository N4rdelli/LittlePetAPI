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
    public class TiposProdutosController : ControllerBase
    {
        private readonly MyContext _context;

        public TiposProdutosController(MyContext context)
        {
            _context = context;
        }

        // GET: api/TipoProdutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProduto>>> GetTiposProdutos()
        {
            if (_context.TiposProdutos == null)
            {
                return NotFound();
            }
            return await _context.TiposProdutos.ToListAsync();
        }

        // GET: api/TipoProdutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProduto>> GetTipoProduto(int id)
        {
            if (_context.TiposProdutos == null)
            {
                return NotFound();
            }
            var tipoProduto = await _context.TiposProdutos.FindAsync(id);

            if (tipoProduto == null)
            {
                return NotFound();
            }

            return tipoProduto;
        }

        [HttpGet("api/tiposprod/getName/{name}")]
        public async Task<ActionResult<IEnumerable<TipoProduto>>> GetTipoProdutoByName(string name)

        {
            var listaTipoProduto = _context.TiposProdutos.Where(u => u.NomeTipoProduto.Contains(name)).ToList();

            if (listaTipoProduto != null)
            {
                return Ok(listaTipoProduto);
            }
            return NoContent();

        }

        // PUT: api/TipoProdutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoProduto(int id, TipoProduto tipoProduto)
        {
            if (id != tipoProduto.TipoProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(tipoProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoProdutoExists(id))
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

        // POST: api/TipoProdutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoProduto>> PostTipoProduto(TipoProduto tipoProduto)
        {
            if (_context.TiposProdutos == null)
            {
                return Problem("Entity set 'MyContext.TiposProdutos'  is null.");
            }
            _context.TiposProdutos.Add(tipoProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoProduto", new { id = tipoProduto.TipoProdutoId }, tipoProduto);
        }

        // DELETE: api/TipoProdutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoProduto(int id)
        {
            if (_context.TiposProdutos == null)
            {
                return NotFound();
            }
            var tipoProduto = await _context.TiposProdutos.FindAsync(id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            _context.TiposProdutos.Remove(tipoProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoProdutoExists(int id)
        {
            return (_context.TiposProdutos?.Any(e => e.TipoProdutoId == id)).GetValueOrDefault();
        }
    }
}
