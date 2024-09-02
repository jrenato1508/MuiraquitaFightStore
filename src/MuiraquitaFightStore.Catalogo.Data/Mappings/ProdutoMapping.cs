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

            builder.OwnsOne(c => c.Tamanho, cm =>
            {
                cm.Property(c => c.TamanhoNumeracao)
                    .HasColumnName("Tamanho")
                    .HasColumnType("Varchar(100)");

                cm.Property(c => c.TamanhoCamisaEVagui)
                    .HasColumnName("Tamanho")
                    .HasColumnType("Varchar(100)");

                cm.Property(c => c.TamanhoCalcaShort)
                    .HasColumnName("TamanhoCalca")
                    .HasColumnType("Varchar(100)");
            });
        }
    }
}


/*
   Terminar de configurar o mapeamento como banco de dados... Temos que pensar que a class produto vai representar todos os possiveis produtos a venda
   logo nomes como TamanhoNumeraçao, TamanhoCalca e vagui não devem ser utilizados, visto que o kimono não será o único produto vendido na loga, logo
   temos que separar e adicionar nomes mais genericos como Tamanho, Tamanho Camisa, Tamanho Calaça ou bermuda... vamos pensar mais sobre isso antes
   de implementar.
 */
