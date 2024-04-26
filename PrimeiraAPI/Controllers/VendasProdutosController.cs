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
    public class VendasProdutosController : ControllerBase
    {
        private readonly MyContext _context;

        public VendasProdutosController(MyContext context)
        {
            _context = context;
        }

        // GET: api/VendasProdutos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendaProduto>>> GetVendasProdutos()
        {
            if (_context.VendasProdutos == null)
            {
                return NotFound();
            }
            return await _context.VendasProdutos.ToListAsync();
        }

        // GET: api/VendasProdutos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendaProduto>> GetVendaProduto(int id)
        {
            if (_context.VendasProdutos == null)
            {
                return NotFound();
            }
            var vendaProduto = await _context.VendasProdutos.FindAsync(id);

            if (vendaProduto == null)
            {
                return NotFound();
            }

            return vendaProduto;
        }

        // PUT: api/VendasProdutos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendaProduto(int id, VendaProduto vendaProduto)
        {
            if (id != vendaProduto.VendaProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(vendaProduto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaProdutoExists(id))
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

        // POST: api/VendasProdutos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VendaProduto>> PostVendaProduto(VendaProduto vendaProduto)
        {
            if (_context.VendasProdutos == null)
            {
                return Problem("Entity set 'MyContext.VendasProdutos'  is null.");
            }

            //Buscar o produto no banco de dados
            var produto = await _context.Produtos.FindAsync(vendaProduto.ProdutoId);
            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            // Verifica se a quantidade do produto é suficiente para venda
            if (produto.QuantidadeProduto < vendaProduto.QuantidadeProdutoVenda)
            {
                return BadRequest("Quantidade Insuficiente");
            }

            // Calcula o valor do item
            var valorItem = produto.PrecoProduto * vendaProduto.QuantidadeProdutoVenda;

            // Busca a venda no banco de dados
            var venda = await _context.Vendas.FindAsync(vendaProduto.VendaId);

            // Atualiza o valor da venda
            venda.ValorTotalVenda += valorItem;

            //Atualiza a quantidade de produto
            produto.QuantidadeProduto -= vendaProduto.QuantidadeProdutoVenda;

            _context.Produtos.Update(produto);
            _context.Vendas.Update(venda);
            _context.VendasProdutos.Add(vendaProduto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendaProduto", new { id = vendaProduto.VendaProdutoId }, vendaProduto);
        }

        // DELETE: api/VendasProdutos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendaProduto(int id)
        {
            if (_context.VendasProdutos == null)
            {
                return NotFound();
            }
            var vendaProduto = await _context.VendasProdutos.FindAsync(id);
            if (vendaProduto == null)
            {
                return NotFound();
            }

            _context.VendasProdutos.Remove(vendaProduto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendaProdutoExists(int id)
        {
            return (_context.VendasProdutos?.Any(e => e.VendaProdutoId == id)).GetValueOrDefault();
        }
    }
}
