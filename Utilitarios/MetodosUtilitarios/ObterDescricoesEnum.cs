using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios.Enumeradores;

namespace Utilitarios.MetodosUtilitarios
{
    public static class ObterDescricoesEnum
    {
        public static string ObterDescricao(this EnumTipoAnimal tipoAnimal)
        {
            switch (tipoAnimal)
            {
                case EnumTipoAnimal.NaoDefinido:
                    return "Não definido";
                case EnumTipoAnimal.Cachorro:
                    return "Cachorro";
                case EnumTipoAnimal.Gato:
                    return "Gato";
                case EnumTipoAnimal.Papagaio:
                    return "Papagaio";
                default:
                    return string.Empty;
            }
        }
    }
}
