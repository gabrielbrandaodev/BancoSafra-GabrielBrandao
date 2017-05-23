using Dados.Configuracao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Repositorio.Base
{
    public class RepositorioBase<TEntity> : IDisposable,
        IRepositorioBase<TEntity> where TEntity : class
    {
        protected DataContext ctx;
        protected DbSet<TEntity> DbSet;

        public RepositorioBase(DataContext context)
        {
            ctx = context;
            DbSet = ctx.Set<TEntity>();
        }

        public void Atualizar(TEntity obj)
        {
            ctx.Entry(obj).State = EntityState.Modified;
        }

        public void Excluir(Func<TEntity, bool> predicate)
        {
            ctx.Set<TEntity>()
                .Where(predicate).ToList()
                .ForEach(del => ctx.Set<TEntity>().Remove(del));
        }

        public void Incluir(TEntity obj)
        {
            ctx.Set<TEntity>().Add(obj);
        }

        public IQueryable<TEntity> Retornar(Func<TEntity, bool> predicate)
        {
            return RetornarTodos().Where(predicate).AsQueryable();
        }

        public TEntity Retornar(params object[] key)
        {
            return ctx.Set<TEntity>().Find(key);
        }

        public IQueryable<TEntity> RetornarTodos()
        {
            return ctx.Set<TEntity>();
        }

        public void SalvarTodos()
        {
            ctx.SaveChanges();
        }

        public void Dispose()
        {
            ctx.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
