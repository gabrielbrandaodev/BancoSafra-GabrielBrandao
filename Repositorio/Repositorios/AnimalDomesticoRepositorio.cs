using Dados.Configuracao;
using Dominio;
using Repositorio.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class AnimalDomesticoRepositorio : RepositorioBase<AnimalDomestico>
    {
        public AnimalDomesticoRepositorio(Contexto contexto)
            :base(contexto)
        {
            public IList<AnimalDomestico> RetornarTodosComPessoaCarregado()
            {
                return RetornarTodos()
                    .Include(x => x.Pessoa)
                    .ToList();
            }
        }
    }
}
