using Negocios;
using Negocios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class PessoaController : Controller
    {
        [HttpGet]
        public ActionResult Listar()
        {
            return View(new PessoaAppService().ObterTodos());
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(PessoaViewModel viewModelRecebida)
        {
            if (!ModelState.IsValid)
                return View();

            new PessoaAppService().Adicionar(viewModelRecebida);

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
            return View(new PessoaAppService().ObterPorId(id));
        }

        [HttpPost]
        public ActionResult Editar(PessoaViewModel viewModelRecebida)
        {
            if (!ModelState.IsValid)
                return View();

            new PessoaAppService().Atualizar(viewModelRecebida);

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
            return View(new PessoaAppService().ObterPorId(id));
        }

        [HttpPost]
        public ActionResult Excluir(PessoaViewModel viewModelRecebida)
        {
            new PessoaAppService().Remover(viewModelRecebida);

            if (!viewModelRecebida.Validador.IsValid)
            {
                ViewBag.ErrosValidacao = viewModelRecebida.Validador.Errors;
                return View(viewModelRecebida);
            }

            return RedirectToAction("Listar");
        }
    }
}