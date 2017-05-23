using Dominio;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validacoes
{
    public class AnimalDomesticoValidacoes : AbstractValidator<AnimalDomestico>
    {
        public AnimalDomesticoValidacoes()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo Nome é de preenchimento obrigatório!")
                .Length(2, 150).WithMessage("O campo Nome deve ter tamanho entre 2 e 150 caracteres!");

            RuleFor(x => x.PessoaId)
                .NotEmpty().WithMessage("Animal doméstico precisa estar vínculado a uma pessoa");
        }
    }
}
