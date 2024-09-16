using MediatR;
using MuiraquitaFightStore.Catalogo.Domain.Events;
using MuiraquitaFightStore.Catalogo.Domain.Interfaces;
using MuiraquitaFightStore.Core.Communication.Mediator;
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
        private readonly IMediatorHandler _mediator;

        public EstoqueService(IProdutoRepository produtoRepository,
                              IMediatorHandler mediator)
        {
            _produtoRepository = produtoRepository;
            _mediator = mediator;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProdutoPorId(produtoId);
            if(produto == null) return false;
            if(!produto.PossuiEstoque(quantidade)) return false;
            produto.DebitarEstoque(quantidade);

            if(produto.QuantidadeEstoque < 10)
            {
               await _mediator.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produtoId, produto.QuantidadeEstoque));
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
