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
    public class ComprasProdutosController : ControllerBase
    {
        private readonly MyContext _context;

        public ComprasProdutosController(MyContext context)
        {
            _context = context;
        }

        // GET: api/ComprasProdutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraProduto>>> GetComprasProdutos()
        {
          if (_context.ComprasProdutos == null)
          {
              return NotFound();
          }
            return await _context.ComprasProdutos.ToListAsync();
        }

        // GET: api/ComprasProdutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompraProduto>> GetComprasProdutos(int id)
        {
          if (_context.ComprasProdutos == null)
          {
              return NotFound();
          }
            var ComprasProdutos = await _context.ComprasProdutos.FindAsync(id);

            if (ComprasProdutos == null)
            {
                return NotFound();
            }

            return ComprasProdutos;
        }

        // PUT: api/ComprasProdutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComprasProdutos(int id, CompraProduto ComprasProdutos)
        {
            if (id != ComprasProdutos.CompraProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(ComprasProdutos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComprasProdutosExists(id))
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

        // POST: api/ComprasProdutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompraProduto>> PostComprasProdutos(CompraProduto ComprasProdutos)
        {
          if (_context.ComprasProdutos == null)
          {
              return Problem("Entity set 'MyContext.ComprasProdutos'  is null.");
          }

            //Buscar o produto no banco de dados
            var produto = await _context.Produtos.FindAsync(ComprasProdutos.ProdutoId);
            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            // Calcula o valor do item
            var valorItem = produto.PrecoProduto * ComprasProdutos.QuantidadeProdutoCompra;

            // Busca a compra no banco de dados
            var compra = await _context.Compras.FindAsync(ComprasProdutos.CompraId);

            // Atualiza o valor da venda
            compra.ValorTotalCompra += valorItem;

            //Atualiza a quantidade de produto
            produto.QuantidadeProduto -= ComprasProdutos.QuantidadeProdutoCompra;

            _context.Produtos.Update(produto);
            _context.Compras.Update(compra);
            _context.ComprasProdutos.Add(ComprasProdutos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComprasProdutos", new { id = ComprasProdutos.CompraProdutoId }, ComprasProdutos);

    }


        // DELETE: api/ComprasProdutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComprasProdutos(int id)
        {
            if (_context.ComprasProdutos == null)
            {
                return NotFound();
            }
            var ComprasProdutos = await _context.ComprasProdutos.FindAsync(id);
            if (ComprasProdutos == null)
            {
                return NotFound();
            }

            _context.ComprasProdutos.Remove(ComprasProdutos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComprasProdutosExists(int id)
        {
            return (_context.ComprasProdutos?.Any(e => e.CompraProdutoId == id)).GetValueOrDefault();
        }
    }
}
