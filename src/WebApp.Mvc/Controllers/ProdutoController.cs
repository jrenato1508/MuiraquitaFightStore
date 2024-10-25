using Microsoft.AspNetCore.Mvc;
using MuiraquitaFightStore.Catalogo.Application.DTOs;
using MuiraquitaFightStore.Catalogo.Application.Service;

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
            return View(await PopularCategoriaMarca(new ProdutoDto()));
        }


        [Route("Novo-Produto")]
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(ProdutoDto produto)
        {
            if(!ModelState.IsValid) return View(await PopularCategoriaMarca(produto));
            
            //var imgPrefixo = Guid.NewGuid() + "_";
            //if (!UploadArquivo(produto.ImagemUpload, imgPrefixo))
            //{
            //    return View("Index");
            //}
            //produto.Imagem = imgPrefixo + produto.ImagemUpload.ToString();

            await _produtoAppService.AdicionarProduto(produto);

            
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            return View(await PopularCategoriaMarca(await _produtoAppService.ObterProdutoPorId(id)));
        }



        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoDto produtoViewModel)
        {
            var produto = await _produtoAppService.ObterProdutoPorId(id);
            produtoViewModel.QuantidadeEstoque = produto.QuantidadeEstoque;

            ModelState.Remove("QuantidadeEstoque");
            if (!ModelState.IsValid) return View(await PopularCategoriaMarca(produtoViewModel));

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

        private async Task<ProdutoDto> PopularCategoriaMarca(ProdutoDto produto)
        {
            produto.Categorias = await _produtoAppService.ObterCategorias();
            produto.Marcas = await _produtoAppService.ObterMarcas();

            return produto;
        }


        //private bool UploadArquivo(string arquivo, string imgPrefixo)
        //{
        //    if(string.IsNullOrEmpty(arquivo)) return false;

        //    var imageDataByteArray = Convert.FromBase64String(arquivo);
        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo);

        //    if(System.IO.File.Exists(filePath)) return false;

        //    System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        //    return true;
        //}
    }
}
