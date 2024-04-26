using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Nome do Produto é obrigatório!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O Nome do Produto deve ter entre 3 e 50 caracteres")]
        public string NomeProduto { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Insira a descrição do Produto.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "A Descrição do Produto deve ter entre 5 e 200 caracteres")]
        public string ProdutoDescricao { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "É obrigatório informar o Preço do Produto!")]
        public double PrecoProduto { get; set; }

        [Display(Name = "Quantidade em Estoque")]
        [Required(ErrorMessage = "Indique a quantidade disponível desse produto!")]
        public double QuantidadeProduto { get; set; }


        //Chaves Estrangeiras
        public int TipoProdutoId { get; set; }
        public TipoProduto? TipoProduto { get; set; }

        public int AnimalProdutoId { get; set; }
        public AnimalProduto? AnimalProduto { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }
}
