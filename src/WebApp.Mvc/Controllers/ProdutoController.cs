using Microsoft.AspNetCore.Mvc;
using MuiraquitaFightStore.Catalogo.Application.DTOs;
using MuiraquitaFightStore.Catalogo.Application.Service;
using MuiraquitaFightStore.Catalogo.Domain.Entitys;
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
           return View(await ObterListaProduto());
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

            await _produtoAppService.AdicionarProduto(ConverterParaProdutoDto(produto));

            
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            var produto = ConverterParaProdutoViewModel(await _produtoAppService.ObterProdutoPorId(id));
            return View(await PopularCategoriaMarca(produto));
        }

        // Testar a action de atualizar produto

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoViewModel produtoViewModel)
        {
            var produto = await _produtoAppService.ObterProdutoPorId(id);
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) return View(await PopularCategoriaMarca(produtoViewModel));

            await _produtoAppService.AtualizarProduto(ConverterParaProdutoDto(produtoViewModel));

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
        
        private ProdutoDto ConverterParaProdutoDto(ProdutoViewModel produtoViewModel)
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


        private ProdutoViewModel ConverterParaProdutoViewModel(ProdutoDto produtoDto)
        {
            ProdutoViewModel Produto = new ProdutoViewModel 
            {
                CategoriaId = produtoDto.CategoriaId,
                MarcaId = produtoDto.MarcaId,
                Nome = produtoDto.Nome,
                Descricao = produtoDto.Descricao,
                Ativo = produtoDto.Ativo,
                Valor = produtoDto.Valor,
                Imagem = produtoDto.Imagem,
                QuantidadeEstoque = produtoDto.QuantidadeEstoque,
                Cor = produtoDto.Cor,
                TamanhoNumeracao = produtoDto?.TamanhoNumeracao,
                TamanhoCamisa = produtoDto?.TamanhoCamisa,
                TamanhoShort = produtoDto?.TamanhoShort,
                Peso = produtoDto?.Peso

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
                CategoriaId = o.CategoriaId,
                MarcaId = o.MarcaId,
                Nome = o.Nome,
                Descricao = o.Descricao,
                Ativo = o.Ativo,
                Valor = o.Valor,
                DataCadastro = o.DataCadastro,
                Imagem = o.Imagem,
                QuantidadeEstoque = o.QuantidadeEstoque,
                Cor = o.Cor,
                TamanhoNumeracao = o.TamanhoNumeracao,
                TamanhoCamisa = o.TamanhoCamisa,
                TamanhoShort = o.TamanhoShort,
                Peso = o.Peso
            });

            // Terminar a conversão da lista para produtoviewmodel

            
        }
    }
}
