using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Base
{
    interface IRepositorioBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> RetornarTodos();
        IQueryable<TEntity> Retornar(Func<TEntity, bool> predicate);
        TEntity Retornar(params object[] key);
        void Atualizar(TEntity obj);
        void SalvarTodos();
        void Incluir(TEntity obj);
        void Excluir(Func<TEntity, bool> predicate);
    }
}
