using Negocios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Interfaces
{
    interface IAnimalDomesticoAppService
    {
        AnimalDomesticoViewModel Adicionar(AnimalDomesticoViewModel animalDomesticoViewModel);
        AnimalDomesticoViewModel Atualizar(AnimalDomesticoViewModel animalDomesticoViewModel);
        AnimalDomesticoViewModel ObterPorId(Guid id);
        AnimalDomesticoViewModel ObterPorNome(string nome);
        void Remover(Guid id);
    }
}
