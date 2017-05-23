using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocios.Interfaces;
using Negocios.ViewModels;
using Dominio;
using Dados.Repositorio.Repositorios;
using Dados.Configuracao;
using Validacoes;
using FluentValidation.Results;

namespace Negocios
{
    public class PessoaAppService : IPessoaAppService
    {
        private DataContext _context;

        public PessoaAppService()
        {
            _context = new DataContext();
        }

        /// <summary>
        /// Adicionar Pessoa
        /// </summary>
        /// <param name="pessoaViewModel">PessoaViewModel</param>
        /// <returns>PessoaViewModel recebida com erros de validação quando necessário</returns>
        public PessoaViewModel Adicionar(PessoaViewModel pessoaViewModel)
        {
            Pessoa pessoa = new Pessoa();
            AtualizarObjeto(pessoa, pessoaViewModel);

            ValidationResult resultadoValidacoes = new PessoaValidacoes().ValidarInclusaoEAlteracao(pessoa);

            if (resultadoValidacoes.IsValid)
            {
                PessoaRepositorio repositorio = new PessoaRepositorio(_context);
                pessoa.PessoaId = Guid.NewGuid();
                repositorio.Incluir(pessoa);
                repositorio.SalvarTodos();
            }
            else
            {
                pessoaViewModel.Validador = resultadoValidacoes;
            }

            return pessoaViewModel;
        }

        /// <summary>
        /// Atualizar Pessoa
        /// </summary>
        /// <param name="pessoaViewModel">PessoaViewModel</param>
        /// <returns>PessoaViewModel recebida com erros de validação quando necessário</returns>
        public PessoaViewModel Atualizar(PessoaViewModel pessoaViewModel)
        {
            PessoaRepositorio repositorio = new PessoaRepositorio(_context);
            Pessoa pessoa = repositorio.RetornarPorId(pessoaViewModel.PessoaId);

            AtualizarObjeto(pessoa, pessoaViewModel);

            ValidationResult resultadoValidacoes = new PessoaValidacoes().ValidarInclusaoEAlteracao(pessoa);

            if (resultadoValidacoes.IsValid)
            {
                repositorio.Atualizar(pessoa);
                repositorio.SalvarTodos();
            }
            else
            {
                pessoaViewModel.Validador = resultadoValidacoes;
            }

            return pessoaViewModel;
        }

        /// <summary>
        /// Obter Pessoa por E-mail
        /// </summary>
        /// <param name="email">E-mail informado</param>
        /// <returns>PessoaViewModel com e-mail correspondente ao informado</returns>
        public PessoaViewModel ObterPorEmail(string email)
        {
            return new PessoaRepositorio(_context)
                .Retornar(x => x.Email == email)
                .Select(s => (PessoaViewModel)s)
                .FirstOrDefault();
        }

        /// <summary>
        /// Obter Pessoa por ID
        /// </summary>
        /// <param name="id">Id da Pessoa</param>
        /// <returns>PessoaViewModel</returns>
        public PessoaViewModel ObterPorId(Guid id)
        {            
            return (PessoaViewModel)new PessoaRepositorio(_context).RetornarPorId(id);
        }

        /// <summary>
        /// Obter Pessoa por Nome
        /// </summary>
        /// <param name="nome">Nome da Pessoa</param>
        /// <returns>PessoaViewModel</returns>
        public PessoaViewModel ObterPorNome(string nome)
        {
            return new PessoaRepositorio(_context)
                .RetornarPorNome(nome)
                .Select(s => (PessoaViewModel)s)
                .FirstOrDefault();
        }

        /// <summary>
        /// Obter todas as Pessoas cadastradas
        /// </summary>
        /// <returns>Lista de PessoaViewModel</returns>
        public IList<PessoaViewModel> ObterTodos()
        {
            return new PessoaRepositorio(_context)
                .RetornarTodos()
                .ToList()
                .Select(s => (PessoaViewModel)s)
                .ToList();
        }

        /// <summary>
        /// Remover Pessoa
        /// </summary>
        /// <param name="viewModel">ViewModelPessoa</param>
        /// <returns>ViewModelPessoa recebida com erros de validação quando houver</returns>
        public PessoaViewModel Remover(PessoaViewModel viewModel)
        {
            PessoaRepositorio repositorio = new PessoaRepositorio(_context);
            var pessoa = repositorio.RetornarPorId(viewModel.PessoaId);
            ValidationResult validador = new PessoaValidacoes().ValidarExclusao(pessoa.PessoaId);

            if (validador.IsValid)
            {
                repositorio.Excluir(x => x.PessoaId == pessoa.PessoaId);
                repositorio.SalvarTodos();
            }
            else
            {
                viewModel.Validador = validador;
            }

            return viewModel;
        }

        #region Métodos Auxiliares

        private void AtualizarObjeto(Pessoa pessoa, PessoaViewModel pessoaViewModel)
        {
            pessoa.Nome = pessoaViewModel.Nome;
            pessoa.Email = pessoaViewModel.Email;
            pessoa.DataDeNascimento = pessoaViewModel.DataDeNascimento;
            pessoa.Celular = pessoaViewModel.Celular;
        }

        #endregion
    }
}
