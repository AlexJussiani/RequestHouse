using RH.Core.Data;
using RH.Produtos.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Produtos.API.Data.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos();
        Task<IEnumerable<Produto>> ObterProdutosEntrada();
        Task<IEnumerable<Produto>> ObterProdutosSaida();
        void Adicionar(Produto produto);
        Task<Produto> ObterPorName(string name); 
        Task<Produto> ObterPorId(Guid id);
        void Atualizar(Produto produto);
        void Remover(Produto produto);
    }
}
