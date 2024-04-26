using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Nome do Fornecedor é obrigatório!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome do Fornecedor deve ter entre 3 e 100 caracteres")]
        public string NomeFornecedor { get; set; }

        [Display(Name = "CNPJ")]
        [Required(ErrorMessage = "O CNPJ do Fornecedor é obrigatório!")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CNPJ do cliente deve ter 14 dígitos")]
        public string CNPJFornecedor { get; set; }
    }
}