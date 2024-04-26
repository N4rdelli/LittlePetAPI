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
    public class FormasPagamentoController : ControllerBase
    {
        private readonly MyContext _context;

        public FormasPagamentoController(MyContext context)
        {
            _context = context;
        }

        // GET: api/FormasPagamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaPagamento>>> GetFormasPagamentos()
        {
            if (_context.FormasPagamentos == null)
            {
                return NotFound();
            }
            return await _context.FormasPagamentos.ToListAsync();
        }

        // GET: api/FormasPagamento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormaPagamento>> GetFormaPagamento(int id)
        {
            if (_context.FormasPagamentos == null)
            {
                return NotFound();
            }
            var formaPagamento = await _context.FormasPagamentos.FindAsync(id);

            if (formaPagamento == null)
            {
                return NotFound();
            }

            return formaPagamento;
        }

        // PUT: api/FormasPagamento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaPagamento(int id, FormaPagamento formaPagamento)
        {
            if (id != formaPagamento.FormaPagamentoId)
            {
                return BadRequest();
            }

            _context.Entry(formaPagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormaPagamentoExists(id))
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

        // POST: api/FormasPagamento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PagamentoTotal")]
        public async Task<ActionResult<FormaPagamento>> PostFormaPagamento(FormaPagamento formaPagamento)
        {
            if (_context.FormasPagamentos == null)
            {
                return Problem("Entity set 'MyContext.FormasPagamentos'  is null.");
            }

            var venda = await _context.Vendas.Where(v => v.VendaId == formaPagamento.VendaId).FirstOrDefaultAsync();
            if (venda == null)
            {
                return NoContent();
            }

            if (venda.ValorTotalVenda > formaPagamento.PagamentoValor)
            {
                return BadRequest("Valor de Pagamento é menor do que o Valor da Venda");
            }

            if (venda.ValorTotalVenda < formaPagamento.PagamentoValor)
            {
                formaPagamento.PagamentoTroco = formaPagamento.PagamentoValor - venda.ValorTotalVenda;
            }

            _context.FormasPagamentos.Add(formaPagamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFormaPagamento", new { id = formaPagamento.FormaPagamentoId }, formaPagamento);
        }

        // DELETE: api/FormasPagamento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormaPagamento(int id)
        {
            if (_context.FormasPagamentos == null)
            {
                return NotFound();
            }
            var formaPagamento = await _context.FormasPagamentos.FindAsync(id);
            if (formaPagamento == null)
            {
                return NotFound();
            }

            _context.FormasPagamentos.Remove(formaPagamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FormaPagamentoExists(int id)
        {
            return (_context.FormasPagamentos?.Any(e => e.FormaPagamentoId == id)).GetValueOrDefault();
        }
    }
}
