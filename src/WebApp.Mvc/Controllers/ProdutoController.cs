﻿using Microsoft.AspNetCore.Mvc;
using MuiraquitaFightStore.Catalogo.Application.DTOs;
using MuiraquitaFightStore.Catalogo.Application.Service;
using MuiraquitaFightStore.WebApp.Mvc.Models.ViewModels;

namespace MuiraquitaFightStore.WebApp.Mvc.Controllers
{
    [Route("Produtos/")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;


        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        [Route("Listar-Produtos")]
        public async Task<IActionResult> Index()
        {
           return View(await _produtoAppService.ObterTodos());
        }


        [Route("Novo-Produto")]
        public async Task<IActionResult> AdicionarProduto()
        {
            return View(await PopularCategoriaMarca(new ProdutoViewModel()));
        }


        [Route("Novo-Produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarProduto(ProdutoViewModel produto)
        {

            if(!ModelState.IsValid) return View(await PopularCategoriaMarca(produto));

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(produto.ImagemUpload, imgPrefixo))
            {
                return View("Index");
            }
            produto.Imagem = imgPrefixo + produto.ImagemUpload.FileName;

            await _produtoAppService.AdicionarProduto(ConverterProdutoDto(produto));

            
            return RedirectToAction("Index");
        }


        //[HttpGet]
        //[Route("editar-produto")]
        //public async Task<IActionResult> AtualizarProduto(Guid id)
        //{
        //    return View(await PopularCategoriaMarca(await _produtoAppService.ObterProdutoPorId(id)));
        //}



        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoDto produtoViewModel)
        {
            var produto = await _produtoAppService.ObterProdutoPorId(id);
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            //ModelState.Remove("QuantidadeEstoque");
            //if (!ModelState.IsValid) return View(await PopularCategoriaMarca(produtoViewModel));

            await _produtoAppService.AtualizarProduto(produtoViewModel);

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            return View("Estoque", await _produtoAppService.ObterProdutoPorId(id));
        }


        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
        {
            if (quantidade > 0)
            {
                await _produtoAppService.ReporEstoque(id, quantidade);
            }
            else
            {
                await _produtoAppService.DebitarEstoque(id, quantidade);
            }

            return View("Index", await _produtoAppService.ObterTodos());
        }

        private async Task<ProdutoViewModel> PopularCategoriaMarca(ProdutoViewModel produtoView)
        {
            ProdutoDto produto = new ProdutoDto();
            produto.Categorias = await _produtoAppService.ObterCategorias();
            produto.Marcas = await _produtoAppService.ObterMarcas();
            produtoView.Categorias = produto.Categorias;
            produtoView.Marcas = produto.Marcas;
                        
            return produtoView;
        }
        
        private ProdutoDto ConverterProdutoDto(ProdutoViewModel produtoViewModel)
        {
            ProdutoDto Produto = new ProdutoDto
            {
                CategoriaId = produtoViewModel.CategoriaId,
                MarcaId = produtoViewModel.MarcaId,
                Nome = produtoViewModel.Nome,
                Descricao = produtoViewModel.Descricao,
                Ativo = produtoViewModel.Ativo,
                Valor = produtoViewModel.Valor,
                Imagem = produtoViewModel.Imagem,
                QuantidadeEstoque = produtoViewModel.QuantidadeEstoque,
                Cor = produtoViewModel.Cor,
                TamanhoNumeracao = produtoViewModel?.TamanhoNumeracao,
                TamanhoCamisa = produtoViewModel?.TamanhoCamisa,
                TamanhoShort = produtoViewModel?.TamanhoShort, 
                Peso = produtoViewModel?.Peso
                                
            };

            return Produto;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);
            
            if (System.IO.File.Exists(filePath)) return false; // adicionar uma mensagem de erro. um customresponse

            using (var steam = new FileStream(filePath, FileMode.Create))
            {
                await arquivo.CopyToAsync(steam);
            }

            return true;

            
        }

        private async Task<IEnumerable<ProdutoViewModel>> ObterListaProduto()
        {
            var produtos = await _produtoAppService.ObterTodos();

            return produtos.Select(o => new ProdutoViewModel
            {
                Id = o.Id,
                no
            }

            // Terminar a conversão da lista para produtoviewmodel

            return comp
        }
    }
}