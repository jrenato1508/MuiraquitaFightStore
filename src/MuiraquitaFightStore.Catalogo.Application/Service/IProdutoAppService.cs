using MuiraquitaFightStore.Catalogo.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Application.Service
{
    public interface IProdutoAppService : IDisposable
    {
        Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo);

        Task<ProdutoDto> ObterProdutoPorId(Guid id);

        Task<IEnumerable<ProdutoDto>> ObterTodos();

        Task<IEnumerable<CategoriaDto>> ObterCategorias();

        Task<IEnumerable<MarcaDto>> ObterMarcas();

        Task AdicionarProduto(ProdutoDto produtoViewModel);

        Task AtualizarProduto(ProdutoDto produtoViewModel);

        Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade);

        Task<ProdutoDto> ReporEstoque(Guid id, int quantidade);

    }
}
