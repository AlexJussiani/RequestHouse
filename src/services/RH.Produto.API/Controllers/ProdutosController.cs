using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.Core.Controllers;
using RH.Produtos.API.Application.DTO;
using RH.Produtos.API.Data.Repository;
using RH.Produtos.API.Services;
using System;
using System.Threading.Tasks;

namespace RH.Produtos.API.Controllers
{
   // [Authorize]
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _repository;
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoRepository repository, IProdutoService produtoService)
        {
            _repository = repository;
            _produtoService = produtoService;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _repository.ObterTodos());
        }

        [HttpGet("api/produto-obterPorId")]
        public async Task<IActionResult> ObterPedidoPorId(Guid produtoId)
        {
            return CustomResponse(await _repository.ObterPorId(produtoId));
        }

        [HttpPost()]
        public async Task<ActionResult> Registrar(ProdutoDTO produto)
        {
            return CustomResponse(await _produtoService.Adicionar(produto));
        }

        [HttpPut()]
        public async Task<ActionResult> Atualizar(Guid idProduto, ProdutoDTO produto)
        {
            return CustomResponse(await _produtoService.Atualizar(idProduto, produto));
        }

        [HttpDelete()]
        public async Task<ActionResult> Remover(Guid idProduto)
        {
            return CustomResponse(await _produtoService.Remover(idProduto));
        }
    }
}
