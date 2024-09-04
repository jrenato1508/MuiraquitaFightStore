using MuiraquitaFightStore.Catalogo.Domain.Entitys;
using MuiraquitaFightStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterProdutoPorId(Guid id);
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
    }
}
