using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class Compra
    {
        [Key]
        public int CompraId { get; set; }

        [Display(Name = "Data de Chegada")]
        [Required(ErrorMessage = "A Data de Chegada da Compra é obrigatória")]
        public DateTime DataChegada { get; set; }

        [Display(Name = "Valor Total")]
        public double? ValorTotalCompra { get; set; } = 0;

        //Chaves Estrangeiras
        public int FornecedorId { get; set; }
        public Fornecedor ? Fornecedor {  get; set; }
    }
}
