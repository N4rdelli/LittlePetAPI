using LittlePetAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "A Data da Venda é obrigatória!")]
        public DateTime DataVenda { get; set; }

        [Display(Name = "Valor Total")]
        public double? ValorTotalVenda { get; set; } = 0;

        //Chaves Estrangeiras
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

    }
}
