using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittlePetAPI.Data;
using LittlePetAPI.Models;

namespace LittlePetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly MyContext _context;

        public ComprasController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Comprass
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompras()
        {
          if (_context.Compras == null)
          {
              return NotFound();
          }
            return await _context.Compras.ToListAsync();
        }

        // GET: api/Comprasss/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compra>> GetComprass(int id)
        {
          if (_context.Compras == null)
          {
              return NotFound();
          }
            var Compras = await _context.Compras.FindAsync(id);

            if (Compras == null)
            {
                return NotFound();
            }

            return Compras;
        }

        // PUT: api/Comprass/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompras(int id, Compra Compras)
        {
            if (id != Compras.CompraId)
            {
                return BadRequest();
            }

            _context.Entry(Compras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComprasExists(id))
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

        // POST: api/Comprass
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compra>> PostCompras(Compra Compras)
        {
          if (_context.Compras == null)
          {
              return Problem("Entity set 'MyContext.Compras'  is null.");
          }
            _context.Compras.Add(Compras);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompras", new { id = Compras.CompraId }, Compras);
        }

        // DELETE: api/Comprass/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompras(int id)
        {
            if (_context.Compras == null)
            {
                return NotFound();
            }
            var Compras = await _context.Compras.FindAsync(id);
            if (Compras == null)
            {
                return NotFound();
            }

            _context.Compras.Remove(Compras);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComprasExists(int id)
        {
            return (_context.Compras?.Any(e => e.CompraId == id)).GetValueOrDefault();
        }
    }
}
