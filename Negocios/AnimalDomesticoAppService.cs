using Dados.Configuracao;
using Dados.Repositorio.Repositorios;
using Dominio;
using FluentValidation.Results;
using Negocios.Interfaces;
using Negocios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validacoes;

namespace Negocios
{
    public class AnimalDomesticoAppService : IAnimalDomesticoAppService
    {
        private DataContext _context;

        public AnimalDomesticoAppService()
        {
            _context = new DataContext();
        }

        /// <summary>
        /// Adicionar Animal Doméstico
        /// </summary>
        /// <param name="animalDomesticoViewModel">ViewModel</param>
        /// <returns>ViewModel recebida com erros de validação quando necessário</returns>
        public AnimalDomesticoViewModel Adicionar(AnimalDomesticoViewModel animalDomesticoViewModel)
        {
            AnimalDomestico animalDomestico = new AnimalDomestico();
            AtualizarObjeto(animalDomestico, animalDomesticoViewModel);

            ValidationResult resultadoValidacoes = new AnimalDomesticoValidacoes().Validate(animalDomestico);

            if (resultadoValidacoes.IsValid)
            {
                AnimalDomesticoRepositorio repositorio = new AnimalDomesticoRepositorio(_context);
                animalDomestico.AnimalDomesticoId = Guid.NewGuid();
                repositorio.Incluir(animalDomestico);
                repositorio.SalvarTodos();
            }
            else
            {
                animalDomesticoViewModel.Validador = resultadoValidacoes;
            }

            return animalDomesticoViewModel;
        }

        /// <summary>
        /// Atualizar Animal Doméstico
        /// </summary>
        /// <param name="animalDomesticoViewModel">ViewModel</param>
        /// <returns>ViewModel recebida com erros de validação quando necessário</returns>
        public AnimalDomesticoViewModel Atualizar(AnimalDomesticoViewModel animalDomesticoViewModel)
        {
            AnimalDomesticoRepositorio repositorio = new AnimalDomesticoRepositorio(_context);
            AnimalDomestico animalDomestico = repositorio.Retornar(x => x.AnimalDomesticoId == animalDomesticoViewModel.AnimalDomesticoId).FirstOrDefault();

            AtualizarObjeto(animalDomestico, animalDomesticoViewModel);

            ValidationResult resultadoValidacoes = new AnimalDomesticoValidacoes().Validate(animalDomestico);

            if (resultadoValidacoes.IsValid)
            {
                repositorio.Atualizar(animalDomestico);
                repositorio.SalvarTodos();
            }
            else
            {
                animalDomesticoViewModel.Validador = resultadoValidacoes;
            }

            return animalDomesticoViewModel;
        }

        /// <summary>
        /// Obter animal doméstico por ID
        /// </summary>
        /// <param name="id">Id do Animal</param>
        /// <returns>Objeto AnimalDomesticoViewModel solicitado</returns>
        public AnimalDomesticoViewModel ObterPorId(Guid id)
        {
            return (AnimalDomesticoViewModel)new AnimalDomesticoRepositorio(_context).Retornar(x => x.AnimalDomesticoId == id).FirstOrDefault();
        }

        /// <summary>
        /// Remover animal doméstico
        /// </summary>
        /// <param name="id">Id do Animal</param>
        public void Remover(Guid id)
        {
            AnimalDomesticoRepositorio repositorio = new AnimalDomesticoRepositorio(_context);
            repositorio.Excluir(x => x.AnimalDomesticoId == id);
            repositorio.SalvarTodos();
        }

        /// <summary>
        /// Obter todos os animais cadastrados
        /// </summary>
        /// <returns>Lista de AnimalViewModel</returns>
        public IList<AnimalDomesticoViewModel> ObterTodos()
        {
            return new AnimalDomesticoRepositorio(_context)
                .RetornarTodosComPessoaCarregado()
                .ToList()
                .Select(s => (AnimalDomesticoViewModel)s)
                .ToList();
        }

        /// <summary>
        /// Obter animal pelo nome
        /// </summary>
        /// <param name="nome">Nome do animal</param>
        /// <returns>Primeiro AnimalViewModel que corresponde a pesquisa</returns>
        public AnimalDomesticoViewModel ObterPorNome(string nome)
        {
            return new AnimalDomesticoRepositorio(_context)
                .Retornar(x => x.Nome.Contains(nome))
                .Select(s => (AnimalDomesticoViewModel)s)
                .FirstOrDefault();
        }

        #region Métodos auxiliares

        private void AtualizarObjeto(AnimalDomestico animalDom, AnimalDomesticoViewModel animalDomViewModel)
        {
            animalDom.Nome = animalDomViewModel.Nome;
            animalDom.PessoaId = animalDomViewModel.PessoaId;
            animalDom.TipoAnimal = animalDomViewModel.TipoAnimal;
            animalDom.AnimalCastrado = animalDomViewModel.AnimalCastrado;
        }

        #endregion
    }
}
