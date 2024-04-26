using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class TipoProduto
    {
        [Key]
        public int TipoProdutoId { get; set; }

        [Display(Name = "Tipo do Produto")]
        [Required(ErrorMessage = "O Tipo do Produto é obrigatório!")]
        public string NomeTipoProduto { get; set; }
    }
}