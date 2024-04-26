using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class VendaProduto
    {
        [Key]
        public int VendaProdutoId { get; set; }

        public int VendaId { get; set; }
        public Venda? Venda { get; set; }

        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public double QuantidadeProdutoVenda { get; set; }
    }
}