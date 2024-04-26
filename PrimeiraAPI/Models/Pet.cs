using LittlePetAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace LittlePetAPI.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }

        [Required(ErrorMessage = "Indique o tipo do pet.")]
        [Display(Name = "Tipo do pet")]
        [StringLength(50)]
        public string TipoPet { get; set; }

        [Required(ErrorMessage = "O Nome do pet é obrigatório!")]
        [Display(Name = "Nome")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O Nome do pet deve ter entre 2 e 50 caracteres")]
        public string NomePet { get; set; }

        [Display(Name = "Raça")]
        [StringLength(50)]
        public string? RacaPet { get; set; }

        [Required(ErrorMessage = "Indique a idade do pet.")]
        [Display(Name = "Idade")]
        public int IdadePet { get; set; }

        [Required(ErrorMessage = "A Carteira de Vacinação do pet é obrigatória!")]
        public string CarteiraVacPet { get; set; }


        //Foreign Key Cliente
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
