using Dados.Configuracao;
using Dados.Repositorio.Base;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Repositorio.Repositorios
{
    public class PessoaRepositorio : RepositorioBase<Pessoa>
    {
        public PessoaRepositorio(DataContext contexto)
            : base(contexto)
        {

        }

        public IList<Pessoa> RetornarPorNome(string nome)
        {
            return Retornar(x => x.Nome == nome).ToList();
        }

        public Pessoa RetornarPorId(Guid id)
        {
            return Retornar(x => x.PessoaId == id).FirstOrDefault();
        }
    }
}
