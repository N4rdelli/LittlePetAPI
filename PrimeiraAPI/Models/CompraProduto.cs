using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class CompraProduto
    {
        [Key]
        public int CompraProdutoId { get; set; }

        public int CompraId { get; set; }
        public Compra ? Compra {  get; set; }

        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

        public double QuantidadeProdutoCompra {  get; set; }
    }
}
