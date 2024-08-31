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
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            #region Mapeamento
            /*
               Uma Categoria(1) possui (:) Muitos(HasMany) produtos(N) e um Produto(1) Possui(:) apenas uma(WithOne) categoria(1), então a relação será
               de 1 : N => Categorias : Produtos. e de 1 : 1 => Produtos : Categorias
               Para essa relação funcionar, precisamos adicionar na entiade Categoria(Catalago.Domain) um IEnumerable de Produtos se não o EntityFrameWorck reclama na hora de 
               fazer esse mapeamento.

                ex: public IEnumerable<Produto> Produtos { get; set; } - Na class Categoria.cs
             */
            #endregion
            // 1 : N => Categorias : Produtos
            builder.HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);

            builder.ToTable("Categorias");
        }
    }
}
