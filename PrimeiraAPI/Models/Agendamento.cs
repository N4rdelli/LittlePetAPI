using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace LittlePetAPI.Models
{
    public class Agendamento
    {
        [Key]
        public int AgendamentoId { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "A Data do Agendamento é obrigatória!")]
        public DateTime DiaHoraAgendamento { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "Insira o valor do agendamento!")]
        public decimal ValorAgendamento { get; set; }

        [Display(Name = "Descrição")]
        public string DescricaoAgendamento { get; set; }

        //Chaves Estrangeiras

        public int ServicoId { get; set; }
        public Servico? Servico { get; set; }

        public int PetId { get; set; }
        public Pet? Pet { get; set; }


    }
}
