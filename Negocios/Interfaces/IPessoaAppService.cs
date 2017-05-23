using Negocios.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Interfaces
{
    interface IPessoaAppService
    {
        PessoaViewModel Adicionar(PessoaViewModel pessoaViewModel);
        PessoaViewModel Atualizar(PessoaViewModel pessoaViewModel);
        PessoaViewModel ObterPorId(Guid id);
        PessoaViewModel ObterPorNome(string nome);
        PessoaViewModel ObterPorEmail(string email);        
        PessoaViewModel Remover(PessoaViewModel id);
    }
}
