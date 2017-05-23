using Dados.EntidadesMapeamento;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Configuracao
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("StringDeConexao")
        {
            Configuration.LazyLoadingEnabled = false; 
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<AnimalDomestico> AnimaisDomesticos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PessoaMap());
            modelBuilder.Configurations.Add(new AnimalDomesticoMap());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
               .Where(p => p.Name == p.ReflectedType.Name + "Id")
               .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                        .Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>()
                  .Configure(p => p.HasMaxLength(150));
        }
    }
}
