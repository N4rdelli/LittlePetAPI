using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class Servico
    {
        [Key]
        public int ServicoId { get; set; }

        [Display(Name = "Serviço")]
        [Required(ErrorMessage = "O Nome do Serviço é obrigatório!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O Nome do Serviço deve ter entre 3 e 50 caracteres")]
        public string NomeServico { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Indique o Preço do Serviço.")]
        public double PrecoServico { get; set; }
    }
}
