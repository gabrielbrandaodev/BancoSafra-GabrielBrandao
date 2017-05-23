using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.ViewModels
{
    public class PessoaViewModel
    {
        public PessoaViewModel()
        {
            PessoaId = new Guid();
            AnimaisDomesticos = new List<AnimalDomesticoViewModel>();
            Validador = new FluentValidation.Results.ValidationResult();
        }

        [Key]
        public Guid PessoaId { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório!")]
        [MinLength(2, ErrorMessage = "O campo Nome deve possuir mais que 2 caracteres!")]
        [MaxLength(150, ErrorMessage = "O campo Nome deve possuir até 150 caracteres!")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório!")]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataDeNascimento { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório!")]
        [MaxLength(150, ErrorMessage = "Máximo 150 caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um E-mail válido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preenchimento obrigatório!")]
        [Display(Name = "Celular")]
        [Phone(ErrorMessage = "Informe um telefone válido")]
        public string Celular { get; set; }

        public IList<AnimalDomesticoViewModel> AnimaisDomesticos { get; set; }

        [ScaffoldColumn(false)]
        public FluentValidation.Results.ValidationResult Validador { get; set; }

        public static explicit operator PessoaViewModel(Pessoa model)
        {
            if (model == null)
                return null;

            var pessoaVM = new PessoaViewModel();

            pessoaVM.PessoaId = model.PessoaId;
            pessoaVM.Nome = model.Nome;
            pessoaVM.DataDeNascimento = model.DataDeNascimento;
            pessoaVM.Email = model.Email;
            pessoaVM.Celular = model.Celular;

            return pessoaVM;
        }
    }
}
