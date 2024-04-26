using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class AnimalProduto
    {
        [Key]
        public int AnimalProdutoId { get; set; }

        [Display(Name = "Tipo de Animal")]
        [Required(ErrorMessage = "O Tipo de Animal do produto é obrigatório!")]
        public string AnimalProdutoNome { get; set; }
    }
}