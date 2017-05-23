using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.Enumeradores;

namespace Dominio
{
    public class AnimalDomestico
    {
        public Guid AnimalDomesticoId { get; set; }
        public string Nome { get; set; }
        public bool AnimalCastrado { get; set; }
        public EnumTipoAnimal TipoAnimal { get; set; }
        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }
    }
}
