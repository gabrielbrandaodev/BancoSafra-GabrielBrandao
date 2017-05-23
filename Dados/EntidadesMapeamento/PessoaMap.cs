using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.EntidadesMapeamento
{
    public class PessoaMap : EntityTypeConfiguration<Pessoa>
    {
        public PessoaMap()
        {
            ToTable("PESSOA");

            HasKey(x => x.PessoaId);

            Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            Property(x => x.Email)
                .IsRequired();

            Property(x => x.DataDeNascimento)
                .IsRequired();

            Property(x => x.Celular)
                .IsRequired();
        }
    }
}
