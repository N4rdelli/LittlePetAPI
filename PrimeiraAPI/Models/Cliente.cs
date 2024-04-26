using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "O Nome do Cliente é obrigatório!")]
        [Display(Name = "Nome")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome do Cliente deve ter entre 3 e 100 caracteres")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "O CPF do Cliente é obrigatório!")]
        [Display(Name = "CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF do Cliente deve ter 11 dígitos")]
        public string CpfCliente { get; set; }

        [Required(ErrorMessage = "A Data de Nascimento do Cliente é obrigatório.")]
        public DateTime NascimentoCliente { get; set; }

        [Required(ErrorMessage = "O Celular do Cliente é obrigatório!")]
        [Display(Name = "Celular")]
        public string CelularCliente { get; set; }

        [Required(ErrorMessage = "O Email do Cliente é obrigatório!")]
        [Display(Name = "Email")]
        //[EmailAddress(ErrorMessage = "O Email do Cliente deve estar em um formato válido!")]
        public string EmailCliente { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [Display(Name = "Senha")]
        public string SenhaCliente { get; set; }
    }
}

