using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class FormaPagamento
    {
        [Key]
        public int FormaPagamentoId { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "A Forma de Pagamento é obrigatória.")]
        public string FormaPagamentoNome { get; set; }


        [Display(Name = "Pagamento")]
        [Required(ErrorMessage = "O Pagamento é obrigatório!")]
        public double PagamentoValor { get; set; } = 0;

        [Display(Name = "Troco")]
        public double? PagamentoTroco { get; set; } = 0;

        public int VendaId { get; set; }
        public Venda? Venda { get; set; }

    }
}
