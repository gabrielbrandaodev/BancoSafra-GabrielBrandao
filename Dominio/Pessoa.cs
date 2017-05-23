using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pessoa
    {
        public Pessoa()
        {
            AnimaisDomesticos = new List<AnimalDomestico>();
        }

        public Guid PessoaId { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public IList<AnimalDomestico> AnimaisDomesticos { get; set; }
    }
}
