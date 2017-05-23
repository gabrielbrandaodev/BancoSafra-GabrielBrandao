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
    public class PessoaRepositorio : RepositorioBase<Pessoa>
    {
        public PessoaRepositorio(Contexto contexto)
            :base(contexto)
        {

        }

        public IList<Pessoa> RetornarPorNome(string nome)
        {
            return Retornar(x => x.Nome == nome).ToList();
        }

        public Pessoa RetornarPorId(int id)
        {
            return Retornar(x => x.PessoaId == id).FirstOrDefault();
        }
    }
}
