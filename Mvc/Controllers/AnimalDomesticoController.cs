using Negocios;
using Negocios.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilitarios.Enumeradores;
using Utilitarios.MetodosUtilitarios;

namespace Mvc.Controllers
{
    public class AnimalDomesticoController : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            return View(new AnimalDomesticoAppService().ObterTodos());
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            CarregarDadosParaView(Guid.Empty);
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(AnimalDomesticoViewModel viewModelRecebida)
        {
            if (!ModelState.IsValid)
            {
                CarregarDadosParaView(viewModelRecebida.PessoaId);
                return View();
            }                

            new AnimalDomesticoAppService().Adicionar(viewModelRecebida);

            if (!viewModelRecebida.Validador.IsValid)
            {
                ViewBag.ErrosValidacao = viewModelRecebida.Validador.Errors;
                return View(viewModelRecebida);
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public ActionResult Editar(Guid id)
        {
            AnimalDomesticoViewModel animalDomestico = new AnimalDomesticoAppService().ObterPorId(id);
            CarregarDadosParaView(animalDomestico.PessoaId);
            return View(animalDomestico);
        }

        [HttpPost]
        public ActionResult Editar(AnimalDomesticoViewModel viewModelRecebida)
        {
            if (!ModelState.IsValid)
            {
                CarregarDadosParaView(viewModelRecebida.PessoaId);
                return View();
            }

            new AnimalDomesticoAppService().Atualizar(viewModelRecebida);

            if (!viewModelRecebida.Validador.IsValid)
            {
                ViewBag.ErrosValidacao = viewModelRecebida.Validador.Errors;
                return View(viewModelRecebida);
            }

            return RedirectToAction("Listar");
        }

        [HttpGet]
        public ActionResult Excluir(Guid id)
        {
            return View(new AnimalDomesticoAppService().ObterPorId(id));
        }

        [HttpPost]
        public ActionResult Excluir(AnimalDomesticoViewModel viewModelRecebida)
        {
            new AnimalDomesticoAppService().Remover(viewModelRecebida.PessoaId);
            return RedirectToAction("Listar");
        }

        #region Métodos auxiliares

        private IEnumerable RetornarTiposDeAnimais()
        {
            var enumTiposDeAnimais = from EnumTipoAnimal e in Enum.GetValues(typeof(EnumTipoAnimal))
                                     select new
                                     {
                                         ID = (int)e,
                                         Name = e.ObterDescricao()
                                     };

            return enumTiposDeAnimais;
        }

        private void CarregarDadosParaView(Guid pessoaSelecionadaId)
        {
            ViewBag.TiposDeAnimais = new SelectList(RetornarTiposDeAnimais(), "ID", "Name");
            ViewBag.Pessoas = new SelectList(new PessoaAppService().ObterTodos(), "PessoaId", "Nome", pessoaSelecionadaId);
        }


        #endregion
    }
}