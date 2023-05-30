using FluentValidation.Results;
using RH.Produtos.API.Application.DTO;
using RH.Produtos.API.Models;
using System;
using System.Threading.Tasks;

namespace RH.Produtos.API.Services
{
    public interface IProdutoService
    {
        Task<ValidationResult> Adicionar(ProdutoDTO produto);
        Task<ValidationResult> Atualizar(Guid idProduto, ProdutoDTO produto);
        Task<ValidationResult> Remover(Guid idProduto);
    }
}
