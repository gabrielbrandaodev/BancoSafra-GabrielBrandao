using Dados.Configuracao;
using Dados.Repositorio.Base;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Repositorio.Repositorios
{
    public class AnimalDomesticoRepositorio : RepositorioBase<AnimalDomestico>
    {
        public AnimalDomesticoRepositorio(DataContext contexto)
            : base(contexto)
        {

        }

        public IList<AnimalDomestico> RetornarTodosComPessoaCarregado()
        {
            return RetornarTodos()
                .ToList();
        }
    }

}
