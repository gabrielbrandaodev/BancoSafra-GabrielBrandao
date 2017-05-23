using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.Enumeradores;
using Utilitarios.MetodosUtilitarios;

namespace Negocios.ViewModels
{
    public class AnimalDomesticoViewModel
    {
        public AnimalDomesticoViewModel()
        {
            Validador = new FluentValidation.Results.ValidationResult();
        }

        [Key]
        public Guid AnimalDomesticoId { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório!")]
        [MinLength(2, ErrorMessage = "O campo Nome deve possuir mais que 2 caracteres!")]
        [MaxLength(150, ErrorMessage = "O campo Nome deve possuir até 150 caractéres!")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Animal Castrado?")]
        public bool AnimalCastrado { get; set; }

        [Display(Name = "Tipo de Animal")]
        public EnumTipoAnimal TipoAnimal { get; set; }

        [ScaffoldColumn(false)]
        public string TipoAnimalToString
        {
            get { return TipoAnimal.ObterDescricao(); }
        }

        [Display(Name = "Proprietário")]
        [Required(ErrorMessage = "Preenchimento obrigatório!")]
        public Guid PessoaId { get; set; }

        public PessoaViewModel Pessoa { get; set; }

        [ScaffoldColumn(false)]
        public FluentValidation.Results.ValidationResult Validador { get; set; }

        public static explicit operator AnimalDomesticoViewModel(AnimalDomestico model)
        {
            if (model == null)
                return null;

            var animalDomesticoVM = new AnimalDomesticoViewModel();

            animalDomesticoVM.AnimalDomesticoId = model.AnimalDomesticoId;
            animalDomesticoVM.Nome = model.Nome;
            animalDomesticoVM.AnimalCastrado = model.AnimalCastrado;
            animalDomesticoVM.TipoAnimal = model.TipoAnimal;
            animalDomesticoVM.PessoaId = model.PessoaId;

            if (model.Pessoa != null)
                animalDomesticoVM.Pessoa = (PessoaViewModel)model.Pessoa;

            return animalDomesticoVM;
        }
    }
}
