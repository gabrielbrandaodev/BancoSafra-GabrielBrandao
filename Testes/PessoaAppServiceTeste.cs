using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negocios;
using Negocios.ViewModels;
using Dados.Configuracao;
using Utilitarios.Enumeradores;

namespace Testes
{
    [TestClass]
    public class PessoaAppServiceTeste
    {
        [ClassInitialize]
        public static void InicializarTestes(TestContext testContext)
        {
            InicializadorContexto inicializador = new InicializadorContexto(true);
            inicializador.DropAndCreateSchema(new DataContext());
        }

        [TestMethod]
        public void Deve_Incluir_Pessoa_Nao_Cadastrada()
        {
            LimparBancoDeDados();

            // PREPARAÇÃO
            PessoaAppService appService = new PessoaAppService();
            PessoaViewModel pessoa = new PessoaViewModel();
            pessoa.Nome = "Gabriel Brandão";
            pessoa.Email = "gacandidob@gmail.com";
            pessoa.Celular = "11952942458";
            pessoa.DataDeNascimento = new DateTime(1992, 07, 03);

            // AÇÃO
            appService.Adicionar(pessoa);

            var retorno = appService.ObterTodos();

            // ASSERTS
            Assert.IsNotNull(retorno);
            Assert.IsTrue(retorno.Count > 0);
            Assert.AreEqual(retorno[0].Nome, pessoa.Nome);
            Assert.AreEqual(retorno[0].Email, pessoa.Email);
            Assert.AreEqual(retorno[0].Celular, pessoa.Celular);
            Assert.AreEqual(retorno[0].DataDeNascimento, pessoa.DataDeNascimento);
        }

        [TestMethod]
        public void Nao_Deve_Incluir_Pessoa_Com_Problemas_Na_Validacao_Campos_Obrigatorios()
        {
            // PREPARAÇÃO
            LimparBancoDeDados();

            PessoaAppService appService = new PessoaAppService();
            PessoaViewModel pessoa = new PessoaViewModel();
            pessoa.Nome = string.Empty;
            pessoa.Email = string.Empty;
            pessoa.Celular = "11952942458";
            pessoa.DataDeNascimento = new DateTime(1992, 07, 03);

            // AÇÃO
            appService.Adicionar(pessoa);

            var retorno = appService.ObterTodos();

            // ASSERTS
            Assert.IsTrue(retorno.Count == 0);
        }

        [TestMethod]
        public void Deve_Atualizar_Pessoa_Cadastrada()
        {
            // PREPARAÇÃO
            LimparBancoDeDados();

            PessoaAppService appService = new PessoaAppService();
            PessoaViewModel pessoa = new PessoaViewModel();
            pessoa.Nome = "Gabriel Brandão";
            pessoa.Email = "gacandidob@gmail.com";
            pessoa.Celular = "11952942458";
            pessoa.DataDeNascimento = new DateTime(1992, 07, 03);

            appService.Adicionar(pessoa);

            // AÇÃO
            PessoaViewModel pessoaAtualizar = appService.ObterPorEmail("gacandidob@gmail.com");
            pessoaAtualizar.Nome = "Atualizando Nome";
            appService.Atualizar(pessoaAtualizar);

            var retorno = appService.ObterTodos();

            // ASSERTS
            Assert.IsNotNull(retorno);
            Assert.IsTrue(retorno.Count > 0);
            Assert.AreEqual(retorno[0].Nome, pessoaAtualizar.Nome);
        }

        [TestMethod]
        public void Deve_Remover_Pessoa_Sem_Animal_Vinculado()
        {
            // PREPARAÇÃO
            LimparBancoDeDados();

            PessoaAppService appService = new PessoaAppService();
            PessoaViewModel pessoa = new PessoaViewModel();
            pessoa.Nome = "Gabriel Brandão";
            pessoa.Email = "gacandidob@gmail.com";
            pessoa.Celular = "11952942458";
            pessoa.DataDeNascimento = new DateTime(1992, 07, 03);

            appService.Adicionar(pessoa);

            // AÇÃO
            appService.Remover(appService.ObterPorEmail("gacandidob@gmail.com"));

            var retorno = appService.ObterTodos();

            // ASSERTS
            Assert.IsTrue(retorno.Count == 0);
        }

        [TestMethod]
        public void Nao_Deve_Remover_Pessoa_Sem_Animal_Vinculado()
        {
            // PREPARAÇÃO
            LimparBancoDeDados();

            PessoaAppService appService = new PessoaAppService();
            PessoaViewModel pessoa = new PessoaViewModel();
            pessoa.Nome = "Gabriel Brandão";
            pessoa.Email = "gacandidob@gmail.com";
            pessoa.Celular = "11952942458";
            pessoa.DataDeNascimento = new DateTime(1992, 07, 03);

            appService.Adicionar(pessoa);

            AnimalDomesticoViewModel animal = new AnimalDomesticoViewModel();
            animal.Nome = "Bob";
            animal.Pessoa = appService.ObterPorEmail(pessoa.Email);
            animal.PessoaId = animal.Pessoa.PessoaId;
            animal.TipoAnimal = EnumTipoAnimal.Cachorro;

            AnimalDomesticoAppService appServiceAnimal = new AnimalDomesticoAppService();
            appServiceAnimal.Adicionar(animal);

            // AÇÃO
            appService.Remover(animal.Pessoa);

            var retorno = appService.ObterTodos();

            // ASSERTS
            Assert.IsTrue(retorno.Count > 0);
        }

        #region Métodos auxiliares

        public void LimparBancoDeDados()
        {
            PessoaAppService appService = new PessoaAppService();
            foreach (var pessoa in appService.ObterTodos())
                appService.Remover(pessoa);
        }

        #endregion
    }
}
