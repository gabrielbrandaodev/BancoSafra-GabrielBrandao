using Dados.Configuracao;
using Dados.Repositorio.Repositorios;
using Dominio;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validacoes
{
    public class PessoaValidacoes : AbstractValidator<Pessoa>
    {
        public PessoaValidacoes()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo Nome é de preenchimento obrigatório!")
                .Length(2, 150).WithMessage("O campo Nome deve ter tamanho entre 2 e 150 caracteres!");

            RuleFor(x => x.DataDeNascimento)
                .NotEmpty().WithMessage("O campo Data de Nascimento é de preenchimento obrigatório!")
                .NotEqual(DateTime.MinValue).WithMessage("Data de Nascimento inválida")
                .NotEqual(DateTime.MaxValue).WithMessage("Data de Nascimento inválida");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo E-mail é de preenchimento obrigatório!")
                .EmailAddress().WithMessage("E-mail informado inválido")
                .MaximumLength(150).WithMessage("O campo E-mail deve possuir no máximo 150 caracteres");

            RuleFor(x => x.Celular)
                .NotEmpty().WithMessage("O campo Celular é de preenchimento obrigatório!");
        }

        private ValidationResult ValidarEmailExistente(Pessoa pessoa)
        {
            ValidationResult result = new ValidationResult();

            var listaPessoas = new PessoaRepositorio(new DataContext())
                .Retornar(x => x.Email == pessoa.Email && x.PessoaId != pessoa.PessoaId);

            if (listaPessoas.Any())
                result.Errors.Add(new ValidationFailure("Email", "E-mail já cadastrado"));

            return result;
        }

        public ValidationResult ValidarInclusaoEAlteracao(Pessoa pessoa)
        {
            ValidationResult result = new ValidationResult();
            result = Validate(pessoa);

            if (!result.IsValid)
                return result;

            result = ValidarEmailExistente(pessoa);

            return result;
        }

        public ValidationResult ValidarExclusao(Guid id)
        {
            ValidationResult result = new ValidationResult();
            AnimalDomesticoRepositorio animalRepositorio = new AnimalDomesticoRepositorio(new DataContext());

            if (animalRepositorio.Retornar(x => x.PessoaId == id).Any())
                result.Errors.Add(new ValidationFailure("AnimaisDomesticos", "Pessoa possui animais relacionados"));

            return result;
        }
    }
}
