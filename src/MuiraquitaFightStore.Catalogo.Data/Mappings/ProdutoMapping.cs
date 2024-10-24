using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MuiraquitaFightStore.Catalogo.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Cor)
                .IsRequired()
                .HasColumnType("varchar(12)");

            builder.OwnsOne(c => c.Tamanho, cm =>
            {
                cm.Property(c => c.TamanhoNumeracao)
                    .HasColumnName("TamanhoNumeracao")
                    .HasColumnType("Varchar(100)");

                cm.Property(c => c.TamanhoCamisa)
                    .HasColumnName("TamanhoCamisa")
                    .HasColumnType("Varchar(100)");

                cm.Property(c => c.TamanhoShort)
                    .HasColumnName("TamanhoShort")
                    .HasColumnType("Varchar(100)");

                cm.Property(c => c.Peso)
                    .HasColumnName("Peso")
                    .HasColumnType("Varchar(3)");
            });

            builder.ToTable("Produtos");
        }
    }
}


