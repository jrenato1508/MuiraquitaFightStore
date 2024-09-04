using Microsoft.EntityFrameworkCore;
using MuiraquitaFightStore.Catalogo.Domain.Entitys;
using MuiraquitaFightStore.Catalogo.Domain.Interfaces;
using MuiraquitaFightStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Data.Repository
{
    public class ProdutoRepositoy : IProdutoRepository
    {
        private readonly CatalogoContext _catalogoContext;

        public ProdutoRepositoy(CatalogoContext catalago)
        {
           _catalogoContext = catalago;
        }

        public IUnitOfWork UnitOfWork => _catalogoContext;


        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _catalogoContext.Produtos.AsNoTracking()   
                                                  .ToListAsync();
        }


        public async Task<Produto> ObterProdutoPorId(Guid id)
        {
            return await _catalogoContext.Produtos.FindAsync(id);
        }


        public async Task<IEnumerable<Produto>> ObterPorCategoria(int codigo)
        {
            return await _catalogoContext.Produtos.AsNoTracking()
                                                  .Include(p => p.Categoria)
                                                  .Where(c => c.Categoria.Codigo == codigo).ToListAsync();
        }


        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _catalogoContext.Categorias.AsNoTracking()
                                                    .ToListAsync();     
        }
                
                
        public void Adicionar(Produto produto)
        {
             _catalogoContext.Add(produto);
        }


        public void Adicionar(Categoria categoria)
        {
            _catalogoContext.Add(categoria);
        }


        public void Atualizar(Produto produto)
        {
            _catalogoContext.Update(produto);
        }


        public void Atualizar(Categoria categoria)
        {
            _catalogoContext.Update(categoria);
        }


        public void Dispose()
        {
            _catalogoContext?.Dispose();
        }
    }
}
