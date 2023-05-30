using AutoMapper;
using FluentValidation.Results;
using RH.Core.Messages;
using RH.Produtos.API.Application.DTO;
using RH.Produtos.API.Data.Repository;
using RH.Produtos.API.Models;
using System;
using System.Threading.Tasks;

namespace RH.Produtos.API.Services
{
    public class ProdutoService : CommandHandler, IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ValidationResult> Adicionar(ProdutoDTO produtoDTO)
        {
            _repository.Adicionar(_mapper.Map<Produto>(produtoDTO));

            return await PersistirDados(_repository.UnitOfWork);
        }

        public async Task<ValidationResult> Atualizar(Guid idProduto, ProdutoDTO produtoDTO)
        {
            var produto = await _repository.ObterPorId(idProduto);

            if(produto == null)
            {
                AdicionarErro("Produto não localizado");
                return ValidationResult;
            }

            produto = AlterarProduto(produto, produtoDTO);

            _repository.Atualizar(produto);
            return await PersistirDados(_repository.UnitOfWork);
        }

        public async Task<ValidationResult> Remover(Guid idProduto)
        {
            var produto = await _repository.ObterPorId(idProduto);

            if (produto == null)
                return ValidationResult;

            _repository.Remover(produto);
            return await PersistirDados(_repository.UnitOfWork);
        }

        private Produto AlterarProduto(Produto produto, ProdutoDTO produtoDTO)
        {
            produto.AlterarNome(produtoDTO.Nome);
            produto.AlterarDescricao(produtoDTO.Descricao);
            produto.AlterarValor(produtoDTO.Valor);
            produto.AlterarStatus(produtoDTO.Ativo);
            produto.AlterarTipoEntrada(produtoDTO.Entrada);
            produto.AlterarTipoSaida(produtoDTO.Saida);

            return produto;
        }
    }
}
