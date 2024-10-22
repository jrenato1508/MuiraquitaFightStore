using AutoMapper;
using MuiraquitaFightStore.Catalogo.Application.DTOs;
using MuiraquitaFightStore.Catalogo.Domain.Entitys;
using MuiraquitaFightStore.Catalogo.Domain.Interfaces;
using MuiraquitaFightStore.Core.DomainObject.AssertionConcem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Application.Service
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMapper _mapper;

        public async Task<IEnumerable<ProdutoDto>> ObterTodos()
        {
           return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterTodos());
        }

        public async Task<IEnumerable<CategoriaDto>> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaDto>>(await _produtoRepository.ObterCategorias());
        }

        public async Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo)
        {
            return _mapper.Map<IEnumerable<ProdutoDto>>( await _produtoRepository.ObterPorCategoria(codigo));
        }

        public async Task<ProdutoDto> ObterProdutoPorId(Guid id)
        {
            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterProdutoPorId(id));
        }

        public Task Adicionar(ProdutoDto produtoViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task AtualizarProduto(ProdutoDto produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);
            _produtoRepository.AdicionarProduto(produto);
            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterProdutoPorId(id));
        }

        public async Task<ProdutoDto> ReporEstoque(Guid id, int quantidade)
        {
            if(!_estoqueService.ReporEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao repor estoque");
            }

            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterProdutoPorId(id));
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
            _estoqueService?.Dispose();
        }
    }
}
