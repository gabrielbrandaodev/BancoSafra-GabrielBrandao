using Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.EntidadesMapeamento
{
    public class AnimalDomesticoMap : EntityTypeConfiguration<AnimalDomestico>
    {
        public AnimalDomesticoMap()
        {
            ToTable("ANIMALDOMESTICO");

            HasKey(x => x.AnimalDomesticoId);

            Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            Property(x => x.AnimalCastrado);

            HasRequired(a => a.Pessoa)
              .WithMany(a => a.AnimaisDomesticos)
              .HasForeignKey(x => x.PessoaId)
              .WillCascadeOnDelete(false);
        }
    }
}
