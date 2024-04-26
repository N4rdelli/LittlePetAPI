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

        /*[Required(ErrorMessage = "O campo CEP é obrigatório!")]
        [Display(Name = "CEP")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "O CEP do Cliente deve ter 7 dígitos")]
        public string CepCliente { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório!")]
        [Display(Name = "Cidade")]
        public string CidadeEnderecoCliente { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório!")]
        [Display(Name = "Bairro")]
        public string BairroEnderecoCliente { get; set; }

        [Required(ErrorMessage = "O campo Rua é obrigatório!")]
        [Display(Name = "Rua")]
        public string RuaEnderecoCliente { get; set; }

        [Display(Name = "Número")]
        [StringLength(4, ErrorMessage = "Sua casa não possui 5 números, ao menos nunca vi!")]
        public int NumeroEnderecoCliente { get; set; }

        [Display(Name = "Complemento")]
        public string? ComplementoEnderecoCliente { get; set; }*/

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

