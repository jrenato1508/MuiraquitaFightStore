using MuiraquitaFightStore.Catalogo.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {

        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);
            if(produto == null) return false;
            if(!produto.PossuiEstoque(quantidade)) return false;
            produto.DebitarEstoque(quantidade);

            if(produto.QuantidadeEstoque < 10)
            {
                //
            }

            _produtoRepository.AtualizarProduto(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }
                

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);
            if(produto ==null) return false;
            produto.ReporEstoque(quantidade);
            _produtoRepository.AtualizarProduto(produto);
            return await _produtoRepository.UnitOfWork.Commit();

        }


        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
